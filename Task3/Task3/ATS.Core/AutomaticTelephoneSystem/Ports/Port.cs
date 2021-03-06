using System;
using ATS.Core.AutomaticTelephoneSystem.EventsArgs;
using ATS.Core.AutomaticTelephoneSystem.Ports.States;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using Logging.Loggers;

namespace ATS.Core.AutomaticTelephoneSystem.Ports
{
    public class Port : IPort
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

        public event EventHandler<PortEndCallEventArgs> EndCallSource;

        public event EventHandler<StationEndCallEventArgs> EndCallTarget;

        public int Id { get; }

        public PortState PortState { get; set; }

        public void PortStartCall(object sender, TerminalStartCallEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Disconnected:
                    Log.LogMessage($"Port state error. Connect terminal {((Terminal)sender).Id} to port {Id}. Port disconnected.");
                    throw new PortException("Порт отключен");
                case PortState.Connected:
                    PortState = PortState.Waiting;
                    OnPortStartCall(this, new PortStartCallEventArgs(((Terminal)sender).PhoneNumber, e.TargetPhoneNumber, ((Terminal)sender).Id));
                    break;
                case PortState.Waiting:
                case PortState.Calling:
                    Log.LogMessage($"Port state error. Connect terminal {((Terminal)sender).Id} to port {Id}. Port busy.");
                    throw new PortException("Порт занят");
            }
        }
        

        public void PortStartCallRequest(object sender, StationStartCallRequestEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Connected:
                    PortState = PortState.Waiting;
                    OnPortStartCallRequest(this, e);
                    break;
                case PortState.Disconnected:
                case PortState.Waiting:
                case PortState.Calling:
                    Log.LogMessage($"Port state error. Start call from terminal {e.SourceTerminalId} request to port {Id}. Port state {PortState}.");
                    throw new PortException();
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
                    Log.LogMessage($"Port state error. Start call answer - source terminal {e.SourceTerminalId}, port {Id}, port state {PortState}.");
                    PortState = PortState.Disconnected;
                    throw new PortException("Ошибка состояния порта. Порт будет отключен");
            }
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
                    Log.LogMessage($"Port state error. Answer call - source terminal {e.SourceTerminalId}, target terminal {e.TargetTerminalId},  port {Id}, port state {PortState}.");
                    PortState = PortState.Disconnected;
                    throw new PortException("Ошибка состояния порта. Порт будет отключен");
            }
        }
        

        public void PortEndCallSource(object sender, EventArgs e)
        {
            switch (PortState)
            {
                case PortState.Calling:
                    PortState = PortState.Connected;
                    OnPortEndCallSource(this, new PortEndCallEventArgs(((Terminal)sender).Id));
                    break;
                default:
                    Log.LogMessage($"Port state error. End call from source - terminal {((Terminal)sender).Id} port {Id}, port state {PortState}.");
                    //PortState = PortState.Disconnected;
                    throw new PortException("Ошибка завершения звонка. Порт не находится в состоянии звонка");
            }
        }
        

        public void PortEndCallTarget(object sender, StationEndCallEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Calling:
                    PortState = PortState.Connected;
                    OnPortEndCallTarget(this, e);
                    break;
                default:
                    Log.LogMessage($"Port state error. End call from target - terminal {e.EndCallTerminalId} port {Id}, port state {PortState}.");
                    PortState = PortState.Disconnected;
                    throw new PortException("Ошибка состояния порта. Порт будет отключен");
            }
        }


        protected virtual void OnPortStartCallAnswer(object sender, PortAnswerRequestEventArgs e)
        {
            AnswerRequest?.Invoke(sender, e);
        }

        protected virtual void OnPortAnswerCall(object sender, StationStartCallAfterAnswerEventArgs e)
        {
            AnswerCall?.Invoke(sender, e);
        }

        protected virtual void OnPortStartCallRequest(object sender, StationStartCallRequestEventArgs e)
        {
            SendRequest?.Invoke(sender, e);
        }

        protected virtual void OnPortStartCall(object sender, PortStartCallEventArgs e)
        {
            StartCall?.Invoke(sender, e);
        }

        protected virtual void OnPortEndCallSource(object sender, PortEndCallEventArgs e)
        {
            EndCallSource?.Invoke(sender, e);
        }

        protected virtual void OnPortEndCallTarget(object sender, StationEndCallEventArgs e)
        {
            EndCallTarget?.Invoke(sender, e);
        }

    }
}
