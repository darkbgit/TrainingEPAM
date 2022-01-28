using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Statistics;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.DAL.Repositories.Implementations;
using WebOrdersInfo.Repositories.Interfaces;

namespace WebOrdersInfo.Services.Implementations.Base
{
    //public abstract class EntityWithNameService<T> : IEntityWithNameService<T> where T : class, IEntityWithName
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    protected EntityWithNameService(IMapper mapper, IUnitOfWork unitOfWork)
    //    {
    //        _mapper = mapper;
    //        _unitOfWork = unitOfWork;
    //    }

    //    public async Task<IEnumerable<NameDto>> GetAll()
    //    {
    //        var result = await _repository.GetAll().ToListAsync();
    //        return result.Select(i => _mapper.Map<NameDto>(i));
    //    }

    //    public async Task<IEnumerable<ManagerWithCountOrdersDto>> GetEntityWithOrdersCount(int take = 10, bool fromTop = true)
    //    {
    //        var entities = _repository.GetAll()
    //            .Select(m => new ManagerWithCountOrdersDto
    //            {
    //                Id = m.Id,
    //                Name = m.Name,
    //                Count = m.Orders.Count
    //            });

    //        IOrderedQueryable<ManagerWithCountOrdersDto> orderedResult;
    //        if (fromTop)
    //        {
    //            orderedResult = entities
    //                .OrderByDescending(r => r.Count);
    //        }
    //        else
    //        {
    //            orderedResult = entities
    //                .OrderBy(r => r.Count);
    //        }

    //        var result = await orderedResult
    //            .Take(take)
    //            .ToListAsync();
    //        return result;
    //    }

    //    public async Task<IEnumerable<ManagerWithCountOrdersDto>> GetEntityWithOrdersCountAndPrice(int take = 10, bool fromTop = true)
    //    {
    //        var entities = _repository.GetAll()
    //            .Select(m => new ManagerWithCountOrdersDto
    //            {
    //                Id = m.Id,
    //                Name = m.Name,
    //                Count = m.Orders.Count
    //            });

    //        IOrderedQueryable<ManagerWithCountOrdersDto> orderedResult;
    //        if (fromTop)
    //        {
    //            orderedResult = entities
    //                .OrderByDescending(r => r.Count);
    //        }
    //        else
    //        {
    //            orderedResult = entities
    //                .OrderBy(r => r.Count);
    //        }

    //        var result = await orderedResult
    //            .Take(take)
    //            .ToListAsync();
    //        return result;
    //    }
    //}
}
