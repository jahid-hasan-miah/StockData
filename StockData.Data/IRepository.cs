using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Data
{
    public interface IRepository<TEntity, TKey>
       where TEntity : class, IEntity<TKey>
    {
        void Add(TEntity entity);
        public TEntity GetById(TKey id);
    }
}
