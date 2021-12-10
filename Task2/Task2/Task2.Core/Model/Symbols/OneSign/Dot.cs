using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.OneSign
{
    public class Dot : Symbol, ISymbol, ISentenceElement
    {
        private const char DOT_CHAR = '.'; 
        public Dot()
            : base(DOT_CHAR, SymbolType.Dot)
        {

        }

        public override string ToString() => _symbol.ToString();
    }
}
