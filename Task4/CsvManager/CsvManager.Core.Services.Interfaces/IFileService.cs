using System.Threading.Tasks;

namespace CsvManager.Core.Services.Interfaces
{
    public interface IFileService
    {
        Task Parse(string filePath);
    }
}