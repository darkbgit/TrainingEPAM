using System;

namespace WebOrdersInfo.Core.DTOs
{
    public abstract class NameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
