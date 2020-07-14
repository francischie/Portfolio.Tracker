using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Portfolio.Tracker.Core.Entities;
using Portfolio.Tracker.Infrastructure.Data.Repositories;
using Portfolio.Tracker.Infrastructure.Services;
using PortfolioTracker.Core.Enums;
using Xunit;

namespace Portfolio.Tracker.Infrastructure.UnitTests.Services
{
    public class PortfolioServiceTests
    {
        private readonly PortfolioService _service;
        
        private Mock<IPortfolioRepository> _portfolioRepository; 
       
        public PortfolioServiceTests()
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
            _portfolioRepository = new Mock<IPortfolioRepository>();

            _portfolioRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(new List<PortfolioEntity>
            {
                new PortfolioEntity {Price = 1065, Quantity = 50, Symbol = "GOOGL", TransactionDate = DateTime.Now, TransactionType = TransactionType.Buy }
            });
        }
        
        [Fact]
        public async Task  GetAllAsync_CacheTest()
        {
            var result = await _service.GetAllAsync();
            
            Assert.Equal(1, result.Count);
        }
        
        [Fact]
        public async Task  GetAllAsync_Dummy()
        {
            var repository = new PortfolioRepository();
            var list = new List<PortfolioEntity>
            {
                new PortfolioEntity {Price = 1065, Quantity = 100, Symbol = "MSFT", TransactionDate = DateTime.Parse("1/2/2018"), TransactionType = TransactionType.Buy },
                new PortfolioEntity {Price = 1065, Quantity = 50, Symbol = "GOOGL", TransactionDate = DateTime.Parse("1/2/2018"), TransactionType = TransactionType.Buy },
                new PortfolioEntity {Price = 1065, Quantity = 150, Symbol = "MSFT", TransactionDate = DateTime.Parse("1/10/2018"), TransactionType = TransactionType.Buy }
            };
            await repository.SaveAsync(list);
        }
        
        [Fact]
        public async Task  GetAllAsync_UseCacheResultTest()
        {
            await _service.GetAllAsync();
            await _service.GetAllAsync();
            
            _portfolioRepository.Verify(a => a.GetAllAsync(), Times.Once);
        }
        
        [Fact]
        public async Task  GetAllAsync_NoCacheNoResultTest()
        {
            _portfolioRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(new List<PortfolioEntity>());
            
            await _service.GetAllAsync();
            await _service.GetAllAsync();
            
            _portfolioRepository.Verify(a => a.GetAllAsync(), Times.Exactly(2));
        }
    }

  
}