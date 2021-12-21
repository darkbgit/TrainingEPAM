using System.Collections.Generic;
using System.Linq;
using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.Stations;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.BillingSystem;
using ATS.Core.ClientsService;
using ATS.Core.Tariffs;
using Logging.Loggers;


namespace ATS.Core.MobileCompanies
{
    public class MobileCompany : IMobileCompany
    {
        private readonly IBilling _billing;

        private readonly IStation _station;

        private readonly ICollection<Contract> _contracts;

        private readonly ICollection<ITerminal> _terminals;

        public MobileCompany(IEnumerable<ITariff> tariffs)
        {
           _contracts = new List<Contract>();
           _terminals = new List<ITerminal>();
           _billing = new Billing(_contracts, tariffs, _terminals);
           _station = new Station();

            _station.StationStartCall += _billing.StartCall;
            _station.StationEndCall += _billing.EndCall;
        }


        //public Contract SingClientContract(Client client)
        //{
        //    var terminal = new Terminal();

        //    terminal.TerminalConnectToPort += _station.ConnectTerminalToPort;

        //    terminal.TerminalDisconnectFromPort += _station.DisconnectTerminalFromPort;

        //    var result = new Contract
        //    {
        //        ClientId = client.Id,
        //        TerminalId = terminal.Id,
        //    };

            
        //    _contracts.Add(result);

        //    return result;
        //}

        public ITerminal SingClientContract(Client client, ITariff tariff)
        {
            ITerminal terminal = new Terminal();

            _terminals.Add(terminal);

            terminal.TerminalConnectToPort += _station.ConnectTerminalToPort;

            terminal.TerminalDisconnectFromPort += _station.DisconnectTerminalFromPort;

            var contract = new Contract
            {
                ClientId = client.Id,
                TerminalId = terminal.Id,
                TariffId = tariff.Id
            };

            _contracts.Add(contract);

            return terminal;
        }

        public void CancelClientContract(Client client)
        {
            var contract = _contracts.FirstOrDefault(c => c.ClientId.Equals(client.Id));

            if (contract == null)
            {
                Log.LogMessage($"Клиент {client.FirstName} {client.LastName} не найден");
                return;
            }

            var terminal = _terminals.First(t => t.Id.Equals(contract.TerminalId));

            terminal.TerminalConnectToPort -= _station.ConnectTerminalToPort;

            terminal.TerminalDisconnectFromPort -= _station.DisconnectTerminalFromPort;

            _terminals.Remove(terminal);

            _contracts.Remove(contract);

        }

        public IBilling Billing => _billing;
    }
}
