using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Exclamation : Symbol, ISymbol
    {
        private const char EXCLAMATION_CHAR = '.';
        public Exclamation()
            : base(EXCLAMATION_CHAR, SymbolType.Exclamation)
        {

        }

        public override string ToString() => _symbol.ToString();
    }
}
