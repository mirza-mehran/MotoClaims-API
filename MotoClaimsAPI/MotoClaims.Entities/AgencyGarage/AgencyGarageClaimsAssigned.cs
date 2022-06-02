using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.AgencyGarage
{
   public class AgencyGarageClaimsAssigned
    {
        public long AGCA_Id { get; set; }
        [Required(ErrorMessage = "The ClaimId field is required.")]
        public long ClaimId { get; set; }
       // [Required(ErrorMessage = "The PolicyId field is required.")]
        public long PolicyId { get; set; }
        [Required(ErrorMessage = "The AgencyGarageId field is required.")]
        public long AgencyGarageId { get; set; }
        [Required(ErrorMessage = "The AgencyGarageBranchId field is required.")]
        public long AgencyGarageBranchId { get; set; }
        [Required(ErrorMessage = "The Status field is required.")]
        public long Status { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
