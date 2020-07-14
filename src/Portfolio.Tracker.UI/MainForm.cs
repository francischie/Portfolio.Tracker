using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Portfolio.Tracker.Core.Entities;
using Portfolio.Tracker.Core.Exceptions;
using Portfolio.Tracker.Infrastructure.Services;

namespace Portfolio.Tracker.UI
{
    public partial class MainForm : Form
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IStockQuoteService _stockQuoteService;
        public MainForm(IPortfolioService portfolioService, IStockQuoteService stockQuoteService)
        {
            _portfolioService = portfolioService;
            _stockQuoteService = stockQuoteService;
            InitializeComponent();
        }

        private async void loadButton_Click(object sender, EventArgs e)
        {
            loadButton.Enabled = false;
            loadButton.Text = "Fetching Data...";
            var tradeData = await _portfolioService.GetAllAsync();
            tradeGridView.DataSource = tradeData;
            tradeGridView.AutoResizeColumns(); 

            await BindProfitAndLoss(tradeData);
            loadButton.Enabled = true;
            loadButton.Text = "Refresh";
        }

        private async Task BindProfitAndLoss(List<PortfolioEntity> tradeData)
        {
            try
            {
                var profitAndLossData = await _stockQuoteService.GetStockInfoAsync(tradeData);
                profitLossGridView.DataSource = profitAndLossData;
                profitLossGridView.AutoResizeColumns();
            }
            catch (StockServiceException e)
            {
                MessageBox.Show(e.Message);
            }
         
        }
    }
}