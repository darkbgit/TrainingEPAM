using System;
using System.IO;

namespace Task2.Core.IO.Files
{
    public class FileAssist : IDisposable
    {
        public FileStream FileStream { get; set; }

        public FileAssist(string filePath)
        {
            FileStream = File.Exists(filePath)
                ? new FileStream(filePath, FileMode.Open, FileAccess.Read)
                : throw new FileNotFoundException($"File {filePath} don't find");
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                FileStream?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FileAssist()
        {
            Dispose(false);
        }
    }
}
