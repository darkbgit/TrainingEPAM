using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Task2.Core.Analyzer.StateMachine;
using Task2.Core.Loggers;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
using Task2.Core.Model.Symbols;
using Task2.Core.Model.Symbols.OneSign;


namespace Task2.Core.Analyzer
{
    public class StreamAnalyzer : IAnalyzer
    {
        private readonly ILogger _logger;
        private readonly Stream _stream;


        public StreamAnalyzer(Stream stream, ILogger logger)
        {
            _logger = logger;
            _stream = stream;
        }

        public StreamAnalyzer(Stream stream)
        {
            _logger = null;
            _stream = stream;
        }

        public IText Analyze()
        {
            var buffer = new AnalyzerBuffer();

            IStateMachine stateMachine = new StateMachine.StateMachine(buffer);

            using (var reader = new StreamReader(_stream, Encoding.Default))
            {
                while (reader.Peek() != -1)
                {
                    var charBuffer = new char[4096];

                    var readLength = reader.Read(charBuffer, 0, charBuffer.Length);

                    for (var i = 0; i < readLength; i++)
                    {
                        var nextSymbol = GetSymbol(charBuffer[i]);

                        try
                        {
                            stateMachine.MoveNext(nextSymbol)?.Invoke(nextSymbol);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            _logger?.Log(e.Message);
                        }
                        catch (StateMachineException e)
                        {
                            _logger?.Log(e.Message);
                        }
                    }
                }
            }

            try
            {
                stateMachine.MoveNext(new EndSymbol()).Invoke(new EndSymbol());
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger?.Log(e.Message);
            }
            catch (StateMachineException e)
            {
                _logger?.Log(e.Message);
            }

            if (buffer.Sentences.Any())
            {
                _logger?.Log($"Сериализовано {buffer.Sentences.Count} предложений");
                return new Text(buffer.Sentences);
            }

            throw new ArgumentException("Stream didn't serialized");
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

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
