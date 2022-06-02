using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Provider
{
   public class Provider_Services_Contract
    {
        public long PSC_Id { get; set; }
        //[Required(ErrorMessage = "The ProviderService Id field is required.")]
        //[RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)",ErrorMessage = "The {0} 0 isn't valid")]
        public long ProviderService_Id { get; set; }
        //[Required(ErrorMessage = "The ServiceType Id field is required.")]
        //[RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = "The {0} 0 isn't valid")]
        public long ServiceType_Id { get; set; }
        [Required(ErrorMessage = "The PSC_Code field is required.")]
        public string PSC_Code { get; set; }
        public string PSC_Description { get; set; }
        [Required(ErrorMessage = "The Provider Id field is required.")]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = "The {0} 0 isn't valid")]
        public long Provider_Id { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
       // public  PS_Prices PS_Prices { get; set; }
    }

    public class PS_Prices
    {
        public long PS_Price_Id { get; set; }
        public long PSC_Id { get; set; }
        //[Required(ErrorMessage = "The Make Id field is required.")]
        //[RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = "The {0} Id 0 isn't valid")]
        public long Make { get; set; }
        public string MakeName { get; set; }
        //[Required(ErrorMessage = "The Model Id field is required.")]
        //[RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = "The {0} Id 0 isn't valid")]
        public long Model { get; set; }
        public string ModelName { get; set; }
        public long Year { get; set; }
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = "The {0} Id 0 isn't valid")]
        [Required(ErrorMessage = "The Price field is required.")]
        public decimal Price { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal Discount { get; set; }
        public long TenantId { get; set; }
        public string Remark { get; set; }
    }

    public class ModelProviderContractServices
    {
        public long PSC_Id { get; set; }
        public string PSC_Code { get; set; }
        public long ServiceId { get; set; }
        public string Service { get; set; }
        public string PSC_Description { get; set; }
        public long ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public long Year { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
    }

    public class ModelProvider_Services_Contract
    {
        public long PSC_Id { get; set; }
        public long Provider_Id { get; set; }
        public long ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public long ServiceId { get; set; }
        public string Service { get; set; }
        public string PSC_Code { get; set; }
        public string PSC_Description { get; set; }
    }

    public class ModelPS_Prices
    {
        public long PS_Price_Id { get; set; }
        public long PSC_Id { get; set; }
        public long MakeId { get; set; }
        public string MakeName { get; set; }
        public long ModelId { get; set; }
        public string ModelName { get; set; }
        public long Year { get; set; }
        public decimal Price { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal Discount { get; set; }
        public long TenantId { get; set; }
        public string Remark { get; set; }
    }
}
