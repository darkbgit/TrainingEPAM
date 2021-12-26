using System;
using System.Collections.Generic;
using ATS.Core.AutomaticTelephoneSystem;

namespace ATS.Core.BillingSystem
{
    public interface IBillingReport
    {
        IEnumerable<ReportRecord> GetReportForClient(Func<BillingRecord, bool> predicate, Guid clientId, PhoneNumber interlocutorPhoneNumber);
    }
}