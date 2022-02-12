using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Pagination;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Repositories.Interfaces;

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

        public async Task<PaginatedList<ClientDto>> GetClientsPerPage(string sortOrder,
            string searchString,
            int pageNumber,
            int pageSize)
        {
            var clients = _unitOfWork.Clients.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.Name.Contains(searchString));
            }

            clients = sortOrder switch
            {
                "name_desc" => clients.OrderByDescending(c => c.Name),
                _ => clients.OrderBy(c => c.Name)
            };

            var items = await clients
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await clients.CountAsync();

            var clientDto = items.Select(i => _mapper.Map<ClientDto>(i)).ToList();

            var result = new PaginatedList<ClientDto>(clientDto,
                count,
                pageNumber,
                pageSize);

            return result;
        }

        public async Task<IEnumerable<ClientDto>> GetAll()
        {
            var result = await _unitOfWork.Clients.GetAll().ToListAsync();
            return result.Select(i => _mapper.Map<ClientDto>(i));
        }

        public async Task<ClientDto> GetById(Guid id)
        {
            var result = await _unitOfWork.Clients
                .FindBy(c => c.Id.Equals(id))
                .FirstOrDefaultAsync();
            return _mapper.Map<ClientDto>(result);
        }

        public async Task<ClientDto> GetByName(string name)
        {
            var result = await _unitOfWork.Clients
                .FindBy(c => c.Name.Equals(name))
                .FirstOrDefaultAsync();
            return _mapper.Map<ClientDto>(result);
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
            await _unitOfWork.Clients.Remove(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
