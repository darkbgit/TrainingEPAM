using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebOrdersInfo.Core.DTOs.Filters;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.Helpers;
using WebOrdersInfo.Models.ViewModels.Orders;


namespace WebOrdersInfo.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
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
        public async Task<IActionResult> Index([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageSize, filter.PageNumber);

            var (ordersDtoPerPage, allOrders) = await _orderService.GetOrdersPerPage(validFilter.PageNumber, validFilter.PageSize, "");

            var i = validFilter.PageSize * (validFilter.PageNumber - 1) + 1;

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
                Pagination = new Paging(validFilter.PageSize, validFilter.PageNumber, allOrders, url)
            };

            return View(ordersListWithPagination);
        }


        [HttpPost]
        public async Task<IActionResult> Index(string filter, int page = 1)
        {
            await _orderService.GetOrdersPerPage(page, 10, "");
            return View();
        }
    }
}
