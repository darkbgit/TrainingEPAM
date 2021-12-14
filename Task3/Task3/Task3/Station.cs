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


        public void OnCall(object sender, PortStartCallEventsArgs e)
        {
            if (ChekPhoneNumber(e.Called))
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

        public bool ChekPhoneNumber(PhoneNumber phoneNumber)
        {
            return true;
        }
    }
}
