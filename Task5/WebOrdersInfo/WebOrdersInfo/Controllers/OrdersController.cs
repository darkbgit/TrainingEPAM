using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.Core.DTOs.Models.Pagination;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.Extensions;
using WebOrdersInfo.Models;
using WebOrdersInfo.Models.ViewModels.Orders;
using WebOrdersInfo.Models.ViewModels.OrdersFilter;
using WebOrdersInfo.Pagination;
using WebOrdersInfo.Pagination2;


namespace WebOrdersInfo.Controllers
{
    [Authorize]
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var paginationFilter = new PaginationFilter();
            var filter = HttpContext.Session.GetData<OrdersFilter>("ordersFilters");

            if (filter is not {IsClear: false})
            {
                filter = await _filterService.GetFilter();
                HttpContext.Session.SetData("ordersFilters", filter);
            }

            var ordersListWithPagination = await GetOrdersList(paginationFilter);

            return View(ordersListWithPagination);
        }


        [HttpPost]
        public async Task<IActionResult> Index(PaginationFilter paginationFilter)
        {
            var filter = HttpContext.Session.GetData<OrdersFilter>("ordersFilters");

            if (filter is not { IsClear: false })
            {
                return BadRequest();
            }

            var ordersListWithPagination = await GetOrdersList(paginationFilter);

            return PartialView("_OrdersListWithPagination", ordersListWithPagination);
        }

        private async Task<OrdersListWithPaginationViewModel> GetOrdersList(PaginationFilter paginationFilter)
        {
            var filter = HttpContext.Session.GetData<OrdersFilter>("ordersFilters");

            if (filter is { IsClear: true })
            {
                filter = await _filterService.GetFilter();
                HttpContext.Session.SetData("ordersFilters", filter);
            }

            var query = _filterService.Query(filter);

            var (ordersDtoPerPage, allOrders) = await _orderService
                .GetOrdersPerPage(paginationFilter.PageNumber, paginationFilter.PageSize, query, filter.OrderBy);

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

            var ordersListWithPagination = new OrdersListWithPaginationViewModel
            {
                OrderList = ordersViewModel,
                //Pagination = new Paging(paginationFilter.PageSize, paginationFilter.PageNumber, allOrders, url),
                Pagination = new PageInfo(paginationFilter.PageNumber, allOrders, paginationFilter.PageSize)
            };

            return ordersListWithPagination;
        }

    }
}
