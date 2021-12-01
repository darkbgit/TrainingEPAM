using Task2.Core.TextObjectModel.Symbols;

namespace Task2.Core.StateMachine
{
    public class StateTransition
    {
        private readonly SymbolType _currentSymbol;
        private readonly SymbolType _nextSymbol;

        public StateTransition(SymbolType currentSymbol, SymbolType nextSymbol)
        {
            _currentSymbol = currentSymbol;
            _nextSymbol = nextSymbol;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * _currentSymbol.GetHashCode() + 31 * _nextSymbol.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is StateTransition other && _currentSymbol == other._currentSymbol && _nextSymbol == other._nextSymbol;
        }
    }
}