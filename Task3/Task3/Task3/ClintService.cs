using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.AutomaticTelephoneSystem.Stations;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.States;

namespace Task3
{
    internal class ClintService
    {
        //private readonly Client _user;

        private readonly Contract _contract;

        private readonly ILogger _logger;

        //private readonly ITerminal _terminal;

        public ClintService(Contract contract, ILogger logger)
        {
            //_user = user;
            _contract = contract;
            _logger = logger;
            //_terminal = terminal;
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
                _contract.Terminal.ConnectToPort();
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
                _contract.Terminal.DisconnectFromPort();
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
    }
}
