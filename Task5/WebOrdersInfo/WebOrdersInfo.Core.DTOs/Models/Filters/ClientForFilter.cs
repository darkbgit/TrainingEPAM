using System;

namespace WebOrdersInfo.Core.DTOs.Models.Filters
{
    public class ClientForFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}