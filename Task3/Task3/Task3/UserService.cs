using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.AutomaticTelephoneSystem.Stations;
using Task3.States;

namespace Task3
{
    internal class UserService
    {
        private readonly User _user;

        private readonly Contract _contract;

        private readonly ILogger _logger;

        public UserService(User user, Contract contract, ILogger logger)
        {
            _user = user;
            _contract = contract;
            _logger = logger;
        }

        public PhoneNumber PhoneNumber => _contract.Terminal.PhoneNumber;

        public void Call(PhoneNumber targetNumber)
        {
            try
            {
                _contract.Terminal.Call(targetNumber);
            }
            catch (PortException ex)
            {
                _logger.Log(ex.Message);
            }
            catch (StationException ex)
            {
                _logger.Log(ex.Message);
            }
        }




        //public Contract Contract => _contract;

        public void ConnectToPort()
        {
            try
            {
                _contract.Terminal.ConnectToPort(_contract.Port);
            }
            catch (PortException ex)
            {
                _logger.Log(ex.Message);
            }
        }


        public void DisconnectFromPort()
        {
            try
            {
                _contract.Terminal.DisconnectFromPort(_contract.Port);
            }
            catch (PortException ex)
            {
                _logger.Log(ex.Message);
            }
        }


        public void Answer(bool isAccept)
        {
            try
            {
                _contract.Terminal.AnswerRequest(isAccept);
            }
            catch (PortException ex)
            {
                _logger.Log(ex.Message);
            }
            catch (StationException ex)
            {
                _logger.Log(ex.Message);
            }
        }


        public void EndCall()
        {
            try
            {
                _contract.Terminal.End();
            }
            catch (PortException ex)
            {
                _logger.Log(ex.Message);
            }
            catch (StationException ex)
            {
                _logger.Log(ex.Message);
            }
        }

        //public void EndCallTerminal()
        //{
        //    if (_contract.Port.PortState is PortState.Calling)
        //    {
        //        _contract.Port.CallEnd(_user);
        //    }
        //}

    }
}
