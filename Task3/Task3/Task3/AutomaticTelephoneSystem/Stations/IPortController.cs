using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.AutomaticTelephoneSystem.Stations
{
    internal interface IPortController
    {
        Port GetPortByPhoneNumber(PhoneNumber phone);
        void ConnectTerminalToPort(Terminal terminal);
        void DisconnectTerminalFromPort(Terminal terminal);
        Port GetPortByTerminalId(Guid id);
    }
}
