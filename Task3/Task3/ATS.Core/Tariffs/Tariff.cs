using System;

namespace ATS.Core.Tariffs
{
    public class Tariff : ITariff
    {
        public Tariff()
        {
            Id = Guid.NewGuid();
        }

        private const double TARIFF_PER_SECOND = 200;

        public Guid Id { get; set; }

        public double GetCost(TimeSpan duration, bool isIncome)
        {
            return isIncome ? 0 : duration.Seconds * TARIFF_PER_SECOND;
        }
    }
}