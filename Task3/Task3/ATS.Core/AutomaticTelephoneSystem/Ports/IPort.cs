using System;
using ATS.Core.AutomaticTelephoneSystem.EventsArgs;
using ATS.Core.AutomaticTelephoneSystem.Ports.States;

namespace ATS.Core.AutomaticTelephoneSystem.Ports
{
    internal interface IPort
    {
        event EventHandler<PortStartCallEventArgs> StartCall;
        event EventHandler<StationStartCallRequestEventArgs> SendRequest;
        event EventHandler<PortAnswerRequestEventArgs> AnswerRequest;
        event EventHandler<StationStartCallAfterAnswerEventArgs> AnswerCall;
        event EventHandler<PortEndCallEventArgs> EndCallSource;
        event EventHandler<StationEndCallEventArgs> EndCallTarget;
        int Id { get; }
        PortState PortState { get; set; }
        void PortStartCall(object sender, TerminalStartCallEventArgs e);
        void PortStartCallRequest(object sender, StationStartCallRequestEventArgs e);
        void PortStartCallAnswer(object sender, TerminalAnswerRequestEventArgs e);
        void PortAnswerCall(object sender, StationStartCallAfterAnswerEventArgs e);
        void PortEndCallSource(object sender, EventArgs e);
        void PortEndCallTarget(object sender, StationEndCallEventArgs e);
    }
}
