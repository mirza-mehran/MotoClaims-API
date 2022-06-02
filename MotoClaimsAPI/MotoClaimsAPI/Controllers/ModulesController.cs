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

    public class ModulesController : ApiController
    {
        IUserManagementService userManagementService;
        public ModulesController()
        {

        }
        public ModulesController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }

        /// <summary>
        /// Get All Modules
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Modules
        ///     
        /// </remarks>
        // GET: Modules
        [HttpGet]
        public HttpResponseMessage GetModules()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<Modules> resultData =  userManagementService.GetModules(0, 0, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Modules>>("Modules not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Modules>>("Get Modules list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Get Modules By Id
        /// </summary>
        /// <remarks>
        ///
        /// Get signle Modules by providing  ID 
        ///     
        /// </remarks>
        // GET: Modules{id}
        //[HttpGet]
        //public IHttpActionResult GetModulesById(long id)
        //{
        //    long tenentId = 2;
        //    IEnumerable<Modules> resultData =  userManagementService.GetModules(id, 0, tenentId);
        //    if (resultData == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(resultData);
        //}

        /// <summary>
        /// Get Modules By accessGroupsId
        /// </summary>
        /// <remarks>
        ///
        /// Get signle Modules by providing accessGroupsId 
        ///     
        /// </remarks>
        // GET: Modules{roleId}
        [HttpGet]
        [Route("api/Modules/{accessGroupsId}")]
        public HttpResponseMessage GetModulesByRoleId(long accessGroupsId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<Modules> resultData =  userManagementService.GetModules(0, accessGroupsId, tenentId);
            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<Modules>>("Modules not found", "Ensure that the accessGroups Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Modules>>("Get Module Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
