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
        private readonly ICollection<CallingTerminalsPour> _callingTerminalsIdPairs;

        private readonly ICollection<PhoneNumber> _waitingPhonesNumbers;

        private readonly ICollection<Terminal> _callingTerminals;

        private readonly IPortController _portController;

        public Station()
        {
            _portController = new PortController();
            _callingTerminalsIdPairs = new List<CallingTerminalsPour>();
            _waitingPhonesNumbers = new List<Guid>();
            _callingTerminals = new List<Terminal>();

            
        }


        public event EventHandler<StationForBillingEventArgs> StationStartCall;

        public event EventHandler<StationForBillingEventArgs> StationEndCall;

        public void OnPortStartCall(object sender, PortStartCallEventsArgs e)
        {
            _waitingPhonesNumbers.Add(e.SourcePhoneNumber);
            _portController.GetPortByPhoneNumber(e.TargetPhoneNumber)
                .PortStartCallRequest(this, new StationStartCallRequestEventsArgs(e.SourcePhoneNumber));
        }

        public void OnPortAnswer(object sender, PortAnswerRequestEventArgs e)
        {
            var callerTerminal = _waitingPhonesNumbers.FirstOrDefault(t => t.Id.Equals(e.CallerTerminalId));

            if (callerTerminal == null)
            {
                throw new StationException();
            }

            if (e.IsAccept)
            {
                _waitingPhonesNumbers.Remove(callerTerminal);
                _callingTerminals.Add(callerTerminal);

                _callingTerminals.Add(e.Called);

                _callingTerminalsIdPairs.Add(new CallingTerminalsPour(callerTerminal.Id, e.Called.Id));

                OnStationStarCall(this, new StationForBillingEventArgs(callerTerminal, e.Called, true));
            }

            _portController.GetPortByPhoneNumber(callerTerminal.PhoneNumber)
                .PortAnswerCall(this, new StationStartCallAnswerEventsArgs(e.IsAccept, e.Called.PhoneNumber));
        }

        public void OnEndCall(object sender, PortEndCallEventsArgs e)
        {
            var terminalsIdsPair = _callingTerminalsIdPairs
                .FirstOrDefault(p => p.CallerTerminalId.Equals(e.EndCallTerminalId) ||
                                     p.CalledTerminalId.Equals(e.EndCallTerminalId));

            var callerTerminal = _callingTerminals
                .FirstOrDefault(t => t.Id.Equals(terminalsIdsPair.CallerTerminalId));

            var calledTerminal = _callingTerminals
                .FirstOrDefault(t => t.Id.Equals(terminalsIdsPair.CalledTerminalId));

            if (terminalsIdsPair == null)
            {
                throw new StationException("Ошибка порта завершения звонка");
            }

            var endCallPhoneNumber = _callingTerminals
                .FirstOrDefault(t => t.Id.Equals(e.EndCallTerminalId))?.PhoneNumber;


            var ph = _callingTerminals
                .FirstOrDefault(t => t.Id == terminalsIdsPair.GetAnotherTerminalId(e.EndCallTerminalId));


            var port = _portController.GetPortByPhoneNumber(_callingTerminals
                .FirstOrDefault(t => t.Id == terminalsIdsPair.GetAnotherTerminalId(e.EndCallTerminalId))
                ?.PhoneNumber);
            
            OnStationStarCall(this, new StationForBillingEventArgs(callerTerminal, calledTerminal, false));

            _callingTerminals.Remove(
                _callingTerminals.FirstOrDefault(t => t.Id.Equals(terminalsIdsPair.CalledTerminalId)));
            _callingTerminals.Remove(
                _callingTerminals.FirstOrDefault(t => t.Id.Equals(terminalsIdsPair.CallerTerminalId)));

            _callingTerminalsIdPairs.Remove(terminalsIdsPair);

            

            port.PortEndCallStation(this, new StationEndCallEventArgs(endCallPhoneNumber));

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

        protected virtual void OnStationStarCall(object sender, StationForBillingEventArgs e)
        {
            StationStartCall?.Invoke(sender, e);
        }

             


    }
}
