using System;

namespace ATS.Core.EventsArgs
{
    public class PortAnswerRequestEventArgs : EventArgs
    {
        public PortAnswerRequestEventArgs(bool isAccept, Guid sourceTerminalId, Guid targetTerminalId)
        {
            IsAccept = isAccept;
            SourceTerminalId = sourceTerminalId;
            TargetTerminalId = targetTerminalId;
        }

        public bool IsAccept { get; set; }

        public Guid SourceTerminalId { get; set; }

        public Guid TargetTerminalId { get; set; }
    }
}