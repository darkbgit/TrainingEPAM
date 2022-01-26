using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.DAL.Repositories.Implementations;

namespace WebOrdersInfo.Services.Implementations
{
    public class OrdersService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void GetOrderById()
        {
            throw new NotImplementedException();
        }

        public void GetAll()
        {
            //_unitOfWork.Orders.GetAll().CountAsync()
        }



        public async Task<Tuple<IEnumerable<OrderWithNamesDto>, int>> GetOrdersPerPage(int pageNumber,
            int newsPerPage,
            Expression<Func<Order, bool>> filter,
            OrderSortEnum sort)
        {
            if (filter == null)
            {
                filter = order => true;
            }

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

            //var result = orders.Select(o => _mapper.Map<OrderDto>(o)).ToList();

            var result = orders.Select(o => new OrderWithNamesDto()
            {
                Id = o.Id,
                Date = o.Date,
                ProductName = o.Product.Name,
                Price = o.Price,
                ClientName = o.Client.Name,
                ManagerName = o.Manager.Name
            }).ToList();

            return new Tuple<IEnumerable<OrderWithNamesDto>, int>(result, count);
        }

        public void GetOrderWithNamesById()
        {
            throw new NotImplementedException();
        }

        public void GetOrdersByClientId()
        {
            throw new NotImplementedException();
        }

        public void GetOrdersByManagerId()
        {
            throw new NotImplementedException();
        }

        public void GetOrdersByProductId()
        {
            throw new NotImplementedException();
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
