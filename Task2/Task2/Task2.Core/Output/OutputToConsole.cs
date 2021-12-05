using System;
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

            StringBuilder builder = new StringBuilder();

            foreach (var sentence in text)
            {
                Print(sentence);

                builder.Append(SPACE_CHAR);

                Console.Write(builder);
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

            Console.Write(builder);
        }
    }
}
