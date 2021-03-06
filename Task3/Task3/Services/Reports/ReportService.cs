using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.BillingSystem;

namespace Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IBillingReport _billing;

        public ReportService(IBillingReport billing)
        {
            _billing = billing;
        }

        public void CreateReportForClient(PhoneNumber clientPhone, Guid clientId, double minCost = 0, double maxCost = double.MaxValue, PhoneNumber interlocutorPhoneNumber = null)
        {
            const int MONTHS_OFFSET = -1;
            DateTime endDate = DateTime.Now.ToUniversalTime();

            DateTime startDate = endDate.AddMonths(MONTHS_OFFSET);

            var result = _billing
                .GetReportForClient(r => r.BeginCall > startDate && 
                                         r.EndCall < endDate, clientId, interlocutorPhoneNumber)
                .Where(r => r.Cost >= minCost && r.Cost <= maxCost)
                .ToList();

            Console.WriteLine();
            Console.WriteLine($"Отчет для {clientPhone} c {startDate:G} по {endDate:G}");
            Console.WriteLine($"{result.Count} зап.");

            PrintReport(result);
        }

        public void CreateReportForClient(PhoneNumber clientPhone, Guid clientId, DateTime startDate, DateTime endDate, double minCost = 0, double maxCost = double.MaxValue, PhoneNumber interlocutorPhoneNumber = null)
        {
            var result = _billing
                .GetReportForClient(r =>r.BeginCall > startDate && r.EndCall < endDate,
                    clientId, interlocutorPhoneNumber)
                .ToList();

            Console.WriteLine();
            Console.WriteLine($"Отчет для {clientPhone} c {startDate:G} по {endDate:G}");
            Console.WriteLine($"{result.Count} зап.");

            PrintReport(result);
        }


        private static void PrintReport(IEnumerable<ReportRecord> records)
        {
            const int COLUMN_COUNT = 6;
            const int CALL_DIRECTION_WIDTH = 5;
            const int PHONE_NUMBER_WIDTH = 20;
            const int START_CALL_WIDTH = 20;
            const int END_CALL_WIDTH = 20;
            const int DURATION_WIDTH = 20;
            const int COST_WIDTH = 10;
            const int TOTAL_WIDTH = CALL_DIRECTION_WIDTH + PHONE_NUMBER_WIDTH + START_CALL_WIDTH + END_CALL_WIDTH + DURATION_WIDTH + COST_WIDTH +
                COLUMN_COUNT - 1;

            var builder = new StringBuilder();
            builder.Append('_', TOTAL_WIDTH);
            builder.Append(Environment.NewLine);
            builder.Append($"{"",CALL_DIRECTION_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Номер телефона",PHONE_NUMBER_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Начало",START_CALL_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Конец",END_CALL_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Продолжительность",DURATION_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Цена",COST_WIDTH}");
            builder.Append(Environment.NewLine);
            builder.Append('_', TOTAL_WIDTH);
            builder.Append(Environment.NewLine);

            foreach (var record in records)
            {
                builder.Append($@"{(record.IsIncome ? "<-" : "->"),CALL_DIRECTION_WIDTH}");
                builder.Append('|');
                builder.Append($"{record.InterlocutorPhoneNumber,PHONE_NUMBER_WIDTH}");
                builder.Append('|');
                builder.Append($"{record.BeginCall,START_CALL_WIDTH:G}");
                builder.Append('|');
                builder.Append($"{record.EndCall,END_CALL_WIDTH:G}");
                builder.Append('|');
                builder.Append($"{record.Duration,DURATION_WIDTH:hh':'mm':'ss}");
                builder.Append('|');
                builder.Append($"{record.Cost,COST_WIDTH}");
                builder.Append(Environment.NewLine);
            }
            Console.WriteLine(builder.ToString());
        }
    }
}
