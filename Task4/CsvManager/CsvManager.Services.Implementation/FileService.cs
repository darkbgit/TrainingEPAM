using System;
using System.IO;
using System.Threading.Tasks;
using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CsvManager.Services.Implementation
{
    public class FileService : IFileService
    {

        private readonly IRecordService _recordService;
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IRecordService recordService, IUnitOfWork unitOfWork)
        {
            _recordService = recordService;
            _unitOfWork = unitOfWork;
        }


        public async Task Parse(string filePath)
        {
            var fileName = Path.GetFileName(filePath);

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
        }


        private async Task<Guid> ParseFileName(string fileName)
        {
            var data = fileName.Split('_');

            if (data.Length != 2)
            {
                throw new Exception();
            }

            return await GetManagerIdBySecondName(fileName);
        }

        private async Task<Guid> GetManagerIdBySecondName(string secondName)
        {
            if (string.IsNullOrWhiteSpace(secondName))
                throw new Exception();

            var id = _unitOfWork.Clients.FindBy(c => c.SecondName == secondName)
                .FirstOrDefaultAsync().Result?.Id;

            if (id == null)
            {
                var manager = new Manager()
                {
                    Id = Guid.NewGuid(),
                    SecondName = secondName
                };

                await _unitOfWork.Managers.Add(manager);

                id = manager.Id;
            }

            return id.Value;
        }
    }
}