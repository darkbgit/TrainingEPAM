using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class PortController : IEnumerable<Port>
    {
        private readonly ICollection<Port> _ports;

        public PortController(ICollection<Port> ports)
        {
            _ports = ports;
        }

        //public Port GetPortByPhoneNumber(PhoneNumber phoneNumber)
        //{

        //}

        public IEnumerator<Port> GetEnumerator()
        {
            return _ports.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_ports).GetEnumerator();
        }
    }
}
