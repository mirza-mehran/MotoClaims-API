using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.GlobelSetting
{
   public class SMTP_Configuration
    {
        public long SMTPConfig_ID { get; set; }
        public string HostName { get; set; }
        public int PortNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool SSL_Enabled { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public long TimeZone_ID { get; set; }
    }
}
