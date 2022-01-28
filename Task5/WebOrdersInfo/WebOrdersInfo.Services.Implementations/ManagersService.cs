using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Statistics;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.DAL.Repositories.Implementations;
using WebOrdersInfo.Repositories.Interfaces;
using WebOrdersInfo.Services.Implementations.Base;

namespace WebOrdersInfo.Services.Implementations
{
    public class ManagerService : IManagerService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ManagerDto>> GetAll()
        {
            var result = await _unitOfWork.Managers.GetAll().ToListAsync();
            return result.Select(i => _mapper.Map<ManagerDto>(i));
        }

        public async Task<IEnumerable<ManagerWithCountOrdersDto>> GetEntityWithOrdersCount(int take = 10, bool fromTop = true)
        {
            var entities = _unitOfWork.Managers.GetAll()
                .Select(m => new ManagerWithCountOrdersDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Count = m.Orders.Count
                });

            IOrderedQueryable<ManagerWithCountOrdersDto> orderedResult;
            if (fromTop)
            {
                orderedResult = entities
                    .OrderByDescending(r => r.Count);
            }
            else
            {
                orderedResult = entities
                    .OrderBy(r => r.Count);
            }

            var result = await orderedResult
                .Take(take)
                .ToListAsync();
            return result;
        }

        public async Task Add(ManagerDto manager)
        {
            var entity = _mapper.Map<Manager>(manager);
            await _unitOfWork.Managers.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(ManagerDto manager)
        {
            var entity = _mapper.Map<Manager>(manager);
            _unitOfWork.Managers.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _unitOfWork.Managers.Remove(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ManagerWithCountOrdersAndTotalPriceDto>> GetEntityWithOrdersCountAndPrice(int take = 10, bool fromTop = true)
        {
            var entities = _unitOfWork.Managers.GetAll()
                .Select(m => new ManagerWithCountOrdersAndTotalPriceDto()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Count = m.Orders.Count,
                    TotalPrice = m.Orders.Sum(o => o.Price)
                });

            IOrderedQueryable<ManagerWithCountOrdersAndTotalPriceDto> orderedResult;
            if (fromTop)
            {
                orderedResult = entities
                    .OrderByDescending(r => r.Count);
            }
            else
            {
                orderedResult = entities
                    .OrderBy(r => r.Count);
            }

            var result = await orderedResult
                .Take(take)
                .ToListAsync();
            return result;
        }

    }
}
