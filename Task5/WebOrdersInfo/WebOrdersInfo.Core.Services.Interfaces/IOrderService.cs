using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IOrderService
    {
        void GetOrderById();
        void GetAll();
        Task<Tuple<IEnumerable<OrderWithNamesDto>, int>> GetOrdersPerPage(int pageNumber,
            int newsPerPage,
            string sortOrder);

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
