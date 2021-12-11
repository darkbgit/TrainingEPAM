using System;
using System.IO;
using System.Text;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.IO.Files
{
    internal class OutputToFile : IOutput
    {
        private readonly string _filePath;

        public OutputToFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Print(string str)
        {
            throw new NotImplementedException();
        }

        public void Print(ISentence sentence)
        {
            throw new NotImplementedException();
        }

        public void Print(IText text)
        {
            const char SPACE_CHAR = ' ';

            using (var file = new FileAssist(_filePath, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(file.FileStream, Encoding.Default))
                {

                    var builder = new StringBuilder();

                    foreach (var sentence in text)
                    {
                        foreach (var element in sentence)
                        {
                            builder.Append(element);
                        }

                        builder.Append(SPACE_CHAR);
                        sw.Write(builder);
                        builder.Clear();
                    }
                }
            }


        }
    }
}
