using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Repositories.Interfaces;

namespace WebOrdersInfo.DAL.Repositories.Implementations.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected readonly WebOrdersInfoContext Db;
        protected readonly DbSet<T> Table;

        protected Repository(WebOrdersInfoContext context)
        {
            Db = context;
            Table = Db.Set<T>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = Table.Where(predicate);

            if (includes.Any())
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

            return result;
        }

        public async Task<T> Get(Guid id)
        {
            return await Table.FirstOrDefaultAsync(item => item.Id.Equals(id));
        }

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public async Task Add(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Update(T element)
        {
            Table.Update(element);
        }

        public async Task Remove(Guid id)
        {
            var entity = await Get(id);
            Table.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> elements)
        {
            Table.RemoveRange(elements);
        }

        public void Dispose()
        {
            Db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
