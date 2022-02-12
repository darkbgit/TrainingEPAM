using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class, IBaseEntity
    {
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        Task<T> Get(Guid id);
        IQueryable<T> GetAll();

        Task Add(T entity);

        void Update(T entity);

        Task Remove(Guid id);
        void RemoveRange(IEnumerable<T> entities);
    }
}
