using System;
using System.Configuration;
using System.IO;
using Task2.Core.Analyzer;
using Task2.Core.IO;
using Task2.Core.Loggers;
using Task2.Core.Model.Interfaces;
using Task2.Core.Output;
using Task2.Core.Tasks;


namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var filePath = ConfigurationManager.AppSettings["TextFileForParsing"]
                ?? throw new ArgumentException("Couldn't get file path from app.settings");


            IOutput output = new OutputToConsole();

            IText text;

            using (var file = new FileAssist(filePath))
            {
                IAnalyzer analyzer = new StreamAnalyzer(file.FileStream, logger);

                text = analyzer.Analyze();
            }

            IWorker worker = new TasksWorker(text, output);

            worker.AllSentencesOrderedByWordsCount();

            worker.WordsFromQuestions(7);

            worker.DeleteWordsFromText(8);

            worker.ExchangeWordsInSentence(2, 7, "hello, duck");

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Error " + (e.ExceptionObject as Exception)?.Message);
            Console.WriteLine("Application will be terminated. Press any key...");
            Console.ReadKey();
        }
    }
}
