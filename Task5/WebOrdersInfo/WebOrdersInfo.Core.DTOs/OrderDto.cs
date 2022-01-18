using System;

namespace WebOrdersInfo.Core.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public Guid ClientId { get; set; }
        public Guid ManagerId { get; set; }
        public Guid ProductId { get; set; }
    }
}
