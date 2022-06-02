using MotoClaims.Entities.Claim;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IClaimsService
    {

        IEnumerable<Claims> GetClaimsByPolicyId(long Id, long tenentId, long userId);
        IEnumerable<Claims> GetClaimsById(long Id, long userId, long tenantId);

        long InsertClaims(Claims obj, long tenantId, long userId, out bool bit);
        string InsertClaimsImage(ClaimAccidentCarPhotos obj, long tenantId, long userId);
        string InsertClaimsDocument(ClaimDocuments obj, long tenantId, long userId);

        long UpdateClaims(Entities.Claim.Claims obj, long tenantId, long userId, out bool bit);
        string UpdateClaimsDocument(ClaimDocuments obj, long tenantId, long userId);

        string UpdateClaimsImage(ClaimAccidentCarPhotos obj, long tenantId, long userId);
        IEnumerable<ClaimType> GetClaimsType(long tenentId, long userId);

        Claims GetClaimsById(long id, long tenantId);

        ClaimActions GetClaimAction(long ClaimId, long RoleId, long tenentId, long userId);

        IEnumerable<Policy> GetPolicies(string civilId, long tenentId, long userId);

        IEnumerable<ClaimRejectionReason> GetClaimRejectionReason(long tenentId, long userId);

        IEnumerable<Status_Master> GetClaimStatus_Master(long Id, long tenentId, long userId);

        IEnumerable<Status_Master> GetClaimAllStatus_Master(long tenentId, long userId);

        string UpdateStatusRejectApprove(Claims obj, long tenentId, long userId);

        string InsertClaimsLeaveMessage(ClaimLeaveMessage obj, long tenantId, long userId, out bool bit);

        IEnumerable<UserProfiles> GetUserProfiles(long id, long roleId, long tenantId, string SearchText);

        IEnumerable<MBClaims> GetMBClaims(long CivilId, long tenentId, long userId);

        MBClaim GetMBClaim(long ClaimId, long tenentId, long userId);

        IEnumerable<ClaimAccidentCarPhotos> GetMBClaimAccidentCarPhotosByClaimId(long ClaimId, long tenantId);
        AllClaimsWithCount GetAllClaim(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long userId);

        string InsertClaimAppeal(ClaimAppeal obj, long tenantId, long userId);
    }
}
