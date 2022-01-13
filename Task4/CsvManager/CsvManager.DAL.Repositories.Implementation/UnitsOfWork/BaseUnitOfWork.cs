using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;

namespace CsvManager.DAL.Repositories.Implementation.UnitsOfWork
{
    public abstract class BaseUnitOfWork<T> : IDisposable where T : class, IBaseEntity
    {
        private readonly CsvManagerContext _db;

        private bool disposed;
        protected IRepository<T> Repository { get; init; }

        protected BaseUnitOfWork(CsvManagerContext db)
        {
            _db = db;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _db.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db?.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
