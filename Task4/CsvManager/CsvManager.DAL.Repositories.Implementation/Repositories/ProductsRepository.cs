using CsvManager.DAL.Core;
using CsvManager.DAL.Core.Entities;

namespace CsvManager.DAL.Repositories.Implementation.Repositories
{
    public class ProductsRepository : Repository<Product>
    {
        public ProductsRepository(CsvManagerContext context)
            : base(context)
        {

        }
        
    }
}