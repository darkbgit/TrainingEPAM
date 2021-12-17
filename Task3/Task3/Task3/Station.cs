using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.EventsArgs;

namespace Task3
{
    public class Station
    {
        private readonly Contracts _contracts;

        public Station(Contracts contracts)
        {
            _contracts = contracts;
        }


        public void OnCall(object sender, PortStartCallEventsArgs e)
        {
            GetPortByPhoneNumber(e.Called).OnRequestFromStation(this, new StationSendRequestEventsArgs(e.Caller));
        }

        public void OnRequestAnswer(object sender, AnswerCallEventArgs e)
        {
            if (e.AnswerTheCall)
            {
                Console.WriteLine($"Звонок между {e.Caller} и {e.Called} начался");
            }
            else
            {
                Console.WriteLine($"Абонент {e.Called} сбросил звонок от {e.Caller}");
            }
        }


        private Port GetPortByPhoneNumber(PhoneNumber phoneNumber) => _contracts.GetPortByPhoneNumber(phoneNumber)
                ?? throw new StationException($"Для номера телефона \"{phoneNumber}\" нет привязанного порта");


    }
}
