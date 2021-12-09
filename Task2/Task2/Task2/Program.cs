using System;
using System.Configuration;
using System.IO;
using Task2.Core.Analyzer;
using Task2.Core.IO;
using Task2.Core.IO.Consoles;
using Task2.Core.IO.Files;
using Task2.Core.Loggers;
using Task2.Core.Model.Interfaces;
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



            IText text;

            using (var file = new FileAssist(filePath))
            {
                IAnalyzer analyzer = new StreamAnalyzer(file.FileStream, logger);

                text = analyzer.Analyze();
            }

            ICommandLine commandLine = new CommandLine();
            IOutput output = new OutputToConsole();

            ITerminal terminal = new Terminal(commandLine, output);
            
            IWorker worker = new TasksWorker(text, output);

            bool breakFlag = true;
            while (breakFlag)
            {
                try
                {
                    var arg = commandLine.CommandLineArgumentParser(args);
                }
                catch (ArgumentException e)
                {
                    terminal.Print(e.Message);
                    terminal.PrintHelp();
                    args = commandLine.GetArguments();
                    continue;
                }
                switch (commandLine.CommandLineArgumentParser(args))
                {
                    case CommandLineCommand.PrintData:
                        terminal.Print(text);
                        break;
                    case CommandLineCommand.PrintAllSentencesOrderedByWordsCount:
                        worker.AllSentencesOrderedByWordsCount();
                        break;
                    case CommandLineCommand.PrintDistinctWordsFromQuestionByWordLength:
                        worker.WordsFromQuestions(Convert.ToInt32(args[1]));
                        break;
                    case CommandLineCommand.DeleteWordsByWordLength:
                        worker.DeleteWordsFromText(Convert.ToInt32(args[1]));
                        break;
                    case CommandLineCommand.ExchangeWordsInSentenceBySubstring:
                        worker.ExchangeWordsInSentence(Convert.ToInt32(args[1]),
                            Convert.ToInt32(args[2]), 
                            args[3]);
                        break;
                    case CommandLineCommand.SaveToFile:
                        break;
                    case CommandLineCommand.Exit:
                        breakFlag = false;
                        continue;
                    case CommandLineCommand.Base:
                    default:
                        terminal.PrintHelp();
                        break;
                }
                args = commandLine.GetArguments();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Error " + (e.ExceptionObject as Exception)?.Message);
            Console.WriteLine("Application will be terminated. Press any key...");
            Console.ReadKey();
        }
    }
}
