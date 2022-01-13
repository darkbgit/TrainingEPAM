using CsvHelper;
using CsvHelper.Configuration;
using CsvManager.Core.DTOs;
using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces.UnitsOfWork;
using CsvManager.Services.Implementation.Csv;
using CsvManager.Services.Implementation.Exceptions;
using CsvManager.Services.Implementation.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvManager.Services.Implementation
{
    public class FileService : IFileService
    {

        private readonly IRecordService _recordService;
        private readonly IGetOrCreateUnitOfWork<Manager> _managerUnitOfWork;
        private readonly ILogger<FileService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAddUnitOfWork<Order> _ordersUnitOfWork;

        private readonly string _destinationFolder;
        private readonly string _separatorInRecord;
        private readonly string _dateFormatInRecord;


        public FileService(IRecordService recordService, ILogger<FileService> logger, IConfiguration configuration, IGetOrCreateUnitOfWork<Manager> managerUnitOfWork, IAddUnitOfWork<Order> ordersUnitOfWork)
        {
            _recordService = recordService;
            _logger = logger;
            _configuration = configuration;
            _managerUnitOfWork = managerUnitOfWork;
            _ordersUnitOfWork = ordersUnitOfWork;
            _destinationFolder = configuration["Folders:BackupFolder"];
            _separatorInRecord = configuration["Records:Separator"];
            _dateFormatInRecord = configuration["Records:DateFormat"];
        }


        public async Task ParseAsync(string filePath, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start parse {filePath}.");

            var fileForParse = new FileInfo(filePath);

            var (manager, date) = await ParseFileName(Path.GetFileNameWithoutExtension(fileForParse.Name), cancellationToken);

            ICollection<OrderDto> orders = new List<OrderDto>();

            var isRecordGood = true;
            var badRecords = 0;
            using (var reader = new StreamReader(fileForParse.FullName, Encoding.Default))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = _separatorInRecord,
                    HasHeaderRecord = false,
                    BadDataFound = context =>
                    {
                        isRecordGood = false;
                        badRecords++;
                        _logger.LogInformation($"{context.RawRecord} couldn't parse. Record skipped.");
                    }

                }))
                {
                    csv.Context.RegisterClassMap<CsvMap>();

                    csv.Context.Maps[typeof(RecordDto)].MemberMaps[0].TypeConverterOption.Format(_dateFormatInRecord);

                    while (await csv.ReadAsync())
                    {
                        Thread.Sleep(100);

                        cancellationToken.ThrowIfCancellationRequested();

                        var record = csv.GetRecord<RecordDto>();

                        if (!isRecordGood) continue;

                        OrderDto order;
                        try
                        {
                            order = await _recordService.ParseAsync(record, cancellationToken);
                        }
                        catch (ServiceException e)
                        {
                            _logger.LogInformation(e.Message);
                            continue;
                        }

                        if (order == null) continue;

                        order.Date = date;
                        orders.Add(order);
                    }
                }
            }

            Task.WaitAll();

            if (orders.Any())
            {
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


                await _ordersUnitOfWork.AddRangeAsync(result, cancellationToken);
                await _ordersUnitOfWork.SaveChangesAsync(cancellationToken);
            }

            Directory.CreateDirectory(_destinationFolder);

            var newFilePath = fileForParse.NewFullName(_destinationFolder);

            File.Move(fileForParse.FullName, newFilePath);

            _logger.LogInformation($"File {filePath} parse is finished. For manager {manager.Name} {orders.Count} records added. {badRecords} records skipped.");
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
                manager = await _managerUnitOfWork.GetOrCreateByNameAsync(data[0], cancellationToken);
                //manager = _unitOfWork.Managers.FindBy(m => m.Name.Equals(data[0])).FirstOrDefault();
                //if (manager == null)
                //{

                //}
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ServiceException("Error get or create manager.", e);
            }


            return new Tuple<Manager, DateTime>(manager, date);
        }
    }
}