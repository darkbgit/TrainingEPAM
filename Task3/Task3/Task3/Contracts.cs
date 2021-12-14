using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Contracts : IEnumerable<Contract>
    {
        private readonly ICollection<Contract> _contracts;

        public Contracts()
        {
            _contracts = new List<Contract>();
        }

        public IEnumerator<Contract> GetEnumerator()
        {
            return _contracts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_contracts).GetEnumerator();
        }

        public void Add(Contract item)
        {
            _contracts.Add(item);
        }

        public bool Remove(Contract item)
        {
            return _contracts.Remove(item);
        }
    }
}
