using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.BillingSystem;
using Task3.EventsArgs;

namespace Task3
{
    internal class PortListener
    {
        private readonly Port _port;

        private readonly IBilling _billing;


        public PortListener(Port port, IBilling billing)
        {
            _port = port;
            _billing = billing;
        }

        public void Port_OnStateChanged(object sender, PortStartCallEventsArgs e)
        {
            //_billing.AddCall(new Record
            //{
            //    BeginCall = e.CallStart,
            //    EndCall = e.CallEnd,
            //    CallerUser = e.Caller,
            //    CalledUser = e.Called
            //});
        }
    }
}
