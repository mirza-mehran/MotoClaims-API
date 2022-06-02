using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.User
{
    public class UserProfile
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Timezone { get; set; }
        public string Localization { get; set; }
        public long CountryId { get; set; }

        public string Theme { get; set; }
        public long RoleId { get; set; }
        public long TenantId { get; set; }
        public string ImageUrl { get; set; }
        public string MobileNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime ExpiryDate { get; set; }
        public long Gender { get; set; }
        public int Status { get; set; }
        public Guid PasswordResetToken { get; set; }
        public int WrongPasswordAttempts { get; set; }
        public bool IsLocked { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsMailSent { get; set; }
        public string ExternalId { get; set; }
        public string Tags { get; set; }
        public string Civil_IdFront { get; set; }
        public string Civil_IdBack { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public List<UserAccessGroups> AccessGroupIds { get; set; }
        public List<UserModule> UserModules { get; set; }
        public string FCM_Token { get; set; }
    }
    public class UserAccessGroups{
        public long AccessGroupId { get; set; }
        public string AccessGroupName { get; set; }
    }
    public class UserModule{
        public long ModuleId { get; set; }
        public string ModuleSystemName { get; set; }
        public List<ModuleAction> ModuleActions { get; set; }
    }
    public class ModuleAction{
        public long ActionId { get; set; }
        public string ActionName { get; set; }
    }
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string TenantId { get; set; }
        public string Token { get; set; }
    }


    public class UserProfiles
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Civil_IdFront { get; set; }
        public string Password { get; set; }
    }

    public class updatePasswordUser
    {
        public string Username { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Code { get; set; }
        public string TenantId { get; set; }
        public string UserId { get; set; }
    }

    public class JWTToken
    {
        public string JWT_Token { get; set; }
    }

    public class FCMToken
    {
        public long UserId { get; set; }
        public string FCM_Token { get; set; }
        public string StatusName { get; set; }
    }
}
