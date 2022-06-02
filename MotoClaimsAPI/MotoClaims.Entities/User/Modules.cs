using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.User
{
    public class Modules
    {
        public long Id { get; set; }
        public long AccessGroupsId { get; set; }
        public string ModuleSystemName { get; set; }
        public string ModuleMenuName { get; set; }
        public string ModuleType { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long TenantId { get; set; }
    }
}
