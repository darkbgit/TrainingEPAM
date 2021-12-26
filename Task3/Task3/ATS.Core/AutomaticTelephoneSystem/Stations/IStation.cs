using System;
using ATS.Core.AutomaticTelephoneSystem.EventsArgs;

namespace ATS.Core.AutomaticTelephoneSystem.Stations
{
    internal interface IStation
    {
        event EventHandler<StationStartCallAfterAnswerEventArgs> StationStartCall;
        event EventHandler<StationEndCallEventArgs> StationEndCall;

        void ConnectTerminalToPort(object sender, EventArgs e);
        void DisconnectTerminalFromPort(object sender, EventArgs e);
    }
}
