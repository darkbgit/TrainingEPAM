using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.EventsArgs;

namespace Task3.BillingSystem
{
    public class Billing : IBilling
    {
        private readonly List<BillingRecord> _records;

        private readonly ILogger _logger;


        public Billing(ILogger logger)
        {
            _logger = logger;
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

        public void BeginAddRecord(Guid sourceTerminalId, Guid targetTerminalId)
        {
            _records.Add(new BillingRecord(_records.Count + 1)
            {
                SourceTerminalId = sourceTerminalId,
                TargetTerminalId = targetTerminalId,
                BeginCall = DateTime.Now.ToUniversalTime()
            });
            _logger.Log(_records.Last().BeginCall.ToLocalTime().ToString());
        }

        public void FinishAddRecord(Guid sourceTerminalId, Guid targetTerminalId)
        {
            var record = _records
                .FirstOrDefault(r => r.IsCompleted == false && r.SourceTerminalId == sourceTerminalId && r.TargetTerminalId == targetTerminalId);
            if (record == null) throw new BillingException("Ошибка биллинговой системы при завершении звонка");

            record.EndCall = DateTime.Now.ToUniversalTime();
            record.IsCompleted = true;

            _logger.Log(_records.Last().EndCall.ToLocalTime().ToString());
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
