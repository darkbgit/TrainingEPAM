using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;

namespace Task3.EventsArgs
{
    public class StationStartCallAnswerEventsArgs : EventArgs
    {
        public StationStartCallAnswerEventsArgs(bool isAccept, PhoneNumber called)
        {
            IsAccept = isAccept;
            Called = called;
        }

        public bool IsAccept { get; set; }

        public PhoneNumber Called { get; set; }

    }
}
