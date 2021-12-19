using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;

namespace Task3.EventsArgs
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
