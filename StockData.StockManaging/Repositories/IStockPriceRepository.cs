using StockData.Data;
using StockData.StockManaging.Entities;

namespace StockData.StockManaging.Repositories
{
    public interface IStockPriceRepository : IRepository<StockPrice, int>
    {
    }
}
