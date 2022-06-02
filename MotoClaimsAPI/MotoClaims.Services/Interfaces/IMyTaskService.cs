using MotoClaims.Entities.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IMyTaskService
    {
        List<MyTaskWithListName> GetClaimsByUserId(long Id, long tenentId, long userId);
        List<MyTaskWithListName> GetPendingTaskClaimsByUserId(long Id, long tenentId, long userId);

    }
}
