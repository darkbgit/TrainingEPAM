using System;
using System.Collections.Generic;
using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.EventsArgs;

namespace ATS.Core.BillingSystem
{
    public interface IBilling : IEnumerable<BillingRecord>
    {
        void StartCall(object sender, StationStartCallAfterAnswerEventArgs e);
        void EndCall(object sender, StationEndCallEventArgs e);
        
    }
}
