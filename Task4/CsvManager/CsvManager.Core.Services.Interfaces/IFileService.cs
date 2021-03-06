using System.Threading;
using System.Threading.Tasks;

namespace CsvManager.Core.Services.Interfaces
{
    public interface IFileService
    {
        Task ParseAsync(string filePath, CancellationToken token);
    }
}