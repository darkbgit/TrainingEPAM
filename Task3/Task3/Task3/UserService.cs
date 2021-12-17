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


        public void Answer(bool answerTheCall)
        {
            try
            {
                _contract.Terminal.Answer(answerTheCall);
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


        public void Reject()
        {

        }

        //public void EndCall()
        //{
        //    if (_contract.Port.PortState is PortState.Calling)
        //    {
        //        _contract.Port.CallEnd(_user);
        //    }
        //}

    }
}
