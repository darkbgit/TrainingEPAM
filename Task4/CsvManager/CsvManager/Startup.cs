using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Implementation;
using CsvManager.DAL.Repositories.Implementation.Repositories;
using CsvManager.DAL.Repositories.Interfaces;
using CsvManager.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace CsvManager
{
    public class Startup
    {
        public Startup()
        {
            ConfigureServices();
        }

        public ServiceProvider Provider { get; private set; }

        private void ConfigureServices()
        {
            Provider = new ServiceCollection()
                .AddDbContext<CsvManagerContext>()

                .AddTransient<IRepository<Client>, ClientsRepository>()
                .AddTransient<IRepository<Manager>, ManagersRepository>()
                .AddTransient<IRepository<Order>, OrdersRepository>()
                .AddTransient<IRepository<Product>, ProductsRepository>()

                .AddScoped<IUnitOfWork, UnitOfWork>()

                .AddScoped<IRecordService, RecordService>()
                .AddScoped<IFileService, FileService>()

                .AddLogging(builder =>
                {
                    var logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.File(@"C:\Log\csvLog.txt", LogEventLevel.Information)
                        .CreateLogger();

                    builder.AddSerilog(logger);
                })

                .BuildServiceProvider();
        }
    }
}
