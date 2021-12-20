using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.EventsArgs;
using Logging.Loggers;

namespace ATS.Core.BillingSystem
{
    public class Billing : IBilling
    {
        private readonly ICollection<BillingRecord> _records;

        private readonly ICollection<Contract> _contracts;


        public Billing()
        {
            _records = new List<BillingRecord>();
        }


        public void OnStation(object sender, StationStartCallAfterAnswerEventArgs e)
        {
            if (e.IsAccept)
            {
                BeginAddRecord(e.SourceTerminalId, e.TargetTerminalId);
            }
            else
            {
                FinishAddRecord(e.SourceTerminalId, e.TargetTerminalId);
            }
            
        }

        private void BeginAddRecord(Guid sourceTerminalId, Guid targetTerminalId)
        {
            _records.Add(new BillingRecord(_records.Count + 1)
            {
                SourceTerminalId = sourceTerminalId,
                TargetTerminalId = targetTerminalId,
                BeginCall = DateTime.Now.ToUniversalTime()
            });
            Log.LogMessage(_records.Last().BeginCall.ToLocalTime().ToString());
        }

        private void FinishAddRecord(Guid sourceTerminalId, Guid targetTerminalId)
        {
            var record = _records
                .FirstOrDefault(r => r.IsCompleted == false && r.SourceTerminalId == sourceTerminalId && r.TargetTerminalId == targetTerminalId);
            if (record == null) throw new BillingException("Ошибка биллинговой системы при завершении звонка");

            record.EndCall = DateTime.Now.ToUniversalTime();
            record.IsCompleted = true;

            Log.LogMessage(_records.Last().EndCall.ToLocalTime().ToString());
        }

        public IEnumerator<BillingRecord> GetEnumerator()
        {
            return _records.GetEnumerator();
        }
        

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _records).GetEnumerator();
        }
    }
}
