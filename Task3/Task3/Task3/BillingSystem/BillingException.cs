using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.BillingSystem
{
    internal class BillingException : Exception
    {
        public BillingException()
        {
        }

        public BillingException(string massage) :
            base(massage)
        {
        }

        public BillingException(string massage, Exception ex) :
            base(massage, ex)
        {
        }
    }
}
