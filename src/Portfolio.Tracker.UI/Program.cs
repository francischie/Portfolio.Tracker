using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Tracker.Infrastructure.Data.Repositories;
using Portfolio.Tracker.Infrastructure.Services;

namespace Portfolio.Tracker.UI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var services = new ServiceCollection();
            ConfigureServices(services);
            
            using (var provider = services.BuildServiceProvider())
            {
                var form = provider.GetService<MainForm>();
                Application.Run(form);
            }
        }

        public static void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<IPortfolioService, PortfolioService>()
                .AddScoped<IPortfolioRepository, PortfolioRepository>()
                .AddScoped<IStockQuoteService, StockQuoteService>()
                .AddScoped<MainForm>()
                .AddMemoryCache()
                .BuildServiceProvider();
        }
    }
}