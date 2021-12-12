using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.OneSign
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
