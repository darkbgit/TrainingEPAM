using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.BillingSystem;
using ATS.Core.ClientsService;


namespace ATS.Core.MobileCompanies
{
    public interface IMobileCompany
    {
        Contract SingClientContract(Client client);
    }
}
