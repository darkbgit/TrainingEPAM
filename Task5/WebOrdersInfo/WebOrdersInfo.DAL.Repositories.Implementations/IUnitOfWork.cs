using System;
using System.Threading.Tasks;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Repositories.Interfaces;

namespace WebOrdersInfo.DAL.Repositories.Implementations
{
    public interface IUnitOfWork : IDisposable

    {
        IRepository<Order> Orders { get; }
        IRepository<User> Users { get; }

        Task<int> SaveChangesAsync();
    }
}