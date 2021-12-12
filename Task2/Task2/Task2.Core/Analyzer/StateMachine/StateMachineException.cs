using System;

namespace Task2.Core.Analyzer.StateMachine
{
    internal class StateMachineException : Exception
    {
        public StateMachineException(string message) : base(message)
        {
        }
    }
}
