using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.EventsArgs;

namespace Task3.AutomaticTelephoneSystem.Stations
{
    public interface IStation
    {
        event EventHandler<StationForBillingEventArgs> StationBilling;
        void OnPortStartCall(object sender, PortStartCallEventsArgs e);
        void OnPortAnswer(object sender, PortAnswerRequestEventArgs e);
        void OnEndCall(object sender, PortEndCallEventsArgs e);
    }
}
