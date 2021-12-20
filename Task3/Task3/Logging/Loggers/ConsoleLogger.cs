using System;

namespace Logging.Loggers
{
    internal class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("logging: " + message);
        }
    }
}
