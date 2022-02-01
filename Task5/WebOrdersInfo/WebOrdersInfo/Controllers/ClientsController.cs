using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.Models.ViewModels.Clients;

namespace WebOrdersInfo.Controllers
{
    [Authorize(Roles = "admin")]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientService clientService, IMapper mapper, ILogger<ClientsController> logger)
        {
            _clientService = clientService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(
            string sortOrder,
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

            var clients = await _clientService.GetClientsPerPage(sortOrder, searchString, pageNumber.Value);

            return View(clients);
        }

        public IActionResult Create() => View();
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ClientDto>(model);
                dto.Id = Guid.NewGuid();

                if (!await ClientNameExists(dto.Name))
                {
                    try
                    {
                        await _clientService.Add(dto);
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
                    ModelState.AddModelError("Name", "Клиент с такой фамилией уже существует");
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

            var client = await _clientService.GetById(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ClientViewModel>(client);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ClientDto>(model);

                if (!await ClientNameExists(dto.Name))
                {
                    try
                    {
                        await _clientService.Update(dto);
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
                    ModelState.AddModelError("Name", "Клиент с такой фамилией уже существует");
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

            var client = await _clientService.GetById(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again.";
            }

            var model = _mapper.Map<ClientViewModel>(client);

            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = await _clientService.GetById(id);

            if (client == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _clientService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private async Task<bool> ClientNameExists(string name)
        {
            return await _clientService.GetByName(name) != null;
        }
    }
}

