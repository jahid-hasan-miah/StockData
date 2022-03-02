using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.StockManaging.Contexts;
using StockData.StockManaging.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.StockManaging.Repositories
{
    public class CompanyRepository : Repository<Company, int>,
        ICompanyRepository
    {
        public CompanyRepository(IStockContext dbContext)
            : base((DbContext)dbContext)
        {
        }
    }
}
