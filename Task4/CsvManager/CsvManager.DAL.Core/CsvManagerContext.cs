using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CsvManager.DAL.Core
{
    public class CsvManagerContext : DbContext
    {
        public CsvManagerContext(DbContextOptions<CsvManagerContext> options)
            : base(options)
        {

        }
    }
}
