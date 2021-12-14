using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.EventsArgs;

namespace Task3
{
    public class Terminal
    {
        private const string PHONE_CODE = "+37529";

        private const int MIN_PHONE_NUMBER = 1;

        private const int MAX_PHONE_NUMBER = 9999999;
        
        public event EventHandler<StartCallEventArgs> StartCalling;

        public Terminal()
        {
            Id = Guid.NewGuid();
            var phone = new Random().Next(MIN_PHONE_NUMBER, MAX_PHONE_NUMBER);
            PhoneNumber =  new PhoneNumber("22-22-85");
        }
        public Guid Id { get; set; }

        

        public PhoneNumber PhoneNumber { get; set; }


        public void StartCall(PhoneNumber called)
        {
            StartCalling?.Invoke(this, new StartCallEventArgs(called));
        }

        public void OnRequest(object sender, SendRequestEventsArgs e)
        {
            Console.WriteLine($"Входящий вызов от {e.Caller.Number}");
        }
    }
}
