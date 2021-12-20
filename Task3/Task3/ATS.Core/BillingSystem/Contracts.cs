using System.Collections;
using System.Collections.Generic;

namespace ATS.Core.BillingSystem
{
    public class Contracts : IEnumerable<Contract>
    {
        private readonly ICollection<Contract> _contracts;

        public Contracts()
        {
            _contracts = new List<Contract>();
        }

        public Contracts(ICollection<Contract> contracts)
        {
            _contracts = contracts;
        }


        public void Add(Contract contract)
        {
            _contracts.Add(contract);
        }

        public IEnumerator<Contract> GetEnumerator()
        {
            return _contracts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_contracts).GetEnumerator();
        }
    }
}
