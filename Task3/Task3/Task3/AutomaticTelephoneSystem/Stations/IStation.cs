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
        event EventHandler<StationStartCallAfterAnswerEventArgs> StationBilling;
        void OnPortStartCall(object sender, PortStartCallEventArgs e);
        void OnPortAnswer(object sender, PortAnswerRequestEventArgs e);
        void OnEndCall(object sender, PortEndCallEventArgs e);
    }
}
