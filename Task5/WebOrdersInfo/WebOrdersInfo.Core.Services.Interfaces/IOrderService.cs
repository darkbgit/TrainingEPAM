using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderById(Guid id);

        Task<Tuple<IEnumerable<OrderWithNamesDto>, int>> GetOrdersPerPage(int pageNumber,
            int newsPerPage,
            Expression<Func<Order, bool>> filter,
            OrderSortEnum sort);

        Task<double> GetMinPrice();
        Task<double> GetMaxPrice();

        Task Add(OrderDto order);
        Task Update(OrderDto order);
        Task Delete(Guid id);
        Task DeleteRange(IEnumerable<OrderDto> orders);
    }
}
