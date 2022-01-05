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
using System.Threading;

namespace CsvManager.Services.Implementation
{
    public class FileService : IFileService
    {

        private readonly IRecordService _recordService;
        private readonly IGetOrCreateUnitOfWork<Manager> _getOrCreateUnitOfWork;
        private readonly ILogger<FileService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDbContextFactory<CsvManagerContext> _dbContextFactory;

        private readonly string _destinationFolder;

        private CancellationToken ct;
        

        public FileService(IRecordService recordService, ILogger<FileService> logger, IConfiguration configuration, IDbContextFactory<CsvManagerContext> dbContextFactory, IGetOrCreateUnitOfWork<Manager> getOrCreateUnitOfWork)
        {
            _recordService = recordService;
            _logger = logger;
            _configuration = configuration;
            _dbContextFactory = dbContextFactory;
            _getOrCreateUnitOfWork = getOrCreateUnitOfWork;
            _destinationFolder = configuration["Folders:BackupFolder"];
        }


        public async Task ParseAsync(string filePath, CancellationToken cancellationToken)
        {
           // ct = cancellationToken;

           //_logger.LogInformation("dfgsdf");
           //for (int i = 0; i < 1000000000; i++)
           //{
           //    i++;
           //    Thread.Sleep(100);
           //    if (cancellationToken.IsCancellationRequested)
           //    {
           //        Console.WriteLine("Token");
           //        cancellationToken.ThrowIfCancellationRequested();
           //        return;
           //    }
           //}

           _logger.LogInformation($"Start parse {filePath}.");

            var fileForParse = new FilePath(filePath);

            var (manager, date) = await ParseFileName(fileForParse.FileName, cancellationToken);

            ICollection<OrderDto> orders = new List<OrderDto>();

            await using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    Thread.Sleep(100);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Token");
                        cancellationToken.ThrowIfCancellationRequested();
                        return;
                    }
                    OrderDto order = null;
                    try
                    {
                        order = await _recordService.ParseAsync(await sr.ReadLineAsync(), cancellationToken);
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
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("t");
                }
                cancellationToken.ThrowIfCancellationRequested();
                var result = orders.Select(o => new Order
                {
                    Id = o.Id,
                    ManagerId = manager.Id,
                    Date = o.Date,
                    ClientId = o.ClientId,
                    ProductId = o.ProductId,
                    Price = o.Price
                }).ToList();


                //await _unitOfWork.Orders.AddRangeAsync(result);
                //await _unitOfWork.SaveChangesAsync();


                await using (var context = _dbContextFactory.CreateDbContext())
                {
                    await context.Orders.AddRangeAsync(result, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }
                
            }

            Directory.CreateDirectory(_destinationFolder);

            var newFilePath =  new FilePath(_destinationFolder, fileForParse.FileName, fileForParse.Extension);

            File.Move(fileForParse.FullPath, newFilePath.FullPathWithNewNameCheck());

            //.LogInformation($"File {filePath} parse is finished. For manager {/*manager.Name*/} {orders.Count} records added.");
        }


        private async Task<Tuple<Manager, DateTime>> ParseFileName(string fileName, CancellationToken cancellationToken)
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
                manager = await _getOrCreateUnitOfWork.GetOrCreateByNameAsync(data[0], cancellationToken);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error get or create manager.", e);
            }
            

            return new Tuple<Manager, DateTime>(manager, date);
        }
    }
}