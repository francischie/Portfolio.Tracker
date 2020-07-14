using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Portfolio.Tracker.Core.Entities;
using Portfolio.Tracker.Core.Models;
using PortfolioTracker.Core.Enums;
using ServiceStack;
using Portfolio.Tracker.Core.Exceptions;

namespace Portfolio.Tracker.Infrastructure.Services
{
    public interface IStockQuoteService
    {
        Task<IEnumerable<ProfitAndLossModel>> GetStockInfoAsync(List<PortfolioEntity> trades);
    }

    public class StockQuoteService : IStockQuoteService
    {
        private const string StockServiceUrl = "https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol={0}&apikey={1}&datatype=csv";
        private readonly IHttpClientFactory _httpClientFactory;

        public StockQuoteService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProfitAndLossModel>> GetStockInfoAsync(List<PortfolioEntity> trades)
        {
            var profitAndLoss= CreateProfitAndLoss(trades);
            
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            foreach (var p in profitAndLoss)
            {
                var url = string.Format(StockServiceUrl, p.Symbol, apiKey);
                var csv = await url.GetStringFromUrlAsync();

                if (csv.Length == 0 || csv.IndexOf("Note", StringComparison.Ordinal) > 0)
                    throw new StockServiceException("Stock Service is not available right now. Please try again later.");

                var result = csv.FromCsv<List<AlphaVantageResponse>>()
                    .Take(2)
                    .ToList();
            
                p.AsOfDate = result[0].Timestamp;
                p.Price = result[0].Close;
                p.MarketValue = p.Price * p.Quantity;
                p.InceptionProfitAndLost = p.MarketValue - p.Cost;

                if (result.Count == 1) continue; 

                p.PreviousClose = result[1].Close;
                p.DailyProfitAndLost = p.MarketValue - p.PreviousClose * p.Quantity;
            }
            return profitAndLoss;
        }

        private static List<ProfitAndLossModel> CreateProfitAndLoss(List<PortfolioEntity> trades)
        {
            var profitAndLost = trades.GroupBy(a => a.Symbol)
                .Select(a => new ProfitAndLossModel
                {
                    Cost = a.Sum(b => b.Quantity * (b.TransactionType == TransactionType.Buy ? 1 : -1) * b.Price),
                    Quantity = a.Sum(b => b.Quantity * (b.TransactionType == TransactionType.Buy ? 1 : -1)),
                    Symbol = a.First().Symbol,
                })
                .Where(a => a.Quantity > 0)
                .ToList();
            return profitAndLost;
        }
    }
}