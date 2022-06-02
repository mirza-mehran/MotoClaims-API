using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.ScheduledCallsAndChat
{
  public  class ScheduledCallsChats
    {
        public long SCC_Id { get; set; }
        public long ScheduledId { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public DateTime Date { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ReadDate { get; set; }
        public long IsRead { get; set; }
        public long IsDelivered { get; set; }
        public long TypeOdMessage { get; set; }
        public string MessageText { get; set; }
        public long Status { get; set; }
        public string FilePath { get; set; }
        public string JsonExtended { get; set; }
        public string Comment { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
