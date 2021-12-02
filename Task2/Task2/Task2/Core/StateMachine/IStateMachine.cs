using Task2.Core.TextObjectModel.Symbols;

namespace Task2.Core.StateMachine
{
    internal interface  IStateMachine
    {
        SymbolChangeDelegate MoveNext(SymbolType nextSymbol);
    }
}
