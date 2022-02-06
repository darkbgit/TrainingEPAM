
using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.DAL.Repositories.Implementations.Repositories
{
    public class ManagersRepository : Repository<Manager>
    {
        public ManagersRepository(WebOrdersInfoContext context)
            : base(context)
        {

        }
        
    }
}