using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.EventsArgs
{
    public class StationSendRequestEventsArgs
    {
        public StationSendRequestEventsArgs(PhoneNumber caller)
        {
            Caller = caller;
        }

        public PhoneNumber Caller { get; set; }

        //public int PortDefenition { get; set; }


        //public PhoneNumber Called { get; set; }
    }
}
