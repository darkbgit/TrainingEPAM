using System;

namespace ATS.Core.AutomaticTelephoneSystem.EventsArgs
{
    public class PortStartCallEventArgs : EventArgs
    {
        public PortStartCallEventArgs(PhoneNumber sourcePhoneNumber, PhoneNumber targetPhoneNumber, Guid sourceTerminalId)
        {
            SourcePhoneNumber = sourcePhoneNumber;
            TargetPhoneNumber = targetPhoneNumber;
            SourceTerminalId = sourceTerminalId;
        }

        public Guid SourceTerminalId { get; }

        public PhoneNumber SourcePhoneNumber { get; }

        public PhoneNumber TargetPhoneNumber { get; }
    }
}
