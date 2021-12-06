﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Output
{
    public class OutputToConsole : IOutput
    {
        public void Print(string str)
        {
            Console.WriteLine(str);
        }

        public void Print(IText text)
        {
            const char SPACE_CHAR = ' ';

            var builder = new StringBuilder();

            foreach (var sentence in text)
            {
                foreach (var element in sentence)
                {
                    builder.Append(element);
                }
                builder.Append(SPACE_CHAR);
                Console.Write(builder);
                builder.Clear();
            }

            Console.WriteLine();
        }

        public void Print(ISentence sentence)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var element in sentence)
            {
                builder.Append(element);
            }

            Console.WriteLine(builder);
        }
    }
}
