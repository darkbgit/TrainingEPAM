using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;

namespace Task3.EventsArgs
{
    public class StationStartCallRequestEventsArgs : EventArgs
    {
        public StationStartCallRequestEventsArgs(PhoneNumber sourcePhoneNumber)
        {
            SourcePhoneNumber = sourcePhoneNumber;
        }

        public PhoneNumber SourcePhoneNumber { get; }
    }
}
