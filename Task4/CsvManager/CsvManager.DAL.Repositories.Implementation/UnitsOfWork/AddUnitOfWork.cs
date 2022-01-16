using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;
using CsvManager.DAL.Repositories.Interfaces.UnitsOfWork;

namespace CsvManager.DAL.Repositories.Implementation.UnitsOfWork
{
    public class AddUnitOfWork<T> : BaseUnitOfWork<T>, IAddUnitOfWork<T> where T: class, IBaseEntity
    {
        public AddUnitOfWork(CsvManagerContext db, IRepositoryFactory repositoryFactory)
            :base(db)
        {
            Repository = repositoryFactory.CreateRepository<T>(db);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await Repository.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await Repository.AddRangeAsync(entities, cancellationToken);
        }
    }
}
