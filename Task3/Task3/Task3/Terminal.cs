using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.EventsArgs;
using Task3.States;

namespace Task3
{
    public class Terminal : ITerminal
    {
        private PhoneNumber _incomingPhoneNumber;

        public event EventHandler<StartCallEventArgs> StartCall;

        public event EventHandler<AnswerCallEventArgs> AnswerCall;

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
        
        protected virtual void OnStartCall(object sender, StartCallEventArgs e)
        {
            if (StartCall == null) throw new PortException("Терминал не подключен к какому либо порту");
            StartCall?.Invoke(sender, e);
        }

        public void OnRequest(object sender, StationSendRequestEventsArgs e)
        {
            _incomingPhoneNumber = e.Caller;
            Console.WriteLine($"Входящий вызов от {e.Caller}");
        }

        

        public void ConnectToPort(Port port)
        {
            if (port.PortState != PortState.Disconnected) throw new PortException("Порт уже используется");

            this.StartCall += port.OnCall;
            port.Request += this.OnRequest;
            this.AnswerCall += port.OnAnswerFromTerminal;
            port.PortState = PortState.Connected;
        }

        public void DisconnectFromPort(Port port)
        {
            switch (port.PortState)
            {
                case PortState.Calling:
                    throw new PortException("Не возможно отключиться от порта во время звонка");
                case PortState.Connected:
                    this.StartCall -= port.OnCall;
                    port.Request -= this.OnRequest;
                    this.AnswerCall -= port.OnAnswerFromTerminal;
                    port.PortState = PortState.Disconnected;
                    break;
                case PortState.Disconnected:
                    throw new PortException("Порт не подключен");
            }
        }



        public void Answer(bool answerTheCall)
        {
            OnAnswer(this, new AnswerCallEventArgs(answerTheCall, _incomingPhoneNumber, PhoneNumber));
        }

        protected virtual void OnAnswer(object sender, AnswerCallEventArgs e)
        {
            if (AnswerCall == null) throw new PortException("Терминал не подключен к какому либо порту");
            AnswerCall?.Invoke(sender, e);
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
