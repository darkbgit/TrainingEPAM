using CsvHelper;
using CsvHelper.Configuration;
using CsvManager.Core.DTOs;
using CsvManager.Core.Services.Interfaces;
using CsvManager.DAL.Core.Entities;
using CsvManager.DAL.Repositories.Interfaces.UnitsOfWork;
using CsvManager.Services.Implementation.Config;
using CsvManager.Services.Implementation.Csv;
using CsvManager.Services.Implementation.Exceptions;
using CsvManager.Services.Implementation.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using FileOptions = CsvManager.Services.Implementation.Config.FileOptions;

namespace CsvManager.Services.Implementation
{
    public class FileService : IFileService
    {
        private readonly IRecordService _recordService;
        private readonly IGetOrCreateUnitOfWork<Manager> _managerUnitOfWork;
        private readonly ILogger<FileService> _logger;
        private readonly IAddUnitOfWork<Order> _ordersUnitOfWork;
        private readonly FolderOptions _folderOptions;
        private readonly FileOptions _fileOptions;
        private readonly RecordOptions _recordOptions;


        public FileService(IRecordService recordService, ILogger<FileService> logger, IGetOrCreateUnitOfWork<Manager> managerUnitOfWork, IAddUnitOfWork<Order> ordersUnitOfWork, IOptions<FolderOptions> folderOptions, IOptions<FileOptions> fileOptions, IOptions<RecordOptions> recordOptions)
        {
            _recordService = recordService;
            _logger = logger;
            _managerUnitOfWork = managerUnitOfWork;
            _ordersUnitOfWork = ordersUnitOfWork;
            _folderOptions = folderOptions.Value;
            _fileOptions = fileOptions.Value;
            _recordOptions = recordOptions.Value;
        }


        public async Task ParseAsync(string filePath, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start parse {filePath}.");

            var fileForParse = new FileInfo(filePath);

            var (manager, date) = await ParseFileName(Path.GetFileNameWithoutExtension(fileForParse.Name), cancellationToken);

            ICollection<OrderDto> orders = new List<OrderDto>();

            var badRecords = 0;
            using (var reader = new StreamReader(fileForParse.FullName, Encoding.Default))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = _recordOptions.Separator,
                    HasHeaderRecord = false
                }))
                {
                    csv.Context.AutoMap<RecordDto>().MemberMaps[0].TypeConverterOption.Format(_recordOptions.DateFormat);

                    while (await csv.ReadAsync())
                    {
                        Thread.Sleep(100);

                        cancellationToken.ThrowIfCancellationRequested();

                        RecordDto record;
                        try
                        {
                            record = csv.GetRecord<RecordDto>();
                        }
                        catch (Exception e)
                        {
                            badRecords++;
                            _logger.LogInformation(e.Message);
                            continue;
                        }
                        
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

            cancellationToken.ThrowIfCancellationRequested();

            var numberSavedOrders = 0;

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

                await _ordersUnitOfWork.AddRangeAsync(result, cancellationToken);
                numberSavedOrders = await _ordersUnitOfWork.SaveChangesAsync(cancellationToken);
            }

            if (badRecords == 0 && numberSavedOrders == orders.Count)
            {
                Directory.CreateDirectory(_folderOptions.BackupFolder);

                File.Move(fileForParse.FullName, fileForParse.NewFullName(_folderOptions.BackupFolder));

                _logger.LogInformation($"File {filePath} parse is finished. For manager {manager.Name} {orders.Count} records added.");
            }
            else
            {
                Directory.CreateDirectory(_folderOptions.BackupWithErrors);

                File.Move(fileForParse.FullName, fileForParse.NewFullName(_folderOptions.BackupWithErrors));

                _logger.LogInformation($"File {filePath} parse is finished. For manager {manager.Name} {orders.Count} records added. {badRecords} BAD RECORDS.");
            }
        }


        private async Task<Tuple<Manager, DateTime>> ParseFileName(string fileName, CancellationToken cancellationToken)
        {
            CsvFileName csvFileName;
            using (TextReader reader = new StringReader(fileName))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = _fileOptions.Separator,
                    HasHeaderRecord = false,
                    BadDataFound = context =>
                    {
                        _logger.LogInformation($"{context.RawRecord} couldn't parse file name.");
                        throw new ServiceException("");
                    }
                }))
                {
                    csv.Context.AutoMap<CsvFileName>().MemberMaps[1].TypeConverterOption.Format(_fileOptions.DateFormat);

                    await csv.ReadAsync();

                    csvFileName = csv.GetRecord<CsvFileName>();
                }
            }

            Manager manager;
            try
            {
                manager = await _managerUnitOfWork.GetOrCreateByNameAsync(csvFileName.ManagerName, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ServiceException("Error get or create manager.", e);
            }

            return new Tuple<Manager, DateTime>(manager, csvFileName.Date);
        }
    }
}