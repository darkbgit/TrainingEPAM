using System;

namespace WebOrdersInfo.Core.DTOs.Filters
{
    public class ManagerForFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}