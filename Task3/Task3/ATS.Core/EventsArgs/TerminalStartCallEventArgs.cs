using System;
using ATS.Core.AutomaticTelephoneSystem;


namespace ATS.Core.EventsArgs
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
