using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.EventsArgs;

namespace Task3
{
    public class Station
    {
        public event EventHandler<SendRequestEventsArgs> SendRequest;

        private List<Port> _initializedPorts = new List<Port>();

        private List<Port> _portsInCall = new List<Port>();

        public void OnCall(object sender, PortStartCallEventsArgs e)
        {
            if (sender is Port port)
            {
                _initializedPorts.Add(port);
            }
            

            if (CheckPhoneNumber(e.Called))
            {
                SendRequest?.Invoke(this, new SendRequestEventsArgs(2, e.Caller));
            }
            //SendRequest?.Invoke(this, new SendRequestEventsArgs(2, e.Caller));
        }

        //public void OnRequestAnswer(object sender,)


        public int GetPortNumberByPhoneNumber(PhoneNumber phoneNumber)
        {
            return 0;
        }

        public bool CheckPhoneNumber(PhoneNumber phoneNumber)
        {
            return true;
        }
    }
}
