using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Analyzer.StateMachine
{
    internal class StateMachineException : Exception
    {
        public StateMachineException(string message) : base(message)
        {
        }
    }
}
