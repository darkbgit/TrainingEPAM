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
        private readonly IOrderService _orderService;

        public FilterService(IClientService clientService,
            IManagerService managerService,
            IProductService productService,
            IOrderService orderService)
        {
            _clientService = clientService;
            _managerService = managerService;
            _productService = productService;
            _orderService = orderService;
        }

        public Expression<Func<Order, bool>> Query(OrdersFilter filter)
        {
            if (filter == null)
            {
                return null;
            }
            Expression<Func<Order, bool>> clientExpression = filter.Clients
                .Where(client => client.IsChecked)
                .Aggregate<ClientForFilter, Expression<Func<Order, bool>>>(null, (current, client) =>
                    current.OrElse(o => o.ClientId.Equals(client.Id)));

            var expression = ((Expression<Func<Order, bool>>)null).AndAlso(clientExpression);

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
            var clientsDto = (await _clientService.GetAll()).OrderBy(c => c.Name);

            var clients = clientsDto.Select(c => new ClientForFilter
            {
                Id = c.Id,
                Name = c.Name,
                IsChecked = false

            }).ToList();

            var managersDto = (await _managerService.GetAll()).OrderBy(n => n.Name);

            var managers = managersDto.Select(m => new ManagerForFilter
            {
                Id = m.Id,
                Name = m.Name,
                IsChecked = false
            }).ToList();

            var productsDto = (await _productService.GetAll()).OrderBy(d => d.Name);

            var products = productsDto.Select(p => new ProductForFilter
            {
                Id = p.Id,
                Name = p.Name,
                IsChecked = false
            }).ToList();

            var min = await _orderService.GetMinPrice();
            var max = await _orderService.GetMaxPrice();


            var filters = new OrdersFilter
            {
                Clients = clients,
                Managers = managers,
                Products = products,
                IsNeedClear = false,
                OrderBy = OrderSortEnum.Date,
                MinPrice = min,
                MaxPrice = max,
                PriceFrom = min,
                PriceTo = max
            };

            return filters;
        }
    }
}