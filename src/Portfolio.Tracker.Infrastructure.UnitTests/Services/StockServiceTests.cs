using Microsoft.Extensions.DependencyInjection;
using Portfolio.Tracker.Infrastructure.Services;

namespace Portfolio.Tracker.Infrastructure.UnitTests.Services
{
    public class StockServiceTests
    {
        private readonly StockQuoteService _service; 
        
        public StockServiceTests()
        {
            SetupDefaultMocks();
            
            var services = new ServiceCollection()
                .AddTransient(a => _portfolioRepository.Object)
                .AddMemoryCache()
                .BuildServiceProvider();

            _service = ActivatorUtilities.CreateInstance<PortfolioService>(services);
        }

        private void SetupDefaultMocks()
        {
            throw new System.NotImplementedException();
        }
    }
}