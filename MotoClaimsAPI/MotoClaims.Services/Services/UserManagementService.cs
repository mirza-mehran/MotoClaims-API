using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
using MotoClaims.Entities.User;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class UserManagementService : IUserManagementService
    {
        public bool DeleteUserProfile(int userProfileId, long tenantId)
        {
            bool obj = false;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    GeneralSettings.ActivityLog(userProfileId, "Delete UserProfile", "UserProfile", "UserId", userProfileId, userProfileId, GeneralSettings.CRUD.DELETE.ToString(), unitOfWork, tenantId);

                    obj = myRepository.DeleteUserProfile(userProfileId, tenantId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Delete UserProfile", GeneralSettings.ToXML(userProfileId), GeneralSettings.CRUD.DELETE.ToString(), userProfileId, tenantId);
                    obj = false;
                }
            }
            return obj;
        }
        public IEnumerable<Roles> GetRoles(long tenantId)
        {
            IEnumerable<Roles> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetRoles(tenantId);
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
        public IEnumerable<Modules> GetModules(long id, long accessGroupsId, long tenantId)
        {
            IEnumerable<Modules> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetModules(id, accessGroupsId, tenantId);
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
        public IEnumerable<AccessGroups> GetAccessGroups(long id, long roleId, long tenantId)
        {
            IEnumerable<AccessGroups> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetAccessGroups(id, roleId, tenantId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }
        public IEnumerable<AccessGroupsModules> GetAccessGroupsModules(long id, long moduleId, long accessGroupId, long tenantId)
        {
            IEnumerable<AccessGroupsModules> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetAccessGroupsModules(id, moduleId, accessGroupId, tenantId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }
        public IEnumerable<UserProfile> GetUserProfileByID(long userProfileId, long tenantId)
        {
            IEnumerable<UserProfile> UserProfiles = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    UserProfiles = myRepository.GetUserProfileByID(userProfileId, tenantId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return UserProfiles;
        }

        public IEnumerable<UserProfile> GetUserProfiles(long id, long roleId, long tenantId)
        {
            IEnumerable<UserProfile> UserProfiles = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    UserProfiles = myRepository.GetAllUserProfiles(id, roleId, tenantId);
                    foreach (var item in UserProfiles)
                    {
                        item.AccessGroupIds = myRepository.GetUserAccessGroups(item.UserId, tenantId).ToList();
                    }
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return UserProfiles;
        }

        public bool InsertUserProfile(UserProfile model, long tenentId, out string errMessage)
        {
            bool obj = false;
            errMessage = string.Empty;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;

                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    UserProfile user = myRepository.GetUserDetailsExist(model.UserId, model.UserName, model.Email, tenentId);
                    if (user != null && user.UserId > 0)
                    {
                        errMessage = "User Already Exist!";
                    }
                    else
                    {

                        unitOfWork.Begin();
                        GeneralSettings.ActivityLog(model, "User Profile", "UserProfile", "UserId", model.UserId, model.CreatedBy, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork);
                        obj = myRepository.InsertUserProfile(model, tenentId);
                        //GeneralSettings.ActivityLog(model, "User Profile", "UserProfile", "UserId", model.UserId, model.CreatedBy, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork);
                        foreach (var id in model.AccessGroupIds)
                        {
                            myRepository.InsertUserAccessGroups(id.AccessGroupId, model.UserId, model.CreatedBy, tenentId);
                        }
                        unitOfWork.Commit();
                       
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(errMessage))
                        unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "SaveUserProfile", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), model.CreatedBy);
                    obj = false;
                    //throw;
                }
            }
            return obj;
        }

        public bool UpdateUserProfile(UserProfile model, long tenentId, out string errMessage)
        {
            bool obj = false;
            errMessage = string.Empty;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;

                try
                {
                   
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    UserProfile user = myRepository.GetUserDetailsExist(model.UserId, model.UserName, model.Email, tenentId);
                    if (user != null && user.UserId > 0)
                    {
                        errMessage = "User Already Exist!";
                    }
                    else
                    {
                        unitOfWork.Begin();
                        GeneralSettings.ActivityLog(model, "User Profile", "UserProfile", "UserId", model.UserId, model.UpdatedBy, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork);
                        obj = myRepository.UpdateUserProfile(model, tenentId);
                        myRepository.DeleteUserAccessGroups(model.UserId, model.UpdatedBy, tenentId);
                        foreach (var id in model.AccessGroupIds)
                        {
                            myRepository.InsertUserAccessGroups(id.AccessGroupId, model.UserId, model.CreatedBy, tenentId);
                        }
                        unitOfWork.Commit();
                    }

                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(errMessage))
                        unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "UpdateUserProfile", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), model.UpdatedBy);
                    obj = false;
                    //throw;
                }
            }
            return obj;
        }


        public bool UpdateUserAcProfile(UserProfile model, long tenentId, out string errMessage)
        {
            bool obj = false;
            errMessage = string.Empty;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;

                try
                {

                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    UserProfile user = myRepository.GetUserDetailsExist(model.UserId, model.UserName, model.Email, tenentId);
                    if (user != null && user.UserId > 0)
                    {
                        errMessage = "User Already Exist!";
                    }
                    else
                    {
                        unitOfWork.Begin();
                        GeneralSettings.ActivityLog(model, "User Profile", "UserProfile", "UserId", model.UserId, model.UpdatedBy, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork);
                        obj = myRepository.UpdateUserProfile(model, tenentId);
                        unitOfWork.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(errMessage))
                        unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "UpdateUserProfile", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), model.UpdatedBy);
                    obj = false;
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<UserProfile> GenericSearch(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId)
        {
            IEnumerable<UserProfile> UserProfiles = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    UserProfiles = myRepository.GenericSearch(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    TotalRecord = 0;
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return UserProfiles;
        }

        public IEnumerable<Actions> GetActions(long id, long tenantId)
        {
            IEnumerable<Actions> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetActions(id, tenantId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<ModuleActions> GetModuleActions(long id, long moduleId, long tenantId)
        {
            IEnumerable<ModuleActions> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetModuleActions(id, moduleId, tenantId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<AccessGroupModuleActions> GetAccessGroupModuleActions(long id, long accessGroupId, long tenantId)
        {
            IEnumerable<AccessGroupModuleActions> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetAccessGroupModuleActions(id, accessGroupId, tenantId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public bool ModifyAccessGroupModuleActions(AccessGroupModuleActionsVM obj, TransactionType tranType, long user, long tenantId)
        {
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    if (tranType == TransactionType.insert)
                        GeneralSettings.ActivityLog(obj, "Access Groups", "AccessGroupModuleActions", "AccessGroupId", obj.AccessGroupId, user, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork);
                    else if (tranType == TransactionType.update)
                        GeneralSettings.ActivityLog(obj, "Access Groups", "AccessGroupModuleActions", "AccessGroupId", obj.AccessGroupId, user, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork);
                    int res = myRepository.ModifyAccessGroup(obj, tranType, user, tenantId);
                    
                    if (res > 0)
                    {
                        if (tranType != TransactionType.delete)
                        {
                            myRepository.DeleteAccessGroupModuleActions(obj.AccessGroupId, 0, user, tenantId);
                            foreach (var module in obj.Modules)
                            {
                                foreach (var action in module.Actions)
                                {
                                    myRepository.InsertAccessGroupModuleActions(obj.AccessGroupId, module.ModuleId, action.ActionId, user, tenantId);
                                }
                            }
                        }
                        else
                        {
                            myRepository.DeleteAccessGroupModuleActions(obj.AccessGroupId, 0, user, tenantId);
                        }
                    }
                    unitOfWork.Commit();
                   
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    if (tranType == TransactionType.insert)
                        GeneralSettings.LogError(ex, "SaveAccessGroup", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), user);
                    else if (tranType == TransactionType.update)
                        GeneralSettings.LogError(ex, "UpdateAccessGroup", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), user);
                    return false;
                    //throw;
                }
            }
            return true;
        }

        public UserProfile GetUserLoginDetails(string UserName)
        {
            UserProfile obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetUserLoginDetails(UserName);
                    if (obj != null && obj.UserId > 0)
                    {
                        obj.UserModules = myRepository.GetUserPermissions(obj.UserId, obj.TenantId).ToList();
                        obj.AccessGroupIds = myRepository.GetUserAccessGroups(obj.UserId, obj.TenantId).ToList();
                    }
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


        public UserProfile GetUserLoginDetail(long userId)
        {
            UserProfile obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetUserLoginDetail(userId);

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

        public bool UpdateUserAccount(UserProfile model, long tenentId, out string errMessage)
        {
            bool obj = false;
            errMessage = string.Empty;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;

                try
                {

                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    UserProfile user = myRepository.GetUserDetailsExist(model.UserId, model.UserName, model.Email, tenentId);
                    if (user != null && user.UserId > 0)
                    {
                        errMessage = "User Already Exist!";
                    }
                    else
                    {
                        unitOfWork.Begin();
                        GeneralSettings.ActivityLog(model, "User Profile", "UserProfile", "UserId", model.UserId, model.UpdatedBy, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork);
                        obj = myRepository.UpdateUserAccount(model, tenentId);
                        unitOfWork.Commit();
                    }

                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(errMessage))
                        unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "UpdateUserProfile", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), model.UpdatedBy);
                    obj = false;
                    //throw;
                }
            }
            return obj;
        }
        //public bool UpdateAccessGroupModuleActions(AccessGroupModuleActionsVM obj, long user, long tenantId)
        //{
        //    bool recordsAffected = false;
        //    using (Repositories dalSession = new Repositories())
        //    {
        //        UnitOfWork unitOfWork = dalSession.UnitOfWork;
        //        unitOfWork.Begin();
        //        try
        //        {
        //            UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
        //            int res = myRepository.ModifyAccessGroup(obj, TransactionType.insert, user, tenantId);
        //            myRepository.DeleteAccessGroupModuleActions(accessGroupId, moduleId, user, tenantId);
        //            int res = myRepository.InsertAccessGroupModuleActions(accessGroupId, moduleId, actionId, user, tenantId);
        //            if (res > 0)
        //                recordsAffected = true;
        //            unitOfWork.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            unitOfWork.Rollback();
        //            //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
        //            //throw;
        //        }
        //    }
        //    return recordsAffected;
        //}

        public bool MBInsertUserProfile(UserProfile model, long tenentId, out string errMessage)
        {
            bool obj = false;
            errMessage = string.Empty;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;

                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    UserProfile user = myRepository.GetUserDetailsExist(model.UserId, model.UserName, model.Email, tenentId);
                    if (user != null && user.UserId > 0)
                    {
                        errMessage = "User Already Exist!";
                    }
                    else
                    {

                        unitOfWork.Begin();
                        GeneralSettings.ActivityLog(model, "User Profile", "UserProfile", "UserId", model.UserId, model.CreatedBy, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork);
                        obj = myRepository.MBInsertUserProfile(model, tenentId);
                        unitOfWork.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(errMessage))
                        unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "SaveUserProfile", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), model.CreatedBy);
                    obj = false;
                    //throw;
                }
            }
            return obj;
        }

        public UserProfile GetUserAlreadyExist(long tenantId, string userName, string email)
        {
            UserProfile obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                try
                {
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    obj = myRepository.GetUserAlreadyExist( tenantId,  userName,  email);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return obj;
        }

        public string InsertUserProfileFCMToken(long tenentId, long userId, string FCMToken)
        {
            string Message = string.Empty;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                try
                {
                    unitOfWork.Begin();
                    UserManagementRepository myRepository = new UserManagementRepository(unitOfWork);
                    GeneralSettings.ActivityLog(FCMToken, "Add FCM Token ", "UserProfile", "UserId", userId, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork);
                    Message = myRepository.InsertUserProfileFCMToken(tenentId, userId, FCMToken);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    GeneralSettings.LogError(ex, "Add FCM Token ", GeneralSettings.ToXML(FCMToken), GeneralSettings.CRUD.INSERT.ToString(), userId);
                    throw ex;
                }
            }
            return Message;
        }
    }
}
