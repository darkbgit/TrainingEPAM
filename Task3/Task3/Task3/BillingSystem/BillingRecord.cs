using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.BillingSystem
{
    public class BillingRecord
    {
        public BillingRecord(int id)
        {
            Id = id;
            IsCompleted = false;
        }

        public int Id { get; set; }
        public DateTime BeginCall { get; set; }
        public DateTime EndCall { get; set; }
        public Guid SourceTerminalId { get; set; }
        public Guid TargetTerminalId { get; set; }
        public bool IsCompleted { get; set; }

    }
}
