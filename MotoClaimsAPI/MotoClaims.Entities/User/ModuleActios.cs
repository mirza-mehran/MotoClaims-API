using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.User
{
    public class ModuleActions
    {
        public long Id { get; set; }
        public long ModuleId { get; set; }
        public long ActionId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long TenantId { get; set; }
        public List<Actions> Actions { get; set; }
        //public Modules Module { get; set; }
        //public Actions MyProperty { get; set; }
    }
}
