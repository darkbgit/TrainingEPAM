using System;

namespace ATS.Core.AutomaticTelephoneSystem.EventsArgs
{
    public class TerminalAnswerRequestEventArgs : EventArgs
    {
        public TerminalAnswerRequestEventArgs(bool isAccept, Guid sourceTerminalId)
        {
            IsAccept = isAccept;
            SourceTerminalId = sourceTerminalId;
        }

        public bool IsAccept { get; }

        public Guid SourceTerminalId { get; }

    }
}