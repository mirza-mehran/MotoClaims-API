using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Claim
{
   public class ClaimActions
    {
        public long ClaimAction_Id { get; set; }
        public string Actor { get; set; }
        public long Claim_Status_Id { get; set; }
        public string Claim_Status { get; set; }
        public string PrimaryAction { get; set; }
        public string OtherAction { get; set; }
    }
}
