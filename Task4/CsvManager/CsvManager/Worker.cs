using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Core.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CsvManager
{
    public class Worker : BackgroundService
    {
        private readonly IFolderService _folderService;
        private readonly ILogger<Worker> _logger;

        private CancellationToken t;
        //private readonly CancellationTokenSource cancellationToken = new CancellationTokenSource();

        public Worker(IFolderService folderService, ILogger<Worker> logger,
            IHostApplicationLifetime appLifetime)
        {
            _folderService = folderService;
            _logger = logger;
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopped.Register(OnStopped);


        }

        //public override Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("Starting FolderWatch service...");
        //    return base.StartAsync(cancellationToken);
        //}

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            t = stoppingToken;
            //cancellationToken.Cancel();

            stoppingToken.Register(() =>
                _logger.LogInformation("FolderWatcher service is stopping."));

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
            _logger.LogInformation("3. OnStopping has been called.");
        }

        private void OnStopped()
        {
            _logger.LogInformation("FolderWatch service was stopped.");
        }
    }
}
