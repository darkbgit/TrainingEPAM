using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class MobileCompany
    {
        private readonly Contracts _contracts;

        private readonly IEnumerable<Port> _ports;

        public MobileCompany(Contracts contracts, IEnumerable<Port> ports)
        {
            _contracts = contracts;
            _ports = ports;
        }

        public void MakeContract(User user)
        {
            var portIndex = new Random().Next(0, _ports.Count() - 1);
            var result = new Contract
            {
                User = user,
                Terminal = new Terminal(),

                Port = _ports.ElementAt(portIndex)
            };

            _contracts.Add(result);
        }

        //User CreateUser()
        //{
        //    var portIndex = new Random().Next(0, _ports.Count() - 1);
        //    var result = new Contract
        //    {
        //        User = user,
        //        Terminal = new Terminal(),

        //        Port = _ports.ElementAt(portIndex)
        //    };

        //    _contracts.Add(result);
        //}

        public Terminal GetTerminalByUser(User user)
        {
            var result = _contracts.FirstOrDefault(c => c.User.Equals(user))?.Terminal;
            return result;
        }
    }
}
