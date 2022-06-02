using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
    public class CommonRepository
    {
        public CommonRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public CommonRepository()
        { 
        }
        private IUnitOfWork unitOfWork = null;
        #region Error And Audit Logs

        public long InsertErrorLog(ErrorLogs data, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("@ID", 0, DbType.Int64, direction: ParameterDirection.Output);
            dbParams.Add("@Source", data.Source);
            dbParams.Add("@ApiName", string.IsNullOrEmpty(data.ApiName) ? "" : data.ApiName);
            dbParams.Add("@ActivityId", string.IsNullOrEmpty(data.ActivityId) ? "" : data.ActivityId);
            dbParams.Add("@Requestobject", data.Requestobject);
            dbParams.Add("@ErrorMsg", data.ErrorMsg);
            dbParams.Add("@ErrorDetails", data.ErrorDetails);
            dbParams.Add("@StackTrace", data.StackTrace);
            dbParams.Add("@CreatedBy", data.CreatedBy);
            dbParams.Add("@TenantId", tenantId);
            unitOfWork.Connection.Execute("usp_InsertErrorLogs", commandType: CommandType.StoredProcedure, transaction: unitOfWork.Transaction, param: dbParams);
            return dbParams.Get<long>("ID");
        }
        public long InsertAuditLog(ActivityLog data, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("@ID", 0, DbType.Int64, direction: ParameterDirection.Output);
            dbParams.Add("@UserId", data.UserId);
            dbParams.Add("@ActivityId", data.ActivityId);
            dbParams.Add("@EntityReference", data.EntityReference);
            dbParams.Add("@CreatedBy", data.UserId);
            dbParams.Add("@TenantId", tenantId);
            unitOfWork.Connection.Execute("usp_InsertActivityLogs", commandType: CommandType.StoredProcedure, transaction: unitOfWork.Transaction, param: dbParams);
            return dbParams.Get<long>("ID");
        }
        public long InsertAuditLogDetails(ActivityDetail data)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("@ID", 0, DbType.Int64, direction: ParameterDirection.Output);
            dbParams.Add("@AuditId", data.AuditId);
            dbParams.Add("@FieldName", data.FieldName);
            dbParams.Add("@OldValue", data.OldValue);
            dbParams.Add("@NewValue", data.NewValue);
            unitOfWork.Connection.Execute("usp_InsertActivityDetails", commandType: CommandType.StoredProcedure, transaction: unitOfWork.Transaction, param: dbParams);
            return dbParams.Get<long>("ID");
        }
        public T GetOldDataById<T>(long ID, string EntityReference, string PrimaryKeyAttr)
        {
            string query = @"select * from " + EntityReference + " where " + PrimaryKeyAttr + " = @ID";
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("@ID", ID);
            return unitOfWork.Connection.Query<T>(query, dbParams, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text).FirstOrDefault<T>();
        }
        public List<ErrorLogs> GetErrorLogList(int StartRow, int EndRow, string query)
        {
            List<ErrorLogs> ErrorLogsList = new List<ErrorLogs>();
            string queryMain = @"SELECT * FROM ( SELECT t.*, ROWNUM AS rn FROM (
                                 select to_date( A.CreatedOn   , 'dd/MM/YY' ) CREATEDON, a.* from ErrorLogs A where 1 = 1   " + query + ") t ) where rn between " + StartRow + " and " + EndRow;
            using (var multi = unitOfWork.Connection.QueryMultiple(queryMain, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text))
            {
                ErrorLogsList = multi.Read<ErrorLogs>().AsList<ErrorLogs>();
            }
            return ErrorLogsList.ToList();
        }

        public List<ErrorLogs> GetErrorLogList(string filterQuery, string filterExport)
        {
            List<ErrorLogs> ErrorLogsList = new List<ErrorLogs>();
            string query = string.Empty;
            if (filterExport.ToLower().Contains("all"))
            {
                query = @"select to_char(A.ERROR_TIME, 'dd/MM/YYYY HH12:MI AM') as ERROR_TIME_STRING, a.* from ErrorLogs A where 1 = 1 order by ID desc";
            }
            else if (filterExport.ToLower().Contains("filter"))
            {
                query = @"select to_char(A.ERROR_TIME, 'dd/MM/YYYY HH12:MI AM') as ERROR_TIME_STRING, a.* from ErrorLogs A where 1 = 1 " + filterQuery + "";
            }
            using (var multi = unitOfWork.Connection.QueryMultiple(query, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text))
            {
                ErrorLogsList = multi.Read<ErrorLogs>().AsList<ErrorLogs>();
            }
            return ErrorLogsList.ToList();
        }


        public long ErrorLogsCount(string query)
        {
            string queryMain = @"select count(*) from ErrorLogs A where 1 = 1  " + query + "";
            return unitOfWork.Connection.ExecuteScalar<long>(queryMain, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text);
        }
        public long ProvidersRefCount(string query)
        {
            string queryMain = @"select count(*) from OC_PROVIDERS A where 1 = 1  " + query + "";
            return unitOfWork.Connection.ExecuteScalar<long>(queryMain, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text);
        }
        public List<ActivityDetail> GetActivityLogDetail(long auditId)
        {
            List<ActivityDetail> modelList = new List<ActivityDetail>();
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("@AuditId", auditId);
            string query = @"select * from ActivityDetails where AuditId = @AuditId";
            modelList = unitOfWork.Connection.Query<ActivityDetail>(query, dbParams, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text).ToList();
            return modelList;
        }
        public List<ActivityLog> GetActivityLogList(int StartRow, int EndRow, string query)
        {
            List<ActivityLog> ActivityLogList = new List<ActivityLog>();
            //string queryMain = @"SELECT * FROM ( SELECT t.*, ROWNUM AS rn FROM (
            //                     SELECT A.*, B.FieldName,B.OldValue, B.NewValue    FROM  ActivityLogs A LEFT JOIN ActivityDetails B ON  A.ID = B.AuditId   WHERE 1=1   " + query + ") t ) where rn between " + StartRow + " and " + EndRow;
            string queryMain = @"SELECT * FROM ( SELECT t.*, ROWNUM AS rn FROM (
                                 SELECT to_date( A.CreatedOn   , 'dd/MM/YY' ) CREATEDON, A.* FROM  ActivityLogs A WHERE 1=1   " + query + ") t ) where rn between " + StartRow + " and " + EndRow;
            using (var multi = unitOfWork.Connection.QueryMultiple(queryMain, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text))
            {
                ActivityLogList = multi.Read<ActivityLog>().AsList<ActivityLog>();
            }
            return ActivityLogList.ToList();
        }

        public IEnumerable<ActivityLog> GetAllLogsByUserId(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId,long UserId, DateTime ToDate, DateTime FromDate)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("UserId", UserId);
            dbParams.Add("PageIndex", PageIndex);
            dbParams.Add("PageSize", PageSize);
            dbParams.Add("SearchText", SearchText);
            dbParams.Add("SearchOption", SearchOption);
            dbParams.Add("SortType", SortType);
            dbParams.Add("SortName", SortName);
            dbParams.Add("ToDate", ToDate);
            dbParams.Add("FromDate", FromDate);
            dbParams.Add("TotalRecord", dbType: DbType.Int64, direction: ParameterDirection.Output);
            var result = unitOfWork.Connection.Query<ActivityLog>("usp_GetAllActivityLogByUserId", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            long TotalRecords = dbParams.Get<long>("TotalRecord");
            TotalRecord = TotalRecords;

            return result;
        }

        public IEnumerable<ActivityLog> GetAllLogsByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, DateTime ToDate, DateTime FromDate)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PageIndex", PageIndex);
            dbParams.Add("PageSize", PageSize);
            dbParams.Add("SearchText", SearchText);
            dbParams.Add("SearchOption", SearchOption);
            dbParams.Add("SortType", SortType);
            dbParams.Add("SortName", SortName);
            dbParams.Add("ToDate", ToDate);
            dbParams.Add("FromDate", FromDate);
            dbParams.Add("TotalRecord", dbType: DbType.Int64, direction: ParameterDirection.Output);
            var result = unitOfWork.Connection.Query<ActivityLog>("usp_GetAllActivityLogByPageIndex", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            long TotalRecords = dbParams.Get<long>("TotalRecord");
            TotalRecord = TotalRecords;

            return result;
        }



        public IEnumerable<ActivityDetail> GetActivityDetailsByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, DateTime ToDate, DateTime FromDate)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PageIndex", PageIndex);
            dbParams.Add("PageSize", PageSize);
            dbParams.Add("SearchText", SearchText);
            dbParams.Add("SearchOption", SearchOption);
            dbParams.Add("SortType", SortType);
            dbParams.Add("SortName", SortName);
            dbParams.Add("ToDate", ToDate);
            dbParams.Add("FromDate", FromDate);

            dbParams.Add("TotalRecord", dbType: DbType.Int64, direction: ParameterDirection.Output);

            var result = unitOfWork.Connection.Query<ActivityDetail>("usp_GetAllActivityDetailsByPageIndex", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            long TotalRecords = dbParams.Get<long>("TotalRecord");
            TotalRecord = TotalRecords;

            return result;
        }

        public IEnumerable<ActivityDetail> GetActivityDetails(long Id, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", Id);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<ActivityDetail>("usp_GetActivityDetails", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public List<ActivityLog> GetActivityLogList(string filterQuery, string filterExport)
        {
            List<ActivityLog> ActivityLogExport = new List<ActivityLog>();
            string query = string.Empty;
            if (filterExport.ToLower().Contains("all"))
            {
                query = @"SELECT to_char(A.ActivityTime, 'dd/MM/YYYY HH12:MI AM') as ActivityTimeString, A.*, B.FieldName,B.OldValue, B.NewValue FROM  ActivityLogs A LEFT JOIN ActivityDetails B ON  A.ID = B.AuditId order by A.ID desc";
            }
            else if (filterExport.ToLower().Contains("filter"))
            {
                query = @"SELECT to_char(A.ActivityTime, 'dd/MM/YYYY HH12:MI AM') as ActivityTimeString, A.*, B.FieldName,B.OldValue, B.NewValue  FROM  ActivityLogs A LEFT JOIN ActivityDetails B ON  A.ID = B.AuditId where 1=1 " + filterQuery + "";
            }
            using (var multi = unitOfWork.Connection.QueryMultiple(query, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text))
            {
                ActivityLogExport = multi.Read<ActivityLog>().AsList<ActivityLog>();
            }
            return ActivityLogExport.ToList();
        }
        public long GetActivityLogListCount(string query)
        {
            //string queryMain = @"SELECT COUNT(*) FROM  ActivityLogs A LEFT JOIN ActivityDetails B ON  A.ID = B.AuditId   WHERE 1=1  " + query + "";
            string queryMain = @"SELECT COUNT(*) FROM  ActivityLogs A  WHERE 1=1  " + query + "";
            return unitOfWork.Connection.ExecuteScalar<long>(queryMain, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.Text);
        }
        
        #endregion


    }
}
