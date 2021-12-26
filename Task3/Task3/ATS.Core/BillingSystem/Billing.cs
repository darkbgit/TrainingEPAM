using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.EventsArgs;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.Tariffs;
using Logging.Loggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ATS.Core.BillingSystem
{
    public class Billing : IBilling, IBillingReport
    {
        private readonly IEnumerable<ITariff> _tariffs;

        private readonly ICollection<BillingRecord> _records;

        private readonly IEnumerable<ITerminal> _terminals;

        private readonly IEnumerable<Contract> _contracts;


        public Billing(IEnumerable<Contract> contracts, IEnumerable<ITariff> tariffs, IEnumerable<ITerminal> terminals)
        {
            _contracts = contracts;
            _tariffs = tariffs;
            _terminals = terminals;
            _records = new List<BillingRecord>();
        }


        public void StartCall(object sender, StationStartCallAfterAnswerEventArgs e)
        {
            BeginAddRecord(e.SourceTerminalId, e.TargetTerminalId);
        }

        public void EndCall(object sender, StationEndCallEventArgs e)
        {
            FinishAddRecord(e.EndCallTerminalId);
        }

        public IEnumerable<ReportRecord> GetReportForClient(Func<BillingRecord, bool> predicate, Guid clientId, PhoneNumber interlocutorPhoneNumber)
        {
            var contract = _contracts.First(c => c.ClientId.Equals(clientId));

            var tariff = _tariffs.First(t => t.Id.Equals(contract.TariffId));

            var interlocutorTerminalId = GetTerminalIdByPhoneNumber(interlocutorPhoneNumber);

            var result = _records.Where(predicate)
                .Where(r => r.IsCompleted &&
                            (r.SourceTerminalId.Equals(contract.TerminalId) || r.TargetTerminalId.Equals(contract.TerminalId)) &&
                            (interlocutorTerminalId == null || r.SourceTerminalId.Equals(interlocutorTerminalId) || r.TargetTerminalId.Equals(interlocutorTerminalId)))
                .Select(r => new ReportRecord
                {
                    BeginCall = r.BeginCall,
                    EndCall = r.EndCall,
                    Duration = r.EndCall - r.BeginCall,
                    IsIncome = r.TargetTerminalId.Equals(contract.TerminalId),
                    InterlocutorPhoneNumber = r.TargetTerminalId.Equals(contract.TerminalId) ?
                        GetPhoneNumberByTerminalId(r.SourceTerminalId) : GetPhoneNumberByTerminalId(r.TargetTerminalId),
                    Cost = tariff.GetCost(r.EndCall - r.BeginCall, r.TargetTerminalId.Equals(contract.TerminalId))
                })
                .ToList();

            return result;
        }


        public IEnumerator<BillingRecord> GetEnumerator()
        {
            return _records.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_records).GetEnumerator();
        }

        private void BeginAddRecord(Guid sourceTerminalId, Guid targetTerminalId)
        {
            _records.Add(new BillingRecord(_records.Count + 1)
            {
                SourceTerminalId = sourceTerminalId,
                TargetTerminalId = targetTerminalId,
                BeginCall = DateTime.Now.ToUniversalTime()
            });
            Log.LogMessage($"Source terminal {sourceTerminalId} target terminal {targetTerminalId} call start.");
        }

        private void FinishAddRecord(Guid endCallTerminalId)
        {
            var record = _records
                .FirstOrDefault(r => r.IsCompleted == false && r.SourceTerminalId == endCallTerminalId || r.TargetTerminalId == endCallTerminalId);

            if (record == null) throw new BillingException("Ошибка биллинговой системы при завершении звонка.");

            record.EndCall = DateTime.Now.ToUniversalTime();
            record.IsCompleted = true;

            Log.LogMessage($"Source terminal {record.SourceTerminalId} target terminal {record.TargetTerminalId} call end, duration {record.EndCall - record.BeginCall:hh':'mm':'ss}.");
        }

        private PhoneNumber GetPhoneNumberByTerminalId(Guid terminalId)
        {
            var terminal = _terminals.FirstOrDefault(t => t.Id.Equals(terminalId));

            return terminal == null ? PhoneNumber.Empty : terminal.PhoneNumber;
        }

        private Guid? GetTerminalIdByPhoneNumber(PhoneNumber phoneNumber)
        {
            var id = _terminals.FirstOrDefault(t => t.PhoneNumber.Equals(phoneNumber))?.Id;
            return id;
        }
    }
}
