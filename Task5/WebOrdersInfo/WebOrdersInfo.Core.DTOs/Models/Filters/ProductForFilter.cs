using System;

namespace WebOrdersInfo.Core.DTOs.Models.Filters
{
    public class ProductForFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}