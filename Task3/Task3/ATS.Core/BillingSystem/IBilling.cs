using System.Collections.Generic;
using ATS.Core.EventsArgs;

namespace ATS.Core.BillingSystem
{
    public interface IBilling : IEnumerable<BillingRecord>
    {
        void OnStation(object sender, StationStartCallAfterAnswerEventArgs e);
    }
}
