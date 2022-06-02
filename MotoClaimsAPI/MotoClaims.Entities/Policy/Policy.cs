using MotoClaims.Entities.Benefits;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MotoClaims.Entities.Policy
{
   public class Policy
    {
        public int Id { get; set; }
        public string PolicyHolderName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DrivingLicenseValidityExpiryDate { get; set; } = DateTime.Now;
        public string Address { get; set; }
        public string PolicyNo { get; set; }
        public string IdentityNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long Status { get; set; }
        public long PolicyType { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public long TenantId { get; set; }
        [Required]
        public long MakeId { get; set; }
        public string MakeName { get; set; }
        [Required]
        public long ModelId { get; set; }
        public string ModelName { get; set; }
        [Required]
        public long ColourId { get; set; }
        public string ColourName { get; set; }
        public string TenantName { get; set; }
        public string CoverageName { get; set; }
        public string CoPayPercentage { get; set; }
        public string Deductibles { get; set; }
        public bool Ceiling { get; set; }
        public long IsAgencyRepair { get; set; }
        public long PolicyId { get; set; }
        [Required]
        public long ProductId { get; set; }
        public virtual List<Benefit> Benefits { get; set; }
        public decimal AnnualPremium { get; set; }
        public string MakeImage { get; set; }

        public string RegistrationNumber { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime DrivingLicenseValidity { get; set; }
        public string IdentificationNumber { get; set; }
        public string Year { get; set; }
        public long Capacity { get; set; }
        public string CarNumber { get; set; }
        public string PlateNumber { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
   }

    public class ModelPolicy
    {
        public int Id { get; set; }
        public string PolicyHolderName { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string PolicyNo { get; set; }
        public string IdentityNo { get; set; }
        public DateTime DrivingLicenseValidity { get; set; }

    }
    public class ModelPolicyWithTotalRecord
    {
        public IEnumerable<ModelPolicy> ModelPolicy { get; set; }
        public long TotalRecord { get; set; }
    }
    public class PolicyId
    {
        public long Id { get; set; }
    }
}
