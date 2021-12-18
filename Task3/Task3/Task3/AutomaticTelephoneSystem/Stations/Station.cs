using System;
using System.Collections.Generic;
using System.Linq;
using Task3.AutomaticTelephoneSystem.Ports;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.EventsArgs;

namespace Task3.AutomaticTelephoneSystem.Stations
{
    public class Station
    {
        private readonly Contracts _contracts;

        private readonly ICollection<CallingTerminalsPour> _callingTerminalsIdPours;

        private readonly ICollection<Terminal> _waitingTerminals;

        private readonly ICollection<Terminal> _callingTerminals;

        public Station(Contracts contracts)
        {
            _contracts = contracts;
            _callingTerminalsIdPours = new List<CallingTerminalsPour>();
            _waitingTerminals = new List<Terminal>();
            _callingTerminals = new List<Terminal>();
        }


        public event EventHandler<StationForBillingEventArgs> StationBilling;

        public void OnPortStartCall(object sender, PortStartCallEventsArgs e)
        {
            _waitingTerminals.Add(e.Caller);
            GetPortByPhoneNumber(e.Called).PortStartCallRequest(this, new StationStartCallRequestEventsArgs(e.Caller.PhoneNumber, e.Caller.Id));
        }

        public void OnPortAnswer(object sender, PortAnswerRequestEventArgs e)
        {
            var callerTerminal = _waitingTerminals.FirstOrDefault(t => t.Id.Equals(e.CallerTerminalId));

            if (callerTerminal == null)
            {
                throw new StationException();
            }

            if (e.IsAccept)
            {
                _waitingTerminals.Remove(callerTerminal);
                _callingTerminals.Add(callerTerminal);

                _callingTerminals.Add(e.Called);

                _callingTerminalsIdPours.Add(new CallingTerminalsPour(callerTerminal.Id, e.Called.Id));

                OnStationBilling(this, new StationForBillingEventArgs(callerTerminal, e.Called, true));
            }

            GetPortByPhoneNumber(callerTerminal.PhoneNumber).PortAnswerCall(this,
                new StationStartCallAnswerEventsArgs(e.IsAccept, e.Called.PhoneNumber));
        }

        public void OnEndCall(object sender, PortEndCallEventsArgs e)
        {
            var terminalsIdsPour = _callingTerminalsIdPours
                .FirstOrDefault(p => p.CallerTerminalId.Equals(e.EndCallTerminalId) ||
                                     p.CalledTerminalId.Equals(e.EndCallTerminalId));

            var callerTerminal = _callingTerminals
                .FirstOrDefault(t => t.Id.Equals(terminalsIdsPour.CallerTerminalId));

            var calledTerminal = _callingTerminals
                .FirstOrDefault(t => t.Id.Equals(terminalsIdsPour.CalledTerminalId));

            if (terminalsIdsPour == null)
            {
                throw new StationException("Ошибка порта завершения звонка");
            }

            var endCallPhoneNumber = _callingTerminals
                .FirstOrDefault(t => t.Id.Equals(e.EndCallTerminalId))?.PhoneNumber;


            var ph = _callingTerminals
                .FirstOrDefault(t => t.Id == terminalsIdsPour.GetAnotherTerminalId(e.EndCallTerminalId));


            var port = GetPortByPhoneNumber(_callingTerminals
                .FirstOrDefault(t => t.Id == terminalsIdsPour.GetAnotherTerminalId(e.EndCallTerminalId))
                ?.PhoneNumber);
            
            OnStationBilling(this, new StationForBillingEventArgs(callerTerminal, calledTerminal, false));

            _callingTerminals.Remove(
                _callingTerminals.FirstOrDefault(t => t.Id.Equals(terminalsIdsPour.CalledTerminalId)));
            _callingTerminals.Remove(
                _callingTerminals.FirstOrDefault(t => t.Id.Equals(terminalsIdsPour.CallerTerminalId)));

            _callingTerminalsIdPours.Remove(terminalsIdsPour);

            

            port.PortEndCallStation(this, new StationEndCallEventArgs(endCallPhoneNumber));

        }

        protected virtual void OnStationBilling(object sender, StationForBillingEventArgs e)
        {
            StationBilling?.Invoke(sender, e);
        }

        private Port GetPortByPhoneNumber(PhoneNumber phoneNumber) => _contracts.GetPortByPhoneNumber(phoneNumber)
                ?? throw new StationException($"Для номера телефона \"{phoneNumber}\" нет привязанного порта");


    }
}
