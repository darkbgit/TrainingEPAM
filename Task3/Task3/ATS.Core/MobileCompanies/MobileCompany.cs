using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.Stations;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.BillingSystem;
using ATS.Core.ClientsService;


namespace ATS.Core.MobileCompanies
{
    public class MobileCompany : IMobileCompany
    {
        private readonly IBilling _billing;

        private readonly IStation _station;

        private readonly Contracts _contracts;




        public MobileCompany()
        {
            _contracts = new Contracts();

            _station = new Station();

            _billing = new Billing();

            _station.StationStartCall += _billing.OnStation;
        }


        public Contract SingClientContract(Client client)
        {
            var terminal = new Terminal();

            terminal.TerminalConnectToPort += _station.ConnectTerminalToPort;

            terminal.TerminalDisconnectFromPort += _station.DisconnectTerminalFromPort;

            //var port = new Port();

            //port.StartCall += _station.OnPortStartCall;

            //port.AnswerRequest += _station.OnPortAnswer;

            //port.EndCallTerminal += _station.OnEndCall;


            var result = new Contract
            {
                Client = client,
                Terminal = terminal,
            };

            
            _contracts.Add(result);

            return result;
        }

        public void CancelClientContract(Client client)
        {

        }

        public IBilling Billing => _billing;
    }
}
