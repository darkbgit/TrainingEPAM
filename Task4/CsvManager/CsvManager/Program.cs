using System;
using Microsoft.Extensions.Configuration;

namespace CsvManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetConnectionString("DefaultConnection");
        }
    }
}
