using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
using MotoClaims.Entities.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
   public class TenantRepository
    {
        public TenantRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IUnitOfWork unitOfWork = null;

        public IEnumerable<ProviderCountry> GetCountry(long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderCountry");
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<ProviderCountry>("usp_GetProviderCountry", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams); 
        }


        public Tenants GetTenantsByID(long tenantId)
        {
            var dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("Operation", "GetTenant");
            return unitOfWork.Connection.QuerySingleOrDefault<Tenants>("usp_GetTenants", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams); ;
        }

        public bool Update_Tenant(Tenants model, long tenantId, long userId)
        {
            string ErrorMessage = null;
            bool bit = false ;
            if (tenantId != 0 && tenantId > 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "UpdateTenant");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("TenantLogoPath", model.TenantLogoPath);
                dbParams.Add("TenantPrimaryPersonName", model.TenantPrimaryPersonName);
                dbParams.Add("TenantPrimaryPersonCountry", model.TenantPrimaryPersonCountry);
                dbParams.Add("TenantPrimaryPersonEmail", model.TenantPrimaryPersonEmail);
                dbParams.Add("TenantPrimaryPersonPhone", model.TenantPrimaryPersonPhone);
                dbParams.Add("TenantPrimaryPersonPassword", model.TenantPrimaryPersonPassword);
                
                dbParams.Add("CreatedBy", 1);
                dbParams.Add("UpdatedBy", 1);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                var affectedrows1 = unitOfWork.Connection.Execute("usp_GetTenants", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows1 != 0)
                {
                    bit = true;
                   // ErrorMessage = "Record has been successfully Updated";
                }
            }
            else
            {
                // ErrorMessage = "Invalid Tenant Id";
                bit = false;
            }
            return bit;
        }



    }
}
