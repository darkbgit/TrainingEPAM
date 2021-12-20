using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;

namespace Task3.EventsArgs
{
    public class StationEndCallEventArgs : EventArgs
    {
        public StationEndCallEventArgs(Guid endCallTerminalId)
        {
            EndCallTerminalId = endCallTerminalId;
        }

        public Guid EndCallTerminalId { get; set; }
    }
}
