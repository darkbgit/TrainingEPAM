using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvManager.DAL.Core.Entities
{
    public class Manager : IBaseEntity
    {
        public Guid Id { get; set; }

        public string SecondName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
