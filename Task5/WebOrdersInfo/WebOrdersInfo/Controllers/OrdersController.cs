using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebOrdersInfo.Core.DTOs;
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

        private readonly ILogger<OrdersController> _logger;

        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IClientService clientService, IManagerService managerService, IProductService productService, IFilterService filterService, IMapper mapper, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _clientService = clientService;
            _managerService = managerService;
            _productService = productService;
            _filterService = filterService;
            _mapper = mapper;
            _logger = logger;
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

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            var model = new CreateOrderViewModel
            {
                Clients = new SelectList(
                    (await _clientService.GetAll())
                    .OrderBy(e => e.Name),
                    "Id", "Name"),
                Managers = new SelectList(
                    (await _managerService.GetAll())
                    .OrderBy(e => e.Name),
                    "Id", "Name"),
                Products = new SelectList(
                    (await _productService.GetAll())
                    .OrderBy(e => e.Name),
                    "Id", "Name")
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateOrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var orderDto = _mapper.Map<OrderDto>(order);
                orderDto.Id = Guid.NewGuid();


                try
                {
                    await _orderService.Add(orderDto);
                }
                catch (Exception e)
                {
                    _logger.LogError("Order can\'t be added");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderById((Guid)id);

            if (order == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditOrderViewModel>(order);

            model.Clients = new SelectList(
                (await _clientService.GetAll())
                .OrderBy(e => e.Name),
                "Id", "Name");
            model.Managers = new SelectList(
                (await _managerService.GetAll())
                .OrderBy(e => e.Name),
                "Id", "Name");
            model.Products = new SelectList(
                (await _productService.GetAll())
                .OrderBy(e => e.Name),
                "Id", "Name");

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditOrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var orderDto = _mapper.Map<OrderDto>(order);
                try
                {
                    await _orderService.Update(orderDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogError("Order can\'t be updated");
                }
                return RedirectToAction(nameof(Index));
            }
            
            //ViewData["RssSourceName"] = new SelectList(await _rssSourceService.GetAllRssSources(), "Id", "Name", news.RssSourceId);
            return View(order);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderService.Delete(id);
            return RedirectToAction(nameof(Index));
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

            var beginNumber = paginationFilter.PageSize * (paginationFilter.PageNumber - 1) + 1;

            var ordersViewModel = ordersDtoPerPage.Select(o => new OrderViewModel
            {
                Id = o.Id,
                SequenceNumber = i++,
                Date = o.Date,
                ProductName = o.ProductName,
                Price = o.Price,
                ClientName = o.ClientName,
                ManagerName = o.ManagerName
            });

            var ordersListWithPagination = new OrdersListWithPaginationViewModel
            {
                //BeginOrdersNumber = beginNumber,
                OrderList = ordersViewModel,
                //Pagination = new Paging(paginationFilter.PageSize, paginationFilter.PageNumber, allOrders, url),
                Pagination = new PageInfo(paginationFilter.PageNumber, allOrders, paginationFilter.PageSize)
            };

            return ordersListWithPagination;
        }

    }
}
