using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.CarReplacement;
using MotoClaims.Entities.Claim;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class CarReplacementService : ICarReplacementService
    {
        public IEnumerable<Status_Master> GetClaimStatus_Master(long Id, long tenentId, long userId)
        {
            IEnumerable<Status_Master> Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                try
                {
                    CarReplacementRepository myRepository = new CarReplacementRepository(unitOfWork);
                    Claims = myRepository.GetClaimStatus_Master(Id, tenentId, userId);
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return Claims;
        }

        public string InsertReplacementCarAssigned(ReplacementCarAssigned obj, long tenentId, long userId)
        {
            string message = null;
            using (Repositories dalSession=new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CarReplacementRepository myrepository = new CarReplacementRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add ReplacementCarAssigned", "ReplacementCarAssigned", "RCA_Id",obj.RCA_Id,userId,GeneralSettings.CRUD.INSERT.ToString(),unitOfWork,tenentId);
                    message = myrepository.InsertReplacementCarAssigned(obj, tenentId,userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {

                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add ReplacementCarAssigned", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(),userId,tenentId);
                }
            }
            return message;

            
        }

        public string InsertReplacementCarRequests(ReplacementCarRequests obj, long tenentId, long userId)
        {
            string message = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CarReplacementRepository myRepository = new CarReplacementRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add ReplacementCarRequests", "ReplacementCarRequests", "RCR_Id", obj.RCR_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenentId);
                    message = myRepository.InsertReplacementCarRequests( obj, tenentId,  userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add ReplacementCarRequests", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenentId);
                }
            }
            return message;
        }
    }
}
