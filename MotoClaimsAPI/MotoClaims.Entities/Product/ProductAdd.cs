using MotoClaims.Entities.Benefits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MotoClaims.Entities.Product
{
   public class ProductAdd
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public string ProductDetails { get; set; }
        public decimal AnnualPremium { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long TenantId { get; set; }
        public virtual Coverage Coverage { get; set; }
        public virtual List<Benefit> Benefit { get; set; }
        public int tempProID { get; set; }
    }
}
