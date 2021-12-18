using System;
using Task3.AutomaticTelephoneSystem.Ports;

namespace Task3.AutomaticTelephoneSystem.Stations
{
    internal class CallingTerminalsPour
    {
        public CallingTerminalsPour(Guid callerTerminalId, Guid calledTerminalId)
        {
            CallerTerminalId = callerTerminalId;
            CalledTerminalId = calledTerminalId;
        }

        public Guid CallerTerminalId { get; }

        public Guid CalledTerminalId { get; }

        public Guid GetAnotherTerminalId(Guid terminalId)
        {
            if (CallerTerminalId == terminalId)
            {
                return CalledTerminalId;
            }
            
            if (CalledTerminalId == terminalId)
            {
                return CallerTerminalId;
            }

            return Guid.Empty;
        }

    }
}
