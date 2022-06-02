using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities
{
   public class Tenants
    {
        public long Id { get; set; }
        public string TenantName { get; set; }
        public Guid TenantKey { get; set; }
        public string TenantSchema { get; set; }
        public string TenantDBUserName { get; set; }
        public string TenantDBUserPassword { get; set; }
        public string TenantDBServer { get; set; }
        public string TenantDBname { get; set; }
        public string TenantLogoPath { get; set; }
        public string TenantPrimaryPersonName { get; set; }
        public string TenantPrimaryPersonPassword { get; set; }
        public string TenantPrimaryPersonPosition { get; set; }
        public string TenantPrimaryPersonEmail { get; set; }
        public long TenantPrimaryPersonCountry { get; set; }
        public string TenantPrimaryPersonPhone { get; set; }
        public DateTime TenantActivationDate { get; set; }
        public int TenantStatus { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public UploadImage ImageModel { get; set; }

    }
}
