using System.Collections.Generic;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;

namespace Task2.Core.TextObjectModel
{
    public class PunctuationMark : IPunctuation, ISentenceElement
    {
        private readonly string _writing; 

        public PunctuationMark(ISymbol symbol)
        {
            _writing = symbol.ToString();
        }

        public PunctuationMark(IEnumerable<ISymbol> symbols)
        {
            _writing = string.Concat(symbols);
        }


        public override string ToString() => _writing;
    }
}
