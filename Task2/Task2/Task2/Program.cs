using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using Task2.Core.Analyzer;
using Task2.Core.TextObjectModel;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;
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

            IText text;

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(fs);

                int counter = 0;

                ICollection<ISymbol> symbolBuffer = new List<ISymbol>();

                ICollection<ISentenceElement> sentenceElementsBuffer = new List<ISentenceElement>();

                ICollection<ISentence> sentencesBuffer = new List<ISentence>();

                ISymbol previousSymbol = null;

                StateMachine stateMachine = new StateMachine();

                while (reader.Peek() != -1)
                {
                    var b = reader.Read();

                    char c = (char)b;

                    ISymbol nextSymbol;

                    var category = char.GetUnicodeCategory(c);

                    try
                    {
                        switch (char.GetUnicodeCategory(c))
                        {
                            case UnicodeCategory.UppercaseLetter:
                            case UnicodeCategory.LowercaseLetter:
                                nextSymbol = new Letter(c);
                                break;
                            case UnicodeCategory.DecimalDigitNumber:
                                nextSymbol = new Digit(c);
                                break;
                            case UnicodeCategory.InitialQuotePunctuation:
                            case UnicodeCategory.FinalQuotePunctuation:
                            case UnicodeCategory.OpenPunctuation:
                            case UnicodeCategory.ClosePunctuation:
                            case UnicodeCategory.DashPunctuation:
                            case UnicodeCategory.OtherPunctuation:
                            case UnicodeCategory.ConnectorPunctuation:
                                switch (c)
                                {
                                    case '.':
                                        nextSymbol = new Dot();
                                        break;
                                    case ',':
                                        nextSymbol = new Comma();
                                        break;
                                    case ';':
                                        nextSymbol = new Semicolon();
                                        break;
                                    default:
                                        nextSymbol = new Punctuation(c);
                                        break;
                                }

                                break;

                            case UnicodeCategory.SpaceSeparator:
                            case UnicodeCategory.Control:
                                nextSymbol = new Space();
                                break;

                            default:
                                throw new ArgumentException("Undefined symbol");
                        }

                        stateMachine.MoveNext(nextSymbol.Type)
                            ?.Invoke(nextSymbol, ref symbolBuffer, ref sentenceElementsBuffer, ref sentencesBuffer);
                    }
                    catch (Exception e)
                    {
                        //throw;
                    }
                }

                stateMachine.MoveNext(SymbolType.End)
                    ?.Invoke(null, ref symbolBuffer, ref sentenceElementsBuffer, ref sentencesBuffer);

                text = new Text(sentencesBuffer);
            }

            ILogger logger = new ConsoleLogger();

            logger.Print(text.ToString());

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

        private static void AnalyzePaurSumbol(ISymbol prevueSymbol, ISymbol currentSymbol)
        {

        }
    }
}
