using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IFilterService
    {
        Expression<Func<Order, bool>> Query(OrdersFilter filter);

        Task<OrdersFilter> GetFilter();
    }
}