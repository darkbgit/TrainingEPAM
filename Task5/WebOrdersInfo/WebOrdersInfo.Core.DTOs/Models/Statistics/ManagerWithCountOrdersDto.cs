﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebOrdersInfo.Core.DTOs.Models.Statistics
{
    public class ManagerWithCountOrdersDto
    {   
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
