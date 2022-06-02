using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.User
{
    public class AccessGroups
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string GroupName { get; set; }
        public string GroupDetails { get; set; }
        public string GroupType { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long TenantId { get; set; }
        public bool IsDefault { get; set; }
        public long InheritAccessGroupId { get; set; }
    }
}
