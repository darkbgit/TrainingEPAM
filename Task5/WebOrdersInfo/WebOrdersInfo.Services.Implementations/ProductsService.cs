using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.Core.DTOs.Models.Pagination;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.DAL.Repositories.Implementations;
using WebOrdersInfo.Repositories.Interfaces;
using WebOrdersInfo.Services.Implementations.Base;

namespace WebOrdersInfo.Services.Implementations
{
    public class ProductsService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductDto>> GetProductsPerPage(string sortOrder,
            string searchString,
            int pageNumber)
        {
            const int PAGE_SIZE = 5;

            var products = _unitOfWork.Products.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }

            products = sortOrder switch
            {
                "name_desc" => products.OrderByDescending(c => c.Name),
                _ => products.OrderBy(c => c.Name)
            };

            var items = await products
                .Skip((pageNumber - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var count = await products.CountAsync();

            var productDto = items.Select(i => _mapper.Map<ProductDto>(i))
                .ToList();

            var result = new PaginatedList<ProductDto>(productDto,
                count,
                pageNumber,
                PAGE_SIZE);

            return result;
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            var result = await _unitOfWork.Products
                .FindBy(c => c.Id.Equals(id))
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> GetByName(string name)
        {
            var result = await _unitOfWork.Products
                .FindBy(c => c.Name.Equals(name))
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(result);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var result = await _unitOfWork.Products.GetAll().ToListAsync();
            return result.Select(i => _mapper.Map<ProductDto>(i));
        }

        public async Task Add(ProductDto product)
        {
            var entity = _mapper.Map<Product>(product);
            await _unitOfWork.Products.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(ProductDto product)
        {
            var entity = _mapper.Map<Product>(product);
            _unitOfWork.Products.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _unitOfWork.Products.Remove(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
