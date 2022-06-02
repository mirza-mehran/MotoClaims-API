using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.CarReplacement
{
   public class ReplacementCarRequests
    {
        public long RCR_Id { get; set; }
        public long ClaimId { get; set; }
        public long PolicyId { get; set; }
        public long RequestedDurationInDDays { get; set; }
        public long ClaimNo { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string Comments { get; set; }
        public long Status { get; set; }
        public string CarNo { get; set; }
        public DateTime SubmissionDate { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
