using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Provider
{
   public class ProviderLocation
    {
        public long Id { get; set; }
        public string BranchName { get; set; }
        public long CountryId { get; set; }
        public string StreetAddress { get; set; }
        public long CityId { get; set; }
        public string Url { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long ProviderId { get; set; }
        public long TenantId { get; set; }
        public long AreaId { get; set; }
    }
}
