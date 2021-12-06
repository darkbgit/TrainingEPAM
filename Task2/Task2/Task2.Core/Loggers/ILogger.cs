using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Loggers
{
    public interface ILogger
    {
        void Log(string message);
    }
}
