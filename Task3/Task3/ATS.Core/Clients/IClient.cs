using System;

namespace ATS.Core.Clients
{
    public interface IClient
    {
        Guid Id { get; }

        public string FirstName { get; }

        public string LastName { get; }
    }
}