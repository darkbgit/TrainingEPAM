﻿using System;
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


        public async Task<IEnumerable<NameDto>> GetAll()
        {
            var result = await _unitOfWork.Products.GetAll().ToListAsync();
            return result.Select(i => _mapper.Map<NameDto>(i));
        }
   
    }
}
