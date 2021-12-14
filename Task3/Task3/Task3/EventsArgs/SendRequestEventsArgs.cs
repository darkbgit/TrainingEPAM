using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.EventsArgs
{
    public class SendRequestEventsArgs
    {
        public SendRequestEventsArgs(int portDefenition, PhoneNumber caller)
        {
            Caller = caller;
            PortDefenition = portDefenition;
        }

        public PhoneNumber Caller { get; set; }

        public int PortDefenition { get; set; }


        //public PhoneNumber Called { get; set; }
    }
}
