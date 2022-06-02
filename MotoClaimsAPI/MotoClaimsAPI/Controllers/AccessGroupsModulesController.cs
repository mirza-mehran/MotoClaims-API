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

    public class AccessGroupsModulesController : ApiController
    {
        IUserManagementService userManagementService;
        public AccessGroupsModulesController()
        {

        }
        public AccessGroupsModulesController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }

        /// <summary>
        /// Get All AccessGroupsModules
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all AccessGroupsModules
        ///     
        /// </remarks>
        // GET: AccessGroupsModules
        [HttpGet]
        public HttpResponseMessage GetAccessGroupsModules()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<AccessGroupsModules> resultData = userManagementService.GetAccessGroupsModules(0, 0,0, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AccessGroupsModules>>("AccessGroupsModules not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AccessGroupsModules>>("Get AccessGroupsModules list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get AccessGroupsModules By Id
        /// </summary>
        /// <remarks>
        ///
        /// Get signle AccessGroupsModules by providing ID 
        ///     
        /// </remarks>
        // GET: AccessGroupsModules{id}
        [HttpGet]
        public HttpResponseMessage GetAccessGroupsModulesById(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<AccessGroupsModules> resultData = userManagementService.GetAccessGroupsModules(id,0, 0, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AccessGroupsModules>>("AccessGroupsModules not found", "Ensure that the AccessGroupsModules Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AccessGroupsModules>>("Get AccessGroupsModules list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }
        /// <summary>
        /// Get AccessGroupsModules By moduleId
        /// </summary>
        /// <remarks>
        ///
        /// Get signle AccessGroupsModules by providing moduleId 
        ///     
        /// </remarks>
        // GET: AccessGroupsModules{moduleId}
        [HttpGet]
        [Route("api/AccessGroupsModules/{moduleId}")]
        public HttpResponseMessage GetAccessGroupsModulesByModuleId(long moduleId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<AccessGroupsModules> resultData = userManagementService.GetAccessGroupsModules(0, moduleId, 0, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AccessGroupsModules>>("AccessGroupsModules not found", "Ensure that the module Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AccessGroupsModules>>("Get AccessGroupsModules By module Id Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }


        /// <summary>
        /// Get AccessGroupsModules By accessGroupId
        /// </summary>
        /// <remarks>
        ///
        /// Get signle AccessGroupsModules by providing accessGroupId 
        ///     
        /// </remarks>
        // GET: AccessGroupsModules{accessGroupId}
        [HttpGet]
        [Route("api/AccessGroupsModules/{accessGroupId}")]
        public HttpResponseMessage GetAccessGroupsModulesByAccessGroupId(long accessGroupId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<AccessGroupsModules> resultData = userManagementService.GetAccessGroupsModules(0, 0, accessGroupId, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AccessGroupsModules>>("AccessGroupsModules not found", "Ensure that the accessGroup Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AccessGroupsModules>>("Get AccessGroupsModules By AccessGroup Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
