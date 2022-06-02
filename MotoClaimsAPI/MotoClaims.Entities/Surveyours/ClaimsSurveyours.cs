using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Surveyours
{
   public class ClaimsSurveyours
    {
        public long CS_Id { get; set; }
        public long SurveyourId { get; set; }
        public long ClaimId { get; set; }
        public long PolicyId { get; set; }
        public string Remarks { get; set; }
        public string Comment { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
