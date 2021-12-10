using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.IO
{
    public interface ITerminal
    {
        void PrintHelp();

        void Print(string str);

        void Print(IText text);

        void Print(ISentence sentence);
    }
}
