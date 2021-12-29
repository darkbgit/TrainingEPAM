using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CsvManager.DAL.Repositories.Implementation
{
    public class GetOrCreateUnitOfWork<T> : IGetOrCreateUnitOfWork<T> where T : class, IEntityWithName, new()

    {
        private readonly CsvManagerContext _db;
        private readonly IRepository<T> _repo;
        private readonly ILogger<GetOrCreateUnitOfWork<T>> _logger;

        private static readonly SemaphoreSlim SemaphoreSlim = new(1, 1);
        

        public GetOrCreateUnitOfWork(IRepository<T> repository, CsvManagerContext db, ILogger<GetOrCreateUnitOfWork<T>> logger)
        {
            _repo = repository;
            _db = db;
            _logger = logger;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _repo.Save();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _repo?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<T> GetOrCreateByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Entity's name for create is null or empty.");

            await SemaphoreSlim.WaitAsync();
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

                await _repo.Add(item);
                await SaveChangesAsync();
                
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