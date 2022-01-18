using System;
using WebOrdersInfo.Core.DTOs.Filters;

namespace WebOrdersInfo.Core.Services.Interfaces
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}