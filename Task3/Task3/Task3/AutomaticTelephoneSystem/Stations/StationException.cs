using System;

namespace Task3.AutomaticTelephoneSystem.Stations
{
    public class StationException : Exception
    {
        public StationException()
        {
        }

        public StationException(string massage):
            base(massage)
        {
        }

        public StationException(string massage, Exception ex) :
            base(massage, ex)
        {
        }
    }
}
