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

        public UnitOfWork(WebOrdersInfoContext context, IRepository<Order> orders, IRepository<User> users)
        {
            _context = context;
            Orders = orders;
            Users = users;
        }

        public IRepository<Order> Orders { get; }

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