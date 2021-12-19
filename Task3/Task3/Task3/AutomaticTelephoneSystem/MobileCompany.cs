using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.AutomaticTelephoneSystem.Stations;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.BillingSystem;

namespace Task3.AutomaticTelephoneSystem
{
    public class MobileCompany : IMobileCompany
    {
        private readonly Billing _billing;

        private readonly Station _station;

        private readonly Contracts _contracts;

        private readonly ILogger _logger;


        public MobileCompany(ILogger logger)
        {
            _logger = logger;

            _contracts = new Contracts();

            _station = new Station();

            _billing = new Billing(_logger);

            _station.StationStartCall += _billing.OnStation;

        }


        public Contract MakeUserContract(Client user)
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
                User = user,
                Terminal = terminal,
                //Port = port
            };

            
            _contracts.Add(result);

            return result;
        }

        public IBilling Billing => _billing;
    }
}
