using System;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.AutomaticTelephoneSystem.Tariffs;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.AutomaticTelephoneSystem
{
    public class Contract
    {
        public Contract()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public Client Client { get; set; }

        public Terminal Terminal { get; set; }

        public Tariff Tariff { get; set; }
    }
}
