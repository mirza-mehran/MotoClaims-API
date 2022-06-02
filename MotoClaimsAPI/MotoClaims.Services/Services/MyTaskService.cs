using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Claim;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class MyTaskService : IMyTaskService
    {
        public List<MyTaskWithListName> GetClaimsByUserId(long Id, long tenentId, long userId)
        {
            List<MyTaskWithListName> Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    MyTaskRepository myRepository = new MyTaskRepository(unitOfWork);
                    Claims = myRepository.GetClaimsByUserId(Id,tenentId,userId);
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

        public List<MyTaskWithListName> GetPendingTaskClaimsByUserId(long Id, long tenentId, long userId)
        {
            List<MyTaskWithListName> Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    MyTaskRepository myRepository = new MyTaskRepository(unitOfWork);
                    Claims = myRepository.GetPendingTaskClaimsByUserId(Id, tenentId, userId);
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
    }
}
