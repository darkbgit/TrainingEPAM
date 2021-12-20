using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.States;

namespace Task3.EventsArgs
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
