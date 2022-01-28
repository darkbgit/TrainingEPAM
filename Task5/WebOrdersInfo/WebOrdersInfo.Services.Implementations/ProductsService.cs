using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebOrdersInfo.Core.DTOs;
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
