using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.EventsArgs
{
    public class PortEndCallEventArgs : EventArgs
    {
        public PortEndCallEventArgs(Guid endCallTerminalId)
        {
            EndCallTerminalId = endCallTerminalId;
        }

        public Guid EndCallTerminalId { get; set; }
    }
}
