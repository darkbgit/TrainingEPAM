using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Repositories.Interfaces;

namespace WebOrdersInfo.Services.Implementations
{
    public class OrdersService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersService> _logger;

        public OrdersService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrdersService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderDto> GetOrderById(Guid id)
        {
            var entity = await _unitOfWork.Orders.Get(id);
            return _mapper.Map<OrderDto>(entity);
        }

        public async Task<Tuple<IEnumerable<OrderWithNamesDto>, int>> GetOrdersPerPage(int pageNumber,
            int newsPerPage,
            Expression<Func<Order, bool>> filter,
            OrderSortEnum sort)
        {
            filter ??= order => true;

            Expression<Func<Order, object>> sortExpression;
            switch (sort)
            {
                case OrderSortEnum.Date:
                default:
                    sortExpression = o => o.Date;
                    break;
                case OrderSortEnum.Product:
                    sortExpression = o => o.Product.Name;
                    break;
                case OrderSortEnum.Price:
                    sortExpression = o => o.Price;
                    break;
                case OrderSortEnum.Client:
                    sortExpression = o => o.Client.Name;
                    break;
                case OrderSortEnum.Manager:
                    sortExpression = o => o.Manager.Name;
                    break;
            }

            var orders = await _unitOfWork.Orders.FindBy(filter,
                o => o.Client,
                o => o.Manager,
                o => o.Product)
                .OrderBy(sortExpression)
                .Skip((pageNumber - 1) * newsPerPage)
                .Take(newsPerPage)
                .ToListAsync();

            var count = await _unitOfWork.Orders.FindBy(filter).CountAsync();

            var result = orders.Select(o => _mapper.Map<OrderWithNamesDto>(o)).ToList();

            return new Tuple<IEnumerable<OrderWithNamesDto>, int>(result, count);
        }

        public async Task<double> GetMinPrice()
        {
            double result = 0;
            try
            {
                result = await _unitOfWork.Orders.GetAll().MinAsync(o => o.Price);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message);
            }
            return result;
        }

        public async Task<double> GetMaxPrice()
        {
            return await _unitOfWork.Orders.GetAll().MaxAsync(o => o.Price);
        }

        public async Task Add(OrderDto order)
        {
            var entity = _mapper.Map<Order>(order);
            await _unitOfWork.Orders.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(OrderDto order)
        {
            var entity = _mapper.Map<Order>(order);
            _unitOfWork.Orders.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _unitOfWork.Orders.Remove(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<OrderDto> orders)
        {
            var entities = orders.Select(ent => _mapper.Map<Order>(ent))
                .ToList();

            _unitOfWork.Orders.RemoveRange(entities);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
