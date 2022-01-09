using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Core.DTOs;
using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Implementation;
using CsvManager.Services.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CsvManager.Services.Implementation
{
    public class RecordService : IRecordService
    {
        private readonly IGetOrCreateUnitOfWork<Client> _clientUnitOfWork;
        private readonly IGetOrCreateUnitOfWork<Product> _productUnitOfWork;
        private readonly ILogger<IRecordService> _logger;

        private readonly string _separator;
        private readonly string _dateFormat;

        public RecordService(ILogger<IRecordService> logger, IConfiguration configuration, IGetOrCreateUnitOfWork<Client> clientUnitOfWork, IGetOrCreateUnitOfWork<Product> productUnitOfWork)
        {
            _logger = logger;
            _clientUnitOfWork = clientUnitOfWork;
            _productUnitOfWork = productUnitOfWork;
            _separator = configuration["Records:Separator"];
            _dateFormat = configuration["Records:DateFormat"];

        }

        public async Task<OrderDto> ParseAsync(string record, CancellationToken cancellationToken)
        {
            
            if (string.IsNullOrWhiteSpace(record))
            {
                throw new ServiceException("Record error. Empty record.");
            }

            var data = record.Split(_separator);

            if (data.Length != 4)
            {
                throw new ServiceException("Record error. Record wrong format.");
            }
                

            if (!double.TryParse(data[3], NumberStyles.Float, CultureInfo.InvariantCulture, out var price))
            {
                throw new ServiceException($"Record error. {data[3]} - price wrong format.");
            }

            if (!DateTime.TryParseExact(data[0], _dateFormat, null, DateTimeStyles.None, out var date))
            {
                throw new ServiceException($"Record error. {data[0]} - date wrong format.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            var order = new OrderDto
            {
                Id = Guid.NewGuid(),
                Date = date,
                ClientId = (await _clientUnitOfWork.GetOrCreateByNameAsync(data[1], cancellationToken)).Id,
                ProductId = (await _productUnitOfWork.GetOrCreateByNameAsync(data[2], cancellationToken)).Id,
                Price = price
            };

            return order;
        }
    }
}