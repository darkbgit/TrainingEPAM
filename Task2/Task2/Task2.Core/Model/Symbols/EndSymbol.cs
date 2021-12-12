using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols
{
    internal class EndSymbol : ISymbol
    {
        public EndSymbol()
        {
            Type = SymbolType.End;
        }
        public SymbolType Type { get; }
    }
}
