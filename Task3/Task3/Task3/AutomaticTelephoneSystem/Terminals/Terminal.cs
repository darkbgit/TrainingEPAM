using System;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.EventsArgs;
using Task3.States;

namespace Task3.AutomaticTelephoneSystem.Terminals
{
    public class Terminal : ITerminal
    {
        private StationStartCallRequestEventsArgs _incomingStationSendRequestEventsArgs;
        
        public Terminal()
        {
            Id = Guid.NewGuid();
            PhoneNumber = GeneratePhoneNumber();
        }

        public event EventHandler<TerminalStartCallEventArgs> StartCall;

        public event EventHandler<TerminalAnswerRequestEventArgs> AnswerCall;

        public event EventHandler EndCall;

        public event EventHandler TerminalConnectToPort;

        public event EventHandler TerminalDisconnectFromPort;

        
        public Guid Id { get; }

        public PhoneNumber PhoneNumber { get; }


        public void Call(PhoneNumber targetNumber)
        {
            Console.WriteLine($"Терминал {Id}: Вызов абонента {targetNumber}");
            OnCall(this, new TerminalStartCallEventArgs(targetNumber));
        }
        

        public void OnRequest(object sender, StationStartCallRequestEventsArgs e)
        {
            _incomingStationSendRequestEventsArgs = e;
            Console.WriteLine($"Терминал {Id}: Входящий вызов от {e.SourcePhoneNumber}");
        }
    
        public void AnswerRequest(bool isAccept)
        {
            OnAnswerRequest(this, new TerminalAnswerRequestEventArgs(isAccept, _incomingStationSendRequestEventsArgs.CallerTerminalId));
        }

        

        public void GetAnswer(object sender, StationStartCallAnswerEventsArgs e)
        {
            if (e.IsAccept)
            {
                Console.WriteLine($"Терминал {Id}: Абонент {e.Called} ответил на звонок. Звонок начался");
            }
            else
            {
                Console.WriteLine($"Терминал {Id}: Абонент {e.Called} отклонил звонок. Звонок завершен");
            }
        }

        public void End()
        {
            Console.WriteLine($"Терминал {Id}: Нажатие кнопки отбой");
            OnEnd(this, EventArgs.Empty);
        }

        


        public void OnPortEndCallByStation(object sender, StationEndCallEventArgs e)
        {
            Console.WriteLine($"Терминал {Id}: Вызов завершен абонентом {e.EndCallPhoneNumber}");
        }


        public void ConnectToPort()
        {
            OnConnectTerminalToPort(this, EventArgs.Empty);
        }

        public void DisconnectFromPort()
        {
            OnDisconnectTerminalFromPort(this, EventArgs.Empty);
        }



        protected virtual void OnAnswerRequest(object sender, TerminalAnswerRequestEventArgs e)
        {
            if (AnswerCall == null) throw new PortException("Терминал не подключен к какому либо порту");
            AnswerCall?.Invoke(sender, e);
        }

        protected virtual void OnEnd(object sender, EventArgs e)
        {
            if (EndCall == null) throw new PortException("Ошибка подключения терминала к порту");
            EndCall?.Invoke(sender, e);
        }

        protected virtual void OnCall(object sender, TerminalStartCallEventArgs e)
        {
            if (StartCall == null) throw new PortException("Терминал не подключен к какому либо порту");
            StartCall?.Invoke(sender, e);
        }

        protected virtual void OnConnectTerminalToPort(object sender, EventArgs e)
        {
            TerminalConnectToPort?.Invoke(sender, e);
        }

        protected virtual void OnDisconnectTerminalFromPort(object sender, EventArgs e)
        {
            TerminalDisconnectFromPort?.Invoke(sender, e);
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
