using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.Models.ViewModels.Products;

namespace WebOrdersInfo.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productService = productService;
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

            var products = await _productService.GetProductsPerPage(sortOrder, searchString, pageNumber.Value);

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ProductDto>(model);
                dto.Id = Guid.NewGuid();

                if (!await ProductNameExists(dto.Name))
                {
                    try
                    {
                        await _productService.Add(dto);
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
                    ModelState.AddModelError("Name", "Продукт с таким названием уже существует");
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

            var product = await _productService.GetById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ProductViewModel>(product);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ProductDto>(model);

                if (!await ProductNameExists(dto.Name))
                {
                    try
                    {
                        await _productService.Update(dto);
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
                    ModelState.AddModelError("Name", "Продукт с таким названием уже существует");
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

            var product = await _productService.GetById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again.";
            }

            var model = _mapper.Map<ProductViewModel>(product);

            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _productService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        private async Task<bool> ProductNameExists(string name)
        {
            return await _productService.GetByName(name) != null;
        }
    }
}
