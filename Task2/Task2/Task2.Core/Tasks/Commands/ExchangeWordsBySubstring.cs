using System;
using System.IO;
using System.Linq;
using System.Text;
using Task2.Core.Analyzer;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Tasks.Commands
{
    internal class ExchangeWordsBySubstring
    {
        private readonly IText _text;

        public ExchangeWordsBySubstring(IText text)
        {
            _text = text;
        }

        public void Execute(int sentenceNumber, int wordLength, string substring)
        {
            var textLength = _text.Count();
            if (sentenceNumber > textLength)
            {
                throw new ArgumentException(
                    $"Номер предложения слишком большой. Количество предложений в тексте {textLength}");
            }


            var stream = StrToStream(substring);

            IAnalyzer analyzer = new StreamAnalyzer(stream);

            var text = analyzer.Analyze();

            int sentenceIndex = sentenceNumber - 1;

            for (int i = 0; i < _text.ElementAt(sentenceIndex).Count(); i++)
            {
                if ((_text.ElementAt(sentenceIndex).ElementAt(i) as Word)?.Length() == wordLength)
                {
                    _text.ElementAt(sentenceIndex).RemoveAt(i);
                    _text.ElementAt(sentenceIndex).AddRangeAt(i, text.First());
                    i += text.First().Count();
                }
            }
        }

        private Stream StrToStream(string substring)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(substring));
        }
    }
}
