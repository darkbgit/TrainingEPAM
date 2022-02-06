using System.Collections.Generic;
using WebOrdersInfo.Core.DTOs.Models.Statistics;

namespace WebOrdersInfo.Models.ViewModels.Statistics
{
    public class StatisticsViewModel
    {
        public IEnumerable<ManagerNameWithGroupingPropertyDto> TopManagersByOrders { get; set; }
    }
}