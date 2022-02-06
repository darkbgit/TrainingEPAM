using System;

namespace WebOrdersInfo.DAL.Core.Entities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}