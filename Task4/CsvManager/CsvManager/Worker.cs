using CsvManager.Core.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CsvManager
{
    public class Worker : BackgroundService
    {
        private readonly IFolderService _folderService;
        private readonly ILogger<Worker> _logger;

        public Worker(IFolderService folderService, ILogger<Worker> logger,
            IHostApplicationLifetime appLifetime)
        {
            _folderService = folderService;
            _logger = logger;
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopped.Register(OnStopped);
            appLifetime.ApplicationStopping.Register(OnStopping);
        }

        //public override Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("Starting FolderWatch service...");
        //    try
        //    {
        //        return base.StartAsync(cancellationToken);
        //    }
        //    catch (OperationCanceledException e)
        //    {
        //        Console.WriteLine(e.Message);
        //        Task.WaitAll();
        //    }

        //}

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Task.Run(async () => await _folderService.RunAsync(stoppingToken), stoppingToken);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.CancellationToken);
            }

            return Task.CompletedTask;
        }

        //public override Task StopAsync(CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("Stopping folder service...");
        //    _folderService?.Cancel();
        //    return Task.CompletedTask;
        //}

        private void OnStarted()
        {
            _logger.LogInformation("FolderWatch service was started.");
        }

        private void OnStopping()
        {
            _logger.LogInformation("FolderWatcher service is stopping...");
            Task.Delay(2000).GetAwaiter().GetResult();
        }

        private void OnStopped()
        {
            _logger.LogInformation("FolderWatch service was stopped.");
            //Task.Delay(2000).GetAwaiter().GetResult();
        }
    }
}
