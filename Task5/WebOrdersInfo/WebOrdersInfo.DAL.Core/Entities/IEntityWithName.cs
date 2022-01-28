using System.Collections.Generic;

namespace WebOrdersInfo.DAL.Core.Entities
{
    public interface IEntityWithName : IBaseEntity
    {
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}