using System;

namespace ATS.Core.AutomaticTelephoneSystem.EventsArgs
{
    public class StationStartCallRequestEventArgs : EventArgs
    {
        public StationStartCallRequestEventArgs(PhoneNumber sourcePhoneNumber, Guid sourceTerminalId)
        {
            SourcePhoneNumber = sourcePhoneNumber;
            SourceTerminalId = sourceTerminalId;
        }

        public Guid SourceTerminalId { get; }
        public PhoneNumber SourcePhoneNumber { get; }
    }
}
