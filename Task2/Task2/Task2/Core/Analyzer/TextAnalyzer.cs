using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.StateMachine;
using Task2.Core.TextObjectModel;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;
using Task2.Core.TextObjectModel.Symbols.OneSign;
using Task2.Output;

namespace Task2.Core.Analyzer
{
    internal class TextAnalyzer : IAnalyzer
    {
        private readonly ILogger _logger;

        public TextAnalyzer(ILogger logger)
        {
            _logger = logger;
        }

        public IText Analyze(string filePath)
        {
            ICollection<string> errorList = new List<string>();

            var buffer = new AnalyzerBuffer();

            IStateMachine stateMachine = new StateMachine.StateMachine();

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(fs);

                while (reader.Peek() != -1)
                {
                    var character = reader.Read();

                    var nextSymbol = GetSymbol((char) character);

                    try
                    {
                        stateMachine.MoveNext(nextSymbol.Type)?.Invoke(nextSymbol, ref buffer);
                    }
                    catch (ArgumentException e)
                    {
                        errorList.Add($"{e.Message} in sentence number {buffer.Sentences.Count + 1}");
                    }
                }
            }

            stateMachine.MoveNext(SymbolType.End).Invoke(null, ref buffer);

            IText text = new Text(buffer.Sentences);

            PrintResult(errorList);

            return text;
        }

        private static ISymbol GetSymbol(char c)
        {
            ISymbol symbol;

            switch (char.GetUnicodeCategory(c))
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.LowercaseLetter:
                    symbol = new Letter(c);
                    break;
                case UnicodeCategory.DecimalDigitNumber:
                    symbol = new Digit(c);
                    break;
                case UnicodeCategory.InitialQuotePunctuation:
                case UnicodeCategory.FinalQuotePunctuation:
                case UnicodeCategory.OpenPunctuation:
                case UnicodeCategory.ClosePunctuation:
                case UnicodeCategory.DashPunctuation:
                case UnicodeCategory.OtherPunctuation:
                case UnicodeCategory.ConnectorPunctuation:
                    symbol = c switch
                    {
                        '.' => new Dot(),
                        '!' => new Exclamation(),
                        '?' => new Question(),
                        _ => new Punctuation(c)
                    };
                    break;
                case UnicodeCategory.SpaceSeparator:
                case UnicodeCategory.Control:
                    symbol = new Space();
                    break;
                default:
                    symbol = new Undefined(c);
                    break;
            }

            return symbol;
        }

        private void PrintResult(IEnumerable<string> errors)
        {
            var enumerable = errors as string[] ?? errors.ToArray();

            if (enumerable.Any())
            {
                _logger.Print($"Количество ошибок при конвертации - {enumerable.Length}");
                foreach (var error in enumerable)
                {
                    _logger.Print(error);
                }
            }
            else
            {
                _logger.Print("Текст сериализован без ошибок");
            }
        }
    }
}
