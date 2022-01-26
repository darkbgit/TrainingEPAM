using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Services.Implementations.Extensions;

namespace WebOrdersInfo.Services.Implementations
{
    public class FilterService : IFilterService
    {
        private readonly IClientService _clientService;
        private readonly IManagerService _managerService;
        private readonly IProductService _productService;

        public FilterService(IClientService clientService, IManagerService managerService, IProductService productService)
        {
            _clientService = clientService;
            _managerService = managerService;
            _productService = productService;
        }

        public Expression<Func<Order, bool>> Query(OrdersFilter filter)
        {

            //Expression<Func<Order, bool>> expression = null;

            Expression<Func<Order, bool>> clientExpression = filter.Clients
                .Where(client => client.IsChecked)
                .Aggregate<ClientForFilter, Expression<Func<Order, bool>>>(null, (current, client) =>
                    current.OrElse(o => o.ClientId.Equals(client.Id)));

            var expression = ((Expression<Func<Order, bool>>) null).AndAlso(clientExpression);

            Expression<Func<Order, bool>> productExpression = filter.Products
                .Where(product => product.IsChecked)
                .Aggregate<ProductForFilter, Expression<Func<Order, bool>>>(null, (current, product) =>
                    current.OrElse(o => o.ProductId.Equals(product.Id)));

            expression = expression.AndAlso(productExpression);

            Expression<Func<Order, bool>> managerExpression = filter.Managers
                .Where(manager => manager.IsChecked)
                .Aggregate<ManagerForFilter, Expression<Func<Order, bool>>>(null, (current, manager) =>
                    current.OrElse(o => o.ManagerId.Equals(manager.Id)));

            expression = expression.AndAlso(managerExpression);

            if (filter.PriceFrom is > 0)
            {
                expression = expression.AndAlso(o => o.Price >= filter.PriceFrom);
            }
            if (filter.PriceTo is > 0)
            {
                expression = expression.AndAlso(o => o.Price <= filter.PriceTo);
            }

            if (filter.DateFrom.HasValue)
            {
                expression = expression.AndAlso(o => o.Date >= filter.DateFrom);
            }
            if (filter.DateTo.HasValue)
            {
                expression = expression.AndAlso(o => o.Date <= filter.DateTo);
            }

            return expression;
        }

        public async Task<OrdersFilter> GetFilter()
        {
            var clients = (await _clientService.GetAll()).OrderBy(c => c.Name);

            var c = clients.Select(c => new ClientForFilter
            {
                Id = c.Id,
                Name = c.Name,
                IsChecked = false

            }).ToList();

            var managers = (await _managerService.GetAll()).OrderBy(c => c.Name);

            var m = managers.Select(m => new ManagerForFilter
            {
                Id = m.Id,
                Name = m.Name,
                IsChecked = false
            }).ToList();

            var products = (await _productService.GetAll()).OrderBy(c => c.Name);

            var p = products.Select(p => new ProductForFilter
            {
                Id = p.Id,
                Name = p.Name,
                IsChecked = false
            }).ToList();

            var filters = new OrdersFilter
            {
                Clients = c,
                Managers = m,
                Products = p,
                IsClear = false,
                OrderBy = OrderSortEnum.Date
            };

            return filters;
        }
    }
}