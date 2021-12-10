using System;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Analyzer.StateMachine
{
    internal interface  IStateMachine
    {
        Action<ISymbol> MoveNext(ISymbol nextSymbol);
    }
}
