﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Output
{
    internal class OutputToFile : IOutput
    {
        public void Print(string str)
        {
            throw new NotImplementedException();
        }

        public void Print(IText text)
        {
            throw new NotImplementedException();
        }

        public void Print(ISentence sentence)
        {
            throw new NotImplementedException();
        }

        public void Output(IText text)
        {
            const char SPACE_CHAR = ' ';
            var outputPath = "";

            if (!File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            using (FileStream fs = new FileStream(outputPath, FileMode.CreateNew))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                StringBuilder builder = new StringBuilder();

                foreach (var sentence in text)
                {
                    builder.Clear();
                    builder.Append(SPACE_CHAR);
                    foreach (var element in sentence)
                    {
                        builder.Append(element);
                    }
                    sw.Write(builder);
                }
            }
        }
    }
}
