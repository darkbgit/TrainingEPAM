using System;

namespace ATS.Core.BillingSystem
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
