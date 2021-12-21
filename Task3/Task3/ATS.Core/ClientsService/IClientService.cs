using System;
using ATS.Core.AutomaticTelephoneSystem;

namespace ATS.Core.ClientsService
{
    public interface IClientService
    {
        PhoneNumber PhoneNumber { get; }
        void Call(PhoneNumber targetNumber);
        void ConnectToPort();
        void DisconnectFromPort();
        void Answer(bool isAccept);
        void EndCall();

        void GetReport(DateTime startDate, DateTime endDate);
    }
}
