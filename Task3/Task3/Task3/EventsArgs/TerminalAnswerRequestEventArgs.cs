using System;
using Task3.AutomaticTelephoneSystem;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.EventsArgs
{
    public class TerminalAnswerRequestEventArgs : EventArgs
    {
        public TerminalAnswerRequestEventArgs(bool isAccept, Guid sourceTerminalId)
        {
            IsAccept = isAccept;
            SourceTerminalId = sourceTerminalId;
        }

        public bool IsAccept { get; set; }

        public Guid SourceTerminalId { get; set; }

    }
}