using System;
using System.Threading.Tasks;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Order> Orders { get; }
        IRepository<Client> Clients { get; }
        IRepository<Manager> Managers { get; }
        IRepository<Product> Products { get; }
        Task<int> SaveChangesAsync();
    }
}