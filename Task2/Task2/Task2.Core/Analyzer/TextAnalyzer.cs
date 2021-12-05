using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Analyzer.StateMachine;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
using Task2.Core.Model.Symbols;
using Task2.Core.Model.Symbols.OneSign;


namespace Task2.Core.Analyzer
{
    public class TextAnalyzer : IAnalyzer
    {
        //private readonly Ilogger _logger;
        private AnalyzerBuffer _buffer;
        private IStateMachine _machine;


        public TextAnalyzer()
        {
            
        }


        public Stream GetStream(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read);

        }

        public IText Analyze(Stream stream)
        {
            _machine = new StateMachine.StateMachine();

            ICollection<string> errorList = new List<string>();

            _buffer = new AnalyzerBuffer();

            IStateMachine stateMachine = new StateMachine.StateMachine();

            using (stream)
            {
                var reader = new StreamReader(stream);

                while (reader.Peek() != -1)
                {
                    var character = reader.Read();

                    var nextSymbol = GetSymbol((char)character);

                    try
                    {
                        stateMachine.MoveNext(nextSymbol)?.Invoke(nextSymbol);
                    }
                    catch (ArgumentException e)
                    {
                        errorList.Add($"{e.Message} in sentence number {_buffer.Sentences.Count + 1}");
                    }
                }
            }

            stateMachine.MoveNext(new NoSymbol()).Invoke(null);

            IText text = new Text(_buffer.Sentences);

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
            //var enumerable = errors as string[] ?? errors.ToArray();

            //_logger.Print("Текст сериализован");
            //_logger.Print($"Количество предложений {_buffer.Sentences.Count}");

            //if (enumerable.Any())
            //{
            //    _logger.Print($"Количество ошибок при конвертации - {enumerable.Length}");
            //    foreach (var error in enumerable)
            //    {
            //        _logger.Print(error);
            //    }
            //}
        }
    }
}
