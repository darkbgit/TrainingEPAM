using System;

namespace ATS.Core.EventsArgs
{
    public class StationStartCallAfterAnswerEventArgs : EventArgs
    {
        public StationStartCallAfterAnswerEventArgs(Guid sourceTerminalIdTerminal, Guid targetTerminalIdTerminal, bool isAccept)
        {
            SourceTerminalId = sourceTerminalIdTerminal;
            TargetTerminalId = targetTerminalIdTerminal;
            IsAccept = isAccept;
        }


        public Guid SourceTerminalId { get; set; }

        public Guid TargetTerminalId { get; set; }

        public bool IsAccept { get; set; }
    }
}
