using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.States;

namespace Task3
{
    internal class UserService
    {
        private readonly User _user;

        private readonly Terminal _terminal;

        public UserService(User user, Terminal terminal)
        {
            _user = user;
            _terminal = terminal;
        }

        public Terminal Terminal => _terminal;

        public void ConnectToPort()
        {
            //if (_contract.Port.PortState is PortState.Disconnected)
            //{
            //    _contract.Port.PortState = PortState.Connected;
            //}

        }

        public void DisconnectFromPort()
        {
            //if (_contract.Port.PortState is PortState.Connected)
            //{
            //    _contract.Port.PortState = PortState.Disconnected;
            //}
        }

        public void CallTo(User calledUser)
        {
            //if (_contract.Port.PortState is PortState.Connected)
            //{
            //    _contract.Port.CallBegin(_user, user);
            //}
        }

        public void EndCall()
        {
            //if (_contract.Port.PortState is PortState.Calling)
            //{
            //    _contract.Port.CallEnd(_user);
            //}
        }

    }
}
