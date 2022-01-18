using System;
using System.Collections.Generic;
using System.Linq;

namespace WebOrdersInfo.Core.DTOs.Filters
{
    public class PaginationFilter
    {
        public PaginationFilter(int pageSize, int pageNumber)
        {
            PageSize = pageSize < 10 ? 10 : pageSize;
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
        }

        public PaginationFilter()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}