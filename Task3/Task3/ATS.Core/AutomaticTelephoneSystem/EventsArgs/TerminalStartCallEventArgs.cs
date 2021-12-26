using System;

namespace ATS.Core.AutomaticTelephoneSystem.EventsArgs
{
    public class TerminalStartCallEventArgs : EventArgs
    {
        public TerminalStartCallEventArgs(PhoneNumber targetPhoneNumber)
        {
            TargetPhoneNumber = targetPhoneNumber;
        }

        public PhoneNumber TargetPhoneNumber { get; }
    }
}
