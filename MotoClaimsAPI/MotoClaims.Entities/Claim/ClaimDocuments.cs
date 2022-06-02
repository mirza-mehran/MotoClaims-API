using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Claim
{
  public  class ClaimDocuments
    {
        public long CD_Id { get; set; }
        public long ClaimId { get; set; }
        public long PolicyId { get; set; }
        public long MakeId { get; set; }
        public long ModelId { get; set; }
        public long DocumentTypeId { get; set; }
        public string Path { get; set; }
        public long ClaimAttachmentId { get; set; }

        public string fileName { get; set; }
        public string type { get; set; }
        public string uri { get; set; }

        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long IsDeleted { get; set; }
        public long IsActive { get; set; }
    
    }
}
