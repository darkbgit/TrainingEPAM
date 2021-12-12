using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.OneSign
{
    public class Undefined : Symbol, ISymbol
    {
        public Undefined(char symbol)
            : base(symbol, SymbolType.Undefined)
        {
        }

        public override string ToString() => _symbol.ToString();
    }
}
