using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CsvManager.DAL.Repositories.Implementation.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        //private readonly CsvManagerContext _db;

        //public RepositoryFactory(CsvManagerContext db)
        //{
        //    _db = db;
        //}

        public IRepository<T> CreateRepository<T>(CsvManagerContext db) where T : class, IBaseEntity
        {
            return new Repository<T>(db);
        }
    }
}
