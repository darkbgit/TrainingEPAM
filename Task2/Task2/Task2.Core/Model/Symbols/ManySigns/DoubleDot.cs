using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.ManySigns
{
    internal class DoubleDot : ISymbol
    {

        public DoubleDot()
        {
            Type = SymbolType.DoubleDot;
        }


        public SymbolType Type { get; }
    }
}
