using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Portfolio.Tracker.Core.Entities;
using Portfolio.Tracker.Core.Exceptions;
using Portfolio.Tracker.Infrastructure.Services;
using PortfolioTracker.Core.Enums;
using RichardSzalay.MockHttp;
using Xunit;

namespace Portfolio.Tracker.Infrastructure.UnitTests.Services
{
    public class StockServiceTests
    {
        private readonly StockQuoteService _service;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private MockHttpMessageHandler _mockHttpMessageHandler;

        public StockServiceTests()
        {
            SetupDefaultMocks();

            var services = new ServiceCollection()
                .AddTransient(a => _httpClientFactory.Object)
                .AddMemoryCache()
                .BuildServiceProvider();

            _service = ActivatorUtilities.CreateInstance<StockQuoteService>(services);
        }

        private void SetupDefaultMocks()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _httpClientFactory.Setup(a => a.CreateClient(It.IsAny<string>()))
                .Returns(_mockHttpMessageHandler.ToHttpClient);
      

        }

        [Theory]
        [InlineData("")]
        [InlineData("\"Note\": \"Thank you for using Alpha Vantage! Our standard API call frequency is ")]
        public async void GetStockInfoAsync_ServiceNotAvailableTest(string invalidContent)
        {
            _mockHttpMessageHandler.Clear();
            _mockHttpMessageHandler.When("*").Respond("text/html", invalidContent);

            var trades = CreatePortfolio();

            await Assert.ThrowsAsync<StockServiceException>(() => _service.GetStockInfoAsync(trades));
        }


        [Fact]
        public async void GetStockInfoAsync_DataTest()
        {
            _mockHttpMessageHandler.When("*").Respond("text/html", @"
timestamp,open,high,low,close,volume
2020-07-13,214.4800,215.8000,206.5000,207.0700,36326100
2020-07-10,213.6200,214.0800,211.0800,213.6700,26178700
            ".Trim());
            var trades = CreatePortfolio();

            var result = await _service.GetStockInfoAsync(trades);

            var stock = result.First();
            Assert.Equal(DateTime.Parse("2020-07-13"), stock.AsOfDate);
            Assert.Equal(100, stock.Quantity);
            Assert.Equal(213.6700M, stock.PreviousClose);
            Assert.Equal(207.0700M, stock.Price);
            Assert.Equal(stock.Quantity * stock.Price,  stock.MarketValue);
            Assert.Equal(214.48M, stock.Open);
            Assert.Equal(215.8M, stock.High);
            Assert.Equal(206.5M, stock.Low);
            Assert.Equal(36326100, stock.Volume);
            Assert.Equal(stock.MarketValue - stock.PreviousClose * stock.Quantity, stock.DailyProfitAndLost);
            Assert.Equal(stock.MarketValue - stock.Cost, stock.InceptionProfitAndLost);
        }
        
        [Fact]
        public async void GetStockInfoAsync_OnlineOneDayHistoryTest()
        {
            _mockHttpMessageHandler.Clear();
            _mockHttpMessageHandler.When("*").Respond("text/html", @"
timestamp,open,high,low,close,volume
2020-07-13,214.4800,215.8000,206.5000,207.0700,36326100
            ".Trim());
            var trades = CreatePortfolio();

            var result = await _service.GetStockInfoAsync(trades);

            var stock = result.First();
            Assert.Equal(0, stock.PreviousClose);
        }

        private static List<PortfolioEntity> CreatePortfolio()
        {
            var trades = new List<PortfolioEntity>
            {
                new PortfolioEntity
                {
                    Price = 1065, Quantity = 50, Symbol = "GOOGL", TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.Buy
                },
                new PortfolioEntity
                {
                    Price = 1080, Quantity = 50, Symbol = "GOOGL", TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.Buy
                },
            };
            return trades;
        }
    }
}