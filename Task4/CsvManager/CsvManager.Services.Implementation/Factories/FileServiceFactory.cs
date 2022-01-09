using System;
using CsvManager.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CsvManager.Services.Implementation.Factories
{
    public class FileServiceFactory : IFileServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FileServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileService CreateFileService()
        {
            return _serviceProvider.GetService<IFileService>();
        }
    }
}
