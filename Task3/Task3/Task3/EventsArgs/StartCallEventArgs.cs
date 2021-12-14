using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.EventsArgs
{
    public class StartCallEventArgs
    {
        public StartCallEventArgs(PhoneNumber called)
        {
            Called = called;
        }

        public PhoneNumber Caller { get; set; }

        public PhoneNumber Called { get; set; }
    }
}
