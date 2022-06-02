using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Policy
{
   public class VehicleColour
    {
        public long Id { get; set; }
        public string ColourName { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
