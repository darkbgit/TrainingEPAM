using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.States;

namespace Task3
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PortState PortState { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public void ConnectToPort()
        {
            if (PortState is PortState.Disconnected)
            {
                PortState = PortState.Connected;
            }
            
        }

        public void DisconnectFromPort()
        {
            if (PortState is PortState.Connected)
            {
                PortState = PortState.Disconnected;
            }
        }

        public void CallTo(User user)
        {
            if (PortState is PortState.Connected)
            {
                PortState = PortState.Calling;
            }
        }
    }
}
