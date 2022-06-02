using MotoClaims.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
    public interface ICommonService
    {
        long InsertErrorLog(ErrorLogs data, long tenantId);
        long InsertAuditLog(ActivityLog data, long tenantId);
        long InsertAuditLogDetails(ActivityLog data);
        List<ErrorLogs> GetErrorLogList(int StartRow, int EndRow, string query);
        List<ErrorLogs> GetErrorLogList(string filterQuery, string filterExport);
        long ErrorLogsCount(string query);
        long ProvidersRefCount(string query);
        List<ActivityLog> GetActivityLogList(int StartRow, int EndRow, string query);
        List<ActivityDetail> GetActivityLogDetail(long auditId);
        List<ActivityLog> GetActivityLogList(string filterQuery, string filterExport);
        long GetActivityLogListCount(string query);
        T GetOldDataById<T>(long ID, string EntityReference, string PrimaryKeyAttr);
        IEnumerable<ActivityLog> GetAllLogsByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, DateTime ToDate, DateTime FromDate);
        IEnumerable<ActivityDetail> GetActivityDetailsByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, DateTime ToDate, DateTime FromDate);

        IEnumerable<ActivityDetail> GetActivityDetails(long Id, long tenantId);

        IEnumerable<ActivityLog> GetAllLogsByUserId(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long UserId, DateTime ToDate, DateTime FromDate);


    }
}
