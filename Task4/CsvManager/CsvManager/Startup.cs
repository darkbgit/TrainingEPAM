using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Core.Services.Interfaces;
using Microsoft.Extensions.Hosting;

namespace CsvManager
{
    public class Startup : BackgroundService
    {
        private readonly IFolderService _folderService;

        public Startup(IFolderService folderService)
        {
            _folderService = folderService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _folderService.Run();

            return Task.CompletedTask;
        }
    }
}
