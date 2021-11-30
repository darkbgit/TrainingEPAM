using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Punctuation : Symbol, ISymbol
    {
        public Punctuation(char symbol)
            : base(symbol, SymbolType.PunctuationMark)
        {
        }

        public override string ToString() => _symbol.ToString();
    }
}
