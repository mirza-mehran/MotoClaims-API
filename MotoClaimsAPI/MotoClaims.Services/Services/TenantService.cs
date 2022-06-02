using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
using MotoClaims.Entities.Provider;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class TenantService : ITenantService
    {
        public Tenants GetTenantsByID(long tenantId)
        {
            Tenants obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    TenantRepository myRepository = new TenantRepository(unitOfWork);
                    obj = myRepository.GetTenantsByID(tenantId);
                    // unitOfWork.Commit();
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

        public IEnumerable<ProviderCountry> GetCountry(long tenantId)
        {
            IEnumerable<ProviderCountry> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    TenantRepository myRepository = new TenantRepository(unitOfWork);
                    obj = myRepository.GetCountry(tenantId);
                    // unitOfWork.Commit();
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

        public bool Update_Tenant(Tenants model, long tenantId,long userId)
        {
            // string ErrorMessage = null;
            bool bit = false;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    TenantRepository myRepository = new TenantRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "User Account", "Tenants", "Id", model.Id, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);
                    bit = myRepository.Update_Tenant(model, tenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "User Account", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }
            return bit;
        }
    }
}
