using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading;

namespace Task2.Core.IO.Consoles
{
    public class CommandLine : ICommandLine
    {

        public CommandLineCommand CommandLineArgumentParser(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    return CommandLineCommand.Base;
                case 1:
                    return args[0] switch
                    {
                        CommandLineArguments.PrintData => CommandLineCommand.PrintData,
                        CommandLineArguments.PrintAllSentencesOrderedByWordsCount => CommandLineCommand.PrintAllSentencesOrderedByWordsCount,
                        CommandLineArguments.Exit => CommandLineCommand.Exit,
                        _ => throw new ArgumentException($"команды \"{args[0]}\" не существует")
                    };
                case 2:
                    CommandLineCommand command;
                    switch (args[0])
                    {
                        case CommandLineArguments.PrintDistinctWordsFromQuestionByWordLength:
                            CheckNumeric(args[1]);
                            command = CommandLineCommand.PrintDistinctWordsFromQuestionByWordLength;
                            break;
                        case CommandLineArguments.DeleteWordsByWordLength:
                            CheckNumeric(args[1]);
                            command = CommandLineCommand.DeleteWordsByWordLength;
                            break;
                        case CommandLineArguments.SaveToFile:
                            CheckPath(args[1]);
                            command = CommandLineCommand.SaveToFile;
                            break;
                        default:
                            throw new ArgumentException($"команды \"{args[0]}\" c одним параметром не существует");
                    }

                   
                    return command;
                case 4:
                    switch (args[0])
                    {
                        case CommandLineArguments.ExchangeWordsInSentenceBySubstring:
                            CheckNumeric(args[1]);
                            CheckNumeric(args[2]);
                            CheckSubstring(args[3]);
                            return CommandLineCommand.ExchangeWordsInSentenceBySubstring;
                        default:
                            throw new ArgumentException($"команды \"{args[0]}\" c тремя параметром не существует");
                    }
                default:
                    throw new ArgumentException("Недопустимое количество аргументов");
            }
        }
        
        public string[] GetArguments()
        {

            return Console.ReadLine()?.Split(' ').ToArray() 
                   ?? Array.Empty<string>();
        }

        private void CheckNumeric(string str)
        {
            if(!uint.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out _))
            {
                throw new ArgumentException($"\"{str}\" не целое положительное число");
            }
        }

        private void CheckPath(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("Строка не должна быть пустой или состоять из одних пробелов");
            }

            _ = Path.GetFullPath(str);
        }

        private void CheckSubstring(string str)
        {
            char[] endOfSentenceChar = {'.', '!', '?'};

            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("Строка не должна быть пустой или состоять из одних пробелов");
            }

            var index = str.IndexOfAny(endOfSentenceChar);
            if (index != -1)
            {
                throw new ArgumentException($"под строка не должна содержать символ конца предложения \"{str[index]}\"");
            }
        }

    }
}
