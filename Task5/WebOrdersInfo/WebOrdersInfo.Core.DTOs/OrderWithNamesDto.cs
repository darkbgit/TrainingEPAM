using System;

namespace WebOrdersInfo.Core.DTOs
{
    public class OrderWithNamesDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public Guid ManagerId { get; set; }
        public string ManagerName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}