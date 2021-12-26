using System;

namespace ATS.Core.Clients
{
    public class Client : IClient
    {
        public Client()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
