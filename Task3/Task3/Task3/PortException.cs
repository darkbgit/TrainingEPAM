﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
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
