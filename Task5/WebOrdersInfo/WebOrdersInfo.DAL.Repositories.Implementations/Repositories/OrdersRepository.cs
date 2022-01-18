using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.DAL.Repositories.Implementations.Repositories
{
    public class OrdersRepository : Repository<Order>
    {
        public OrdersRepository(WebOrdersInfoContext context)
            : base(context)
        {

        }
        
    }
}