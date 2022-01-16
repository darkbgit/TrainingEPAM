using CsvManager.Core.Services.Interfaces;
using CsvManager.Services.Implementation.Config;
using CsvManager.Services.Implementation.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CsvManager.Services.Implementation
{
    public class FolderService : IFolderService
    {
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly ILogger<FolderService> _logger;
        private readonly FolderOptions _folderOptions;

        private FileSystemWatcher _watcher;
        private CancellationToken _cancellationToken;

        public FolderService(IOptions<FolderOptions> options, IFileServiceFactory fileServiceFactory, ILogger<FolderService> logger)
        {
            _fileServiceFactory = fileServiceFactory;
            _logger = logger;
            _folderOptions = options.Value;
            InitWatcher();
        }


        public async Task RunAsync(CancellationToken ct)
        {
            _logger.LogInformation($"Start watch folder {_folderOptions.InitFolder} ...");
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
                _logger.LogInformation(exception.Message);
            }
        }

        private async Task ParseAll()
        {
            var files = Directory.EnumerateFiles(_folderOptions.InitFolder, _folderOptions.FilesPattern);

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
                    _logger.LogInformation(e.Message);
                }

            }
        }

        private void InitWatcher()
        {
            _watcher = new FileSystemWatcher(_folderOptions.InitFolder, _folderOptions.FilesPattern);
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