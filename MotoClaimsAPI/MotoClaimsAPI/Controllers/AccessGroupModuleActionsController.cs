using MotoClaims.Entities;
using MotoClaims.Entities.User;
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
using System.Threading.Tasks;
using System.Web.Http;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]

    public class AccessGroupModuleActionsController : ApiController
    {
        IUserManagementService userManagementService;
        public AccessGroupModuleActionsController()
        {

        }
        public AccessGroupModuleActionsController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }

        /// <summary>
        /// Get AccessGroupModuleActions
        /// </summary>
        /// <remarks>
        ///
        /// Get all access group module actions 
        ///     
        /// </remarks>

        // GET: GetAccessGroupModuleActions
        [HttpGet]
        public HttpResponseMessage GetAccessGroupModuleActions()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

          
            IEnumerable<AccessGroupModuleActions> resultData = userManagementService.GetAccessGroupModuleActions(0, 0, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AccessGroupModuleActions>>("AccessGroupModuleActions not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }

            return ResponseMessages<IEnumerable<AccessGroupModuleActions>>("Get AccessGroupModuleActions list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        ///  Get AccessGroupModuleActionsById
        /// </summary>
        /// <remarks>
        ///
        /// Get signle access group module action by providing id 
        ///     
        /// </remarks>
        // GET: AccessGroupModuleActions{id}
        //[HttpGet]
        //public IHttpActionResult GetAccessGroupModuleActionsById(long id)
        //{
        //    long tenentId = 2;
        //    IEnumerable<AccessGroupModuleActions> resultData = userManagementService.GetAccessGroupModuleActions(id, 0, tenentId);
        //    if (resultData == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(resultData);
        //}
        /// <summary>
        /// Get AccessGroupModuleActionsByAccessGroupId
        /// </summary>
        /// <remarks>
        ///
        /// Get all access group module actions by providing access group id 
        ///     
        /// </remarks>
        // GET: AccessGroupModuleActionsByAccessGroupId{accessGroupId}
        [Route("api/AccessGroupModuleActions/{accessGroupId}")]
        [HttpGet]
        public HttpResponseMessage GetAccessGroupModuleActionsByAccessGroupId(long accessGroupId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<AccessGroupModuleActions> resultData = userManagementService.GetAccessGroupModuleActions(0, accessGroupId, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AccessGroupModuleActions>>("AccessGroupModuleActions_ByAccessGroupId not found", "Ensure that the accessGroupId included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }

            return ResponseMessages<IEnumerable<AccessGroupModuleActions>>("Get AccessGroupModuleActions By AccessGroupId Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }


        /// <summary>
        ///  Insert AccessGroupModuleActions
        /// </summary>
        /// <remarks>
        ///
        /// Insert access group module actions
        ///     
        /// </remarks>
        // POST: AccessGroupModuleActions
        [HttpPost]
        public HttpResponseMessage InsertAccessGroupModuleActions(AccessGroupModuleActionsVM obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long user = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            bool inserted = userManagementService.ModifyAccessGroupModuleActions(obj, TransactionType.insert, user, tenantId);
            if (!inserted)
            {
                return ResponseMessages<AccessGroupModuleActionsVM>("Error While Saving AccessGroupModuleActions!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AccessGroupModuleActionsVM>("AccessGroupModuleActions Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }
        /// <summary>
        ///  Update AccessGroupModuleActions
        /// </summary>
        /// <remarks>
        ///
        /// Update access group module actions by providing access group id
        ///     
        /// </remarks>
        // PUT: AccessGroupModuleActions
        [Route("api/AccessGroupModuleActions/{id:long}")]
        [HttpPut]
        public HttpResponseMessage UpdateAccessGroupModuleActions([FromUri]long id, [FromBody]AccessGroupModuleActionsVM obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long user = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            obj.AccessGroupId = id;
            bool updated = userManagementService.ModifyAccessGroupModuleActions(obj, TransactionType.update, user, tenantId);
            if (!updated)
            {
                return ResponseMessages<AccessGroupModuleActionsVM>("Error While Updating AccessGroupModuleActions!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AccessGroupModuleActionsVM>("AccessGroupModuleActions Updating Successfully.", string.Empty, (int)HttpStatusCode.OK, null);

        }
        /// <summary>
        ///  Delete AccessGroupModuleActions
        /// </summary>
        /// <remarks>
        ///
        /// Delete access group module actions by providing access group id
        ///     
        /// </remarks>
        // DELETE: AccessGroupModuleActions
        [Route("api/AccessGroupModuleActions/{id:long}")]
        [HttpDelete]
        public HttpResponseMessage DeleteAccessGroupModuleActions([FromUri]long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long user = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            AccessGroupModuleActionsVM obj = new AccessGroupModuleActionsVM();
            obj.AccessGroupId = id;
            bool deleted = userManagementService.ModifyAccessGroupModuleActions(obj, TransactionType.delete, user, tenantId);
            if (!deleted)
            {
                return ResponseMessages<AccessGroupModuleActionsVM>("Error While Delete AccessGroupModuleActions!", "Ensure that the AccessGroupModuleActions Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AccessGroupModuleActionsVM>("AccessGroupModuleActions Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
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
