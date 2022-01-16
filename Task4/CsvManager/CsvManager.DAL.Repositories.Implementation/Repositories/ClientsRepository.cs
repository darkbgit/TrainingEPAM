using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;

namespace CsvManager.DAL.Repositories.Implementation.Repositories
{
    public class ClientsRepository : Repository<Client>
    {
        public ClientsRepository(CsvManagerContext context)
            : base(context)
        {

        }
        
    }
}