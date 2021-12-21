using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.BillingSystem;
using ATS.Core.ClientsService;
using ATS.Core.Tariffs;


namespace ATS.Core.MobileCompanies
{
    public interface IMobileCompany
    {
        ITerminal SingClientContract(Client client, ITariff tariff);

        IBilling Billing { get; }
    }
}
