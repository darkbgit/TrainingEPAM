using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;

namespace Task3.EventsArgs
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
