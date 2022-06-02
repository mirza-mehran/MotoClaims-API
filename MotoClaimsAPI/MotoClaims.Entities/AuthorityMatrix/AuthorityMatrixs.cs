using MotoClaims.Entities.JsonObject;
using MotoClaims.Entities.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.AuthorityMatrix
{
   public class AuthorityMatrixs
    {
        public long AM_Assign_ID { get; set; }
        public string AM_Assign_Name { get; set; }
        public string AM_Assign_Details { get; set; }
        public string AM_Assign_MakeID { get; set; }
        public string AM_Assign_ModelID { get; set; }
        public long AM_Assign_YearFrom { get; set; }
        public long AM_Assign_YearTo { get; set; }
        public string AM_Assign_Product { get; set; }
        public long AM_Assign_RepairOption { get; set; }
        public long AM_Assign_ToUser { get; set; }

        public string UserName { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public List<JsonObjectModels> Makes { get; set; }
        public List<JsonObjectModels> Models { get; set; }
    }



}
