using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.DAL.Core
{
    public class WebOrdersInfoContext : IdentityDbContext<User, Role, Guid>
    {
        public WebOrdersInfoContext(DbContextOptions<WebOrdersInfoContext> options)
            : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
