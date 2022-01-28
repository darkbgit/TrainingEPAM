using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.DAL.Repositories.Implementations;
using WebOrdersInfo.Repositories.Interfaces;
using WebOrdersInfo.Services.Implementations.Base;

namespace WebOrdersInfo.Services.Implementations
{
    public class ClientsService : IClientService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClientsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetAll()
        {
            var result = await _unitOfWork.Clients.GetAll().ToListAsync();
            return result.Select(i => _mapper.Map<ClientDto>(i));
        }

        public async Task Add(ClientDto client)
        {
            var entity = _mapper.Map<Client>(client);
            await _unitOfWork.Clients.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(ClientDto client)
        {
            var entity = _mapper.Map<Client>(client);
            _unitOfWork.Clients.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _unitOfWork.Clients.Remove(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
