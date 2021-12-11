using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Task2.Core.Model;

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
                            CheckNumeric(args[1], Params.MaxSymbolsInWord);
                            command = CommandLineCommand.PrintDistinctWordsFromQuestionByWordLength;
                            break;
                        case CommandLineArguments.DeleteWordsByWordLength:
                            CheckNumeric(args[1], Params.MaxSymbolsInWord);
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
                            CheckNumeric(args[2], Params.MaxSymbolsInWord);
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
            const char QUOTATION = '"';
            const char SEPARATOR = ' ';

            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                return Array.Empty<string>();
            }

            if (line.Count(ch => ch == QUOTATION) % 2 != 0)
            {
                throw new ArgumentException("Неверный формат командной строки");
            }

            var arguments = line.Split(SEPARATOR).ToList();

            int index = arguments.FindIndex(s => s.First() == QUOTATION);

            while (index != -1)
            {
                int nextIndex = arguments.FindIndex(index, s => s.Last() == QUOTATION);

                if (nextIndex == -1)
                {
                    throw new ArgumentException("Неверная расстановка кавычек");
                }

                arguments[index] = arguments[index][1..];
                for (int i = index; i < nextIndex; i++)
                {
                    arguments[index] += ' ' + arguments[index + 1];
                    arguments.RemoveAt(index + 1);
                }

                arguments[index] = arguments[index][..^1];

                index = arguments.FindIndex(index + 1, s => s.First() == QUOTATION);
            }

            return arguments.ToArray();
        }

        private static void CheckNumeric(string str, int maxValue = int.MaxValue)
        {
            const int MIN_VALUE = 1;

            if (!int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out var number) ||
                number < MIN_VALUE || number > maxValue)
            {
                throw new ArgumentException($"\"{str}\" Длина слова должна быть в диапазоне {MIN_VALUE}-{maxValue}");
            }
        }

        private void CheckPath(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException("Неверный путь файла");
            }

            if (Path.GetFileNameWithoutExtension(str).IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                throw new ArgumentException("Неверное имя файла");
            }

            _ = new FileInfo(str);
        }

        private void CheckSubstring(string str)
        {
            char[] endOfSentenceChar = { '.', '!', '?' };

            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("Строка не должна быть пустой или состоять из одних пробелов");
            }

            var index = str.IndexOfAny(endOfSentenceChar);
            if (index != -1)
            {
                throw new ArgumentException($"подстрока не должна содержать символ конца предложения \"{str[index]}\"");
            }
        }

    }
}
