using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsvManager.DAL.Repositories.Implementation.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected readonly CsvManagerContext Db;
        protected readonly DbSet<T> Table;

        public Repository(CsvManagerContext db)
        {
            Db = db;
            Table = Db.Set<T>();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = Table.Where(predicate);
            if (includes.Any())
            {
                result = includes
                    .Aggregate(result,
                        (current, include) => current.Include(include));
            }
            return result.ToList();
        }

        public async Task<T> Get(Guid id)
        {
            return await Table.FirstOrDefaultAsync(item => item.Id.Equals(id));
        }

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await Table.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await Table.AddRangeAsync(entities, cancellationToken);
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

        //public async Task<int> SaveAsync(CancellationToken cancellationToken)
        //{
        //    return await Db.SaveChangesAsync(cancellationToken);
        //}
    }
}