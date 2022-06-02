using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Claim;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.User;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace MotoClaims.Services.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimActions GetClaimAction(long ClaimId, long RoleId, long tenentId, long userId)
        {
            ClaimActions ClaimActions = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    ClaimActions = myRepository.GetClaimAction( ClaimId, RoleId, tenentId, userId);
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return ClaimActions;
        }

        public IEnumerable<ClaimRejectionReason> GetClaimRejectionReason(long tenentId, long userId)
        {

            IEnumerable<ClaimRejectionReason> Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    Claims = myRepository.GetClaimRejectionReason(tenentId,  userId);
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

        public IEnumerable<Claims> GetClaimsById(long Id, long userId, long tenantId)
        {
            IEnumerable<Claims> Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                   // Claims = myRepository.GetClaimsById( Id,  userId,  tenantId);
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

        public Claims GetClaimsById(long id, long tenantId)
        {
            Claims Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    Claims = myRepository.GetClaimsById(id,tenantId);
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

        public IEnumerable<Claims> GetClaimsByPolicyId(long Id, long tenentId, long userId)
        {
            IEnumerable<Claims> Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    Claims = myRepository.GetClaimsByPolicyId(Id,  tenentId,  userId);
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

        public IEnumerable<Status_Master> GetClaimStatus_Master( long Id, long tenentId, long userId)
        {

            IEnumerable<Status_Master> Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    Claims = myRepository.GetClaimStatus_Master(Id, tenentId,  userId);
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

        public IEnumerable<ClaimType> GetClaimsType( long tenentId, long userId)
        {
            IEnumerable<ClaimType> ClaimsType = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    ClaimsType = myRepository.GetClaimsType( tenentId,  userId);
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return ClaimsType;
        }

        public IEnumerable<Policy> GetPolicies(string civilId, long tenentId, long userId)
        {
            IEnumerable<Policy> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    obj = myRepository.GetPolicies(civilId,tenentId, userId);
                    // unitOfWork.Commit();
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

        public long InsertClaims(Claims obj, long tenantId, long userId, out bool bit)
        {

            long Claim_Id =0;
            using (Repositories dalSession = new Repositories())
            {

                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add Claims", "Claims", "ClaimId", obj.ClaimId, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    Claim_Id = myRepository.InsertClaims(obj,  tenantId,  userId, out  bit);
                    //foreach (var item in obj.ClaimDocuments)
                    //{
                    //    myRepository.InsertClaimsDocument(item, tenantId, userId);
                    //}
                    //foreach (var item in obj.ClaimAccidentCarPhotos)
                    //{
                    //    myRepository.InsertClaimsImage(item, tenantId, userId);
                    //}
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Claims", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }

            return Claim_Id;
        }

        public string InsertClaimsDocument(ClaimDocuments obj, long tenantId, long userId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add Claim Documents", "ClaimDocuments", "CD_Id", obj.CD_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);

                    ErrorMessage = myRepository.InsertClaimsDocument(obj,tenantId,userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Claim Documents", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                }
            }
            return ErrorMessage;
        }

        public string InsertClaimsImage(ClaimAccidentCarPhotos obj, long tenantId, long userId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add Claim Image", "ClaimAccidentCarPhotos", "CACP_Id", obj.ClaimId, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);

                    ErrorMessage = myRepository.InsertClaimsImage(obj,tenantId,userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Claim Image", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                }
            }
            return ErrorMessage;
        }

        public string InsertClaimsLeaveMessage(ClaimLeaveMessage obj, long tenantId, long userId, out bool bit)
        {
            string message = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add ClaimLeaveMessage", "ClaimLeaveMessage", "LM_Id", obj.LM_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    message = myRepository.InsertClaimsLeaveMessage(obj, tenantId, userId, out bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add ClaimLeaveMessage", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }

            return message;
        }

        public long UpdateClaims(Claims obj, long tenantId, long userId, out bool bit)
        {

            long Claim_Id = 0;
            using (Repositories dalSession = new Repositories())
            {

                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Update Claims", "Claims", "ClaimId", obj.ClaimId, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);
                    Claim_Id = myRepository.UpdateClaims(obj, tenantId, userId, out bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Update Claims", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                    bit = false;
                }
            }

            return Claim_Id;
        }

        public string UpdateClaimsDocument(ClaimDocuments obj, long tenantId, long userId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Update Claim Documents", "ClaimDocuments", "CD_Id", obj.CD_Id, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);

                    ErrorMessage = myRepository.UpdateClaimsDocument(obj, tenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Update Claim Documents", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                }
            }
            return ErrorMessage;
        }

        public string UpdateClaimsImage(ClaimAccidentCarPhotos obj, long tenantId, long userId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Update Claim Image", "ClaimAccidentCarPhotos", "CACP_Id", obj.ClaimId, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);

                    ErrorMessage = myRepository.InsertClaimsImage(obj, tenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Update Claim Image", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                }
            }
            return ErrorMessage;
        }

        public string UpdateStatusRejectApprove(Claims obj, long tenentId, long userId)
        {

            string message = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add Claims", "Claims", "ClaimId", obj.ClaimId, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenentId);
                    message = myRepository.UpdateStatusRejectApprove(obj, tenentId,  userId);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Claims", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenentId);
                }
            }

            return message;
        }

        public IEnumerable<UserProfiles> GetUserProfiles(long id, long roleId, long tenantId, string SearchText)
        {
            IEnumerable<UserProfiles> UserProfiles = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    UserProfiles = myRepository.GetUserProfiles(id, roleId, tenantId, SearchText);
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

        public IEnumerable<MBClaims> GetMBClaims(long CivilId, long tenentId, long userId)
        {
            IEnumerable<MBClaims> claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    claims = myRepository.GetMBClaims( CivilId,  tenentId, userId);
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return claims;
        }

        public MBClaim GetMBClaim(long ClaimId, long tenentId, long userId)
        {
            MBClaim claim = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    claim = myRepository.GetMBClaim( ClaimId,  tenentId,  userId);
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return claim;
        }

        public IEnumerable<Status_Master> GetClaimAllStatus_Master(long tenentId, long userId)
        {
            IEnumerable<Status_Master> Claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    Claims = myRepository.GetClaimAllStatus_Master(tenentId, userId);
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

        public IEnumerable<ClaimAccidentCarPhotos> GetMBClaimAccidentCarPhotosByClaimId(long ClaimId, long tenantId)
        {
            IEnumerable<ClaimAccidentCarPhotos> claims = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    claims = myRepository.GetMBClaimAccidentCarPhotosByClaimId(ClaimId, tenantId);
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return claims;
        }

        public AllClaimsWithCount GetAllClaim(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long userId)
        {
            AllClaimsWithCount obj = null;

            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    obj = myRepository.GetAllClaim( PageIndex,  PageSize,  SearchText,  SearchOption,  SortType,  SortName,  tenentId,  userId);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return obj;
        }
        public string InsertClaimAppeal(ClaimAppeal obj, long tenantId, long userId)
        {
            string message = null;

            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ClaimsRepository myRepository = new ClaimsRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add ClaimAppeal", "ClaimAppeal", "CA_Id", obj.CA_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    message = myRepository.InsertClaimAppeal(obj, tenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    GeneralSettings.LogError(ex, "Add ClaimAppeal", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    throw;
                }
            }

            return message;
        }
    }
}
