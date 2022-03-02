using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.StockManaging.Contexts;
using StockData.StockManaging.Entities;

namespace StockData.StockManaging.Repositories
{
    public class StockPriceRepository : Repository<StockPrice, int>,
        IStockPriceRepository
    {
        public StockPriceRepository(IStockContext dbContext)
            : base((DbContext)dbContext)
        {
        }
    }
}
