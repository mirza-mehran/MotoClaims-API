using MotoClaims.Entities.Surveyours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
    public interface ISurveyorService
    {
        string InsertSurveyourClaimsAssigned(ClaimsSurveyours obj, long tenantId, long userId);
    }
}
