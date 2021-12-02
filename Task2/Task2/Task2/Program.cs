using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using Task2.Core.StateMachine;
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

            var filePath = ConfigurationManager.AppSettings["TextFileForParsing"]
                ?? throw new ArgumentException("Couldn't get file path from app.settings");


            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} don't find");
            }

            IText text;
            List<string> errorList = new List<string>();

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(fs);

                int counter = 0;

                ICollection<ISymbol> symbolBuffer = new List<ISymbol>();

                ICollection<ISentenceElement> sentenceElementsBuffer = new List<ISentenceElement>();

                ICollection<ISentence> sentencesBuffer = new List<ISentence>();

                ISymbol previousSymbol = null;

                IStateMachine stateMachine = new StateMachine();

                int skip = 0;


                while (reader.Peek() != -1)
                {
                    var b = reader.Read();

                    char c = (char) b;

                    ISymbol nextSymbol;

                    var category = char.GetUnicodeCategory(c);

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
                            nextSymbol = c switch
                            {
                                '.' => new Dot(),
                                '!' => new Exclamation(),
                                '?' => new Question(),
                                _ => new Punctuation(c)
                            };
                            break;
                        case UnicodeCategory.SpaceSeparator:
                        case UnicodeCategory.Control:
                            nextSymbol = new Space();
                            break;
                        default:
                            nextSymbol = new Undefined(c);
                            break;
                    }

                    try
                    {
                        var command = stateMachine.MoveNext(nextSymbol.Type);
                        command?.Invoke(nextSymbol, ref symbolBuffer, ref sentenceElementsBuffer, ref sentencesBuffer);
                    }
                    catch (ArgumentException e)
                    {
                        errorList.Add($"{e.Message} in sentence number {sentencesBuffer.Count + 1}");

                        //var command = stateMachine.MoveNext(nextSymbol.Type);
                        //command?.Invoke(nextSymbol, ref symbolBuffer, ref sentenceElementsBuffer, ref sentencesBuffer);
                    }
                }

                var endCommand = stateMachine.MoveNext(SymbolType.End);
                endCommand.Invoke(null, ref symbolBuffer, ref sentenceElementsBuffer, ref sentencesBuffer);

                text = new Text(sentencesBuffer);
            }

            ILogger logger = new ConsoleLogger();

            if (errorList.Any())
            {
                logger.Print($"Количество ошибок при конвертации - {errorList.Count}");
                foreach (var error in errorList)
                {
                    logger.Print(error);
                }
            }
            else
            {
                logger.Print("Текст сериализован без ошибок");
            }

            



            logger.Print(text.ToString());

            TasksWorker worker = new TasksWorker(logger);

            worker.AllSentencesOrderedByWordsCount(text);

            //    worker.WordsFromQuestions(5);

            //    worker.DeleteWordsFromText(8);

            //    worker.ExchangeWordsInSentence(8, 7);

        }

    }
}
