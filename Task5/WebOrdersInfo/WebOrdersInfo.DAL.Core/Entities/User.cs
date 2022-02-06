using System;
using Microsoft.AspNetCore.Identity;

namespace WebOrdersInfo.DAL.Core.Entities
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        
    }
}