using System.Collections.Generic;
using System.Linq;
using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.Stations;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.BillingSystem;
using ATS.Core.Clients;
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

        private readonly ICollection<ITariff> _tariffs;

        public MobileCompany()
        {
           _contracts = new List<Contract>();
           _terminals = new List<ITerminal>();
           _tariffs = new List<ITariff>();

           _billing = CreateBilling();
           _station = CreateStation();
        }

        public IBillingReport Billing => (IBillingReport)_billing;

        public IEnumerable<ITariff> Tariffs => _tariffs;


        public ITerminal SingClientContract(IClient client, ITariff tariff)
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

        public void CancelClientContract(IClient client)
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

        public void AddTariff(ITariff tariff)
        {
            _tariffs.Add(tariff);
        }

        private IBilling CreateBilling()
        {
            return new Billing(_contracts, _tariffs, _terminals);
        }

        private IStation CreateStation()
        {
            IStation station = new Station();

            station.StationStartCall += _billing.StartCall;
            station.StationEndCall += _billing.EndCall;

            return station;
        }
    }
}
