using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvManager.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CsvManager.Services.Implementation
{
    public class FolderService : IFolderService
    {
        private readonly string _folderName;
        private readonly IFileServiceFactory _fileServiceFactory;

        public FolderService(IConfiguration configuration, IFileServiceFactory fileServiceFactory)
        {
            _fileServiceFactory = fileServiceFactory;
            _folderName = configuration["Folders:InitFolder"];
        }

        public async Task Parse()
        {
            var files = Directory.EnumerateFiles(_folderName);

            var tasks = files.Select(f => _fileServiceFactory.CreateFileService().Parse(f));

            await Task.WhenAll(tasks);
        }

    }
}