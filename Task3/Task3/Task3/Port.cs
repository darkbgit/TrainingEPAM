using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.EventsArgs;
using Task3.States;

namespace Task3
{
    public class Port
    {

        public Port(int portNumber)
        {
            PortNumber = portNumber;
        }

        //public delegate void PortHandler(object sender, PortStateChangedEventsArgs e);

        public event EventHandler<PortStartCallEventsArgs> Calling;

        public event EventHandler<SendRequestEventsArgs> SendRequest;

        public int PortNumber { get; set; }

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
                    Calling?.Invoke(this, new PortStartCallEventsArgs(caller, e.Called));
                    break;
                case PortState.Calling:
                    throw new PortException("Порт занят");
            }
        }


        public void OnRequest(object sender, SendRequestEventsArgs e)
        {
            if (PortNumber != e.PortDefenition) return;

            switch (PortState)
            {
                case PortState.Disconnected:
                    throw new PortException("Вызываемый абонент отключен");
                case PortState.Connected:
                    PortState = PortState.Calling;
                    SendRequest?.Invoke(this, e);
                    break;
                case PortState.Calling:
                    throw new PortException("Вызываемый абонент занят");
            }
        }
    }
}
