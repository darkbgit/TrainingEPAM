using System;
using Microsoft.AspNetCore.Identity;

namespace WebOrdersInfo.DAL.Core.Entities
{
    public sealed class Role : IdentityRole<Guid>, IBaseEntity
    {
        public Role(string name)
            : base(name)
        {
            Id = Guid.NewGuid();
        }
    }
}