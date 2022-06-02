using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities
{
   public class TimeZoneConfig
    {
        public int TimeZoneConfig_ID { get; set; }
        public string TimeZone { get; set; }
        public string TimeZone_Offset { get; set; }
        public string TimeZone_Abbrevation { get; set; }
        public string TimeZone_AltName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
