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

    public class ActionsController : ApiController
    {
        IUserManagementService userManagementService;
        public ActionsController()
        {

        }
        public ActionsController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }
        /// <summary>
        /// Get Actions
        /// </summary>
        /// <remarks>
        ///
        /// Get all actions 
        ///     
        /// </remarks>

        // GET: GetActions
        [HttpGet]
        public HttpResponseMessage GetActions()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<Actions> resultData = userManagementService.GetActions(0,tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Actions>>("Actions not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Actions>>("Get Actions list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        ///  Get ActionsById
        /// </summary>
        /// <remarks>
        ///
        /// Get signle action by providing id 
        ///     
        /// </remarks>
        // GET: Actions{id}
        [HttpGet]
        public HttpResponseMessage GetActionsById(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<Actions> resultData = userManagementService.GetActions(id,tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Actions>>("Actions not found", "Ensure that the Actions Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Actions>>("Get Actions Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
