using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Claim;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.ScheduledCallsAndChat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
   public class ScheduledCallsAndChatRepository
    {
        public ScheduledCallsAndChatRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;


        public IEnumerable<Policy> GetPolicies(long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetPolicies");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("tempPolicyId", null);
            var result = unitOfWork.Connection.Query<Policy>("usp_GetPolicies", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public Claims GetClaimsById(long id, long tenantId)
        {
            Claims claims = new Claims();
            var dbParams = new DynamicParameters();
            dbParams.Add("ClaimId", id);
            dbParams.Add("TenantId", tenantId);

            for (int i = 0; i <= 0; i++)
            {
                if (i == 0)
                {
                    dbParams.Add("@Operation", "GetClaim");
                    var result = unitOfWork.Connection.Query<Claims>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    claims = result.First();
                }
            }

            return claims;
        }

        public string InsertScheduledCalls(ScheduledCalls obj, long tenentId, long userId)
        {
            string message = null;
            if (obj.ClaimId != 0)
            {

                var claimList = GetClaimsById(obj.ClaimId, tenentId);
                var list =  GetPolicies( tenentId,  userId);
                var  IdentityNo = list.Where(x => x.Id == claimList.PolicyId).FirstOrDefault();
                Guid uuid = Guid.NewGuid();
                string myuuidAsString = uuid.ToString();

                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "InsertScheduledCalls");
                dbParams.Add("TenantId", tenentId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("PolicyId", claimList.PolicyId);
                dbParams.Add("Date", obj.Date);
                dbParams.Add("StartTime", obj.StartTime);
                dbParams.Add("EndTime", obj.EndTime);
                dbParams.Add("AssignedTo", obj.AssignedTo);
                dbParams.Add("ChannelId", userId + IdentityNo.IdentityNo + myuuidAsString + obj.ClaimId);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", 0);
                dbParams.Add("IsActive", 1);
               
                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyScheduledCallsAndChatService", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

                InsertScheduledCallsAssigned(obj,  tenentId,  userId);

                DynamicParameters dbParam = new DynamicParameters();
                dbParam.Add("Operation", "Insert");
                dbParam.Add("TenantId", tenentId);
                dbParam.Add("Claim_Id", obj.ClaimId);
                dbParam.Add("Status_Id", 17);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows != 0)
                {
                    message = "Claim ScheduledCalls has Successfully Insert!";
                }
                else
                {
                    message = "Claim ScheduledCalls has not Successfully Insert!";
                }
            }
            else
            {
                message = "Ensure that the Claim Id included in the request is correct";
            }

            return message;
        }

        public ConfigScheduledCallTimeSlots GetConfigScheduledCallTimeSlots(long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetConfigScheduledCallTimeSlots");
            dbParams.Add("TenantId", tenentId);
            return unitOfWork.Connection.QueryFirstOrDefault<ConfigScheduledCallTimeSlots>("usp_GetScheduledCallExceptionSlots", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<ScheduledCallExceptionSlots> GetScheduledCallExceptionSlots(long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetScheduledCallExceptionSlots");
            dbParams.Add("TenantId", tenentId);
            return unitOfWork.Connection.Query<ScheduledCallExceptionSlots>("usp_GetScheduledCallExceptionSlots", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<ScheduledCallsList> GetScheduledCalls(long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetScheduledCalls");
            dbParams.Add("TenantId", tenentId); 
            return unitOfWork.Connection.Query<ScheduledCallsList>("usp_GetScheduledCalls", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public string InsertScheduledCallsAssigned(ScheduledCalls obj, long tenentId, long userId)
        {
            string message = null;
            if (userId != 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "InsertScheduledCallsAssigned");
                dbParams.Add("TenantId", tenentId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("TimeSlotUser", userId);
                dbParams.Add("TimeSlotDate", obj.Date);
                dbParams.Add("TimeSlot", obj.StartTime + "To" + obj.EndTime);

                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyScheduledCallsAndChatService", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                if (affectedrows != 0)
                {
                    message = "ScheduledCallsAssigned has Successfully Insert!";
                }
                else
                {
                    message = "ScheduledCallsAssigned has not Successfully Insert!";
                }
            }
            else
            {
                message = "Ensure that the Claim Id included in the request is correct";
            }

            return message;
        }

        public string UpdateScheduledCalls(ScheduledCalls obj, long tenentId, long userId)
        {
            string message = null;
            if (obj.SC_Id != 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "UpdateScheduledCalls");
                dbParams.Add("TenantId", tenentId);
                dbParams.Add("SC_Id", obj.SC_Id);
                dbParams.Add("Date", obj.Date);
                dbParams.Add("StartTime", obj.StartTime);
                dbParams.Add("EndTime", obj.EndTime);
             
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", 0);
                dbParams.Add("IsActive", 1);


                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyScheduledCallsAndChatService", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

                if (affectedrows != 0)
                {
                    message = "Claim ReScheduledCalls has Successfully Update!";
                }
                else
                {
                    message = "Claim ReScheduledCalls has not Successfully Update!";
                }
            }
            else
            {
                message = "Ensure that the SC_Id included in the request is correct";
            }

            return message;
        }

        public string CancelCall(long SC_Id, long tenentId, long userId)
        {
            string message = null;
            if (SC_Id != 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "CancelCall");
                dbParams.Add("TenantId", tenentId);
                dbParams.Add("SC_Id", SC_Id);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);

                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyScheduledCallsAndChatService", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

                if (affectedrows != 0)
                {
                    message = "Cancel ScheduledCalls has Successfully Insert!";
                }
                else
                {
                    message = "Cancel ScheduledCalls has not Successfully Insert!";
                }
            }
            else
            {
                message = "Ensure that the SC_Id Id included in the request is correct";
            }

            return message;
        }

        public IEnumerable<ScheduledCalls> GetMBScheduledCalls(long civilId, long tenantId,long userId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Operation", "GetMBScheduledCalls");
            dynamicParameters.Add("TenantId",tenantId);
            dynamicParameters.Add("CivilId", civilId);
            return unitOfWork.Connection.Query<ScheduledCalls>("usp_GetScheduledCalls",transaction:unitOfWork.Transaction,commandType:CommandType.StoredProcedure,param:dynamicParameters);
        }
    }
}
