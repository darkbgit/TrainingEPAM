
using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.DAL.Repositories.Implementations.Repositories
{
    public class ClientsRepository : Repository<Client>
    {
        public ClientsRepository(WebOrdersInfoContext context)
            : base(context)
        {

        }
        
    }
}