using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using Task2.Core.Analyzer;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols.OneSign;
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

            var filePath = ConfigurationManager.AppSettings["TextFileForParsing"];

            if (filePath == null)
            {
                return;
            }

            if (!File.Exists(filePath))
            {
                return;
            }


            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(fs);
                int counter = 0;

                List<ISymbol> symbolBuffer = new List<ISymbol>();

                List<ISentenceElement> sentenceElementsBuffer = new List<ISentenceElement>();

                List<ISentence> sentencesBuffer = new List<ISentence>();

                ISymbol previousSymbol;

                while (reader.Peek() != -1)
                {
                    var b = reader.Read();

                    char c = (char)b;

                    ISymbol currentSymbol;
                    switch (char.GetUnicodeCategory(c))
                    {
                            case UnicodeCategory.DecimalDigitNumber:
                            case UnicodeCategory.UppercaseLetter:
                            case UnicodeCategory.LowercaseLetter:
                                currentSymbol = new Dot();
                                break;
                    }

                } 
            }

            //var stream = File.OpenRead(filePath);


            //    var fileText = File.ReadAllText(file);



            //    IAnalyzer analyzer = new TextAnalyzer();

            //    var analyzedText = analyzer.Analyze(fileText);



            //    ILogger logger = new ConsoleLogger();

            //    TasksWorker worker = new TasksWorker(logger);

            //    worker.AllSentencesOrderedByWordsCount(analyzedText);

            //    worker.WordsFromQuestions(5);

            //    worker.DeleteWordsFromText(8);

            //    worker.ExchangeWordsInSentence(8, 7);

        }
    }
}
