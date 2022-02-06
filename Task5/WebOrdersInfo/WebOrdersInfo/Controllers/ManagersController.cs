using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.Models.ViewModels.Managers;

namespace WebOrdersInfo.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManagersController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public ManagersController(IManagerService managerService,
            IMapper mapper,
            ILogger<OrdersController> logger)
        {
            _managerService = managerService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null || !pageNumber.HasValue)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var managers = await _managerService.GetManagersPerPage(sortOrder, searchString, pageNumber.Value, Utilities.Constants.MANAGERS_PER_PAGE);
            
            return View(managers);
        }


        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ManagerDto>(model);
                dto.Id = Guid.NewGuid();

                if (!await ManagerNameExists(dto.Name))
                {
                    try
                    {
                        await _managerService.Add(dto);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException e)
                    {
                        _logger.LogError(e.Message);
                        ModelState.AddModelError("", "Unable to save changes. Try again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Менеджер с такой фамилией уже существует");
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _managerService.GetById((Guid)id);

            if (manager == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ManagerViewModel>(manager);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ManagerDto>(model);

                if (!await ManagerNameExists(dto.Name))
                {
                    try
                    {
                        await _managerService.Update(dto);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException e)
                    {
                        _logger.LogError(e.Message);
                        ModelState.AddModelError("", "Unable to save changes. Try again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Менеджер с такой фамилией уже существует");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _managerService.GetById(id.Value);

            if (manager == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again.";
            }

            var model = _mapper.Map<ManagerViewModel>(manager);

            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var manager = await _managerService.GetById(id);

            if (manager == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _managerService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        private async Task<bool> ManagerNameExists(string name)
        {
            return await _managerService.GetByName(name) != null;
        }
    }
}
