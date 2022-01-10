using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvManager.Core.DTOs;
using Microsoft.Extensions.Configuration;

namespace CsvManager.Services.Implementation.Csv
{
    public sealed class CsvMap : CsvHelper.Configuration.ClassMap<RecordDto>
    {
        public CsvMap()
        {
            //var dateFormat = configuration["Records:DateFormat"];

            Map(m => m.Date).Index(0);
            Map(m => m.ClientName).Index(1);
            Map(m => m.ProductName).Index(2);
            Map(m => m.Cost).Index(3);
        }
    }
}
