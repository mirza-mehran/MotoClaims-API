using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IProviderServicesContractService
    {
        IEnumerable<ProviderServiceType> GetProviderServiceType(long ProviderTypeId, long tenantId);
        IEnumerable<ProviderServices> GetProviderServices(long ServiceTypeId, long tenantId);

        IEnumerable<ProviderServices> GetProviderServices(long ProviderId, long tenantId, long userId);
        IEnumerable<PolicyMake> GetMake(long tenentId, long userId);
        IEnumerable<PolicyModel> GetModel(long Id, long tenentId, long userId);
        string InsertProviderContract(Provider_Services_Contract obj, long tenantId, long userId, out bool bit);
        string InsertProviderContractPrice(PS_Prices obj, long tenantId, long userId, out bool bit);

        string UpdateProviderContract(Provider_Services_Contract obj, long tenantId, long userId, out bool bit);
        string UpdateProviderContractPrice(PS_Prices obj, long tenantId, long userId, out bool bit);

        ModelProvider_Services_Contract GetProvider_Services_Contract(long Id, long tenentId, long userId);
        bool DeleteProvider_Services_Contract(long Id, long TenantId, long userId);
        IEnumerable<ModelProviderContractServices> GetProvider_Services_Contrac_By_ProviderId(long ProviderId, long tenentId, long userId);

        IEnumerable<PS_Prices> GetProvider_Services_Contrac_Prices(long PSC_Id, long tenentId, long userId);

        bool DeleteProvider_Services_Contract_Price(long Id, long TenantId, long userId);

        PS_Prices GetProvider_Services_Price(long Id, long tenentId, long userId);

        PS_Prices GetDate(long Id);

    }
}
