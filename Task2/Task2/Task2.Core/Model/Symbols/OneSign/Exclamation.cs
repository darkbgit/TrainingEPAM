using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.OneSign
{
    public class Exclamation : Symbol, ISymbol, ISentenceElement
    {
        private const char EXCLAMATION_CHAR = '!';
        public Exclamation()
            : base(EXCLAMATION_CHAR, SymbolType.Exclamation)
        {

        }

        public override string ToString() => _symbol.ToString();
    }
}
