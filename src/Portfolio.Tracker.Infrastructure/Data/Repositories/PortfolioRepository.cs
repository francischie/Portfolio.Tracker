using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Portfolio.Tracker.Core.Entities;

namespace Portfolio.Tracker.Infrastructure.Data.Repositories
{
    public interface IPortfolioRepository
    {
        Task<List<PortfolioEntity>> GetAllAsync();
        Task SaveAsync(List<PortfolioEntity> portfolio);
    }

    [ExcludeFromCodeCoverage]
    public class PortfolioRepository : IPortfolioRepository
    {
        private const string PortfolioPath = "portfolio.xml";

        public Task<List<PortfolioEntity>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                using (var fs = new FileStream(PortfolioPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    var reader = XmlReader.Create(fs);
                    var serializer = new DataContractSerializer(typeof(List<PortfolioEntity>));
                    return (List<PortfolioEntity>) serializer.ReadObject(reader);
                }
            });
        }

        /// <summary>
        /// This method was only use for generating test xml data. Not meant for saving. 
        /// </summary>
        /// <param name="portfolio"></param>
        /// <returns></returns>
        public Task SaveAsync(List<PortfolioEntity> portfolio)
        {
            return Task.Run(() =>
            {
                using (var fs = new FileStream(PortfolioPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var serializer = new DataContractSerializer(typeof(List<PortfolioEntity>));
                    serializer.WriteObject(fs, portfolio);
                }
            });
        }
    }
}