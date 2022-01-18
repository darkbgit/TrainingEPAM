﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace WebOrdersInfo.DAL.Core.Entities
{
    public class Client : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

}