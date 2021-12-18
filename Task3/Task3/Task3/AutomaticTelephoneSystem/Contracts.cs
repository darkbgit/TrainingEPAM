using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Task3.AutomaticTelephoneSystem.Ports;

namespace Task3.AutomaticTelephoneSystem
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

        public Port GetPortByPhoneNumber(PhoneNumber phoneNumber)
        {
            return _contracts
                .FirstOrDefault(c => c.Terminal.PhoneNumber.Equals(phoneNumber))?.Port;
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
