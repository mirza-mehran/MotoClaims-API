using MotoClaims.Entities.Benefits;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Product
{
  public class ProductByID
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public string ProductDetails { get; set; }
        public decimal AnnualPremium { get; set; }
        public bool Status { get; set; }
        public string CoverageName { get; set; }
        public string CoPayPercentage { get; set; }
        public string Deductibles { get; set; }
        public long IsAgencyRepair { get; set; }
        public long TenantId { get; set; }
        public List<Benefit> Benefit { get; set; }
    }
}
