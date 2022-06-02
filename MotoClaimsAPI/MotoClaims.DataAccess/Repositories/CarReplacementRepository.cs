using Dapper;
using MotoClaims.DataAccess.Generics;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.CarReplacement;
using MotoClaims.Entities.Claim;
using MotoClaims.Entities.ScheduledCallsAndChat;
using MotoClaims.Entities.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
  public  class CarReplacementRepository
    {
        
        public CarReplacementRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IUnitOfWork unitOfWork = null;
        DBGenerics dBGenerics = new DBGenerics();

        public string InsertReplacementCarRequests(ReplacementCarRequests obj, long tenentId, long userId)
        {
            string message = null;
            if (obj.ClaimId != 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "InsertReplacementCarRequests");
                dbParams.Add("TenantId", tenentId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("RequestedDurationInDDays", obj.RequestedDurationInDDays);
                dbParams.Add("ClaimNo", obj.ClaimNo);
                dbParams.Add("InsuranceCompanyName", obj.InsuranceCompanyName);
                dbParams.Add("Comments", obj.Comments);
                dbParams.Add("Status", obj.Status);
                dbParams.Add("CarNo", obj.CarNo);
                dbParams.Add("SubmissionDate", obj.SubmissionDate);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", 0);
                dbParams.Add("IsActive", 1);

                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyReplacementCarRequests", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

                DynamicParameters dbCarParam = new DynamicParameters();
                dbCarParam.Add("TenantId", tenentId);
                dbCarParam.Add("ClaimId", obj.ClaimId);
                dbCarParam.Add("StatusId", 21);
                dbCarParam.Add("CreatedBy", userId);
                dbCarParam.Add("UpdatedBy", userId);
                dbCarParam.Add("UpdatedDate", DateTime.Now);
                dbCarParam.Add("IsDeleted", false);
                dbCarParam.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyReplacementCarStatusMapping", dbCarParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);


                DynamicParameters dbParam = new DynamicParameters();
                dbParam.Add("Operation", "Insert");
                dbParam.Add("TenantId", tenentId);
                dbParam.Add("Claim_Id", obj.ClaimId);
                dbParam.Add("Status_Id", 18);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                

                if (affectedrows != 0)
                {
                    message = "ReplacementCarRequests has Successfully Insert!";
                }
                else
                {
                    message = "ReplacementCarRequests has not Successfully Insert!";
                }
            }
            else
            {
                message = "Ensure that the Claim Id included in the request is correct";
            }

            return message;
        }

        public string InsertReplacementCarAssigned(ReplacementCarAssigned obj, long tenentId, long userId)
        {
            string message = null;
            if (obj.ClaimId != 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "InsertReplacementCarAssigned");
                dbParams.Add("TenantId", tenentId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("ReplacementCarAgencyId", obj.ReplacementCarAgencyId);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", 0);
                dbParams.Add("IsActive", 1);

                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyReplacementCarRequests", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

                DynamicParameters dbCarParam = new DynamicParameters();
                dbCarParam.Add("TenantId", tenentId);
                dbCarParam.Add("ClaimId", obj.ClaimId);
                dbCarParam.Add("StatusId", 22);
                dbCarParam.Add("CreatedBy", userId);
                dbCarParam.Add("UpdatedBy", userId);
                dbCarParam.Add("UpdatedDate", DateTime.Now);
                dbCarParam.Add("IsDeleted", false);
                dbCarParam.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyReplacementCarStatusMapping", dbCarParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                DynamicParameters dbParam = new DynamicParameters();
                dbParam.Add("Operation", "Insert");
                dbParam.Add("TenantId", tenentId);
                dbParam.Add("Claim_Id", obj.ClaimId);
                dbParam.Add("Status_Id", 26);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                ///FCM NOTIFICATION
                var resultData = GetUserFCMTokenByID(userId, tenentId, 26);
                dBGenerics.SendMessage(resultData.FCM_Token, "Claimoto", resultData.StatusName);

                if (affectedrows != 0)
                {
                    message = "ReplacementCarAssigned has Successfully Insert!";
                }
                else
                {
                    message = "ReplacementCarAssigned has not Successfully Insert!";
                }
            }
            else
            {
                message = "Ensure that the Claim Id included in the request is correct";
            }

            return message;
        }

        public IEnumerable<Status_Master> GetClaimStatus_Master(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetReplacementCarStatusMapping");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("ClaimId", Id);
            return unitOfWork.Connection.Query<Status_Master>("usp_GetReplacementCarStatusMapping", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public FCMToken GetUserFCMTokenByID(long userProfileId, long tenantId, long StatusId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetFCMToken");
            dbParams.Add("userId", userProfileId);
            dbParams.Add("TenentId", tenantId);
            dbParams.Add("StatusId", StatusId);
            return unitOfWork.Connection.QueryFirstOrDefault<FCMToken>("usp_ModifyFCMToken", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

    }
}
