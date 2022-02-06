using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.DAL.Repositories.Implementations.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(WebOrdersInfoContext context)
            : base(context)
        {

        }
    }
}