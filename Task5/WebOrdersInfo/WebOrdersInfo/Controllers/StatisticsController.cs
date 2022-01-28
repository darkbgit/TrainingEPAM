using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.Models.ViewModels.Statistics;

namespace WebOrdersInfo.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IManagerService _managerService;

        public StatisticsController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<JsonResult> TopManagersByOrders(bool fromTop, int take)
        {
            var result = await _managerService.GetEntityWithOrdersCount(take, fromTop);

            var r1 = Json(result.Select(r => new { r.Name, r.Count }));

            return new JsonResult(r1);
        }
    }
}
