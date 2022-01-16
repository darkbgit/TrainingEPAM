namespace CsvManager.Core.Services.Interfaces
{
    public interface IFileServiceFactory
    {
        IFileService CreateFileService();
    }
}