using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Output
{
    public class ConsoleLogger : ILogger
    {
        public void Print(string str)
        {
            Console.WriteLine(str);
        }
    }
}
