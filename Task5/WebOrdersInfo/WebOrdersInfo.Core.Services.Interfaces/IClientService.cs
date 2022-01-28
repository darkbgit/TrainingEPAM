using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAll();

        Task Add(ClientDto order);
        Task Update(ClientDto order);
        Task Delete(Guid id);
    }
}