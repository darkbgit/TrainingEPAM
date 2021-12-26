using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.AutomaticTelephoneSystem.Ports;
using ATS.Core.AutomaticTelephoneSystem.Ports.States;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using Logging.Loggers;

namespace ATS.Core.AutomaticTelephoneSystem.Stations
{
    internal class PortController : IPortController
    {
        private readonly Port[] _ports;

        private readonly Dictionary<int, (Guid, PhoneNumber)> _portTerminalWithPhoneIdsDictionary;

        public PortController()
        {
            _ports = new[]
            {
                new Port(1),
                new Port(2),
                new Port(3),
                new Port(4),
                new Port(5),
                new Port(6),
                new Port(7),
                new Port(8),
                new Port(9),
                new Port(10)
            };

            _portTerminalWithPhoneIdsDictionary = new Dictionary<int, (Guid, PhoneNumber)>();
        }

        public IEnumerable<Port> Ports => _ports;

        public void ConnectTerminalToPort(Terminal terminal)
        {
            var port = GetFreePort();

            if (port == null)
            {
                var message = $"It is impossible connect {terminal.PhoneNumber} to a port. No free ports available.";
                Log.LogMessage(message);
                throw new StationException(message);
            }

            terminal.StartCall += port.PortStartCall;
            port.SendRequest += terminal.OnRequest;
            terminal.AnswerCall += port.PortStartCallAnswer;
            port.AnswerCall += terminal.GetAnswer;
            terminal.EndCall += port.PortEndCallSource;
            port.EndCallTarget += terminal.OnEndCallByTarget;

            port.PortState = PortState.Connected;

            Log.LogMessage($"Terminal {terminal.Id} connected to port {port.Id}");

            _portTerminalWithPhoneIdsDictionary.Add(port.Id, (terminal.Id, terminal.PhoneNumber));
        }

        public void DisconnectTerminalFromPort(Terminal terminal)
        {
            var port = _ports.FirstOrDefault(p =>
                p.Id == _portTerminalWithPhoneIdsDictionary.FirstOrDefault(v => v.Value.Item1 == terminal.Id).Key);

            if (port is not { PortState: PortState.Connected })
            {
                var message = $"It is impossible disconnect {terminal.PhoneNumber} from port.";
                Log.LogMessage(message);
                throw new StationException(message);
            }

            terminal.StartCall -= port.PortStartCall;
            port.SendRequest -= terminal.OnRequest;
            terminal.AnswerCall -= port.PortStartCallAnswer;
            port.AnswerCall -= terminal.GetAnswer;
            terminal.EndCall -= port.PortEndCallSource;
            port.EndCallTarget -= terminal.OnEndCallByTarget;

            port.PortState = PortState.Disconnected;

            Log.LogMessage($"Terminal {terminal.Id} disconnected from port {port.Id}");

            _portTerminalWithPhoneIdsDictionary.Remove(port.Id);
        }



        public Port GetPortByPhoneNumber(PhoneNumber phoneNumber)
        {
            var port = _ports.FirstOrDefault(p =>
                p.Id == _portTerminalWithPhoneIdsDictionary.FirstOrDefault(v => v.Value.Item2 == phoneNumber).Key);

            if (port is not { PortState: PortState.Connected })
            {
                var message = $"Phone {phoneNumber} doesn't have connected port";
                Log.LogMessage(message);
                throw new StationException(message);
            }

            return port;
        }

        public Port GetPortByTerminalId(Guid id)
        {
            var port = _ports.FirstOrDefault(p =>
                p.Id == _portTerminalWithPhoneIdsDictionary.FirstOrDefault(v => v.Value.Item1 == id).Key);

            if (port == null)
            {
                var message = $"Terminal {id} doesn't have connected port";
                Log.LogMessage(message);
                throw new StationException(message);
            }

            return port;
        }

        
        private Port GetFreePort() => _ports.FirstOrDefault(p => p.PortState == PortState.Disconnected);

    }
}
