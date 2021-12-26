using System;
using ATS.Core.AutomaticTelephoneSystem;

namespace Services.Reports
{
    public interface IReportService
    {
        void CreateReportForClient(PhoneNumber clientPhone, Guid clientId, double minCost = 0, double maxCost = double.MaxValue, PhoneNumber interlocutorPhoneNumber = null);
        void CreateReportForClient(PhoneNumber clientPhone, Guid clientId, DateTime startDate, DateTime endDate, double minCost = 0, double maxCost = double.MaxValue, PhoneNumber interlocutorPhoneNumber = null);
    }
}
