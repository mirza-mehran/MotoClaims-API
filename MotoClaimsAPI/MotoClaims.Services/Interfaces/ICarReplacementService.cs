using MotoClaims.Entities.CarReplacement;
using MotoClaims.Entities.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface ICarReplacementService
   {
        string InsertReplacementCarRequests(ReplacementCarRequests obj, long tenentId, long userId);
        string InsertReplacementCarAssigned(ReplacementCarAssigned obj, long tenentId, long userId);
        IEnumerable<Status_Master> GetClaimStatus_Master(long Id, long tenentId, long userId);
   }
}
