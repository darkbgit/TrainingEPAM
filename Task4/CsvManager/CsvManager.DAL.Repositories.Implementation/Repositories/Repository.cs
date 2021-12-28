using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsvManager.DAL.Repositories.Implementation.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected readonly CsvManagerContext Db;
        protected readonly DbSet<T> Table;

        protected Repository(CsvManagerContext db)
        {
            Db = db;
            Table = Db.Set<T>();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = Table.Where(predicate);
            if (includes.Any())
            {
                result = includes
                    .Aggregate(result,
                        (current, include) => current.Include(include));
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

        public async Task AddRange(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            Table.UpdateRange(entities);
        }

        public async void Remove(Guid id)
        {
            var entity = await Get(id);
            Table.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
        }
    }
}