using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Task2.Core.Analyzer;
using Task2.Output;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sAll = ConfigurationManager.AppSettings;
            //try
            //{
            //    var files = ConfigurationManager.AppSettings.AllKeys.Select(k => sAll[k].ToString());
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}

            var files = ConfigurationManager.AppSettings.AllKeys.Select(k => sAll[k].ToString());

            foreach (var file in files)
            {

                var fileText = File.ReadAllText(file);



                IAnalyzer analyzer = new TextAnalyzer();

                var analyzedText = analyzer.Analyze(fileText);



                ILogger logger = new ConsoleLogger();

                TasksWorker worker = new TasksWorker(logger);

                worker.AllSentencesOrderedByWordsCount(analyzedText);

                worker.WordsFromQuestions(5);

                worker.DeleteWordsFromText(8);

                worker.ExchangeWordsInSentence(8, 7);
            }
        }
    }
}
