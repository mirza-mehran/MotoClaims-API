using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Product
{
   public class Coverage
    {
        public int Id { get; set; }
        public string CoverageName { get; set; }
        public string CoPayPercentage { get; set; }
        public string Deductibles { get; set; }
        public bool Ceiling { get; set; }
        public long IsAgencyRepair { get; set; }
        public int ProductId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
