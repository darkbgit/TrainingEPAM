using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebOrdersInfo.Core.DTOs.Models.Filters;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Extensions;
using WebOrdersInfo.Models.ViewModels.Orders;
using WebOrdersInfo.Models.ViewModels.OrdersFilter;

namespace WebOrdersInfo.Controllers
{
    [Authorize]
    public class OrdersFilterController : Controller
    {
        private readonly IFilterService _filterService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersFilterController> _logger;

        public OrdersFilterController(IMapper mapper,
            IFilterService filterService, ILogger<OrdersFilterController> logger)
        {
            _mapper = mapper;
            _filterService = filterService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            OrdersFilter filter;
            try
            {
                filter = await _filterService.GetFilter();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
            
            var filterViewModel = _mapper.Map<OrdersFilterViewModel>(filter);

            HttpContext.Session.SetData("ordersFilters", filter);

            return PartialView("_Filters", filterViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(OrdersFilterViewModel model)
        {
            OrdersFilter filter;
            if (model.IsClear)
            {
                try
                {
                    filter = await _filterService.GetFilter();
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return BadRequest();
                }
                model = _mapper.Map<OrdersFilterViewModel>(filter);
                HttpContext.Session.SetData("ordersFilters", filter);
            }
            else if (ModelState.IsValid && Validate(model))
            {
                filter = _mapper.Map<OrdersFilter>(model);
                model = _mapper.Map<OrdersFilterViewModel>(filter);
                ModelState.Clear();
                HttpContext.Session.SetData("ordersFilters", filter);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
            
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
