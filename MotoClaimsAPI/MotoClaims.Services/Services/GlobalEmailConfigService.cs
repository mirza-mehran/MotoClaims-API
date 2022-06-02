using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
using MotoClaims.Entities.GlobelSetting;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class GlobalEmailConfigService : IGlobalEmailConfigService
    {
        public SMTP_Configuration GetSMTP_Configuration(long tenantId)
        {
            SMTP_Configuration  obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    GlobalEmailConfigRepository myRepository = new GlobalEmailConfigRepository(unitOfWork);
                    obj = myRepository.GetSMTP_Configuration(tenantId);
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

        public string UpdateSMTP_Configuration(SMTP_Configuration model, long tenantId, long userId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    GlobalEmailConfigRepository myRepository = new GlobalEmailConfigRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model,"SMTP/Timezone Management", "SMTP_Configuration", "SMTPConfig_ID", model.SMTPConfig_ID, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.UpdateSMTP_Configuration(model, tenantId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Update SMTP/Timezone Management", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                    //throw;
                }
            }
            return ErrorMessage;
        }

        public IEnumerable<TimeZoneConfig> GetTimeZone(long tenantId)
        {
            IEnumerable<TimeZoneConfig> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    GlobalEmailConfigRepository myRepository = new GlobalEmailConfigRepository(unitOfWork);
                    obj = myRepository.GetTimeZone(tenantId);
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

        public GlobalEmailConfiguration GetGlobelEmail_Signature(long tenantId)
        {
            GlobalEmailConfiguration obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    GlobalEmailConfigRepository myRepository = new GlobalEmailConfigRepository(unitOfWork);
                    obj = myRepository.GetGlobelEmail_Signature(tenantId);
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

        public string InsertGlobelEmail_Signature(GlobalEmailConfiguration model, long tenantId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    GlobalEmailConfigRepository myRepository = new GlobalEmailConfigRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Add Email Signature", "GlobalEmailConfiguration", "GlobalEmail_ID", model.GlobalEmail_ID, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.InsertGlobelEmail_Signature(model, tenantId, out  bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Email Signature", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }

            return ErrorMessage;
        }

        public string UpdateGlobelEmail_Signature(GlobalEmailConfiguration model, long tenantId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    GlobalEmailConfigRepository myRepository = new GlobalEmailConfigRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Edit Email Signature", "GlobalEmailConfiguration", "GlobalEmail_ID", model.GlobalEmail_ID, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.UpdateGlobelEmail_Signature(model, tenantId, out bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Edit Email Signature", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }
    }
}
