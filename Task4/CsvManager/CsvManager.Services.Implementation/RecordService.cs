using System;
using System.Globalization;
using System.Threading.Tasks;
using CsvManager.Core.DTOs;
using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CsvManager.Services.Implementation
{
    public class RecordService : IRecordService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Parse(string record)
        {
            var data = record.Split(';');

            if (data.Length != 4)
                throw new Exception();

            if (!double.TryParse(data[3], NumberStyles.Float, CultureInfo.InvariantCulture, out var price))
            {
                throw new Exception();
            }

            var order = new OrderDto
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Parse(data[0]),
                ClientId = await GetClientIdBySecondName(data[1]),
                ProductId = await GetProductIdByName(data[2]),
                Price = price
            };

            return order;
        }

        private async Task<Guid> GetClientIdBySecondName(string secondName)
        {
            if (string.IsNullOrWhiteSpace(secondName))
                throw new Exception();

            var id = (await _unitOfWork.Clients
                .FindBy(c => c.SecondName == secondName)
                .FirstOrDefaultAsync())
                ?.Id;

            if (id == null)
            {
                var client = new Client
                {
                    Id = Guid.NewGuid(),
                    SecondName = secondName
                };

                await _unitOfWork.Clients.Add(client);

                id = client.Id;
            }

            return id.Value;
        }

        private async Task<Guid> GetProductIdByName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new Exception();

            var id = (await _unitOfWork.Products
                .FindBy(c => c.Name == productName)
                .FirstOrDefaultAsync())
                ?.Id;

            if (id == null)
            {
                var product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = productName
                };

                await _unitOfWork.Products.Add(product);

                id = product.Id;
            }

            return id.Value;
        }
        
        public async Task<int> Add(OrderDto order)
        {
            var entity = new Order()
            {
                Id = Guid.NewGuid(),
                ClientId = order.ClientId,
                Date = order.Date,
                Price = order.Price,
                ProductId = order.ProductId
            };

            await _unitOfWork.Orders.Add(entity);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}