using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.ManySigns
{
    public class Ellipsis : ISymbol, ISentenceElement
    {
        private const string ELLIPSIS_WRITING = "...";
        public Ellipsis()
        {
            Type = SymbolType.Ellipsis;
        }

        public SymbolType Type { get; }

        public override string ToString() => ELLIPSIS_WRITING;
    }
}
