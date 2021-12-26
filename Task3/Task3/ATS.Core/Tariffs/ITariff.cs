using System;

namespace ATS.Core.Tariffs
{
    public interface ITariff
    {
        Guid Id { get; set; }
        double GetCost(TimeSpan duration, bool isIncome);
    }
}
