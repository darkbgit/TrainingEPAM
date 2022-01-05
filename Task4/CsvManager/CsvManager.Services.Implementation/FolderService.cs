using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvManager.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CsvManager.Services.Implementation
{
    public class FolderService : IFolderService, IDisposable
    {
        private readonly string _folderName;
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly string _filesPattern;
        private readonly ILogger<FolderService> _logger;

        private FileSystemWatcher _watcher;

        public FolderService(IConfiguration configuration, IFileServiceFactory fileServiceFactory, ILogger<FolderService> logger)
        {
            _fileServiceFactory = fileServiceFactory;
            _logger = logger;
            _folderName = configuration["Folders:InitFolder"];
            _filesPattern = configuration["Folders:FilesPattern"];
            InitWatcher();
        }

        public async Task Run()
        {
            _logger.LogInformation($"Start watch folder {_folderName} ...");
            await ParseAll();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"File {e.Name} added. Start parse.");
            var t = Task.Factory.StartNew(() => _fileServiceFactory.CreateFileService().Parse(e.FullPath));
        }

        private async Task ParseAll()
        {
            var files = Directory.EnumerateFiles(_folderName, _filesPattern);

            if (files.Any())
            {
                _logger.LogInformation($"On start found {files.Count()} files for parsing. Begin parse.");
                var tasks = files.Select(f => _fileServiceFactory.CreateFileService().Parse(f));
                
                await Task.WhenAll(tasks);
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
        }
    }
}