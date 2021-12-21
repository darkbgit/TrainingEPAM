using System;
using ATS.Core.AutomaticTelephoneSystem;

namespace ATS.Core.Reports
{
    public interface IReportService
    {
        void CreateReportForClient(DateTime startDate, DateTime endDate, Guid clientId, PhoneNumber clientPhone);
    }
}
