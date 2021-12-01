using Task2.Core.TextObjectModel.Symbols;

namespace Task2.Core.StateMachine
{
    public interface  IStateMachine
    {
        SymbolChangeDelegate MoveNext(SymbolType nextSymbol);
    }
}
