using System;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Core.Entities;

namespace CsvManager.DAL.Repositories.Interfaces.UnitsOfWork
{
    public interface IGetOrCreateUnitOfWork<T> : IDisposable where T : class, IEntityWithName, new()
    {
        Task<T> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}