using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;

namespace Task2.Core.TextObjectModel
{
    internal class EndOfSentencePunctuationMark: IPunctuation, ISentenceElement
    {
        private readonly string _writing;

        public EndOfSentencePunctuationMark(ISymbol symbol)
        {
            _writing = symbol.ToString();
        }

        public EndOfSentencePunctuationMark(IEnumerable<ISymbol> symbols)
        {
            _writing = string.Concat(symbols);
        }


        public override string ToString() => _writing;

    }
}
