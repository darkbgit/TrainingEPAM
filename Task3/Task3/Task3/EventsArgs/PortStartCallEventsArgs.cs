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
        public PortStartCallEventsArgs(Terminal caller, PhoneNumber called)
        {
            Caller = caller;
            Called = called;
        }

        public Terminal Caller { get; set; }

        public PhoneNumber Called { get; set; }
    }
}
