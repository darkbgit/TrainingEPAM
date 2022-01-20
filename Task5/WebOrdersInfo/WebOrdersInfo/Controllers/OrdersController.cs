using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebOrdersInfo.Core.DTOs.Filters;
using WebOrdersInfo.Core.DTOs.Pagination;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.Extensions;
using WebOrdersInfo.Models;
using WebOrdersInfo.Models.ViewModels.Orders;
using WebOrdersInfo.Models.ViewModels.OrdersFilter;
using WebOrdersInfo.Pagination;
using WebOrdersInfo.Pagination2;


namespace WebOrdersInfo.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;
        private readonly IManagerService _managerService;
        private readonly IProductService _productService;
        private readonly IFilterService _filterService;

        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IClientService clientService, IManagerService managerService, IProductService productService, IFilterService filterService, IMapper mapper)
        {
            _orderService = orderService;
            _clientService = clientService;
            _managerService = managerService;
            _productService = productService;
            _filterService = filterService;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index(int PageNumber = 1)
        //{
        //    var (ordersDtoPerPage, allOrders) = await _orderService.GetOrdersPerPage(PageNumber, Utilities.Constants.OrdersPerPage, "");

        //    var i = Utilities.Constants.OrdersPerPage * (PageNumber - 1) + 1;

        //    var ordersViewModel = ordersDtoPerPage.Select(o => new OrderViewModel
        //    {
        //        Id = i++,
        //        Date = o.Date,
        //        ProductName = o.ProductName,
        //        Price = o.Price,
        //        ClientName = o.ClientName,
        //        ManagerName = o.ManagerName
        //    });

        //    var url = Request.GetEncodedUrl();

        //    var ordersListWithPagination = new OrdersListWithPaginationViewModel
        //    {
        //        OrderList = ordersViewModel,
        //        Pagination = new Paging(Utilities.Constants.OrdersPerPage, PageNumber, allOrders, url)
        //    };

        //    return View(ordersListWithPagination);
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var paginationFilter = new PaginationFilter();
            var filter = HttpContext.Session.GetData<OrdersFilter>("ordersFilters");

            if (filter is not {IsClear: false})
            {
                filter = await _filterService.GetFilter();
                HttpContext.Session.SetData("ordersFilters", filter);
            }

            var query = _filterService.Query(filter);

            var (ordersDtoPerPage, allOrders) = await _orderService.GetOrdersPerPage(paginationFilter.PageNumber, paginationFilter.PageSize, query, filter.OrderBy);

            var i = paginationFilter.PageSize * (paginationFilter.PageNumber - 1) + 1;

            var ordersViewModel = ordersDtoPerPage.Select(o => new OrderViewModel
            {
                Id = i++,
                Date = o.Date,
                ProductName = o.ProductName,
                Price = o.Price,
                ClientName = o.ClientName,
                ManagerName = o.ManagerName
            });


            var url = Request.GetEncodedUrl();

            var ordersFilterViewModel = _mapper.Map<OrdersFilterViewModel>(filter);

            var ordersListWithPagination = new OrdersListWithPaginationViewModel
            {
                OrderList = ordersViewModel,
                //Pagination = new Paging(paginationFilter.PageSize, paginationFilter.PageNumber, allOrders, url),
                //Filters = ordersFilterViewModel
                Pagination = new PageInfo(paginationFilter.PageNumber, allOrders, paginationFilter.PageSize)
            };

            return View(ordersListWithPagination);
        }


        [HttpPost]
        public async Task<IActionResult> Index(PaginationFilter paginationFilter)
        {

            var filter = HttpContext.Session.GetData<OrdersFilter>("ordersFilters");

            var query = _filterService.Query(filter);

            var (ordersDtoPerPage, allOrders) = await _orderService.GetOrdersPerPage(paginationFilter.PageNumber, paginationFilter.PageSize, query, filter.OrderBy);

            var i = paginationFilter.PageSize * (paginationFilter.PageNumber - 1) + 1;

            var ordersViewModel = ordersDtoPerPage.Select(o => new OrderViewModel
            {
                Id = i++,
                Date = o.Date,
                ProductName = o.ProductName,
                Price = o.Price,
                ClientName = o.ClientName,
                ManagerName = o.ManagerName
            });

            var url = Request.GetEncodedUrl();

            var ordersListWithPagination = new OrdersListWithPaginationViewModel
            {
                OrderList = ordersViewModel,
                //Pagination = new Paging(paginationFilter.PageSize, paginationFilter.PageNumber, allOrders, url),
                Pagination = new PageInfo(paginationFilter.PageNumber, allOrders, paginationFilter.PageSize)
            };

            return PartialView("_OrdersListWithPagination", ordersListWithPagination);
        }


        private async Task<OrdersFilter> GetFilterModel()
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
                IsClear = false
            };

            HttpContext.Session.SetData("ordersFilters", filters);

            return filters;
        }
    }
}
