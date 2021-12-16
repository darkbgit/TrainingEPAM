using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class StationException : Exception
    {
        public StationException()
        {
        }

        public StationException(string messege):
            base(messege)
        {
        }

        public StationException(string messege, Exception ex) :
            base(messege, ex)
        {
        }
    }
}
