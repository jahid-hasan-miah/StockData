using StockData.Data;
using StockData.StockManaging.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.StockManaging.UnitOfWorks
{
    public interface IStockManagingUnitOfWork : IUnitOfWork
    {
        public IStockPriceRepository StockPrices { get; }
        public ICompanyRepository Companies { get; }
    }
}
