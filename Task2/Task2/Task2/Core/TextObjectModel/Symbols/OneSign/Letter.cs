using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Letter : Symbol, ISymbol
    {
        public Letter(char symbol):
            base(symbol, SymbolType.LetterOrDigit)
        {
            
        }

        public override string ToString() => _symbol.ToString();
    }
}
