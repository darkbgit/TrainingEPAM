using CsvManager.Core.DTOs;
using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces.UnitsOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CsvManager.Services.Implementation
{
    public class RecordService : IRecordService
    {
        private readonly IGetOrCreateUnitOfWork<Client> _clientUnitOfWork;
        private readonly IGetOrCreateUnitOfWork<Product> _productUnitOfWork;


        public RecordService(IGetOrCreateUnitOfWork<Client> clientUnitOfWork, IGetOrCreateUnitOfWork<Product> productUnitOfWork)
        {
            _clientUnitOfWork = clientUnitOfWork;
            _productUnitOfWork = productUnitOfWork;
        }

        public async Task<OrderDto> ParseAsync(RecordDto record, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var client = await _clientUnitOfWork.GetOrCreateByNameAsync(record.ClientName, cancellationToken);

            var product = await _productUnitOfWork.GetOrCreateByNameAsync(record.ProductName, cancellationToken);

            var order = new OrderDto
            {
                Id = Guid.NewGuid(),
                ClientId = client.Id,
                ProductId = product.Id,
                Price = record.Cost
            };

            return order;
        }
    }
}