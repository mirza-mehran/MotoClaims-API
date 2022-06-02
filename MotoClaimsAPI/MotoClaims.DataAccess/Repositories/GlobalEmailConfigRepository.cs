using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
using MotoClaims.Entities.GlobelSetting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
  public class GlobalEmailConfigRepository
    {
        public GlobalEmailConfigRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IUnitOfWork unitOfWork = null;

        public IEnumerable<TimeZoneConfig> GetTimeZone(long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetTimeZone");
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<TimeZoneConfig>("usp_GetGlobalEmailConfig", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams); 
        }

        public SMTP_Configuration GetSMTP_Configuration(long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetSMTPConfigAndTimezone");
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.QuerySingleOrDefault<SMTP_Configuration>("usp_GetGlobalEmailConfig", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams); 
        }

        public string UpdateSMTP_Configuration(SMTP_Configuration model,long tenantId)
        {
            string ErrorMessage = null;
            if (tenantId != 0  && tenantId > 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "UpdateSMTPConfigAndTimezone");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("HostName", model.HostName);
                dbParams.Add("PortNo", model.PortNo);
                dbParams.Add("UserName", model.UserName);
                //dbParams.Add("Password", model.Password);
                if (string.IsNullOrEmpty(model.Password))
                {
                    dbParams.Add("Password", "");
                }
                else
                {
                    dbParams.Add("Password", model.Password);
                }
                dbParams.Add("SSL_Enabled", model.SSL_Enabled);

                dbParams.Add("TimeZone_ID", model.TimeZone_ID);

                dbParams.Add("CreatedBy", 1);
                dbParams.Add("UpdatedBy", 1);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                var affectedrows1 = unitOfWork.Connection.Execute("usp_GlobalEmailConfig", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows1 != 0)
                {
                    ErrorMessage = "Record has been successfully Updated";
                }
            }
            else
            {
                ErrorMessage = "Invalid Tenant Id";
            }
            return ErrorMessage;
        }

        public GlobalEmailConfiguration GetGlobelEmail_Signature(long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GlobalEmailConfiguration_Signature");
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.QuerySingleOrDefault<GlobalEmailConfiguration>("usp_GetGlobalEmailConfig", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }
        public string InsertGlobelEmail_Signature(GlobalEmailConfiguration model, long tenantId,out bool bit)
        {
            string ErrorMessage = null;
            bit = false;
            if (tenantId != 0 && tenantId > 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "InsertEmailSignature");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("CCEmail", model.EmailCC);
                dbParams.Add("BCCEmail", model.EmailBCC);
                dbParams.Add("Email_Signature", model.Email_Signature);

                var affectedrows1 = unitOfWork.Connection.Execute("usp_GlobalEmailConfig", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows1 != 0)
                {
                    ErrorMessage = "Record has been successfully Inserted";
                    bit = true;
                }
            }
            else
            {
                ErrorMessage = "Invalid Tenant Id";
                bit = false;
            }
            return ErrorMessage;
        }

        public string UpdateGlobelEmail_Signature(GlobalEmailConfiguration model, long tenantId, out bool bit)
        {
            string ErrorMessage = null;
            bit = false;
            if (tenantId != 0 && tenantId > 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "UpdateEmailSignature");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("CCEmail", model.EmailCC);
                dbParams.Add("BCCEmail", model.EmailBCC);
                dbParams.Add("Email_Signature", model.Email_Signature);

                var affectedrows1 = unitOfWork.Connection.Execute("usp_GlobalEmailConfig", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows1 != 0)
                {
                    ErrorMessage = "Record has been successfully Updated";
                    bit = true;
                }
            }
            else
            {
                ErrorMessage = "Invalid Tenant Id";
                bit = false;
            }
            return ErrorMessage;
        }

        //public string Send_Email(long ToText,long FromText,string SubjectText)
        //{
        //    MailMessage
        //}
    }
}
