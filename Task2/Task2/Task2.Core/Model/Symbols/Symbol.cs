namespace Task2.Core.Model.Symbols
{
    public abstract class Symbol
    {
        protected readonly char _symbol;
        protected Symbol(char symbol, SymbolType type)
        {
            _symbol = symbol;
            Type = type;
        }

        public SymbolType Type { get; }

        public char SymbolChar => _symbol;

        public override string ToString() => _symbol.ToString();

    }
}
