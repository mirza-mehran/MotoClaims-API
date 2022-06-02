using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.User;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        public IEnumerable<UserProfiles> GetEmails(string Email, long tenantId)
        {
            IEnumerable<UserProfiles> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ForgotPasswordRepository myRepository = new ForgotPasswordRepository(unitOfWork);
                    obj = myRepository.GetEmails(Email,tenantId);
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public bool UpdateUserAccount(updatePasswordUser model, long tenentId, out string errMessage)
        {
            bool obj = false;
            errMessage = string.Empty;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;

                try
                {

                    ForgotPasswordRepository myRepository = new ForgotPasswordRepository(unitOfWork);
                    unitOfWork.Begin();
                    GeneralSettings.ActivityLog(model, "User Profile", "UserProfile", "UserId",Convert.ToInt64(model.UserId),Convert.ToInt64(model.UserId), GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork);
                    obj = myRepository.UpdateUserAccount(model, tenentId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(errMessage))
                        unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "UpdateUserProfile", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(),Convert.ToInt64(model.UserId));
                    obj = false;
                    //throw;
                }
            }
            return obj;
        }
    }
}
