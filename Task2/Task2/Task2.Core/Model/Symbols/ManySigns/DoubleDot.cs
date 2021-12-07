using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.ManySigns
{
    public class DoubleDot : ISymbol
    {

        public DoubleDot()
        {
            Type = SymbolType.DoubleDot;
        }


        public SymbolType Type { get; }
    }
}
