using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvManager.DAL.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CsvManager.DAL.Core
{
    public class CsvManagerContext : DbContext
    {
        //public CsvManagerContext(DbContextOptions<CsvManagerContext> options)
        //    : base(options)
        //{

        //}

        private readonly IConfiguration _configuration;

        public CsvManagerContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var s = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

                        //@"Server=DESKTOP-UIGGTGJ;Database=CsvManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true2
            }
        }


    }
}
