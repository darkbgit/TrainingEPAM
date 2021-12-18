using System;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.EventsArgs;
using Task3.States;

namespace Task3.AutomaticTelephoneSystem.Terminals
{
    public class Terminal : ITerminal
    {
        private StationStartCallRequestEventsArgs _incomingStationSendRequestEventsArgs;

        public event EventHandler<StartCallEventArgs> StartCall;

        public event EventHandler<TerminalAnswerRequestEventArgs> AnswerCall;

        public event EventHandler EndCall;

        public Terminal()
        {
            Id = Guid.NewGuid();
            PhoneNumber = GeneratePhoneNumber();
        }
        public Guid Id { get; set; }

        public PhoneNumber PhoneNumber { get; set; }



        public void Call(PhoneNumber targetNumber)
        {
            Console.WriteLine($"Терминал {Id}: Вызов абонента {targetNumber}");
            OnCall(this, new StartCallEventArgs(targetNumber));
        }
        
        protected virtual void OnCall(object sender, StartCallEventArgs e)
        {
            if (StartCall == null) throw new PortException("Терминал не подключен к какому либо порту");
            StartCall?.Invoke(sender, e);
        }

        public void OnSendRequest(object sender, StationStartCallRequestEventsArgs e)
        {
            _incomingStationSendRequestEventsArgs = e;
            Console.WriteLine($"Терминал {Id}: Входящий вызов от {e.Caller}");
        }
    
        public void AnswerRequest(bool isAccept)
        {
            OnAnswerRequest(this, new TerminalAnswerRequestEventArgs(isAccept, _incomingStationSendRequestEventsArgs.CallerTerminalId));
        }

        protected virtual void OnAnswerRequest(object sender, TerminalAnswerRequestEventArgs e)
        {
            if (AnswerCall == null) throw new PortException("Терминал не подключен к какому либо порту");
            AnswerCall?.Invoke(sender, e);
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

        protected virtual void OnEnd(object sender, EventArgs e)
        {
            if (EndCall == null) throw new PortException("Ошибка подключения терминала к порту");
            EndCall?.Invoke(sender, e);
        }


        public void OnPortEndCallByStation(object sender, StationEndCallEventArgs e)
        {
            Console.WriteLine($"Терминал {Id}: Вызов завершен абонентом {e.EndCallPhoneNumber}");
        }


        public void ConnectToPort(Port port)
        {
            if (port.PortState != PortState.Disconnected) throw new PortException("Порт уже используется");

            StartCall += port.PortStartCall;
            port.SendRequest += OnSendRequest;
            AnswerCall += port.PortStartCallAnswer;
            port.AnswerCall += GetAnswer;
            EndCall += port.PortEndCallTerminal;
            port.EndCallStation += OnPortEndCallByStation;

            port.PortState = PortState.Connected;
        }

        public void DisconnectFromPort(Port port)
        {
            switch (port.PortState)
            {
                case PortState.Waiting:
                case PortState.Calling:
                    throw new PortException("Не возможно отключиться от порта во время звонка");
                case PortState.Connected:
                    StartCall -= port.PortStartCall;
                    port.SendRequest -= OnSendRequest;
                    AnswerCall -= port.PortStartCallAnswer;
                    port.AnswerCall -= GetAnswer;
                    EndCall -= port.PortEndCallTerminal;
                    port.EndCallStation -= OnPortEndCallByStation;

                    port.PortState = PortState.Disconnected;
                    break;
                case PortState.Disconnected:
                    throw new PortException("Порт не подключен");
            }
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
