using System;
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
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Repositories.Implementation.UnitsOfWork;
using CsvManager.DAL.Repositories.Interfaces.UnitsOfWork;
using CsvManager.Services.Implementation.Config;
using CsvManager.Services.Implementation.Factories;
using FileOptions = CsvManager.Services.Implementation.Config.FileOptions;

namespace CsvManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(@"C:\Log\csvLog.txt", LogEventLevel.Information)
                .CreateLogger();

            using var singleGlobal = new SingleGlobal(0);

            if (!singleGlobal.IsSingle)
            {
                Log.Information("Application is already running! Exiting the application.");
                Thread.Sleep(1000);
                return;
            }

            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();

            Task.WaitAll();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, appConfig) =>
                {
                    appConfig.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json");
                })
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<CsvManagerContext>(ServiceLifetime.Transient)
                        .AddDbContextFactory<CsvManagerContext>()
                        .AddTransient<IRepository<Client>, ClientsRepository>()
                        .AddTransient<IRepository<Manager>, ManagersRepository>()
                        .AddTransient<IRepository<Order>, OrdersRepository>()
                        .AddTransient<IRepository<Product>, ProductsRepository>()

                        .AddTransient(typeof(IGetOrCreateUnitOfWork<>), typeof(GetOrCreateUnitOfWork<>))

                        .AddTransient(typeof(IAddUnitOfWork<>), typeof(AddUnitOfWork<>))

                        .AddTransient<IFolderService, FolderService>()
                        .AddTransient<IRecordService, RecordService>()
                        .AddTransient<IFileService, FileService>()

                        .AddTransient<IRepositoryFactory, RepositoryFactory>()

                        .AddTransient<IFileServiceFactory, FileServiceFactory>()

                        .AddHostedService<Worker>()
                        .Configure<FolderOptions>(hostContext.Configuration.GetSection("FolderOptions"))
                        .Configure<FileOptions>(hostContext.Configuration.GetSection("FileOptions"))
                        .Configure<RecordOptions>(hostContext.Configuration.GetSection("RecordOptions"));
                })
                
                .UseSerilog();
    }
}
