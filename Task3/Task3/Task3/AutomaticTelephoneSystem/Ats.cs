using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Task3.BillingSystem;

namespace Task3.AutomaticTelephoneSystem
{
    internal class Ats
    {
        private readonly IBilling _billing;

        private readonly Port[] _ports;

        public Ats()
        {
            _billing = new Billing();
            _ports = new[]
            {
                new Port(1),
                new Port(2),
                new Port(3),
                new Port(4),
                new Port(5),
                new Port(6),
                new Port(7),
                new Port(8),
                new Port(9),
                new Port(10)
            };
        }

        public Contract MakeUserContract(User user)
        {
            var portIndex = new Random().Next(0, _ports.Length - 1);
            var result = new Contract
            {
                User = user,
                Terminal = new Terminal(),
                
                Port = _ports[portIndex]
            };
            return result;
        }

        public IBilling Billing => _billing;
    }
}
