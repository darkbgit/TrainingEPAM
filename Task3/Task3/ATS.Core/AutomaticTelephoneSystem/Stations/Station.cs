using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.AutomaticTelephoneSystem.EventsArgs;
using ATS.Core.AutomaticTelephoneSystem.Ports;
using ATS.Core.AutomaticTelephoneSystem.Ports.States;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using Logging.Loggers;

namespace ATS.Core.AutomaticTelephoneSystem.Stations
{
    public class Station : IStation
    {
        private readonly ICollection<CallingTerminalsPair> _callingTerminalsIdPairs;

        private readonly ICollection<Guid> _callingTerminalsIds;

        private readonly IPortController _portController;

        public Station()
        {
            _portController = new PortController();

            ConnectPortsToStation(_portController.Ports);

            _callingTerminalsIdPairs = new List<CallingTerminalsPair>();
            _callingTerminalsIds = new List<Guid>();
        }


        public event EventHandler<StationStartCallAfterAnswerEventArgs> StationStartCall;

        public event EventHandler<StationEndCallEventArgs> StationEndCall;

        public void ConnectTerminalToPort(object sender, EventArgs e)
        {
            if (sender is Terminal terminal)
            {
                _portController.ConnectTerminalToPort(terminal);
            }
        }

        public void DisconnectTerminalFromPort(object sender, EventArgs e)
        {
            if (sender is Terminal terminal)
            {
                _portController.DisconnectTerminalFromPort(terminal);
            }
        }

        private void OnStartCall(object sender, PortStartCallEventArgs e)
        {
            _portController.GetPortByPhoneNumber(e.TargetPhoneNumber)
                .PortStartCallRequest(this, new StationStartCallRequestEventArgs(e.SourcePhoneNumber, e.SourceTerminalId));
        }

        private void OnAnswer(object sender, PortAnswerRequestEventArgs e)
        {
            if (e.IsAccept)
            {
                _callingTerminalsIds.Add(e.SourceTerminalId);

                _callingTerminalsIds.Add(e.TargetTerminalId);

                _callingTerminalsIdPairs.Add(new CallingTerminalsPair(e.SourceTerminalId, e.TargetTerminalId));

                OnStationStarCall(this, new StationStartCallAfterAnswerEventArgs(e.SourceTerminalId, e.TargetTerminalId, e.IsAccept));
            }

            var port = _portController.GetPortByTerminalId(e.SourceTerminalId);

            port.PortAnswerCall(this, new StationStartCallAfterAnswerEventArgs(e.SourceTerminalId, e.TargetTerminalId, e.IsAccept));
        }

        private void OnEndCall(object sender, PortEndCallEventArgs e)
        {
            var terminalsIdsPair = _callingTerminalsIdPairs
                .FirstOrDefault(p => p.SourceTerminalId.Equals(e.EndCallTerminalId) || p.TargetTerminalId.Equals(e.EndCallTerminalId));

            if (terminalsIdsPair == null)
            {
                Log.LogMessage($"Terminal {e.EndCallTerminalId} not found in station calling terminals pairs.");
                throw new StationException("Ошибка порта завершения звонка.");
            }

            var port = _portController.GetPortByTerminalId(_callingTerminalsIds
                .FirstOrDefault(t => t == terminalsIdsPair.GetAnotherTerminalId(e.EndCallTerminalId)));
            
            OnStationEndCall(this, new StationEndCallEventArgs(e.EndCallTerminalId));

            _callingTerminalsIds.Remove(
                _callingTerminalsIds.FirstOrDefault(t => t.Equals(terminalsIdsPair.TargetTerminalId)));
            _callingTerminalsIds.Remove(
                _callingTerminalsIds.FirstOrDefault(t => t.Equals(terminalsIdsPair.SourceTerminalId)));

            _callingTerminalsIdPairs.Remove(terminalsIdsPair);
            

            port.PortEndCallTarget(this, new StationEndCallEventArgs(e.EndCallTerminalId));

        }

        

        protected virtual void OnStationStarCall(object sender, StationStartCallAfterAnswerEventArgs e)
        {
            StationStartCall?.Invoke(sender, e);
        }


        protected virtual void OnStationEndCall(object sender, StationEndCallEventArgs e)
        {
            StationEndCall?.Invoke(sender, e);
        }

        private void ConnectPortsToStation(IEnumerable<Port> ports)
        {
            foreach (var port in ports)
            {
                port.StartCall += OnStartCall;
                port.AnswerRequest += OnAnswer;
                port.EndCallSource += OnEndCall;
            }
        }
    }
}
