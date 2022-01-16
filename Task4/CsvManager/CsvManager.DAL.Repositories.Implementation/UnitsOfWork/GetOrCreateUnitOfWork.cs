using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;
using CsvManager.DAL.Repositories.Interfaces.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace CsvManager.DAL.Repositories.Implementation.UnitsOfWork
{
    public class GetOrCreateUnitOfWork<T> : BaseUnitOfWork<T>, IGetOrCreateUnitOfWork<T> where T : class, IEntityWithName, new()

    {
        private readonly IRepository<T> _repo;
        private readonly ILogger<GetOrCreateUnitOfWork<T>> _logger;

        private static readonly SemaphoreSlim SemaphoreSlim = new(1, 1);
        

        public GetOrCreateUnitOfWork(CsvManagerContext db, IRepositoryFactory repositoryFactory, ILogger<GetOrCreateUnitOfWork<T>> logger):
            base(db)
        {
            _repo = repositoryFactory.CreateRepository<T>(db);
            _logger = logger;
        }


        public async Task<T> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Entity's name for create is null or empty.");

            await SemaphoreSlim.WaitAsync(cancellationToken);
            try
            {
                var item = _repo
                    .FindBy(c => c.Name.Equals(name))
                    .FirstOrDefault();

                if (item != null) return item;

                item = new T
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };

                await _repo.AddAsync(item, cancellationToken);
                await SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation($"Add new {typeof(T).Name} {name}.");

                return item;
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }
    }
}