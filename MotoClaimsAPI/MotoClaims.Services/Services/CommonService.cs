using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Common;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class CommonService: ICommonService
    {
        #region Error And Audit Logs

        public long InsertErrorLog(ErrorLogs data, long tenantId)
        {
            long result = -1;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    //Your database code here
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    result = myRepository.InsertErrorLog(data, tenantId);
                    if (result > 0)
                        unitOfWork.Commit();

                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return result;
        }
        public List<ErrorLogs> GetErrorLogList(int StartRow, int EndRow, string query)
        {
            List<ErrorLogs> ErrorLogsList = new List<ErrorLogs>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    ErrorLogsList = myRepository.GetErrorLogList(StartRow, EndRow, query);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return ErrorLogsList;
        }

        public List<ErrorLogs> GetErrorLogList(string filterQuery, string filterExport)
        {
            List<ErrorLogs> ErrorLogsList = new List<ErrorLogs>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    ErrorLogsList = myRepository.GetErrorLogList(filterQuery, filterExport);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return ErrorLogsList;
        }
        public long ErrorLogsCount(string query)
        {
            long count = 0;
            using (Repositories dalSession = new Repositories())
            {

                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    count = myRepository.ErrorLogsCount(query);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    throw;
                }
            }
            return count;
        }


        public long ProvidersRefCount(string query)
        {
            long count = 0;
            using (Repositories dalSession = new Repositories())
            {

                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    count = myRepository.ProvidersRefCount(query);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    throw;
                }
            }
            return count;
        }

        public long InsertAuditLog(ActivityLog data, long tenantId)
        {
            long result = -1;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    //Your database code here
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    result = myRepository.InsertAuditLog(data, tenantId);
                    if (result > 0)
                        unitOfWork.Commit();

                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return result;
        }
        public long InsertAuditLogDetails(ActivityLog data)
        {
            long result = -1;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    //Your database code here
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    result = myRepository.InsertAuditLogDetails(data);
                    if (result > 0)
                        unitOfWork.Commit();

                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return result;
        }
        public List<ActivityDetail> GetActivityLogDetail(long auditId)
        {
            List<ActivityDetail> activityLogDetail = new List<ActivityDetail>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    activityLogDetail = myRepository.GetActivityLogDetail(auditId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return activityLogDetail;
        }
        public List<ActivityLog> GetActivityLogList(int StartRow, int EndRow, string query)
        {
            List<ActivityLog> ActivityLogList = new List<ActivityLog>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    ActivityLogList = myRepository.GetActivityLogList(StartRow, EndRow, query);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return ActivityLogList;
        }
        public List<ActivityLog> GetActivityLogList(string filterQuery, string filterExport)
        {
            List<ActivityLog> ActivityLogList = new List<ActivityLog>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    ActivityLogList = myRepository.GetActivityLogList(filterQuery, filterExport);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return ActivityLogList;
        }
        public long GetActivityLogListCount(string query)
        {
            long count = 0;
            using (Repositories dalSession = new Repositories())
            {

                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    count = myRepository.GetActivityLogListCount(query);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    throw;
                }
            }
            return count;
        }
        public T GetOldDataById<T>(long ID, string EntityReference, string PrimaryKeyAttr)
        {

            T oldObject = default(T);
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    oldObject = myRepository.GetOldDataById<T>(ID, EntityReference, PrimaryKeyAttr);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return oldObject;
        }

        public IEnumerable<ActivityLog> GetAllLogsByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, DateTime ToDate, DateTime FromDate)
        {
            IEnumerable<ActivityLog> ActivityLogList = new List<ActivityLog>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    ActivityLogList = myRepository.GetAllLogsByPageIndex(out  TotalRecord,PageIndex,  PageSize,  SearchText,  SearchOption,  SortType,  SortName,  tenentId,  ToDate,  FromDate);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return ActivityLogList;
        }

        public IEnumerable<ActivityDetail> GetActivityDetailsByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, DateTime ToDate, DateTime FromDate)
        {
            IEnumerable<ActivityDetail> ActivityLogList = new List<ActivityLog>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    ActivityLogList = myRepository.GetActivityDetailsByPageIndex(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, ToDate, FromDate);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return ActivityLogList;
        }

        public IEnumerable<ActivityDetail> GetActivityDetails(long Id,long tenantId)
        {
            IEnumerable<ActivityDetail> ActivityLogList = new List<ActivityLog>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    ActivityLogList = myRepository.GetActivityDetails(Id,tenantId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return ActivityLogList;
        }

        public IEnumerable<ActivityLog> GetAllLogsByUserId(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long UserId, DateTime ToDate, DateTime FromDate)
        {
            IEnumerable<ActivityLog> ActivityLogList = new List<ActivityLog>();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    CommonRepository myRepository = new CommonRepository(unitOfWork);
                    ActivityLogList = myRepository.GetAllLogsByUserId(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, UserId, ToDate, FromDate);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return ActivityLogList;
        }

        #endregion
    }
}
