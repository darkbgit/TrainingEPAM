using System;
using System.Collections.Generic;
using System.Linq;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.EventsArgs;
using Task3.States;

namespace Task3.AutomaticTelephoneSystem.Stations
{
    public class Station
    {
        private readonly ICollection<CallingTerminalsPair> _callingTerminalsIdPairs;

        //private readonly ICollection<PhoneNumber> _waitingPhonesNumbers;

        private readonly ICollection<Guid> _waitingTerminalsIds;

        private readonly ICollection<Guid> _callingTerminalsIds;

        private readonly IPortController _portController;

        public Station()
        {
            _portController = new PortController();


            _callingTerminalsIdPairs = new List<CallingTerminalsPair>();
            //_waitingPhonesNumbers = new List<PhoneNumber>();
            _callingTerminalsIds = new List<Guid>();
            _waitingTerminalsIds = new List<Guid>();
        }


        public event EventHandler<StationStartCallAfterAnswerEventArgs> StationStartCall;

        public event EventHandler<StationEndCallEventArgs> StationEndCall;

        public void OnPortStartCall(object sender, PortStartCallEventArgs e)
        {
            _waitingTerminalsIds.Add(e.SourceTerminalId);
            //_waitingPhonesNumbers.Add(e.SourcePhoneNumber);
            _portController.GetPortByPhoneNumber(e.TargetPhoneNumber)
                .PortStartCallRequest(this, new StationStartCallRequestEventArgs(e.SourcePhoneNumber, e.SourceTerminalId));
        }

        public void OnPortAnswer(object sender, PortAnswerRequestEventArgs e)
        {
            //var sourceTerminalId = _waitingTerminalsIds.First(t => t.Equals(e.SourceTerminalId));

            if (e.IsAccept)
            {
                _waitingTerminalsIds.Remove(e.SourceTerminalId);

                _callingTerminalsIds.Add(e.SourceTerminalId);

                _callingTerminalsIds.Add(e.TargetTerminalId);

                _callingTerminalsIdPairs.Add(new CallingTerminalsPair(e.SourceTerminalId, e.TargetTerminalId));

                OnStationStarCall(this, new StationStartCallAfterAnswerEventArgs(e.SourceTerminalId, e.TargetTerminalId, e.IsAccept));
            }

            var port = _portController.GetPortByTerminalId(e.SourceTerminalId);

            if (port.PortState != PortState.Waiting)
            {

            }

            port.PortAnswerCall(this, new StationStartCallAfterAnswerEventArgs(e.SourceTerminalId, e.TargetTerminalId, e.IsAccept));
        }

        public void OnEndCall(object sender, PortEndCallEventArgs e)
        {
            var terminalsIdsPair = _callingTerminalsIdPairs
                .FirstOrDefault(p => p.SourceTerminalId.Equals(e.EndCallTerminalId) && p.TargetTerminalId.Equals(e.EndCallTerminalId));

            //var callerTerminal = _callingTerminals
            //    .FirstOrDefault(t => t.Id.Equals(terminalsIdsPair.SourceTerminalId));

            //var calledTerminal = _callingTerminals
            //    .FirstOrDefault(t => t.Id.Equals(terminalsIdsPair.TargetTerminalId));

            if (terminalsIdsPair == null)
            {
                throw new StationException("Ошибка порта завершения звонка");
            }

            //var endCallPhoneNumber = _callingTerminals
            //    .FirstOrDefault(t => t.Id.Equals(e.EndCallTerminalId))?.PhoneNumber;


            //var ph = _callingTerminals
            //    .FirstOrDefault(t => t.Id == terminalsIdsPair.GetAnotherTerminalId(e.EndCallTerminalId));


            var port = _portController.GetPortByTerminalId(_callingTerminalsIds
                .FirstOrDefault(t => t == terminalsIdsPair.GetAnotherTerminalId(e.EndCallTerminalId)));
            
            OnStationEndCall(this, new StationEndCallEventArgs(e.EndCallTerminalId));

            _callingTerminalsIds.Remove(
                _callingTerminalsIds.FirstOrDefault(t => t.Equals(terminalsIdsPair.TargetTerminalId)));
            _callingTerminalsIds.Remove(
                _callingTerminalsIds.FirstOrDefault(t => t.Equals(terminalsIdsPair.SourceTerminalId)));

            _callingTerminalsIdPairs.Remove(terminalsIdsPair);

            

            port.PortEndCallStation(this, new StationEndCallEventArgs(e.EndCallTerminalId));

        }

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

        protected virtual void OnStationStarCall(object sender, StationStartCallAfterAnswerEventArgs e)
        {
            StationStartCall?.Invoke(sender, e);
        }


        protected virtual void OnStationEndCall(object sender, StationEndCallEventArgs e)
        {
            StationEndCall?.Invoke(sender, e);
        }


    }
}
