using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;

namespace CsvManager.DAL.Repositories.Implementation.Repositories
{
    public class ManagersRepository : Repository<Manager>
    {
        public ManagersRepository(CsvManagerContext context)
            : base(context)
        {

        }
    }
}
