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


        public void OnStation(object sender, StationForBillingEventArgs e)
        {
            if (e.IsStart)
            {
                BeginAddRecord(e.Caller, e.Called);
            }
            else
            {
                FinishAddRecord(e.Caller, e.Called);
            }
            
        }

        public void BeginAddRecord(Terminal callerTerminal, Terminal calledTerminal)
        {
            _records.Add(new BillingRecord(_records.Count + 1)
            {
                CallerTerminal = callerTerminal,
                CalledTerminal = calledTerminal,
                BeginCall = DateTime.Now.ToUniversalTime()
            });
            _logger.Log(_records.Last().BeginCall.ToLocalTime().ToString());
        }

        public void FinishAddRecord(Terminal callerTerminal, Terminal calledTerminal)
        {
            var record = _records
                .FirstOrDefault(r => r.IsCompleted == false && r.CallerTerminal == callerTerminal || r.CalledTerminal == calledTerminal);
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
