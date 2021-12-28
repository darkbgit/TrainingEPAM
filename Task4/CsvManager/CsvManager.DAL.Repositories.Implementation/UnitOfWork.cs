using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;

namespace CsvManager.DAL.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CsvManagerContext _db;

        public UnitOfWork(IRepository<Manager> managers, IRepository<Client> clients, IRepository<Product> products, IRepository<Order> orders, CsvManagerContext db)
        {
            Managers = managers;
            Clients = clients;
            Products = products;
            Orders = orders;
            _db = db;
        }

        public IRepository<Manager> Managers { get; }
        public IRepository<Client> Clients { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Order> Orders { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
