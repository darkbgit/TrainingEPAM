using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class StationException : Exception
    {
        public StationException(string message):
            base(message)
        {
        }
    }
}
