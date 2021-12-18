using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;

namespace Task3.EventsArgs
{
    public class StartCallEventArgs : EventArgs
    {
        public StartCallEventArgs(PhoneNumber called)
        {
            Called = called;
        }

        public PhoneNumber Called { get; set; }
    }
}
