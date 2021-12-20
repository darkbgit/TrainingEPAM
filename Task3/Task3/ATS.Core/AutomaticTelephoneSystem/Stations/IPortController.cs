using System;
using System.Collections.Generic;
using ATS.Core.AutomaticTelephoneSystem.Ports;
using ATS.Core.AutomaticTelephoneSystem.Terminals;

namespace ATS.Core.AutomaticTelephoneSystem.Stations
{
    internal interface IPortController
    {
        IEnumerable<Port> Ports { get; }
        Port GetPortByPhoneNumber(PhoneNumber phone);
        Port GetPortByTerminalId(Guid id);
        void ConnectTerminalToPort(Terminal terminal);
        void DisconnectTerminalFromPort(Terminal terminal);
    }
}
