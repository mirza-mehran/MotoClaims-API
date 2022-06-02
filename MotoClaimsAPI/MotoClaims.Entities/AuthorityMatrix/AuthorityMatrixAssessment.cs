using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.AuthorityMatrix
{
  public  class AuthorityMatrixAssessment
    {
        public long AM_Assess_ID { get; set; }
        public string AM_Assess_Name { get; set; }
        public string AM_Assess_Details { get; set; }
        public bool AM_Assess_Type { get; set; }
        public long AM_Assess_AmountFrom { get; set; }
        
        public long AM_Assess_AmountTo { get; set; }
        public bool AM_Assess_AssignType { get; set; }
        public long AM_Assess_AssignUser { get; set; }
        public string UserName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public virtual List<AMatrixAssess_Services> AMatrixAssess_Service { get; set; }
    }

    public class AuthorityMatrixsAssessment
    {
        public long AM_Assess_ID { get; set; }
        public string AM_Assess_Name { get; set; }
        public bool AM_Assess_Type { get; set; }
        public long AM_Assess_AmountFrom { get; set; }
        public long AM_Assess_AmountTo { get; set; }
        public long AM_Assess_AssignUser { get; set; }

        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public long Service_Count { get; set; }

       
    }
}
