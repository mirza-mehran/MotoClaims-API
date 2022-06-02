using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.ScheduledCallsAndChat
{
  public class ScheduledCallExceptionSlots
  {
        public long SCES_Id { get; set; }
        public DateTime Date { get; set; }
        public long LimitOfSlots { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long IsDeleted { get; set; }
        public long IsActive { get; set; }
  }
}
