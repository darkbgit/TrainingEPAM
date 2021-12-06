using System;
using System.Configuration;
using System.IO;
using Task2.Core.Analyzer;
using Task2.Core.Loggers;
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


            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} don't find");
            }

            IOutput output = new OutputToConsole();

            

            IAnalyzer analyzer = new TextAnalyzer(logger);

            var fs = analyzer.GetStream(filePath);

            var text = analyzer.Analyze(fs);

            //logger.Output(text);

            IWorker worker = new TasksWorker(output);

            worker.AllSentencesOrderedByWordsCount(text);

            //worker.WordsFromQuestions(5, text);

            //worker.DeleteWordsFromText(8, text);

            //worker.ExchangeWordsInSentence(8, 7);

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Error " + (e.ExceptionObject as Exception)?.Message);
            Console.WriteLine("Application will be terminated. Press any key...");
            Console.ReadKey();
        }
    }
}
