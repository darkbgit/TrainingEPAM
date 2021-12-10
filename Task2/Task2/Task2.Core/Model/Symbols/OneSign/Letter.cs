using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.OneSign
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
