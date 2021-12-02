using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    internal class Undefined : Symbol, ISymbol
    {
        public Undefined(char symbol)
            : base(symbol, SymbolType.Undefined)
        {
        }

        public override string ToString() => _symbol.ToString();
    }
}
