using MotoClaims.Entities.AgencyGarage;
using MotoClaims.Entities.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IAgenciesService
    {
        AgencyGarageWithCount GetAgencyGarage(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long ProviderTypeId);
        IEnumerable<ProviderLocation> GetAllLocationByProviderId(long Id, long tenentId);

        string InsertAgencyGarageClaimsAssigned(AgencyGarageClaimsAssigned obj, long tenantId, long userId);
    }
}
