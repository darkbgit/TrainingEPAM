using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Dot : Symbol, ISymbol
    {
        private const char DOT_CHAR = '.'; 
        public Dot()
            : base(DOT_CHAR, SymbolType.Dot)
        {

        }

        public override string ToString() => _symbol.ToString();
    }
}
