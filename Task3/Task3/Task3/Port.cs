using System;
using Task3.EventsArgs;
using Task3.States;

namespace Task3
{
    public class Port
    {
        public Port()
        {
            Id = Guid.NewGuid();
            PortState = PortState.Disconnected;
        }


        public event EventHandler<PortStartCallEventsArgs> StartCall;

        public event EventHandler<StationSendRequestEventsArgs> Request;

        public Guid Id { get; set; }

        public PortState PortState { get; set; }

        public void OnCall(object sender, StartCallEventArgs e)
        {
            switch (PortState)
            {
                case PortState.Disconnected:
                    throw new PortException("Порт отключен");
                case PortState.Connected:
                    var caller = (sender as Terminal).PhoneNumber;
                    PortState = PortState.Calling;
                    OnStartCall(this, new PortStartCallEventsArgs(caller, e.Called));
                    break;
                case PortState.Calling:
                    throw new PortException("Порт занят");
            }
        }

        public void OnRequest(object sender, StationSendRequestEventsArgs e)
        {
            switch (PortState)
            {
                case PortState.Disconnected:
                    throw new PortException("Вызываемый аббонент отключен");
                case PortState.Connected:
                    PortState = PortState.Calling;
                    OnRquest(this, e);
                    break;
                case PortState.Calling:
                    throw new PortException("Вызываемый аббонент занят");
            }
        }

        protected virtual void OnStartCall(object sender, PortStartCallEventsArgs e)
        {
            StartCall?.Invoke(sender, e);
        }

        protected virtual void OnRquest(object sender, StationSendRequestEventsArgs e)
        {
            Request?.Invoke(sender, e);
        }
    }
}
