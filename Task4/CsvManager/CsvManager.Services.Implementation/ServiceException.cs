using System;

namespace CsvManager.Services.Implementation
{
    public class ServiceException : Exception
    {
        public ServiceException()
            : base() { }

        public ServiceException(string massage)
            : base(massage) { }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException) { }
        
    }
}