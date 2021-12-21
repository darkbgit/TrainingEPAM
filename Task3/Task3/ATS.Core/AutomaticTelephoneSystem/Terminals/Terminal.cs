using System;
using ATS.Core.AutomaticTelephoneSystem.Ports;
using ATS.Core.EventsArgs;

namespace ATS.Core.AutomaticTelephoneSystem.Terminals
{
    public class Terminal : ITerminal, ITerminalService
    {

        private Guid _currentCallingTerminalId;
        
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
            //if (Equals(PhoneNumber, targetNumber))
            //{
            //    Console.WriteLine($"");

            //}
            Console.WriteLine($"Терминал {Id}: Вызов абонента {targetNumber}");
            OnCall(this, new TerminalStartCallEventArgs(targetNumber));
        }
        

        public void OnRequest(object sender, StationStartCallRequestEventArgs e)
        {
            _currentCallingTerminalId = e.SourceTerminalId;
            Console.WriteLine($"Терминал {Id}: Входящий вызов от {e.SourcePhoneNumber}");
        }
    
        public void AnswerRequest(bool isAccept)
        {
            OnAnswerRequest(this, new TerminalAnswerRequestEventArgs(isAccept, _currentCallingTerminalId));
        }

        

        public void GetAnswer(object sender, StationStartCallAfterAnswerEventArgs e)
        {
            if (e.IsAccept)
            {
                Console.WriteLine($"Терминал {Id}: Абонент ответил на звонок. Звонок начался");
            }
            else
            {
                Console.WriteLine($"Терминал {Id}: Абонент отклонил звонок. Звонок завершен");
            }
        }

        public void End()
        {
            Console.WriteLine($"Терминал {Id}: Нажатие кнопки отбой");
            OnEnd(this, EventArgs.Empty);
        }

        
        public void OnEndCallByTarget(object sender, StationEndCallEventArgs e)
        {
            Console.WriteLine($"Терминал {Id}: Вызов завершен другим абонентом");
        }


        public void ConnectToPort()
        {
            Console.WriteLine($"Терминал {Id}: Запрос на подключение к порту");
            OnConnectTerminalToPort(this, EventArgs.Empty);
        }

        public void DisconnectFromPort()
        {
            Console.WriteLine($"Терминал {Id}: Запрос на отключения от порта");
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
