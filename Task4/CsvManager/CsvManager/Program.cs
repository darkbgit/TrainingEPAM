using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
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

namespace CsvManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json").Build();

            //var section = config.GetConnectionString("DefaultConnection");



            var host = CreateHostBuilder(args).Build();

            
            //await host.RunAsync();

            var service = host.Services.GetService<IFileService>();

            await service.Parse("Petro2_12112.csv");

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, appConfig) =>
                {
                    appConfig.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
                    //.Build();
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<CsvManagerContext>()
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
                        });

                    //.BuildServiceProvider();
                })
                .UseSerilog();


        //static void BuildConfig(IConfigurationBuilder builder)
        //{
        //    builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //        .AddJsonFile("appsettings.json");
        //}
    }
}
