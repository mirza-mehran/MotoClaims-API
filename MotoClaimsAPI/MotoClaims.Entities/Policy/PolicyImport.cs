using MotoClaims.Entities.Benefits;
using MotoClaims.Entities.VehicleParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Policy
{
    public class PolicyImport
    {
        public string PolicyHolderName { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string PolicyNo { get; set; }
        public string IdentityNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long Status { get; set; }
        public long PolicyType { get; set; }
        public long MakeId { get; set; }
        public long ModelId { get; set; }
        public long ColourId { get; set; }

        public string CoPayPercentage { get; set; }
        public string Deductibles { get; set; }
        public long IsAgencyRepair { get; set; }
        public long PolicyId { get; set; }
        public long ProductId { get; set; }

        public virtual List<Benefit> Benefits { get; set; }

        public string RegistrationNumber { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime DrivingLicenseValidity { get; set; }
        public string IdentificationNumber { get; set; }
        public string Year { get; set; }
        public long Capacity { get; set; }
        public string CarNumber { get; set; }
        public string PlateNumber { get; set; }
  
        public bool HasError { get; set; }
        public List<ErrorList> ErrorList { get; set; }
    }
}
