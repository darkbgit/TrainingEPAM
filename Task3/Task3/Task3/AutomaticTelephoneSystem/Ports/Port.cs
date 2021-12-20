using System;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.EventsArgs;
using Task3.States;

namespace Task3.AutomaticTelephoneSystem.Ports
{
    public class Port
    {
        public Port(int id)
        {
            Id = id;
            PortState = PortState.Disconnected;
        }


        public event EventHandler<PortStartCallEventArgs> StartCall;

        public event EventHandler<StationStartCallRequestEventArgs> SendRequest;

        public event EventHandler<PortAnswerRequestEventArgs> AnswerRequest;

        public event EventHandler<StationStartCallAfterAnswerEventArgs> AnswerCall;

        public event EventHandler<PortEndCallEventArgs> EndCallTerminal;

        public event EventHandler<StationEndCallEventArgs> EndCallStation;

        public int Id { get; }

        public PortState PortState { get; set; }

        public void PortStartCall(object sender, TerminalStartCallEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Disconnected:
                    throw new PortException("Порт отключен");
                case PortState.Connected:
                    PortState = PortState.Waiting;
                    OnPortStartCall(this, new PortStartCallEventArgs(((Terminal)sender).PhoneNumber, e.TargetPhoneNumber, ((Terminal)sender).Id));
                    break;
                case PortState.Waiting:
                case PortState.Calling:
                    throw new PortException("Порт занят");
            }
        }
        

        public void PortStartCallRequest(object sender, StationStartCallRequestEventArgs e)
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
        

        public void PortStartCallAnswer(object sender, TerminalAnswerRequestEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Waiting:
                    PortState = e.IsAccept ? PortState.Calling : PortState.Connected;
                    OnPortStartCallAnswer(this, new PortAnswerRequestEventArgs(e.IsAccept, e.SourceTerminalId, ((Terminal)sender).Id));
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

        public void PortAnswerCall(object sender, StationStartCallAfterAnswerEventArgs e)
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
        protected virtual void OnPortAnswerCall(object sender, StationStartCallAfterAnswerEventArgs e)
        {
            AnswerCall?.Invoke(sender, e);
        }


        public void PortEndCallTerminal(object sender, EventArgs e)
        {
            switch (PortState)
            {
                case PortState.Calling:
                    PortState = PortState.Connected;
                    OnPortEndCallTerminal(this, new PortEndCallEventArgs((sender as Terminal).Id));
                    break;
                default:
                    //PortState = PortState.Disconnected;
                    throw new PortException("Ошибка завершения звонка. Порт не находится в состоянии звонка");
            }
        }
        protected virtual void OnPortEndCallTerminal(object sender, PortEndCallEventArgs e)
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


        protected virtual void OnPortStartCallRequest(object sender, StationStartCallRequestEventArgs e)
        {
            SendRequest?.Invoke(sender, e);
        }

        protected virtual void OnPortStartCall(object sender, PortStartCallEventArgs e)
        {
            StartCall?.Invoke(sender, e);
        }


        protected virtual void OnPortEndCallStation(object sender, StationEndCallEventArgs e)
        {
            EndCallStation?.Invoke(sender, e);
        }

    }
}
