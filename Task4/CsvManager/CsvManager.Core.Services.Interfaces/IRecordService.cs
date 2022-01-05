using System;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Core.DTOs;

namespace CsvManager.Core.Services.Interfaces
{
    public interface IRecordService
    {
        Task<OrderDto> ParseAsync(string record, CancellationToken cancellationToken);
    }
}