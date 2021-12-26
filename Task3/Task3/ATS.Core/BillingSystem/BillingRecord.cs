using System;

namespace ATS.Core.BillingSystem
{
    public class BillingRecord
    {
        public BillingRecord(int id)
        {
            Id = id;
            IsCompleted = false;
        }

        internal int Id { get; }
        public DateTime BeginCall { get; set; }
        public DateTime EndCall { get; set; }
        public Guid SourceTerminalId { get; set; }
        public Guid TargetTerminalId { get; set; }
        public double Cost { get; set; }
        internal bool IsCompleted { get; set; }


    }
}
