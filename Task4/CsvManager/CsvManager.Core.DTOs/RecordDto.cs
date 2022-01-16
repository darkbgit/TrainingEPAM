using System;

namespace CsvManager.Core.DTOs
{
    public class RecordDto
    {
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
        public string ProductName { get; set; }
        public double Cost { get; set; }
    }
}