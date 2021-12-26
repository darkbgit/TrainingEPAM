using System;

namespace ATS.Core.AutomaticTelephoneSystem.EventsArgs
{
    public class StationStartCallAfterAnswerEventArgs : EventArgs
    {
        public StationStartCallAfterAnswerEventArgs(Guid sourceTerminalIdTerminal, Guid targetTerminalIdTerminal, bool isAccept)
        {
            SourceTerminalId = sourceTerminalIdTerminal;
            TargetTerminalId = targetTerminalIdTerminal;
            IsAccept = isAccept;
        }


        public Guid SourceTerminalId { get; }

        public Guid TargetTerminalId { get; }

        public bool IsAccept { get; }
    }
}
