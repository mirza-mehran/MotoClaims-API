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

    public class AccessGroupsController : ApiController
    {
        IUserManagementService userManagementService;
        public AccessGroupsController()
        {

        }
        public AccessGroupsController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }

        /// <summary>
        /// Get AccessGroups
        /// </summary>
        /// <remarks>
        ///
        /// Get signle AccessGroups by providing Role ID 
        ///     
        /// </remarks>

        // GET: AccessGroups
        [HttpGet]
        public HttpResponseMessage GetAccessGroups()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<AccessGroups> resultData =  userManagementService.GetAccessGroups(0, 0, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AccessGroups>>("AccessGroups not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AccessGroups>>("Get AccessGroups list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get AccessGroups By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get signle AccessGroups by providing ID 
        ///     
        /// </remarks>
        // GET: AccessGroups{id}
        [HttpGet]
        public HttpResponseMessage GetAccessGroupsById(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<AccessGroups> resultData =  userManagementService.GetAccessGroups(id, 0, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AccessGroups>>("AccessGroups not found", "Ensure that the AccessGroups Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AccessGroups>>("Get AccessGroups By Id Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get AccessGroups By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get signle AccessGroups by providing Role ID 
        ///     
        /// </remarks>
        // GET: AccessGroups{roleId}
        //[HttpGet]
        //[Route("api/AccessGroups/{roleId}")]
        //public IHttpActionResult GetAccessGroupsByRoleId(long roleId)
        //{
        //    long tenentId = 2;
        //    IEnumerable<AccessGroups> resultData =  userManagementService.GetAccessGroups(0, roleId, tenentId);
        //    if (resultData == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(resultData);
        //}

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
