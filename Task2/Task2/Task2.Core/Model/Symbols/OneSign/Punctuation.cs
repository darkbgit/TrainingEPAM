using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.OneSign
{
    public class Punctuation : Symbol, ISymbol, ISentenceElement
    {
        public Punctuation(char symbol)
            : base(symbol, SymbolType.PunctuationMark)
        {
        }

        public override string ToString() => _symbol.ToString();
    }
}
