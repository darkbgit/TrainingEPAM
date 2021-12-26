using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Loggers
{
    public static class Log
    {
        private static readonly ILogger Logger;

        static Log()
        {
            Logger = new ConsoleLogger();
        }

        public static void LogMessage(string message)
        {
            Logger.Log(DateTime.Now.ToString("G") + ": " + message);
        }
    }
}
