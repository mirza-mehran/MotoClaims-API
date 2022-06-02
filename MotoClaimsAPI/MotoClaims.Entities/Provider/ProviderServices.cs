using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Provider
{
   public class ProviderServices
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long ProviderServiceId { get; set; }
        public long ProviderId { get; set; }
        public string Service { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
    }
}
