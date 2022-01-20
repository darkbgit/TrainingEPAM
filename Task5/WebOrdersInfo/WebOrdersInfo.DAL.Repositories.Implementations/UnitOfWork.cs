using System;
using System.Threading.Tasks;
using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Repositories.Interfaces;

namespace WebOrdersInfo.DAL.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebOrdersInfoContext _context;

        public UnitOfWork(WebOrdersInfoContext context, IRepository<Order> orders, IRepository<User> users, IRepository<Client> clients, IRepository<Manager> managers, IRepository<Product> products)
        {
            _context = context;
            Orders = orders;
            Users = users;
            Clients = clients;
            Managers = managers;
            Products = products;
        }

        public IRepository<Order> Orders { get; }
        public IRepository<Client> Clients { get; }
        public IRepository<Manager> Managers { get; }
        public IRepository<Product> Products { get; }

        public IRepository<User> Users { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}