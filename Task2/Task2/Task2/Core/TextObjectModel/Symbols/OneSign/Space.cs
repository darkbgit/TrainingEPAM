using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Space : Symbol, ISymbol, ISentenceElement
    {
        private  const char SPACE_CHAR = ' ';
        public Space():
            base(SPACE_CHAR, SymbolType.Space)
        {

        }

        public override string ToString() => _symbol.ToString();
    }
}
