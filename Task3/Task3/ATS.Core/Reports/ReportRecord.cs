using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.AutomaticTelephoneSystem;

namespace ATS.Core.Reports
{
    public class ReportRecord
    {
        public PhoneNumber InterlocutorPhoneNumber { get; set; }

        public bool IsIncome { get; set; }

        public DateTime BeginCall { get; set; }

        public DateTime EndCall { get; set; }

        public TimeSpan Duration { get; set; }

        public double Cost { get; set; }
    }
}
