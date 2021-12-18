using System;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.EventsArgs;
using Task3.States;

namespace Task3.AutomaticTelephoneSystem.Ports
{
    public class Port
    {
        public Port()
        {
            Id = Guid.NewGuid();
            PortState = PortState.Disconnected;
        }


        public event EventHandler<PortStartCallEventsArgs> StartCall;

        public event EventHandler<StationStartCallRequestEventsArgs> SendRequest;

        public event EventHandler<PortAnswerRequestEventArgs> AnswerRequest;

        public event EventHandler<StationStartCallAnswerEventsArgs> AnswerCall;

        public event EventHandler<PortEndCallEventsArgs> EndCallTerminal;

        public event EventHandler<StationEndCallEventArgs> EndCallStation;

        public Guid Id { get; set; }

        public PortState PortState { get; set; }

        public void PortStartCall(object sender, StartCallEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Disconnected:
                    throw new PortException("Порт отключен");
                case PortState.Connected:
                    PortState = PortState.Waiting;
                    OnPortStartCall(this, new PortStartCallEventsArgs(sender as Terminal, e.Called));
                    break;
                case PortState.Waiting:
                case PortState.Calling:
                    throw new PortException("Порт занят");
            }
        }
        protected virtual void OnPortStartCall(object sender, PortStartCallEventsArgs e)
        {
            StartCall?.Invoke(sender, e);
        }

        public void PortStartCallRequest(object sender, StationStartCallRequestEventsArgs e)
        {
            switch (PortState)
            {
                case PortState.Disconnected:
                    throw new PortException("Вызываемый абонент отключен");
                case PortState.Connected:
                    PortState = PortState.Waiting;
                    OnPortStartCallRequest(this, e);
                    break;
                case PortState.Waiting:
                case PortState.Calling:
                    throw new PortException("Вызываемый абонент занят");
            }
        }
        
        protected virtual void OnPortStartCallRequest(object sender, StationStartCallRequestEventsArgs e)
        {
            SendRequest?.Invoke(sender, e);
        }



        public void PortStartCallAnswer(object sender, TerminalAnswerRequestEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Waiting:
                    PortState = e.IsAccept ? PortState.Calling : PortState.Connected;
                    OnPortStartCallAnswer(this, new PortAnswerRequestEventArgs(e.IsAccept, e.CallerTerminalId, sender as Terminal));
                    break;
                default:
                    PortState = PortState.Disconnected;
                    throw new PortException("Ошибка состояния порта. Порт будет отключен");
            }
        }

        protected virtual void OnPortStartCallAnswer(object sender, PortAnswerRequestEventArgs e)
        {
            AnswerRequest?.Invoke(sender, e);
        }

        public void PortAnswerCall(object sender, StationStartCallAnswerEventsArgs e)
        {
            switch (PortState)
            {
                case PortState.Waiting:
                    PortState = e.IsAccept ? PortState.Calling : PortState.Connected;
                    OnPortAnswerCall(this, e);
                    break;
                default:
                    PortState = PortState.Disconnected;
                    throw new PortException("Ошибка состояния порта. Порт будет отключен");
            }
        }
        protected virtual void OnPortAnswerCall(object sender, StationStartCallAnswerEventsArgs e)
        {
            AnswerCall?.Invoke(sender, e);
        }


        public void PortEndCallTerminal(object sender, EventArgs e)
        {
            switch (PortState)
            {
                case PortState.Calling:
                    PortState = PortState.Connected;
                    OnPortEndCallTerminal(this, new PortEndCallEventsArgs((sender as Terminal).Id));
                    break;
                default:
                    //PortState = PortState.Disconnected;
                    throw new PortException("Ошибка завершения звонка. Порт не находится в состоянии звонка");
            }
        }
        protected virtual void OnPortEndCallTerminal(object sender, PortEndCallEventsArgs e)
        {
            EndCallTerminal?.Invoke(sender, e);
        }

        public void PortEndCallStation(object sender, StationEndCallEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Calling:
                    PortState = PortState.Connected;
                    OnPortEndCallStation(this, e);
                    break;
                default:
                    PortState = PortState.Disconnected;
                    throw new PortException("Ошибка состояния порта. Порт будет отключен");
            }
        }
        protected virtual void OnPortEndCallStation(object sender, StationEndCallEventArgs e)
        {
            EndCallStation?.Invoke(sender, e);
        }

    }
}
