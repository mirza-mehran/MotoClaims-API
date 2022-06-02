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
using System.Web;
using System.Web.Http;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    public class RolesController : ApiController
    {
        IUserManagementService userManagementService;
        public RolesController(){

        }
        public RolesController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }


        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Roles
        ///     
        /// </remarks>
        /// 
        // GET: Roles
        [HttpGet]
        public HttpResponseMessage GetRoles()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<Roles> resultData = userManagementService.GetRoles(tenentId);
            if (resultData.Count() == 0)
            {
                return  ResponseMessages<IEnumerable<Roles>>("Roles not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Roles>>("Get Roles list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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