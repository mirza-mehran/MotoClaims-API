using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.VehicleParts
{
   public class Parts
    {
        //public long PartId { get; set; }
        public long? ModelId { get; set; }
        public long? MakeId { get; set; }
        public string PartName { get; set; }
        public string PartDetails { get; set; }
        public string OEMNumber { get; set; }
        public string ArticleNumber { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        //public long CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public long UpdatedBy { get; set; }
        //public DateTime UpdatedDate { get; set; }
        //public bool IsDeleted { get; set; }
        //public bool IsActive { get; set; }
        public bool HasError { get; set; }
        public List<ErrorList> ErrorList { get; set; }
    }
    public class ErrorList
    {
        public string ErrorType { get; set; }
        public string FiledName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
