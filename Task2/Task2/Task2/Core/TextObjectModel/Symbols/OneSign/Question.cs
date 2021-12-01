using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Question : Symbol, ISymbol
    {
        private const char QUESTION_CHAR = '?';
        public Question()
            : base(QUESTION_CHAR, SymbolType.Question)
        {

        }

        public override string ToString() => _symbol.ToString();
    }
}
