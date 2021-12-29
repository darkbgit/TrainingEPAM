using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvManager.Core.DTOs;
using CsvManager.DAL.Core;
using CsvManager.DAL.Repositories.Implementation.Repositories;
using Microsoft.Extensions.Configuration;


namespace CsvManager.Services.Implementation
{
    public class FileService : IFileService
    {

        private readonly IRecordService _recordService;
        private readonly IGetOrCreateUnitOfWork<Manager> _getOrCreateUnitOfWork;
        private readonly ILogger<FileService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDbContextFactory<CsvManagerContext> _dbContextFactory;
        

        public FileService(IRecordService recordService, ILogger<FileService> logger, IConfiguration configuration, IDbContextFactory<CsvManagerContext> dbContextFactory, IGetOrCreateUnitOfWork<Manager> getOrCreateUnitOfWork)
        {
            _recordService = recordService;
            _logger = logger;
            _configuration = configuration;
            _dbContextFactory = dbContextFactory;
            _getOrCreateUnitOfWork = getOrCreateUnitOfWork;
        }


        public async Task Parse(string filePath)
        {
            _logger.LogInformation($"Start parse {filePath}.");

            var fileName = Path.GetFileName(filePath);

            var fileEnd = _configuration["Files:Pattern"];

            if (!fileName.EndsWith(fileEnd))
            {
                throw new ServiceException($"File error. File don't ends with {fileEnd}.");
            }

            var (manager, date) = await ParseFileName(fileName[..^fileEnd.Length]);

            ICollection<OrderDto> orders = new List<OrderDto>();

            await using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    OrderDto order = null;
                    try
                    {
                        order = await _recordService.Parse(await sr.ReadLineAsync());
                    }
                    catch (ServiceException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                    if (order != null)
                    {
                        orders.Add(order);
                    }
                }
            }

            Task.WaitAll();
            
            if (orders.Any())
            {
                var result = orders.Select(o => new Order
                {
                    Id = o.Id,
                    ManagerId = manager.Id,
                    Date = o.Date,
                    ClientId = o.ClientId,
                    ProductId = o.ProductId,
                    Price = o.Price
                }).ToList();


                //await _unitOfWork.Orders.AddRange(result);
                //await _unitOfWork.SaveChangesAsync();


                await using (var context = _dbContextFactory.CreateDbContext())
                {
                    await context.Orders.AddRangeAsync(result);
                    await context.SaveChangesAsync();
                }


                var destinationFolder = _configuration["Folders:BackupFolder"];

                //Directory.CreateDirectory(destinationFolder);

                //File.Move(filePath, Path.Combine(destinationFolder, fileName));

            }

            _logger.LogInformation($"File {filePath} parse is finished. For manager {manager.Name} {orders.Count} records added.");
        }


        private async Task<Tuple<Manager, DateTime>> ParseFileName(string fileName)
        {
            var separator = _configuration["Files:Separator"];
            var dateFormat = _configuration["Files:DateFormat"];

            if (string.IsNullOrWhiteSpace(separator))
            {
                throw new ServiceException("Couldn't get separator char for record from configuration.");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ServiceException("File name error. Empty file name.");
            }
            var data = fileName.Split(separator);

            if (data.Length != 2)
            {
                throw new ServiceException("File name error. File name wrong format.");
            }

            if (!DateTime.TryParseExact(data[1], dateFormat, null, DateTimeStyles.None, out var date))
            {
                throw new ServiceException("File name error. Date wrong format.");
            }

            Manager manager;
            try
            {
                manager = await _getOrCreateUnitOfWork.GetOrCreateByName(data[0]);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error get or create manager.", e);
            }
            

            return new Tuple<Manager, DateTime>(manager, date);
        }
    }
}