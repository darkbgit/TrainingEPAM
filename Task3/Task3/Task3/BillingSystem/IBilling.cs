using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.BillingSystem
{
    internal interface IBilling : IEnumerable<Record>
    {
        void AddCall(Record record);
    }
}
