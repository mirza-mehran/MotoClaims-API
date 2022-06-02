using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.User
{
    public class AccessGroupModuleActions
    {
        public long Id { get; set; }
        public long AccessGroupId { get; set; }
        public long ModuleId { get; set; }
        public long ActionId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long TenantId { get; set; }
    }
    public class AccessGroupModuleActionsVM
    {
        public long AccessGroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDetails { get; set; }
        public string GroupType { get; set; }
        public bool IsActive { get; set; }
        public long InheritAccessGroupId { get; set; }
        public List<ModulesVM> Modules { get; set; }
    }
    public class ModulesVM
    {
        public long ModuleId { get; set; }
        public List<ActionsVM> Actions { get; set; }
    }
    public class ActionsVM
    {
        public long ActionId { get; set; }
    }
}
