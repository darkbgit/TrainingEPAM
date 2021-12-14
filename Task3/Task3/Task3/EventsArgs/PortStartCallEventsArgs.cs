using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.States;

namespace Task3.EventsArgs
{
    public class PortStartCallEventsArgs
    {


        public PortStartCallEventsArgs(PhoneNumber caller, PhoneNumber called)
        {
            Caller = caller;
            Called = called;
        }

        public int Port { get; set; }

        public PhoneNumber Caller { get; set; }

        public PhoneNumber Called { get; set; }
    }
}
