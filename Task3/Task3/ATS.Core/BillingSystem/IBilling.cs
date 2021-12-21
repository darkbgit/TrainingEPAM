using System;
using System.Collections.Generic;
using ATS.Core.EventsArgs;
using ATS.Core.Reports;

namespace ATS.Core.BillingSystem
{
    public interface IBilling : IEnumerable<BillingRecord>
    {
        void StartCall(object sender, StationStartCallAfterAnswerEventArgs e);
        void EndCall(object sender, StationEndCallEventArgs e);
        IEnumerable<ReportRecord> GetReportForClient(Func<BillingRecord, bool> predicate, Guid clientId);
    }
}
