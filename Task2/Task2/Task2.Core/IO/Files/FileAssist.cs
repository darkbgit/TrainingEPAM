using System;
using System.IO;

namespace Task2.Core.IO.Files
{
    public class FileAssist : IDisposable
    {
        public FileAssist(string filePath, FileMode fileMode, FileAccess fileAccess)
        {
            FileStream = new FileStream(filePath, fileMode, fileAccess);
        }

        public FileStream FileStream { get; set; }

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
