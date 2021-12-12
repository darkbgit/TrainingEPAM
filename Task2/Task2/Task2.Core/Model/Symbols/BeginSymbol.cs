using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols
{
    internal class BeginSymbol : ISymbol
    {
        public BeginSymbol()
        {
            Type = SymbolType.Begin;
        }
        public SymbolType Type { get; }
    }
}
