using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Pagination;
using WebOrdersInfo.Core.DTOs.Models.Statistics;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IManagerService
    {
        Task<IEnumerable<ManagerDto>> GetAll();
        Task<PaginatedList<ManagerDto>> GetManagersPerPage(string sortOrder,
            string searchString,
            int pageNumber);
        Task<ManagerDto> GetById(Guid id);
        Task<ManagerDto> GetByName(string name);

        Task<IEnumerable<ManagerWithCountOrdersDto>> GetEntityWithOrdersCount(int take = 10, bool fromTop = true);

        Task Add(ManagerDto order);
        Task Update(ManagerDto order);
        Task Delete(Guid id);
    }
}