using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs.Models.Statistics;
using WebOrdersInfo.Core.Services.Interfaces;

namespace WebOrdersInfo.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(IManagerService managerService, ILogger<StatisticsController> logger)
        {
            _managerService = managerService;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }


        public async Task<JsonResult> TopManagersByOrders(bool fromTop, int take, string period, string groupBy)
        {
            var (from, to) = GetPeriod(period);

            try
            {
                IEnumerable<ManagerNameWithGroupingPropertyDto> result;
                switch (groupBy)
                {
                    case "ordersCount":
                    default:
                        result = await _managerService.GetManagersWithOrdersCount(take, fromTop, from, to);
                        break;
                    case "ordersPriceSum":
                        result = await _managerService.GetManagersWithTotalPrice(take, fromTop, from, to);
                        break;
                }
                var jsonResult = Json(result.Select(r => new { r.Name, r.GroupingProperty }));

                return new JsonResult(jsonResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }

        private static (DateTime from, DateTime to) GetPeriod(string period)
        {
            DateTime from;
            DateTime to;
            DateTime dt = DateTime.Now.ToUniversalTime();
            switch (period)
            {
                case "thisMonth":
                default:
                    from = new DateTime(dt.Year, dt.Month, 1);
                    to = dt;
                    break;
                case "previousMonth":
                    from = new DateTime(dt.Year, dt.Month, 1).AddMonths(-1);
                    to = new DateTime(dt.Year, dt.Month, 1, 23, 59, 59).AddDays(-1);
                    break;
                case "thisYear":
                    from = new DateTime(dt.Year, 1, 1);
                    to = dt;
                    break;
                case "previousYear":
                    from = new DateTime(dt.Year - 1, 1, 1);
                    to = new DateTime(dt.Year - 1, 12, 31, 23, 59, 59);
                    break;
            }

            return (from, to);
        }
    }
}
