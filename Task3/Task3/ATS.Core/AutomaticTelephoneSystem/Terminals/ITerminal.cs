using System;

namespace ATS.Core.AutomaticTelephoneSystem.Terminals
{
    public interface ITerminal
    {
        event EventHandler TerminalConnectToPort;
        event EventHandler TerminalDisconnectFromPort;

        Guid Id { get; }
        PhoneNumber PhoneNumber { get; }

        void AnswerRequest(bool isAccept);
        void Call(PhoneNumber targetNumber);
        void ConnectToPort();
        void DisconnectFromPort();
        void End();
        void Print(string message);
    }
}
