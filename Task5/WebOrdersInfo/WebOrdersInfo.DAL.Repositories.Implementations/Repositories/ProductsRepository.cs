
using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.DAL.Repositories.Implementations.Repositories
{
    public class ProductsRepository : Repository<Product>
    {
        public ProductsRepository(WebOrdersInfoContext context)
            : base(context)
        {

        }
        
    }
}