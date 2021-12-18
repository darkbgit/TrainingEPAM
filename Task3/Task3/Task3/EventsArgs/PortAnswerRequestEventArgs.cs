using System;
using Task3.AutomaticTelephoneSystem;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.EventsArgs
{
    public class PortAnswerRequestEventArgs : EventArgs
    {
        public PortAnswerRequestEventArgs(bool isAccept, Guid callerTerminalId, Terminal called)
        {
            IsAccept = isAccept;
            CallerTerminalId = callerTerminalId;
            Called = called;
        }

        public bool IsAccept { get; set; }

        public Guid CallerTerminalId { get; set; }

        public Terminal Called { get; set; }
    }
}