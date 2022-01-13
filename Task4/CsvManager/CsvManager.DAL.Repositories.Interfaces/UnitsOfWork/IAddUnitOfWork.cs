using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Core.Entities;

namespace CsvManager.DAL.Repositories.Interfaces.UnitsOfWork
{
    public interface IAddUnitOfWork<T> : IDisposable where T : class, IBaseEntity
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}