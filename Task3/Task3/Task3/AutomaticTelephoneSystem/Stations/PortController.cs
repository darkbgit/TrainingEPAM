using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.States;

namespace Task3.AutomaticTelephoneSystem.Stations
{
    internal class PortController : IPortController, IDisposable
    {
        private readonly Port[] _ports;

        private readonly Dictionary<int, Terminal> _portIdTerminalDictionary;

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


            _portIdTerminalDictionary = new Dictionary<int, Terminal>();
        }

        public void ConnectTerminalToPort(Terminal terminal)
        {
            var port = GetFreePort();

            if (port == null)
            {
                throw new StationException(
                    $"Невозможно выдать порт абоненту {terminal.PhoneNumber}. Нет свободных портов.");
            }

            terminal.StartCall += port.PortStartCall;
            port.SendRequest += terminal.OnRequest;
            terminal.AnswerCall += port.PortStartCallAnswer;
            port.AnswerCall += terminal.GetAnswer;
            terminal.EndCall += port.PortEndCallTerminal;
            port.EndCallStation += terminal.OnPortEndCallByStation;

            port.PortState = PortState.Connected;

            _portIdTerminalDictionary.Add(port.Id, terminal);
        }

        public void DisconnectTerminalFromPort(Terminal terminal)
        {
            var port = _ports.FirstOrDefault(p =>
                p.Id == _portIdTerminalDictionary.FirstOrDefault(v => v.Value == terminal).Key);

            if (port is not { PortState: PortState.Connected })
            {
                throw new StationException(
                    $"Невозможно отключить абонента {terminal.PhoneNumber} от порта.");
            }

            terminal.StartCall -= port.PortStartCall;
            port.SendRequest -= terminal.OnRequest;
            terminal.AnswerCall -= port.PortStartCallAnswer;
            port.AnswerCall -= terminal.GetAnswer;
            terminal.EndCall -= port.PortEndCallTerminal;
            port.EndCallStation -= terminal.OnPortEndCallByStation;

            port.PortState = PortState.Disconnected;

            _portIdTerminalDictionary.Remove(port.Id);
        }



        public Port GetPortByPhoneNumber(PhoneNumber phoneNumber)
        {
            var port = _ports.FirstOrDefault(p =>
                p.Id == _portIdTerminalDictionary.FirstOrDefault(v => Equals(v.Value.PhoneNumber, phoneNumber)).Key);

            if (port is not { PortState: PortState.Connected })
            {
                throw new StationException(
                    $"Для номера телефона \"{phoneNumber}\" нет привязанного порта");
            }

            return port;
        }

        public Port GetPortByTerminalId(Guid id)
        {
            var port = _ports.FirstOrDefault(p =>
                p.Id == _portIdTerminalDictionary.FirstOrDefault(v => Equals(v.Value.Id, id)).Key);

            if (port == null)
            {
                throw new StationException(
                    $"Для терминала \"{id}\" нет привязанного порта");
            }

            return port;
        }

        public void ConnectPortsToStation(IStation station)
        {
            foreach (var port in _ports)
            {
                port.StartCall += station.OnPortStartCall;
            }
        }

        private Port GetFreePort() => _ports.FirstOrDefault(p => p.PortState == PortState.Disconnected);

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~PortController()
        {
            ReleaseUnmanagedResources();
        }
    }
}
