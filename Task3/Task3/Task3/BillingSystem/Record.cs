﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneSystem.Terminals;

namespace Task3.BillingSystem
{
    public class Record
    {
        public Record(int id)
        {
            Id = id;
            IsCompleted = false;
        }

        public int Id { get; set; }
        public DateTime BeginCall { get; set; }
        public DateTime EndCall { get; set; }
        public Terminal CallerTerminal { get; set; }
        public Terminal CalledTerminal { get; set; }
        public bool IsCompleted { get; set; }

    }
}
