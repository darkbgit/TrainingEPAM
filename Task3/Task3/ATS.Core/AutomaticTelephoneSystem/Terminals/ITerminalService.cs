using System;
using ATS.Core.EventsArgs;

namespace ATS.Core.AutomaticTelephoneSystem.Terminals
{
    public interface ITerminalService
    {
        public event EventHandler<TerminalStartCallEventArgs> StartCall;
        public event EventHandler<TerminalAnswerRequestEventArgs> AnswerCall;
        public event EventHandler EndCall;
        public event EventHandler TerminalConnectToPort;
        public event EventHandler TerminalDisconnectFromPort;

        PhoneNumber PhoneNumber { get; }

        void AnswerRequest(bool isAccept);
        void Call(PhoneNumber targetNumber);
        void ConnectToPort();
        void DisconnectFromPort();
        void End();
        void GetAnswer(object sender, StationStartCallAfterAnswerEventArgs e);
        void OnEndCallByTarget(object sender, StationEndCallEventArgs e);
        void OnRequest(object sender, StationStartCallRequestEventArgs e);
    }
}
