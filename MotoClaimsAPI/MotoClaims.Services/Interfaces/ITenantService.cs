using MotoClaims.Entities;
using MotoClaims.Entities.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
  public interface ITenantService
    {
        IEnumerable<ProviderCountry> GetCountry(long tenantId);
        Tenants GetTenantsByID(long tenantId);
        bool Update_Tenant(Tenants model, long tenantId,long userId);
    }
}
