using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoClaims.Entities;
using MotoClaims.Entities.User;

namespace MotoClaims.Services.Interfaces
{
    public interface IUserManagementService
    {
        IEnumerable<UserProfile> GetUserProfiles(long id, long roleId, long tenantId);
        IEnumerable<UserProfile> GetUserProfileByID(long userProfileId, long tenantId);
        IEnumerable<UserProfile> GenericSearch(out long TotalRecord,long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName,long tenentId);

        bool InsertUserProfile(UserProfile model,long tenentId, out string errMessage);
        bool UpdateUserProfile(UserProfile model,long tenentId, out string errMessage);

        bool UpdateUserAccount(UserProfile model, long tenentId, out string errMessage);

        bool UpdateUserAcProfile(UserProfile model, long tenentId, out string errMessage);

        bool DeleteUserProfile(int userProfileId, long tenantId);

        IEnumerable<Roles> GetRoles(long tenantId);
        IEnumerable<Modules> GetModules(long id, long accessGroupsId, long tenantId);
        IEnumerable<AccessGroups> GetAccessGroups(long id, long roleId, long tenantId);
        IEnumerable<AccessGroupsModules> GetAccessGroupsModules(long id, long moduleId, long accessGroupId, long tenantId);
        IEnumerable<Actions> GetActions(long id, long tenantId);
        IEnumerable<ModuleActions> GetModuleActions(long id, long moduleId, long tenantId);
        IEnumerable<AccessGroupModuleActions> GetAccessGroupModuleActions(long id, long accessGroupId, long tenantId);
        bool ModifyAccessGroupModuleActions(AccessGroupModuleActionsVM obj, TransactionType tranType, long user, long tenantId);
        //bool UpdateAccessGroupModuleActions(long accessGroupId, long moduleId, long actionId, long user, long tenantId);
        UserProfile GetUserLoginDetails(string UserName);
        UserProfile GetUserLoginDetail(long userId);
        bool MBInsertUserProfile(UserProfile model, long tenentId, out string errMessage);
        UserProfile GetUserAlreadyExist(long tenantId, string userName, string email);
        string InsertUserProfileFCMToken(long tenentId, long userId, string FCMToken);

    }
}
