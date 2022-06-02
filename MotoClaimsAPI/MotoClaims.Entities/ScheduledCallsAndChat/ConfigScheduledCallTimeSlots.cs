using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.ScheduledCallsAndChat
{
   public class ConfigScheduledCallTimeSlots
    {
        public long CSCTS_Id { get; set; }
        public long IntervalTime { get; set; }
        public long IntervalGap { get; set; }
        public string WeekDay1 { get; set; }
        public string WeekDay2 { get; set; }
        public TimeSpan DayStartTime { get; set; }
        public TimeSpan DayEndTime { get; set; }
        public bool EnableLiveCall { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long IsDeleted { get; set; }
        public long IsActive { get; set; }
    }
}
