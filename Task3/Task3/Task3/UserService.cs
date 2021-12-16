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

        private readonly Contract _contract;

        public UserService(User user, Contract contract)
        {
            _user = user;
            _contract = contract;
        }

        public PhoneNumber PhoneNumber => _contract.Terminal.PhoneNumber;

        public void Call(PhoneNumber targetNumber)
        {
            _contract.Terminal.Call(targetNumber);
        }




        //public Contract Contract => _contract;

        public void ConnectToPort()
        {

        }

        //public void DisconnectFromPort()
        //{
        //    if (_contract.Port.PortState is PortState.Connected)
        //    {
        //        _contract.Port.PortState = PortState.Disconnected;
        //    }
        //}

        //public void CallTo(User user)
        //{
        //    if (_contract.Port.PortState is PortState.Connected)
        //    {
        //        _contract.Port.CallBegin(_user, user);
        //    }
        //}

        //public void EndCall()
        //{
        //    if (_contract.Port.PortState is PortState.Calling)
        //    {
        //        _contract.Port.CallEnd(_user);
        //    }
        //}

    }
}
