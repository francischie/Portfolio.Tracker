using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Tracker.Infrastructure.Services;
using Xunit;

namespace Portfolio.Tracker.IntegrationTests
{
    public class StockQuoteServiceTests
    {
        private readonly PortfolioService _portfolioService;
        private readonly StockQuoteService _stockQuoteService;
        
        public StockQuoteServiceTests()
        {
            var services  = new ServiceCollection();
            UI.Program.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            
            _portfolioService = ActivatorUtilities.CreateInstance<PortfolioService>(provider);
            _stockQuoteService = ActivatorUtilities.CreateInstance<StockQuoteService>(provider);
        }
        
        [Fact]
        public async Task GetStockInfoData_Test()
        {
            var trades = await _portfolioService.GetAllAsync();
            var result = await _stockQuoteService.GetStockInfoAsync(trades);
            
            Assert.Equal(2, result.Count());
        }
    }
}