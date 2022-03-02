using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.StockManaging.Contexts;
using StockData.StockManaging.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.StockManaging.UnitOfWorks
{
    public class StockManagingUnitOfWork : UnitOfWork,IStockManagingUnitOfWork
    {
        public IStockPriceRepository StockPrices { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public StockManagingUnitOfWork(IStockContext stockContext,
            IStockPriceRepository stockPrice,
            ICompanyRepository company):base((DbContext)stockContext)
        {
            StockPrices = stockPrice;
            Companies = company;
        }
    }
}
