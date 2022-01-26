using System;
using System.Collections.Generic;

namespace WebOrdersInfo.Core.DTOs.Models.Filters
{
    public class OrdersFilter
    {
        public IEnumerable<ClientForFilter> Clients { get; set; }
        public IEnumerable<ProductForFilter> Products { get; set; }
        public IEnumerable<ManagerForFilter> Managers { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public bool IsClear { get; set; }

        public OrderSortEnum OrderBy { get; set; }
    }
}