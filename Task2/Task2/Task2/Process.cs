using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Process
    {
        public enum ProcessState
        {
            Inactive,
            Active,
            Paused,
            Terminated
        }

        public enum Command
        {
            Begin,
            End,
            Pause,
            Resume,
            Exit
        }

        class StateTransition
        {
            private readonly ProcessState CurrentState;
            private readonly Command Command;

            public StateTransition(ProcessState currentState, Command command)
            {
                CurrentState = currentState;
                Command = command;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj is StateTransition other && this.CurrentState == other.CurrentState && this.Command == other.Command;
            }
        }

        private Dictionary<StateTransition, ProcessState> transitions;

        public ProcessState CurrentState { get; private set; }

        public Process()
        {

        }
    }
}
