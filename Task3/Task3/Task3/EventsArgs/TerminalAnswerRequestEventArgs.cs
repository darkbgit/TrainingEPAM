using System;
using Task3.AutomaticTelephoneSystem;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.EventsArgs
{
    public class TerminalAnswerRequestEventArgs : EventArgs
    {
        public TerminalAnswerRequestEventArgs(bool isAccept, Guid callerTerminalId)//, Terminal calledTerminal)
        {
            IsAccept = isAccept;
            CallerTerminalId = callerTerminalId;
            //Called = calledTerminal;
        }

        public bool IsAccept { get; set; }

        public Guid CallerTerminalId { get; set; }

        //public Terminal Called { get; set; }
    }
}