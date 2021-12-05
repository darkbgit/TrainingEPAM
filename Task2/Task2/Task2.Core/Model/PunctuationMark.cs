using System.Collections.Generic;
using Task2.Core.Model.Interfaces;
using Task2.Core.Model.Symbols;

namespace Task2.Core.Model
{
    public class PunctuationMark : ISentenceElement
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
