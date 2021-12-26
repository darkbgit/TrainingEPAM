using System;

namespace ATS.Core.AutomaticTelephoneSystem.Ports
{
    public class PortException : Exception
    {
        public PortException()
        {

        }

        public PortException(string message) :
            base(message)
        {

        }

        public PortException(string message, Exception ex) :
            base(message, ex)
        {

        }

    }
}
