using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Pagination;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAll();
        Task<PaginatedList<ClientDto>> GetClientsPerPage(string sortOrder,
            string searchString,
            int pageNumber);
        Task<ClientDto> GetById(Guid id);
        Task<ClientDto> GetByName(string name);

        Task Add(ClientDto order);
        Task Update(ClientDto order);
        Task Delete(Guid id);
    }
}