using System.Collections.Generic;
using WebOrdersInfo.Helpers;

namespace WebOrdersInfo.Models.ViewModels.Orders
{
    public class OrdersListWithPaginationViewModel
    {
        public IEnumerable<OrderViewModel> OrderList { get; set; }

        public Paging Pagination { get; set; }
    }
}