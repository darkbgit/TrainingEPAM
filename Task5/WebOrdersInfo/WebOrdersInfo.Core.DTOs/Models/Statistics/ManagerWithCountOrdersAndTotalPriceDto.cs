using System;

namespace WebOrdersInfo.Core.DTOs.Models.Statistics
{
    public class ManagerWithCountOrdersAndTotalPriceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double TotalPrice { get; set; }
    }
}