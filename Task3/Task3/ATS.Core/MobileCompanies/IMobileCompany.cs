using System.Collections.Generic;
using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.BillingSystem;
using ATS.Core.Clients;
using ATS.Core.Tariffs;


namespace ATS.Core.MobileCompanies
{
    public interface IMobileCompany
    {
        ITerminal SingClientContract(IClient client, ITariff tariff);

        IBillingReport Billing { get; }
        IEnumerable<ITariff> Tariffs { get; }
        void AddTariff(ITariff tariff);
        void CancelClientContract(IClient client);
    }
}
