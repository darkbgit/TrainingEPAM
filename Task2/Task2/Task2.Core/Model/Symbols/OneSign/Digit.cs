using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.OneSign
{
    public class Digit : Symbol, ISymbol
    {
        public Digit(char symbol):
            base(symbol, SymbolType.LetterOrDigit)
        {

        }
    }
}
