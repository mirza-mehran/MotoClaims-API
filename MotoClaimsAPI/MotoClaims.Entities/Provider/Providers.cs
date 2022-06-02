using MotoClaims.Entities.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Provider
{
   public class Providers
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long Status { get; set; }
        public long TenantId { get; set; }
        public long ProviderTypeId { get; set; }
        public string Image { get; set; }
        public UploadImage ImageModel { get; set; }

        public List<ProviderContact> ProviderContacts { get; set; }
        public List<ProviderServices> ProviderServices { get; set; }
        public List<ProviderLocation> ProviderLocations { get; set; }
        public List<ProviderServiceMapping> ProviderServiceMapping { get; set; }

    }
}
