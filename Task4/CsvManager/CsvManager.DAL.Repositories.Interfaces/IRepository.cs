using CsvManager.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CsvManager.DAL.Repositories.Interfaces
{
    public interface IRepository<T> :IDisposable where T : class , IBaseEntity
    {
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        Task<T> Get(Guid id);
        IQueryable<T> GetAll();

        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Remove(Guid id);
        void RemoveRange(IEnumerable<T> entities);

        public Task<int> Save();
    }
}
