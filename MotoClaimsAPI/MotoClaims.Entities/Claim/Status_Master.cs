using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Claim
{
   public class Status_Master
    {
        public long Claim_Id { get; set; }
        public long Status_Id { get; set; }
        public string StatusName { get; set; }
        public string StepComments { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
