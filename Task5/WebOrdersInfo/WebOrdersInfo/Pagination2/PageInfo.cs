using System;
using WebOrdersInfo.Utilities;

namespace WebOrdersInfo.Pagination2
{
    public class PageInfo
    {
        public PageInfo(int pageNumber, int totalNews, int pageSize = Constants.ORDERS_PER_PAGE)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalNews = totalNews;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalNews { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalNews / PageSize);
    }
}
