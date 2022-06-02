using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.AuthorityMatrix
{
   public class AMatrixAssess_Services
    {
        public long AMA_ID { get; set; }
        public long AM_Assess_ID { get; set; }
        public bool AMA_Service_Type { get; set; }
        public long AMA_Service_Code { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public string Service { get; set; }
    }

    public class AuthorityMatrixAssess_Services
    {
        public long Id { get; set; }
        public string Service { get; set; }
        public long ServiceTypeID { get; set; }
        public string Name { get; set; }
    }
}
