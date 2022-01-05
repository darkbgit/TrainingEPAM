using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvManager.Services.Implementation
{
    internal class FilePath
    {
        public FilePath(string filePath)
        {
            Directory = Path.GetDirectoryName(filePath);
            FileName = Path.GetFileNameWithoutExtension(filePath);
            Extension = Path.GetExtension(filePath);
        }

        public FilePath(string directory, string fileName, string extension)
        {
            Directory = directory;
            FileName = fileName;
            Extension = extension;
        }

        public string FileName { get; private set; }

        public string Directory { get; }

        public string Extension { get; }



        public string FullPath =>
            Path.Combine(Directory, FileName + Extension);

        public string FullPathWithNewNameCheck()
        {
            var name = FileName;
            
            int i = 1;
            while (File.Exists(FullPath))
            {
                FileName = name +$"({i++})";
            }

            return FullPath;
        }
    }
}
