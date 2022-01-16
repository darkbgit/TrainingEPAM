using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;

namespace CsvManager.DAL.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>(CsvManagerContext db) where T : class, IBaseEntity;
    }
}