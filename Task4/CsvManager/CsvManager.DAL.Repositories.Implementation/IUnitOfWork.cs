using System;
using System.Threading.Tasks;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;

namespace CsvManager.DAL.Repositories.Implementation
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Manager> Managers { get; }
        IRepository<Client> Clients { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }

        Task<int> SaveChangesAsync();
    }
}