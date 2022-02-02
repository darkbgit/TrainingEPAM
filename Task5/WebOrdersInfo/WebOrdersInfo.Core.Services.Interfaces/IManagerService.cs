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

        Task<IEnumerable<ManagerNameWithGroupingPropertyDto>> GetManagersWithOrdersCount(int take, bool fromTop, DateTime from, DateTime to);
        Task<IEnumerable<ManagerNameWithGroupingPropertyDto>> GetManagersWithTotalPrice(int take, bool fromTop, DateTime from, DateTime to);

        Task Add(ManagerDto order);
        Task Update(ManagerDto order);
        Task Delete(Guid id);
    }
}