using System;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;

namespace CsvManager.DAL.Repositories.Implementation
{
    public interface IGetOrCreateUnitOfWork<T> : IDisposable where T : class, IEntityWithName, new()
    {
        Task<T> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}