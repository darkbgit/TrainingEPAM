using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Statistics;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();

        Task Add(ProductDto order);
        Task Update(ProductDto order);
        Task Delete(Guid id);
    }
}