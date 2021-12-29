using System;
using System.Threading.Tasks;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;

namespace CsvManager.DAL.Repositories.Implementation
{
    public interface IGetOrCreateUnitOfWork<T> : IDisposable where T : class, IEntityWithName, new()
    {
        Task<T> GetOrCreateByName(string name);

        Task<int> SaveChangesAsync();
    }
}