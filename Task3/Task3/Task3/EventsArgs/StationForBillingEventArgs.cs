using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.EventsArgs
{
    public class StationForBillingEventArgs : EventArgs
    {
        public StationForBillingEventArgs(Terminal callerTerminal, Terminal calledTerminal, bool isStart)
        {
            Caller = callerTerminal;
            Called = calledTerminal;
            IsStart = isStart;
        }


        public Terminal Caller { get; set; }

        public Terminal Called { get; set; }

        public bool IsStart { get; set; }
    }
}
