using System;
using ATS.Core.EventsArgs;

namespace ATS.Core.AutomaticTelephoneSystem.Stations
{
    public interface IStation
    {
        event EventHandler<StationStartCallAfterAnswerEventArgs> StationStartCall;
        event EventHandler<StationEndCallEventArgs> StationEndCall;

        void ConnectTerminalToPort(object sender, EventArgs e);
        void DisconnectTerminalFromPort(object sender, EventArgs e);
    }
}
