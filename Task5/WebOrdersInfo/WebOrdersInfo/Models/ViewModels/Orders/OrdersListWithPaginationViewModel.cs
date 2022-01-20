using System.Collections.Generic;
using WebOrdersInfo.Models.ViewModels.OrdersFilter;
using WebOrdersInfo.Pagination;
using WebOrdersInfo.Pagination2;

namespace WebOrdersInfo.Models.ViewModels.Orders
{
    public class OrdersListWithPaginationViewModel
    {
        public IEnumerable<OrderViewModel> OrderList { get; set; }

        //public Paging Pagination { get; set; }

        public PageInfo Pagination { get; set; }

        public OrdersFilterViewModel Filters { get; set; }
    }
}