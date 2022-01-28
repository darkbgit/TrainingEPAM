using System.Collections.Generic;
using WebOrdersInfo.Core.DTOs.Models.Statistics;

namespace WebOrdersInfo.Models.ViewModels.Statistics
{
    public class StatisticsViewModel
    {
        public IEnumerable<ManagerWithCountOrdersDto> TopManagersByOrders { get; set; }
    }
}