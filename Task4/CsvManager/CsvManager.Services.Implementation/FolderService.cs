using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Core.Services.Interfaces;
using CsvManager.Services.Implementation.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CsvManager.Services.Implementation
{
    public class FolderService : IFolderService
    {
        private readonly string _folderName;
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly string _filesPattern;
        private readonly ILogger<FolderService> _logger;

        private FileSystemWatcher _watcher;
        private  CancellationToken _cancellationToken;

        public FolderService(IConfiguration configuration, IFileServiceFactory fileServiceFactory, ILogger<FolderService> logger)
        {
            _fileServiceFactory = fileServiceFactory;
            _logger = logger;
            _folderName = configuration["Folders:InitFolder"];
            _filesPattern = configuration["Folders:FilesPattern"];
            InitWatcher();
        }


        public async Task RunAsync(CancellationToken ct)
        {
            _logger.LogInformation($"Start watch folder {_folderName} ...");
            _cancellationToken = ct;
            await ParseAll();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"File {e.Name} added. Start parse.");
            try
            {
                Task.Run(async () => await _fileServiceFactory.CreateFileService().ParseAsync(e.FullPath, _cancellationToken), _cancellationToken);
            }
            catch (ServiceException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private async Task ParseAll()
        {
            var files = Directory.EnumerateFiles(_folderName, _filesPattern);

            if (files.Any())
            {
                _logger.LogInformation($"On start found {files.Count()} files for parsing. Begin parse.");

                var tasks = files.Select(async f =>
                    await _fileServiceFactory.CreateFileService().ParseAsync(f, _cancellationToken));
                try
                {
                    await Task.WhenAll(tasks);
                }
                catch (ServiceException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        private void InitWatcher()
        {
            _watcher = new FileSystemWatcher(_folderName, _filesPattern);
            _watcher.Created += OnCreated;
            _watcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            _watcher.Created -= OnCreated;
            _watcher.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}