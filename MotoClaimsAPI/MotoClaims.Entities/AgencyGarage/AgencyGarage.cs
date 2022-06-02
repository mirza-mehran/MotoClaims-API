using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.AgencyGarage
{
    public class AgencyGarage
    {
        public long AgencyGarageId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string AssignedClaims { get; set; }
        public long Pending { get; set; }
        public long Under_Repair { get; set; }
        public long Closed { get; set; }
        public string POCName { get; set; }
        public long RowCounts { get; set; }
    }

    public class AgencyGarageWithCount
    {
        public IEnumerable<AgencyGarage> AgencyGarage { get; set; }
        public long Count { get; set; }
    }
}
