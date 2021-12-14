using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Contract
    {
        public Contract()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public User User { get; set; }

        public Terminal Terminal { get; set; }

        public Port Port { get; set; }

    }
}
