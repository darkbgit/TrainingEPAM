using System;
using System.Threading.Tasks;
using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Implementation;
using CsvManager.DAL.Repositories.Implementation.Repositories;
using CsvManager.DAL.Repositories.Interfaces;
using CsvManager.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetConnectionString("DefaultConnection");


            var serviceProvider = new ServiceCollection()
                .AddTransient<IRepository<Client>, ClientsRepository>()
                .AddTransient<IRepository<Manager>, ManagersRepository>()
                .AddTransient<IRepository<Order>, OrdersRepository>()
                .AddTransient<IRepository<Product>, ProductsRepository>()

                .AddScoped<IUnitOfWork, UnitOfWork>()

                .AddScoped<IRecordService, RecordService>()
                .AddScoped<IFileService, FileService>()

                .BuildServiceProvider();

            var service = serviceProvider.GetService<IFileService>();

            await service.Parse("");

        }


    }
}
