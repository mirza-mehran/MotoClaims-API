using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.ScheduledCallsAndChat;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class ScheduledCallsAndChatService : IScheduledCallsAndChatService
    {
        public string CancelCall(long SC_Id, long tenentId, long userId)
        {
            string message = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ScheduledCallsAndChatRepository myRepository = new ScheduledCallsAndChatRepository(unitOfWork);
                    GeneralSettings.ActivityLog(userId, "Update ScheduledCalls", "ScheduledCalls", "SC_Id", userId, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenentId);
                    message = myRepository.CancelCall( SC_Id,tenentId,userId);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Update ScheduledCalls", GeneralSettings.ToXML(userId), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenentId);
                }
            }
            return message;
        }

        public ConfigScheduledCallTimeSlots GetConfigScheduledCallTimeSlots(long tenentId, long userId)
        {
            ConfigScheduledCallTimeSlots obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ScheduledCallsAndChatRepository myRepository = new ScheduledCallsAndChatRepository(unitOfWork);
                    obj = myRepository.GetConfigScheduledCallTimeSlots(tenentId,userId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<ScheduledCalls> GetMBScheduledCalls(long civilId, long tenantId, long userId)
        {
            IEnumerable<ScheduledCalls> obj = null;
            using (Repositories dalSession=new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                try
                {
                    ScheduledCallsAndChatRepository scheduledCallsAndChatRepository = new ScheduledCallsAndChatRepository(unitOfWork);
                    obj = scheduledCallsAndChatRepository.GetMBScheduledCalls(civilId,tenantId,userId);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return obj;
        }

        public IEnumerable<ScheduledCallExceptionSlots> GetScheduledCallExceptionSlots(long tenentId, long userId)
        {
            IEnumerable<ScheduledCallExceptionSlots> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ScheduledCallsAndChatRepository myRepository = new ScheduledCallsAndChatRepository(unitOfWork);
                    obj = myRepository.GetScheduledCallExceptionSlots(tenentId , userId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<ScheduledCallsList> GetScheduledCalls(long tenentId, long userId)
        {
            IEnumerable<ScheduledCallsList> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ScheduledCallsAndChatRepository myRepository = new ScheduledCallsAndChatRepository(unitOfWork);
                    obj = myRepository.GetScheduledCalls(tenentId, userId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }



        public string InsertScheduledCalls(ScheduledCalls obj, long tenentId, long userId)
        {
            string message = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ScheduledCallsAndChatRepository myRepository = new ScheduledCallsAndChatRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add ScheduledCalls", "ScheduledCalls", "SC_Id", obj.SC_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenentId);
                    message = myRepository.InsertScheduledCalls( obj,  tenentId,  userId);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add ScheduledCalls", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenentId);
                }
            }
            return message;
        }

        //public string InsertScheduledCallsAssigned(ScheduledCallsAssigned obj, long tenentId, long userId)
        //{
        //    string message = null;
        //    using (Repositories dalSession = new Repositories())
        //    {
        //        UnitOfWork unitOfWork = dalSession.UnitOfWork;
        //        unitOfWork.Begin();
        //        try
        //        {
        //            ScheduledCallsAndChatRepository myRepository = new ScheduledCallsAndChatRepository(unitOfWork);
        //            GeneralSettings.ActivityLog(obj, "Update Claims ScheduledCallsAssigned", "Claims", "ClaimId", obj.ClaimId, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenentId);
        //            message = myRepository.InsertScheduledCallsAssigned(obj,  tenentId,  userId);

        //            unitOfWork.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            unitOfWork.Rollback();
        //            GeneralSettings.LogError(ex, "Update Claims ScheduledCallsAssigned", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenentId);
        //        }
        //    }
        //    return message;
        //}

        public string UpdateScheduledCalls(ScheduledCalls obj, long tenentId, long userId)
        {
            string message = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ScheduledCallsAndChatRepository myRepository = new ScheduledCallsAndChatRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Update ScheduledCalls", "ScheduledCalls", "SC_Id", obj.SC_Id, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenentId);
                    message = myRepository.UpdateScheduledCalls(obj, tenentId, userId);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Update ScheduledCalls", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenentId);
                }
            }
            return message;
        }
    }
}
