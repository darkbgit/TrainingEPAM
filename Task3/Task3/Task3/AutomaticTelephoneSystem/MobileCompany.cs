using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Task3.BillingSystem;

namespace Task3.AutomaticTelephoneSystem
{
    public class MobileCompany : IMobileCompany
    {
        private readonly IBilling _billing;

        private readonly Station _station;

        private readonly Contracts _contracts;


        public MobileCompany()
        {
            _contracts = new Contracts();

            _station = new Station(_contracts);

            _billing = new Billing();
        }


        public Contract MakeUserContract(User user)
        {
            var terminal = new Terminal();

            var port = new Port();

            terminal.StartCall += port.OnCall;

            port.StartCall += _station.OnCall;

            port.Request += terminal.OnRequest;


            var result = new Contract
            {
                User = user,
                Terminal = terminal,
                Port = port
            };

            
            _contracts.Add(result);

            return result;
        }

        public IBilling Billing => _billing;
    }
}
