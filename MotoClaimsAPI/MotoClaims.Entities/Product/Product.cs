using MotoClaims.Entities.Benefits;
using MotoClaims.Entities.VehicleParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Product
{
   public class Product
    {
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public string ProductDetails { get; set; }
        public decimal AnnualPremium { get; set; }
        public bool Status { get; set; }
    
        public string CoPayPercentage { get; set; }
        public string Deductibles { get; set; }
        public long IsAgencyRepair { get; set; }

        public string BenefitDetails { get; set; }

        public virtual Coverage Coverage { get; set; }
        public virtual List<Benefit> Benefit { get; set; }

        public bool HasError { get; set; }
        public List<ErrorList> ErrorList { get; set; }

    }

    public class Benefits
    {
        public string BenefitDetails { get; set; }
    }

}
