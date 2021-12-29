using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CsvManager.DAL.Repositories.Implementation
{
    public class GetOrCreateManager : GetOrCreateUnitOfWork<Manager>
    {
        public GetOrCreateManager(CsvManagerContext context, IRepository<Manager> repository, ILogger<GetOrCreateUnitOfWork<Manager>> logger) : base(repository, context, logger)
        {
        }
    }
}
