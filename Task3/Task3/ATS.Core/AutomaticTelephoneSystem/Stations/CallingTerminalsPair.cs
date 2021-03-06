using System;

namespace ATS.Core.AutomaticTelephoneSystem.Stations
{
    internal class CallingTerminalsPair
    {
        public CallingTerminalsPair(Guid sourceTerminalId, Guid targetTerminalId)
        {
            SourceTerminalId = sourceTerminalId;
            TargetTerminalId = targetTerminalId;
        }

        public Guid SourceTerminalId { get; }

        public Guid TargetTerminalId { get; }

        public Guid GetAnotherTerminalId(Guid terminalId)
        {
            if (SourceTerminalId == terminalId)
            {
                return TargetTerminalId;
            }
            
            if (TargetTerminalId == terminalId)
            {
                return SourceTerminalId;
            }

            return Guid.Empty;
        }

    }
}
