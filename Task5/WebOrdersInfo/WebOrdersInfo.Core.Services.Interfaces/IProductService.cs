using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IProductService
    {

        Task<IEnumerable<NameDto>> GetAll();

    }
}
