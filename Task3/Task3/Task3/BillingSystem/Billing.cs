using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.BillingSystem
{
    internal class Billing : IBilling
    {
        private readonly List<Record> _records;

        public Billing()
        {
            _records = new List<Record>();
        }


        public void AddCall(Record record)
        {
            _records.Add(record);
        }


        public IEnumerator<Record> GetEnumerator()
        {
            return _records.GetEnumerator();
        }
        

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _records).GetEnumerator();
        }
    }
}
