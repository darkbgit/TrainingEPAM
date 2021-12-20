using System;
using ATS.Core.AutomaticTelephoneSystem.Tariffs;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.ClientsService;

namespace ATS.Core.BillingSystem
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
