using System;
using System.Threading;
using System.Threading.Tasks;

namespace CsvManager.Core.Services.Interfaces
{
    public interface IFolderService : IDisposable
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}