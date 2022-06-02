using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Surveyours;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
   public class SurveyorRepository
    {
        public SurveyorRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;

        public string InsertSurveyourClaimsAssigned(ClaimsSurveyours obj, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (obj.ClaimId > 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("SurveyourId", obj.SurveyourId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("Remarks", obj.Remarks);
                dbParams.Add("Comment", obj.Comment);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                var affectedrows2 = unitOfWork.Connection.Execute("usp_ModifySurveyourClaimsAssigned", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows2 != 0)
                {
                    ErrorMessage = "ClaimsAssigned to Surveyour Successfully";
                }
                else
                {
                    ErrorMessage = "ClaimsAssigned to Surveyour isn't Successfull ";
                }

                DynamicParameters dbParam = new DynamicParameters();
                dbParam.Add("Operation", "Insert");
                dbParam.Add("TenantId", tenantId);
                dbParam.Add("Claim_Id", obj.ClaimId);
                dbParam.Add("Status_Id", 27);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            }
            else
            {
                return ErrorMessage = "SurveyourClaimsAssigned has not Successfully Save";
            }
            return ErrorMessage;
        }

    }
}
