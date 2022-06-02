using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
    public class ForgotPasswordRepository
    {
        public ForgotPasswordRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;

        public IEnumerable<UserProfiles> GetEmails(string Email,long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetEmail");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("Email", Email);
            return unitOfWork.Connection.Query<UserProfiles>("usp_GetForgotPassword", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public bool UpdateUserAccount(updatePasswordUser model, long tenentId)
        {
            var bit = false;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "UpdateUserAccountPassword");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("Id", model.UserId);
            dbParams.Add("Email", model.Email);
            dbParams.Add("UserName", model.Username);
            if (string.IsNullOrEmpty(model.password))
            {
                dbParams.Add("Password", "");
            }
            else
            {
                dbParams.Add("Password", model.password);
            }

            var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyUserProfile", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            if (AffectedRow != 0)
            {
                return bit = true;
            }
            return bit;
        }


    }
}
