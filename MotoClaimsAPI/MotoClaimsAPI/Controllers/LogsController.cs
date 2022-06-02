using MotoClaims.Entities.Common;
using MotoClaims.Services.Interfaces;
using MotoClaimsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class LogsController : ApiController
    {
        ICommonService _iCommonService;

        public LogsController()
        {

        }

        public LogsController(ICommonService iCommonService)
        {
            _iCommonService = iCommonService;
        }


        /// <summary>
        /// Get ActivityLogs By UserId
        /// </summary>
        /// <remarks>
        ///
        /// Get ActivityLogs  by providing PageIndex , PageSize and UserId
        ///     
        /// </remarks>
        [HttpGet]
        [Route("ActivityLogs")]
        public HttpResponseMessage GetAllLogsByUserId(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, DateTime ToDate, DateTime FromDate)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);


            long TotalRecord = 0;
            IEnumerable<ActivityLog> resultData = _iCommonService.GetAllLogsByUserId(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, userId, ToDate, FromDate);

            ActivityLogTotalRecords obj = new ActivityLogTotalRecords();
            obj.ActivityLogs = resultData;
            obj.TotalRecords = TotalRecord;

            if (obj.ActivityLogs.Count() == 0)
            {
                return ResponseMessages<ActivityLogTotalRecords>("ActivityLogs not found", "Ensure that the FromDate, ToDate, PageIndex and PageSize parameter included in the request are correct", (int)HttpStatusCode.OK, obj);
            }
            return ResponseMessages<ActivityLogTotalRecords>("Get ActivityLogs Successfully", string.Empty, (int)HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Get ActivityLogs Pagination
        /// </summary>
        /// <remarks>
        ///
        /// Get ActivityLogs Pagination by providing PageIndex and PageSize
        ///     
        /// </remarks>
        [HttpGet]
        [Route("ActivityLogs/Pagination")]
        public HttpResponseMessage GetAllLogsByPageIndex( long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, DateTime ToDate, DateTime FromDate)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);


            long TotalRecord = 0;
            IEnumerable<ActivityLog> resultData = _iCommonService.GetAllLogsByPageIndex(out  TotalRecord,  PageIndex,  PageSize,  SearchText,  SearchOption,  SortType,  SortName,  tenentId,  ToDate,  FromDate);

            ActivityLogTotalRecords obj = new ActivityLogTotalRecords();
            obj.ActivityLogs = resultData;
            obj.TotalRecords = TotalRecord;

            if (obj.ActivityLogs.Count() == 0)
            {
                return ResponseMessages<ActivityLogTotalRecords>("ActivityLogs not found", "Ensure that the FromDate, ToDate, PageIndex and PageSize parameter included in the request are correct", (int)HttpStatusCode.OK, obj);
            }
            return ResponseMessages<ActivityLogTotalRecords>("Get ActivityLogs Successfully", string.Empty, (int)HttpStatusCode.OK, obj);
        }


        /// <summary>
        /// Get ActivityDetails Pagination
        /// </summary>
        /// <remarks>
        ///
        /// Get ActivityDetails Pagination by providing PageIndex and PageSize
        ///     
        /// </remarks>
        [HttpGet]
        [Route("ActivityDetails/Pagination")]
        public HttpResponseMessage GetActivityDetailsByPageIndex(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, DateTime ToDate, DateTime FromDate)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            long TotalRecord = 0;
            IEnumerable<ActivityDetail> resultData = _iCommonService.GetActivityDetailsByPageIndex(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, ToDate, FromDate);


            ActivityDetailsTotalRecords obj = new ActivityDetailsTotalRecords();
            obj.ActivityDetail = resultData;
            obj.TotalRecords = TotalRecord;

            if (obj.ActivityDetail.Count() == 0)
            {
                return ResponseMessages<ActivityDetailsTotalRecords>("ActivityDetail not found", "Ensure that the FromDate, ToDate, PageIndex and PageSize parameter included in the request are correct", (int)HttpStatusCode.OK, obj);
            }
            return ResponseMessages<ActivityDetailsTotalRecords>("Get ActivityDetail Successfully", string.Empty, (int)HttpStatusCode.OK, obj);

        }


        /// <summary>
        /// Get ActivityDetails 
        /// </summary>
        /// <remarks>
        ///
        /// Get ActivityDetails by providing ActivityLogs Id
        ///     
        /// </remarks>
        [HttpGet]
        [Route("ActivityDetails")]
        public HttpResponseMessage GetActivityDetails(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);

            IEnumerable<ActivityDetail> resultData = _iCommonService.GetActivityDetails( Id, tenantId);

            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ActivityDetail>>("ActivityDetails not found", "Ensure that the ActivityDetails Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ActivityDetail>>("Get ActivityDetails Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        [NonAction]
        public HttpResponseMessage ResponseMessages<T>(string Message, string Detail, int Status, dynamic resultData)
        {

            var postUser = new ExceptionError<T>
            {
                timestamp = DateTime.Now.ToString(),
                message = Message,
                detail = Detail,
                status = Status,
                data = resultData
            };

            var jsonString = JsonConvert.SerializeObject(postUser);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return response;
        }

    }
}
