using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Product
{
   public class ProductList
    {
        public int id { get; set; }
        public string ProductName { get; set; }
        public string ProductDetails { get; set; }
        public decimal AnnualPremium { get; set; }
        public long ProductType { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
