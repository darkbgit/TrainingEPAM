using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Implementation;
using CsvManager.DAL.Repositories.Implementation.Repositories;
using CsvManager.DAL.Repositories.Interfaces;
using CsvManager.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.IO;
using System.Threading.Tasks;

namespace CsvManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(@"C:\Log\csvLog.txt", LogEventLevel.Information)
                .CreateLogger();


            await CreateHostBuilder(args).Build().RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, appConfig) =>
                {
                    appConfig.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<CsvManagerContext>(ServiceLifetime.Transient)
                        .AddDbContextFactory<CsvManagerContext>()
                        .AddTransient<IRepository<Client>, ClientsRepository>()
                        .AddTransient<IRepository<Manager>, ManagersRepository>()
                        .AddTransient<IRepository<Order>, OrdersRepository>()
                        .AddTransient<IRepository<Product>, ProductsRepository>()

                        .AddTransient(typeof(IGetOrCreateUnitOfWork<>), typeof(GetOrCreateUnitOfWork<>))

                        .AddTransient<IFolderService, FolderService>()
                        .AddTransient<IRecordService, RecordService>()
                        .AddTransient<IFileService, FileService>()

                        .AddTransient<IFileServiceFactory, FileServiceFactory>()

                        .AddHostedService<Startup>();

                })
                .UseSerilog();

    }
}
