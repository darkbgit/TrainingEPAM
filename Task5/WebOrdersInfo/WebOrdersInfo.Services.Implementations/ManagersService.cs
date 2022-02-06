using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Pagination;
using WebOrdersInfo.Core.DTOs.Models.Statistics;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Repositories.Interfaces;

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

        public async Task<PaginatedList<ManagerDto>> GetManagersPerPage(string sortOrder,
            string searchString,
            int pageNumber,
            int pageSize)
        {
            var managers = _unitOfWork.Managers.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                managers = managers.Where(m => m.Name.Contains(searchString));
            }

            managers = sortOrder switch
            {
                "name_desc" => managers.OrderByDescending(m => m.Name),
                _ => managers.OrderBy(m => m.Name)
            };

            var items = await managers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await managers.CountAsync();

            var managerDto = items.Select(i => _mapper.Map<ManagerDto>(i)).ToList();

            var result = new PaginatedList<ManagerDto>(managerDto,
                count,
                pageNumber,
                pageSize);

            return result;
        }

        public async Task<IEnumerable<ManagerDto>> GetAll()
        {
            var result = await _unitOfWork.Managers.GetAll().ToListAsync();
            return result.Select(i => _mapper.Map<ManagerDto>(i));
        }



        public async Task<ManagerDto> GetById(Guid id)
        {
            var result = await _unitOfWork.Managers
                .FindBy(m => m.Id.Equals(id))
                .FirstOrDefaultAsync();
            return _mapper.Map<ManagerDto>(result);
        }

        public async Task<ManagerDto> GetByName(string name)
        {
            var result = await _unitOfWork.Managers
                .FindBy(m => m.Name.Equals(name))
                .FirstOrDefaultAsync();
            return _mapper.Map<ManagerDto>(result);
        }

        public async Task<IEnumerable<ManagerNameWithGroupingPropertyDto>> GetManagersWithTotalPrice(int take, bool fromTop,
            DateTime fromDateTime, DateTime toDateTime)
        {
            var entities = _unitOfWork.Orders
                .FindBy(o => o.Date >= fromDateTime && o.Date <= toDateTime,
                    o => o.Manager)
                .GroupBy(o => o.Manager.Name)
                .Select(g => new ManagerNameWithGroupingPropertyDto
                {
                    Name = g.Key,
                    GroupingProperty = Convert.ToInt32(g.Sum(o => o.Price))
                });

            IOrderedQueryable<ManagerNameWithGroupingPropertyDto> ent;
            if (fromTop)
            {
                ent = entities
                    .OrderByDescending(r => r.GroupingProperty);
            }
            else
            {
                ent = entities
                    .OrderBy(r => r.GroupingProperty);
            }

            var result = await ent
                .ThenBy(m => m.Name)
                .Take(take)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ManagerNameWithGroupingPropertyDto>> GetManagersWithOrdersCount(int take, bool fromTop,
            DateTime fromDateTime, DateTime toDateTime)
        {
            var entities = _unitOfWork.Orders
                .FindBy(o => o.Date >= fromDateTime && o.Date <= toDateTime,
                    o => o.Manager)
                .GroupBy(o => o.Manager.Name)
                .Select(g => new ManagerNameWithGroupingPropertyDto()
                {
                    Name = g.Key,
                    GroupingProperty = g.Count()
                });

            IOrderedQueryable<ManagerNameWithGroupingPropertyDto> ent;
            if (fromTop)
            {
                ent = entities
                    .OrderByDescending(r => r.GroupingProperty);
            }
            else
            {
                ent = entities
                    .OrderBy(r => r.GroupingProperty);
            }

            var result = await ent
                .ThenBy(m => m.Name)
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



    }
}
