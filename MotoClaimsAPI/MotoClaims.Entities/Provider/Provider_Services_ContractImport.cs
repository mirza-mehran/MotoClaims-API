using MotoClaims.Entities.VehicleParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Provider
{
   public class Provider_Services_ContractImport
    {
        public string PSC_Code { get; set; }
        public string PSC_Description { get; set; }
        public long ProviderService_Id { get; set; }
        public long ServiceType_Id { get; set; }
        public long Provider_Id { get; set; }

        public bool HasError { get; set; }
        public List<ErrorList> ErrorList { get; set; }
    }


    public class Provider_ServicesPrices_Import
    {
        public long PSC_Id { get; set; }
        public long MakeId { get; set; }
        public long ModelId { get; set; }
        public long Year { get; set; }
        public decimal Price { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal Discount { get; set; }
        public string Remark { get; set; }

        public bool HasError { get; set; }
        public List<ErrorList> ErrorList { get; set; }
    }

}
