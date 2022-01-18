using System;
using System.Net.Http.Headers;

namespace WebOrdersInfo.DAL.Core.Entities
{
    public class Order : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        public Guid ManagerId { get; set; }
        public virtual Manager Manager { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } 
    }
}