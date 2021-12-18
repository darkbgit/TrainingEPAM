using System;

namespace Task3.AutomaticTelephoneSystem.Ports
{
    internal class PortException : Exception
    {
        public PortException()
        {

        }

        public PortException(string messege) :
            base(messege)
        {

        }

        public PortException(string messege, Exception ex) :
            base(messege, ex)
        {

        }

    }
}
