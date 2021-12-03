using System;
using System.Configuration;
using System.IO;
using Task2.Core.Analyzer;
using Task2.Output;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var filePath = ConfigurationManager.AppSettings["TextFileForParsing"]
                ?? throw new ArgumentException("Couldn't get file path from app.settings");


            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} don't find");
            }

            ILogger logger = new ConsoleLogger();

            IAnalyzer analyzer = new TextAnalyzer(logger);

            var text = analyzer.Analyze(filePath);

            logger.Print(text.ToString());

            TasksWorker worker = new TasksWorker(logger);

            worker.AllSentencesOrderedByWordsCount(text);

            //    worker.WordsFromQuestions(5);

            //    worker.DeleteWordsFromText(8);

            //    worker.ExchangeWordsInSentence(8, 7);

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Error " + (e.ExceptionObject as Exception)?.Message);
            Console.WriteLine("Application will be terminated. Press any key...");
            Console.ReadKey();
        }
    }
}
