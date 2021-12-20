using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.Ports;
using ATS.Core.AutomaticTelephoneSystem.Stations;
using ATS.Core.BillingSystem;
using Logging.Loggers;


namespace ATS.Core.ClientsService
{
    public class ClientService : IClientService
    {
        //private readonly Client _user;

        private readonly Contract _contract;

        public ClientService(Contract contract)
        {
            //_user = user;
            _contract = contract;
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
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
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
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
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
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
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
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
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
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
            }
        }
    }
}
