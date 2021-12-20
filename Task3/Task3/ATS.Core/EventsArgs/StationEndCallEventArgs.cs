using System;

namespace ATS.Core.EventsArgs
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
