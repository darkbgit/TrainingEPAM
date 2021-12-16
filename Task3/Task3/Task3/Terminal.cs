using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.EventsArgs;

namespace Task3
{
    public class Terminal : ITerminal
    {

        public event EventHandler<StartCallEventArgs> StartCall;

        public Terminal()
        {
            Id = Guid.NewGuid();
            PhoneNumber = GeneratePhoneNumber();
        }
        public Guid Id { get; set; }

        

        public PhoneNumber PhoneNumber { get; set; }


        public void Call(PhoneNumber targetNumber)
        {
            OnStartCall(this, new StartCallEventArgs(targetNumber));
        }

        public void OnRequest(object sender, StationSendRequestEventsArgs e)
        {
            Console.WriteLine($"Входящий вызов от {e.Caller.Number}");
        }

        protected virtual void OnStartCall(object sender, StartCallEventArgs e)
        {
            StartCall?.Invoke(sender, e);
        }

        private PhoneNumber GeneratePhoneNumber()
        {
            const string PHONE_CODE = "+37529";
            const int MIN_PHONE_NUMBER = 1;
            const int MAX_PHONE_NUMBER = 9999999;

            var phone = new Random().Next(MIN_PHONE_NUMBER, MAX_PHONE_NUMBER);

            return new PhoneNumber(PHONE_CODE + $"{phone:d7}");
        }
    }
}
