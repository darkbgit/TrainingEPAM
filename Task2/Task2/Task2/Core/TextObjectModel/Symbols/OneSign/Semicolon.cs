using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Semicolon : Symbol, ISymbol, ISentenceElement
    {
        private const char SEMICOLON_CHAR = ';';
        public Semicolon()
            : base(SEMICOLON_CHAR, SymbolType.PunctuationMark)
        {

        }
    }
}
