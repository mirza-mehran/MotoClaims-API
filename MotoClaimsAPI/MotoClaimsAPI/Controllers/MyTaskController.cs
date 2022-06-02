using MotoClaims.Entities.Claim;
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
    [RoutePrefix("api/MyTask")]
    public class MyTaskController : ApiController
    {
        IMyTaskService _iMyTaskService;
        public MyTaskController()
        {

        }

        public MyTaskController(IMyTaskService iMyTaskService)
        {
            _iMyTaskService = iMyTaskService;
        }

        /// <summary>
        /// Get Claims
        /// </summary>
        /// <remarks>
        ///
        /// Get Array of claims by providing User ID 
        ///     
        /// </remarks>

        [Route("Claims")]
        [HttpGet]
        public HttpResponseMessage GetClaims(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            List<MyTaskWithListName> resultData = _iMyTaskService.GetClaimsByUserId(Id,  tenentId,  userId);
            if (resultData == null)
            {
                return ResponseMessages<List<MyTaskWithListName>>("Claims not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<List<MyTaskWithListName>>("Get Claims list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get Pending Claims
        /// </summary>
        /// <remarks>
        ///
        /// Get Array of pending claims by providing User ID 
        ///     
        /// </remarks>

        [Route("PendingClaims")]
        [HttpGet]
        public HttpResponseMessage GetPendingClaims(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            List<MyTaskWithListName> resultData = _iMyTaskService.GetPendingTaskClaimsByUserId(Id, tenentId, userId);
            if (resultData == null)
            {
                return ResponseMessages<List<MyTaskWithListName>>("Claims not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<List<MyTaskWithListName>>("Get Claims list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
