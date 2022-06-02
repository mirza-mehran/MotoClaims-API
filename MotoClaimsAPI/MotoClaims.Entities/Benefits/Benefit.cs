using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Benefits
{
    public class Benefit
    {
        public int Id { get; set; }
        public string BenefitName { get; set; }
        public string BenefitDetails { get; set; }
        public bool Status { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long ProductId { get; set; }
    }
}
