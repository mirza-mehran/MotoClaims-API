using MotoClaims.Entities;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
    public interface IProviderService
    {
        IEnumerable<Provider> GetGarage(long tenantId);
        IEnumerable<Provider> GetAgency(long tenantId);
        IEnumerable<Provider> GetCarAgency(long tenantId);
        IEnumerable<Provider> GetSurveyor(long tenantId);


        IEnumerable<ProviderCountry> GetProviderCountry(long tenantId);
        IEnumerable<ProviderCity> GetProviderCity(long CountryId, long tenantId);
        IEnumerable<Areas> GetProviderArea(long CityId, long tenantId);
        IEnumerable<ProviderServiceType> GetProviderServiceType(long tenantId);
        IEnumerable<ProviderServices> GetProviderServices(long ServiceTypeId, long tenantId);


        string InsertProviderImage(ImagesUpload model, long tenentId, long userId);
        long PostProvider(Providers model,long tenantId,long userId);
        long UpdateProvider(Providers model, long tenantId, long userId);
        bool DeleteProvider(long Id, long TenantId, long userId);

        // Providers GetProviderByID(long id, long tenantId);

        Providers GetProviderByID(long id, long tenantId);

        string GetProviderName(long ProviderTypeId, string name, long tenantId);

        IEnumerable<ModelProvider> GetAllProviderByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long ProviderTypeId);

        IEnumerable<Provider> GetAllGarage(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId);

    }
}
