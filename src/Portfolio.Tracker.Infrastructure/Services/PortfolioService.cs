using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Portfolio.Tracker.Core.Entities;
using Portfolio.Tracker.Infrastructure.Data.Repositories;

namespace Portfolio.Tracker.Infrastructure.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioEntity>> GetAllAsync();
    }

    public class PortfolioService : IPortfolioService
    {
        private readonly IMemoryCache _cache;
        private readonly IPortfolioRepository _portfolioRepository; 

        public PortfolioService(IMemoryCache cache, IPortfolioRepository portfolioRepository)
        {
            _cache = cache;
            _portfolioRepository = portfolioRepository;
        }

        public Task<List<PortfolioEntity>> GetAllAsync()
        {
            var cacheKey = $"{nameof(GetAllAsync)}";
            return _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                var list = (await _portfolioRepository.GetAllAsync()).ToList();
                entry.AbsoluteExpiration = list.Any()
                    ? DateTimeOffset.Now.AddMinutes(10)
                    : DateTimeOffset.Now.AddMilliseconds(-1);
                return list;
            });
        }
        
        
    }
}