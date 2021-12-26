using System;
using ATS.Core.AutomaticTelephoneSystem;

namespace Services.ClientsService
{
    public interface IClientService
    {
        PhoneNumber PhoneNumber { get; }
        void Call(PhoneNumber targetNumber);
        void ConnectToPort();
        void DisconnectFromPort();
        void Answer(bool isAccept);
        void EndCall();


        void GetReport(DateTime startDate, DateTime endDate, PhoneNumber interlocutorPhoneNumber = null);
        void GetReport(PhoneNumber interlocutorPhoneNumber = null);
    }
}
