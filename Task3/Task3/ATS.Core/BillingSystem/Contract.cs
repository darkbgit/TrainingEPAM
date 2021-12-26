using System;


namespace ATS.Core.BillingSystem
{
    public class Contract
    {
        public Contract()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public Guid ClientId { get; set; }

        public Guid TerminalId { get; set; }

        public Guid TariffId { get; set; }
    }
}
