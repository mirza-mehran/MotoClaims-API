using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.ScheduledCallsAndChat
{
   public class ScheduledCalls
    {
        public long SC_Id { get; set; }
        public long ClaimId { get; set; }
        public long PolicyId { get; set; }
        public DateTime Date { get; set; }
        public string Participants { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public long AssignedTo { get; set; }
        public long Status { get; set; }
        public string Remarks { get; set; }
        public string Comment { get; set; }
        public string ChannelId { get; set; }
        public string UserName { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
   }

    public class ScheduledCallsList
    {
        public long SC_Id { get; set; }
        public string PolicyNo { get; set; }
        public long ClaimId { get; set; }
        public string ImageUrl { get; set; }
        public string PolicyHolderName { get; set; }
        public string MakeName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Date { get; set; }
        public string ChannelId { get; set; }
    }
}
