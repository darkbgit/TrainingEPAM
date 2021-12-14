using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.BillingSystem
{
    internal class Record
    {
        public DateTime BeginCall { get; set; }
        public DateTime EndCall { get; set; }
        public User CallerUser { get; set; }
        public User CalledUser { get; set; }
    }
}
