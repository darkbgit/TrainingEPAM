using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.Extensions;
using WebOrdersInfo.Models.ViewModels.Orders;
using WebOrdersInfo.Models.ViewModels.OrdersFilter;

namespace WebOrdersInfo.Controllers
{
    [Authorize]
    public class OrdersFilterController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IManagerService _managerService;
        private readonly IProductService _productService;
        private readonly IFilterService _filterService;

        private readonly IMapper _mapper;

        public OrdersFilterController(IClientService clientService, IManagerService managerService, IProductService productService, IMapper mapper, IFilterService filterService)
        {
            _clientService = clientService;
            _managerService = managerService;
            _productService = productService;
            _mapper = mapper;
            _filterService = filterService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var filters = await _filterService.GetFilter();

            var filterViewModel = _mapper.Map<OrdersFilterViewModel>(filters);

            HttpContext.Session.SetData("ordersFilters", filters);

            return PartialView("_Filters", filterViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(OrdersFilterViewModel model)
        {
            if (model.IsClear)
            {
                var filters = await _filterService.GetFilter();
                var filterViewModel = _mapper.Map<OrdersFilterViewModel>(filters);
                HttpContext.Session.SetData("ordersFilters", filters);
                return Ok();
            }
            if (ModelState.IsValid && Validate(model))
            {
                var filters = _mapper.Map<OrdersFilter>(model);
                HttpContext.Session.SetData("ordersFilters", filters);
                return Ok();
            }

            HttpContext.Response.StatusCode = 400;
            return PartialView("_Filters", model);
        }

        private bool Validate(OrdersFilterViewModel model)
        {
            if (model.PriceFrom > model.PriceTo)
            {
                ModelState.AddModelError("PriceTo", $"Конечная цена {model.PriceTo} должна быть больше начальной {model.PriceFrom}");
                return false;
            }
            if (model.DateFrom > model.DateTo)
            {
                ModelState.AddModelError("DateTo", "Дата окончания выборки должна быть больше начальной");
                return false;
            }
            return true;
        }
    }
}
