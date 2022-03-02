using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockData.StockManaging.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICompanyService _companyService;
        private readonly IStockPriceService _stockPriceService;
        public Worker(ILogger<Worker> logger,ICompanyService companyService,IStockPriceService stockPriceService)
        {
            _logger = logger;
            _companyService = companyService;
            _stockPriceService = stockPriceService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _companyService.FetchData();
                _stockPriceService.FetchData();
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
