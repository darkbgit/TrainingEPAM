using System.IO;

namespace CsvManager.Services.Implementation.Extensions
{
    public static class FileInfoExtension
    {
        public static string NewFullName(this FileInfo oldFileInfo, string destinationDirectory)
        {
            var name = Path.GetFileNameWithoutExtension(oldFileInfo.Name);

            var newName = oldFileInfo.Name;

            int i = 1;

            string newFullName = Path.Combine(destinationDirectory, name + oldFileInfo.Extension);

            while (File.Exists(newFullName))
            {
                newFullName = Path.Combine(destinationDirectory, name + $"({i++})" + oldFileInfo.Extension);
            }

            return newFullName;
        }
    }
}
