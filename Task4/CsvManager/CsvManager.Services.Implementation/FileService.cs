using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;


namespace CsvManager.Services.Implementation
{
    public class FileService : IFileService
    {

        private readonly IRecordService _recordService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FileService> _logger;
        public FileService(IRecordService recordService, IUnitOfWork unitOfWork, ILogger<FileService> logger)
        {
            _recordService = recordService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task Parse(string filePath)
        {
            var fileName = Path.GetFileName(filePath);


            if (fileName.EndsWith(".csv"))
            {
                fileName = fileName[..^4];
            }
            else
            {
                throw new Exception();
            }

            var managerId = await ParseFileName(fileName);
            await using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                var order = await _recordService.Parse(await sr.ReadLineAsync());

                if (order != null)
                {
                    await _unitOfWork.Orders.Add(new Order
                    {
                        Id = order.Id,
                        ManagerId = managerId,
                        Date = order.Date,
                        ClientId = order.ClientId,
                        ProductId = order.ProductId,
                        Price = order.Price
                    });
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }


        private async Task<Guid> ParseFileName(string fileName)
        {
            var data = fileName.Split('_');

            if (data.Length != 2)
            {
                throw new Exception();
            }

            return await GetManagerIdBySecondName(data[0]);
        }

        private async Task<Guid> GetManagerIdBySecondName(string secondName)
        {
            if (string.IsNullOrWhiteSpace(secondName))
                throw new Exception();

            var id = (await _unitOfWork.Managers
                .FindBy(c => c.SecondName.Equals(secondName))
                .FirstOrDefaultAsync())
                ?.Id;

            if (id == null)
            {
                var manager = new Manager()
                {
                    Id = Guid.NewGuid(),
                    SecondName = secondName
                };

                await _unitOfWork.Managers.Add(manager);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Add new manager {secondName}");


                id = manager.Id;
            }

            return id.Value;
        }
    }
}