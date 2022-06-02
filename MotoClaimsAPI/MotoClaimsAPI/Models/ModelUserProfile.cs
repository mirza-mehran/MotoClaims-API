using MotoClaims.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoClaimsAPI.Models
{
    public class ModelUserProfile
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; }
        public string CurrentPassword { get; set; }
        public long CountryId { get; set; }
        public string Civil_IdFront { get; set; }
        public string Civil_IdBack { get; set; }
        public UploadImage ImageModel { get; set; }
        public List<UserAccessGroups> AccessGroupIds { get; set; }
    }

    public class ModelUserProfileWithTotalRecord
    {
        public IEnumerable<ModelUserProfile> ModelUserProfileWithTotalRecords { get; set; }
        public long TotalRecord { get; set; }
    }
    public class UserAccessGroups
    {
        public long AccessGroupId { get; set; }
        public string AccessGroupName { get; set; }
    }
}