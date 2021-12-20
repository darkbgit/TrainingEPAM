using System;

namespace ATS.Core.EventsArgs
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
