using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Pagination;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<PaginatedList<ProductDto>> GetProductsPerPage(string sortOrder,
            string searchString,
            int pageNumber,
            int pageSize);
        Task<ProductDto> GetById(Guid id);
        Task<ProductDto> GetByName(string name);

        Task Add(ProductDto order);
        Task Update(ProductDto order);
        Task Delete(Guid id);
    }
}