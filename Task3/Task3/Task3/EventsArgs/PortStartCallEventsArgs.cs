﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem;
using Task3.AutomaticTelephoneSystem.Terminals;
using Task3.States;

namespace Task3.EventsArgs
{
    public class PortStartCallEventsArgs : EventArgs
    {
        public PortStartCallEventsArgs(PhoneNumber sourcePhoneNumber, PhoneNumber targetPhoneNumber)
        {
            SourcePhoneNumber = sourcePhoneNumber;
            TargetPhoneNumber = targetPhoneNumber;
        }

        public PhoneNumber SourcePhoneNumber { get; }

        public PhoneNumber TargetPhoneNumber { get; }
    }
}
