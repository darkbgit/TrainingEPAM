using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Filters;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IOrderService
    {
        void GetOrderById();
        void GetAll();
        Task<Tuple<IEnumerable<OrderWithNamesDto>, int>> GetOrdersPerPage(int pageNumber,
            int newsPerPage,
            Expression<Func<Order, bool>> filter,
            OrderSortEnum sort);

        void GetOrderWithNamesById();

        void GetOrdersByClientId();
        void GetOrdersByManagerId();
        void GetOrdersByProductId();

        Task Add(OrderDto order);
        Task Update(OrderDto order);
        Task Delete(Guid id);
        Task DeleteRange(IEnumerable<OrderDto> orders);

        
    }
}
