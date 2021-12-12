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

            using (var file = new FileAssist(filePath, FileMode.Open, FileAccess.Read))
            {
                using IAnalyzer analyzer = new StreamAnalyzer(file.FileStream, logger);

                text = analyzer.Analyze();
            }

            IOutput output = new OutputToConsole();

            ITerminal terminal = new Terminal(new CommandLine(), output);
            
            IWorker worker = new TasksWorker(text, output);

            CommandLineCommand command;
            try
            {
                command = terminal.CommandLineArgumentParser(args);
            }
            catch (ArgumentException e)
            {
                terminal.Print(e.Message);
                command = CommandLineCommand.Base;
            }

            bool breakFlag = true;
            while (breakFlag)
            {
                try
                {
                    switch (command)
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
                            worker.SaveToFile(args[1]);
                            break;
                        case CommandLineCommand.Exit:
                            breakFlag = false;
                            continue;
                        case CommandLineCommand.UndefinedCommand:
                        case CommandLineCommand.Base:
                        default:
                            terminal.PrintHelp();
                            break;
                    }
                }
                catch (ArgumentException e)
                {
                    terminal.Print(e.Message);
                }

                try
                {
                    (command, args) = terminal.CommandLineArgumentParser();
                }
                catch (ArgumentException e)
                {
                    terminal.Print(e.Message);
                    command = CommandLineCommand.Base;
                }
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
