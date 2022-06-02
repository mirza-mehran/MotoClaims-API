using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.ScheduledCallsAndChat
{
   public class ScheduledCallsAssigned
   {
        public long ClaimId { get; set; }
        public string TimeSlotUser { get; set; }
        public TimeSpan TimeSlot { get; set; }
        public DateTime TimeSlotDate { get; set; }
   }
}
