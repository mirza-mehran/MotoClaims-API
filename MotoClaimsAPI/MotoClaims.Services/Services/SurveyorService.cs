using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Surveyours;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class SurveyorService : ISurveyorService
    {
        public string InsertSurveyourClaimsAssigned(ClaimsSurveyours obj, long tenantId, long userId)
        {
            string message = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    SurveyorRepository myRepository = new SurveyorRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add ClaimsSurveyours", "ClaimsSurveyours", "CS_Id", obj.CS_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    message = myRepository.InsertSurveyourClaimsAssigned( obj,  tenantId,  userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add ClaimsSurveyours", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                }
            }

            return message;
        }
    }
}
