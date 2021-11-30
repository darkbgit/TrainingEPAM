using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Digit : Symbol, ISymbol
    {
        public Digit(char symbol):
            base(symbol, SymbolType.Digit)
        {

        }

        public override string ToString() => _symbol.ToString();
    }
}
