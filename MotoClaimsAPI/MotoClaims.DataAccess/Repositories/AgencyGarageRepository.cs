using Dapper;
using MotoClaims.DataAccess.Generics;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.AgencyGarage;
using MotoClaims.Entities.Provider;
using MotoClaims.Entities.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
   public class AgencyGarageRepository
    {
        public AgencyGarageRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;
        DBGenerics dBGenerics = new DBGenerics();

        public AgencyGarageWithCount GetAgencyGarage(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long ProviderTypeId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PageIndex", PageIndex);
            dbParams.Add("PageSize", PageSize);
            dbParams.Add("SearchText", SearchText);
            dbParams.Add("SearchOption", SearchOption);
            dbParams.Add("SortType", SortType);
            dbParams.Add("SortName", SortName);
            dbParams.Add("ProviderTypeId", ProviderTypeId);
            var list = unitOfWork.Connection.Query<AgencyGarage>("usp_GetAgencyGarageClaimsAssigned", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

            AgencyGarageWithCount agencyGarageWithCount = new AgencyGarageWithCount();
            agencyGarageWithCount.AgencyGarage = list;
            agencyGarageWithCount.Count = list.Select(x => x.RowCounts).FirstOrDefault();

            return agencyGarageWithCount;
        }

        public IEnumerable<ProviderLocation> GetAllLocationByProviderId(long Id, long tenentId)
        {
            DynamicParameters dbParam = new DynamicParameters();
            dbParam.Add("TenantId", tenentId);
            dbParam.Add("Id", Id);
            dbParam.Add("Operation", "GetProviderLocation");
            var location = unitOfWork.Connection.Query<ProviderLocation>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParam);
            return location;
        }

        public string InsertAgencyGarageClaimsAssigned(AgencyGarageClaimsAssigned obj, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (obj.ClaimId > 0 )
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("AgencyGarageId", obj.AgencyGarageId);
                dbParams.Add("AgencyGarageBranchId", obj.AgencyGarageBranchId);
                dbParams.Add("Status", obj.Status);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                var affectedrows2 = unitOfWork.Connection.Execute("usp_ModifyAgencyGarageClaimsAssigned", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows2 != 0)
                {
                    ErrorMessage = "AgencyGarageClaimsAssigned Successfully Save";
                }
                else
                {
                    ErrorMessage = "AgencyGarageClaimsAssigned has not Successfully Save";
                }

                DynamicParameters dbParam = new DynamicParameters();
                dbParam.Add("Operation", "Insert");
                dbParam.Add("TenantId", tenantId);
                dbParam.Add("Claim_Id", obj.ClaimId);
                dbParam.Add("Status_Id", 25);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                ///FCM NOTIFICATION
                var resultData = GetUserFCMTokenByID(userId, tenantId, 25);
                dBGenerics.SendMessage(resultData.FCM_Token, "Claimoto", resultData.StatusName);
            }
            else
            {
                return ErrorMessage = "AgencyGarageClaimsAssigned has not Successfully Save";
            }
            return ErrorMessage;
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
