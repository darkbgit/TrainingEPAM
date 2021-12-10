using System;

namespace Task2.Core.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("logging: " + message);
        }
    }
}
