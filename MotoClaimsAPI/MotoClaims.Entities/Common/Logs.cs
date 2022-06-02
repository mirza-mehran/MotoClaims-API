using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Common
{
    public class ErrorLogs
    {
        public long Id { get; set; }
        [DisplayName("Error Source")]
        public string Source { get; set; }
        [DisplayName("API Name")]
        public string ApiName { get; set; }
        [DisplayName("Activity Name")]
        public string ActivityId { get; set; }
        [DisplayName("Request Object")]
        public string Requestobject { get; set; }
        [DisplayName("Error Message")]
        public string ErrorMsg { get; set; }
        [DisplayName("Error Details")]
        public string ErrorDetails { get; set; }
        [DisplayName("Stack Trace")]
        public string StackTrace { get; set; }
        [DisplayName("Error Time")]
        public DateTime ErrorTime { get; set; }
        [DisplayName("Created By")]
        public long CreatedBy { get; set; }
        [DisplayName("Updated By")]
        public long UpdatedBy { get; set; }
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Updated On")]
        public DateTime UpdatedOn { get; set; }
        [DisplayName("Date & Time")]
        public string ErrorTimeString { get; set; }
        public DateTime? Createdon { get; set; }
    }


    public class ActivityLog : ActivityDetail
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        [DisplayName("User ID")]
        public long UserId { get; set; }
        [DisplayName("Activity")]
        public string ActivityId { get; set; }
        [DisplayName("Activity Time")]
        public DateTime ActivityTime { get; set; }
        [DisplayName("Entity")]
        public string EntityReference { get; set; }
        [DisplayName("Created By")]
        public long CreatedBy { get; set; }
        [DisplayName("Updated By")]
        public long UpdatedBy { get; set; }
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Updated On")]
        public DateTime UpdatedOn { get; set; }
        [DisplayName("Date & Time")]
        public string ActivityTimeString { get; set; }
        public DateTime? Createdon { get; set; }


    }

    public class ActivityDetail
    {
        [DisplayName("Id")]
        public long Id { get; set; }
        public long AuditId { get; set; }
        [DisplayName("Edit Field")]
        public string FieldName { get; set; }
        [DisplayName("Field Old Value")]
        public string OldValue { get; set; }
        [DisplayName("Field New Value")]
        public string NewValue { get; set; }
        public string ScreenName { get; set; }
    }
    public class ActivityLogTotalRecords
    {
        public virtual IEnumerable<ActivityLog> ActivityLogs { get; set; }
        public long TotalRecords { get; set; }
    }

    public class ActivityDetailsTotalRecords
    {
        public virtual IEnumerable<ActivityDetail> ActivityDetail { get; set; }
        public long TotalRecords { get; set; }
    }
}
