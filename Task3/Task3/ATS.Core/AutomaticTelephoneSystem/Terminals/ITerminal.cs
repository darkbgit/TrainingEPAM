using System;
using ATS.Core.EventsArgs;

namespace ATS.Core.AutomaticTelephoneSystem.Terminals
{
    internal interface ITerminal
    {
        event EventHandler EndCall;
        event EventHandler<TerminalStartCallEventArgs> StartCall;
        event EventHandler<TerminalAnswerRequestEventArgs> AnswerCall;

        PhoneNumber PhoneNumber { get; }

        void AnswerRequest(bool isAccept);
        void Call(PhoneNumber targetNumber);
        void ConnectToPort();
        void DisconnectFromPort();
        void End();
        void GetAnswer(object sender, StationStartCallAfterAnswerEventArgs e);
        void OnPortEndCallByStation(object sender, StationEndCallEventArgs e);
        void OnRequest(object sender, StationStartCallRequestEventArgs e);
    }
}
