using System;

namespace ATS.Core.AutomaticTelephoneSystem.EventsArgs
{
    public class PortAnswerRequestEventArgs : EventArgs
    {
        public PortAnswerRequestEventArgs(bool isAccept, Guid sourceTerminalId, Guid targetTerminalId)
        {
            IsAccept = isAccept;
            SourceTerminalId = sourceTerminalId;
            TargetTerminalId = targetTerminalId;
        }

        public bool IsAccept { get; }

        public Guid SourceTerminalId { get; }

        public Guid TargetTerminalId { get; }
    }
}