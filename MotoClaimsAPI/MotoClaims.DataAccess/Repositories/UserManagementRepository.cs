using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MotoClaims.Entities;

namespace MotoClaims.DataAccess.Repositories
{
    public class UserManagementRepository
    {
        public UserManagementRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;
        public IEnumerable<UserProfile> GetAllUserProfiles(long id, long roleId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", id);
            dbParams.Add("RoleId", roleId);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<UserProfile>("usp_GetUserProfiles", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<Roles> GetRoles(long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<Roles>("usp_GetRoles", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<Modules> GetModules(long id, long accessGroupsId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", id);
            dbParams.Add("AccessGroupsId", accessGroupsId);
            dbParams.Add("TenantId", tenantId);
            return  unitOfWork.Connection.Query<Modules>("usp_GetModules", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<AccessGroups> GetAccessGroups(long id, long roleId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", id);
            dbParams.Add("RoleId", roleId);
            dbParams.Add("TenantId", tenantId);
            return  unitOfWork.Connection.Query<AccessGroups>("usp_GetAccessGroups", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<AccessGroupsModules> GetAccessGroupsModules(long id, long moduleId, long accessGroupId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", id);
            dbParams.Add("ModuleId", moduleId);
            dbParams.Add("AccessGroupId", accessGroupId);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<AccessGroupsModules>("usp_GetAccessGroupsModules", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<Actions> GetActions(long id, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", id);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<Actions>("usp_GetActions", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<ModuleActions> GetModuleActions(long id, long moduleId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", id);
            dbParams.Add("ModuleId", moduleId);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<ModuleActions>("usp_GetModuleActions", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<AccessGroupModuleActions> GetAccessGroupModuleActions(long id, long accessGroupId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", id);
            dbParams.Add("AccessGroupId", accessGroupId);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<AccessGroupModuleActions>("usp_GetAccessGroupModuleActions", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public int DeleteAccessGroupModuleActions(long accessGroupId, long moduleId, long updatedUser, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("AccessGroupId", accessGroupId);
            dbParams.Add("ModuleId", moduleId);
            dbParams.Add("UpdatedBy", updatedUser);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Execute("usp_DeleteAccessGroupModuleActions", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public int InsertAccessGroupModuleActions(long accessGroupId, long moduleId, long actionId, long createdUser, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("AccessGroupId", accessGroupId);
            dbParams.Add("ModuleId", moduleId);
            dbParams.Add("ActionId", actionId);
            dbParams.Add("CreatedBy", createdUser);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Execute("usp_InsertAccessGroupModuleActions", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public int DeleteUserAccessGroups(long userId, long updatedUser, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("UserId", userId);
            dbParams.Add("UpdatedBy", updatedUser);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Execute("usp_DeleteUserAccessGroups", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public int InsertUserAccessGroups(long accessGroupId, long userId, long createdUser, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("AccessGroupId", accessGroupId);
            dbParams.Add("UserId", userId);
            dbParams.Add("CreatedBy", createdUser);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Execute("usp_InsertUserAccessGroups", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public int ModifyAccessGroup(AccessGroupModuleActionsVM obj, TransactionType tranType, long user, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", obj.AccessGroupId);
            dbParams.Add("GroupName", obj.GroupName);
            dbParams.Add("GroupDetails", obj.GroupDetails);
            dbParams.Add("GroupType", obj.GroupType);
            dbParams.Add("InheritAccessGroupId", obj.InheritAccessGroupId);
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("IsActive", obj.IsActive);
            dbParams.Add("CreatedBy", user);
            dbParams.Add("UpdatedBy", user);
            dbParams.Add("TranType", tranType.ToString());
            dbParams.Add("AccessGroupId", tranType.ToString(), DbType.Int64, ParameterDirection.Output);
            int res = unitOfWork.Connection.Execute("usp_ModifyAccessGroups", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            long returnId = dbParams.Get<long>("AccessGroupId");
            obj.AccessGroupId = returnId;
            return res;
        }

        public IEnumerable<UserAccessGroups> GetUserAccessGroups(long userId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("UserId", userId);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<UserAccessGroups>("usp_GetUserAccessGroups", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<UserProfile> GetUserProfileByID(long userProfileId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", userProfileId);
            dbParams.Add("TenantId", tenantId);
            var result = unitOfWork.Connection.Query<UserProfile>("usp_GetUserProfiles", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

            return result;
        }

        public UserProfile GetUserLoginDetails(string userName)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("UserName", userName);
            return unitOfWork.Connection.QuerySingleOrDefault<UserProfile>("usp_GetUserLoginDetails", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public UserProfile GetUserAlreadyExist(long tenantId,string userName,string email)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("Username", userName);
            dbParams.Add("Email", email);

            return unitOfWork.Connection.QueryFirstOrDefault<UserProfile>("usp_GetUserAlreadyExist", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public UserProfile GetUserLoginDetail(long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("UserId", userId);
            return unitOfWork.Connection.QuerySingleOrDefault<UserProfile>("usp_GetUserProfiles", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public UserProfile GetUserDetailsExist(long userId,string userName, string email, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("UserId", userId);
            dbParams.Add("UserName", userName);
            dbParams.Add("Email", email);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.QueryFirstOrDefault<UserProfile>("usp_GetUserDetailsExist", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public List<UserModule> GetUserPermissions(long userId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("UserId", userId);
            dbParams.Add("TenantId", tenantId);
            var actionDictionary = new Dictionary<long, UserModule>();
            return unitOfWork.Connection.Query<UserModule, ModuleAction, UserModule>("usp_GetUserPremissions",
            (modules, actions) =>
            {
                UserModule userModuleEntry;
                if (!actionDictionary.TryGetValue(modules.ModuleId, out userModuleEntry))
                {
                    userModuleEntry = modules;
                    userModuleEntry.ModuleActions = new List<ModuleAction>();
                    actionDictionary.Add(userModuleEntry.ModuleId, userModuleEntry);
                }
                userModuleEntry.ModuleActions.Add(actions);
                return userModuleEntry;
            }
            ,transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams, splitOn: "ModuleId,ActionId").Distinct().ToList();
        }

        public IEnumerable<UserProfile> GetUserProfileSorting(long SortId, long tenentId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            if (SortId == 1)
            {
                dbParams.Add("SortId", 1);
                dbParams.Add("Sorting", "name");
                dbParams.Add("TenantId", tenentId);
            }
            else
            {
                dbParams.Add("SortId", 2);
                dbParams.Add("Sorting", "date");
                dbParams.Add("TenantId", tenentId);
            }
            return unitOfWork.Connection.Query<UserProfile>("usp_GetUserProfiles", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

        }

        public IEnumerable<UserProfile> GenericSearch(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId)
        {

            DynamicParameters dbParams = new DynamicParameters();

            if (PageIndex == 0)
            {
                PageIndex = 1;
            }
         
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PageIndex", PageIndex);
            dbParams.Add("PageSize", PageSize);
            dbParams.Add("SearchText", SearchText);
            dbParams.Add("SearchOption", SearchOption);
            dbParams.Add("SortType", SortType);
            dbParams.Add("SortName", SortName);
            dbParams.Add("TotalRecord", dbType: DbType.Int64, direction: ParameterDirection.Output);
            var result = unitOfWork.Connection.Query<UserProfile>("usp_GenericSearch", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

            // var result= unitOfWork.Connection.Query<UserProfile>("usp_GenericSearch", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
             TotalRecord = dbParams.Get<long>("TotalRecord");
            return result;
        }

        public IEnumerable<UserProfile> GetUserProfilesSearching(string Searching,long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Searching", Searching);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<UserProfile>("usp_GetUserProfiles", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams); ;
        }

        public bool InsertUserProfile(UserProfile model,long tenentId)
        {
            var bit = false;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "Insert");
            dbParams.Add("UserName", model.UserName);
            dbParams.Add("FirstName", model.FirstName);
            dbParams.Add("LastName", model.LastName);
            dbParams.Add("MobileNo", model.MobileNo);
            dbParams.Add("Email", model.Email);
            if (string.IsNullOrEmpty(model.Password))
            {
                dbParams.Add("Password", "");
            }
            else
            {
                dbParams.Add("Password", model.Password);
            }
            dbParams.Add("RoleId", model.RoleId);
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("Status", 1);
            dbParams.Add("IsDeleted", model.IsDeleted);
            dbParams.Add("IsActive", model.IsActive);
            dbParams.Add("ImageUrl", model.ImageUrl);

            var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyUserProfile", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            if (AffectedRow != 0)
            {
                return bit = true;
            }
            return bit;
        }

        public bool UpdateUserProfile(UserProfile model,long tenentId)
        {

            var bit = false;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "Update");
            dbParams.Add("UserName", model.UserName);
            dbParams.Add("Id", model.UserId);
            dbParams.Add("FirstName", model.FirstName);
            dbParams.Add("LastName", model.LastName);
            dbParams.Add("MobileNo", model.MobileNo);
            dbParams.Add("Email", model.Email);
            if (string.IsNullOrEmpty(model.Password))
            {
              dbParams.Add("Password","");
            }
            else
            {
                dbParams.Add("Password", model.Password);
            }
            dbParams.Add("RoleId", model.RoleId);
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("Status", model.Status);
            dbParams.Add("ImageUrl", model.ImageUrl);

            var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyUserProfile", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            if (AffectedRow != 0)
            {
                return bit = true;
            }
            return bit;
        }

        public bool DeleteUserProfile(int userProfileId, long tenantId)
        {
            var bit = false;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "Delete");
            dbParams.Add("Id", userProfileId);
            dbParams.Add("TenantId", tenantId);
            var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyUserProfile", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            if (AffectedRow != 0)
            {
                return bit = true;
            }
            return bit;
        }

        public bool UpdateUserAccount(UserProfile model, long tenentId)
        {

            var bit = false;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "UpdateUserAccount");
            dbParams.Add("UserName", model.UserName);
            dbParams.Add("Id",       model.UserId);
            dbParams.Add("FirstName",model.FirstName);
            dbParams.Add("LastName", model.LastName);
            dbParams.Add("MobileNo", model.MobileNo);
            dbParams.Add("Email",    model.Email);
            if (string.IsNullOrEmpty(model.Password))
            {
                dbParams.Add("Password", "");
            }
            else
            {
                dbParams.Add("Password", model.Password);
            }
            dbParams.Add("RoleId", model.RoleId);
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("Status", model.Status);
            dbParams.Add("CountryId", model.CountryId);
            dbParams.Add("ImageUrl", model.ImageUrl);

            var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyUserProfile", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            if (AffectedRow != 0)
            {
                return bit = true;
            }
            return bit;
        }

        public bool MBInsertUserProfile(UserProfile model, long tenentId)
        {
            var bit = false;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "MBInsert");
            dbParams.Add("UserName", model.UserName);
            dbParams.Add("FirstName", model.FirstName);
            dbParams.Add("LastName", "Client");
            dbParams.Add("MobileNo", model.MobileNo);
            dbParams.Add("Email", model.Email);
            dbParams.Add("RoleId", model.RoleId);
            if (string.IsNullOrEmpty(model.Password))
            {
                dbParams.Add("Password", "");
            }
            else
            {
                dbParams.Add("Password", model.Password);
            }
            dbParams.Add("Civil_IdFront", model.Civil_IdFront);
            dbParams.Add("Civil_IdBack", model.Civil_IdBack);
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("Status", 1);
            dbParams.Add("IsDeleted", model.IsDeleted);
            dbParams.Add("IsActive", model.IsActive);
            dbParams.Add("ImageUrl", model.ImageUrl);

            var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyUserProfile", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            if (AffectedRow != 0)
            {
                return bit = true;
            }
            return bit;
        }

        public string InsertUserProfileFCMToken(long tenentId,long userId,string FCMToken)
        {
            string message = null;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "insert");
            dbParams.Add("TenentId", tenentId);
            dbParams.Add("userId", userId);
            dbParams.Add("FCMToken", FCMToken);
           
            var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyFCMToken", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            if (AffectedRow != 0)
            {
                message = "FCM Token has been successfully";
            }
            else
            {
                message = "FCM Token has not been successfully";
            }

            return message;
        }

    }
}
