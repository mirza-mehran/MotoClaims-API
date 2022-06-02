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

    public class ModuleActionsController : ApiController
    {
        IUserManagementService userManagementService;
        public ModuleActionsController()
        {

        }
        public ModuleActionsController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }
        /// <summary>
        /// Get ModuleActions
        /// </summary>
        /// <remarks>
        ///
        /// Get all module actions 
        ///     
        /// </remarks>

        // GET: GetModuleActions
        [HttpGet]
        public HttpResponseMessage GetModuleActions()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<ModuleActions> resultData = userManagementService.GetModuleActions(0, 0,tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ModuleActions>>("ModuleActions not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ModuleActions>>("Get ModuleActions list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        ///  Get ModuleActionsById
        /// </summary>
        /// <remarks>
        ///
        /// Get signle module action by providing id 
        ///     
        /// </remarks>
        // GET: ModuleActions{id}
        [HttpGet]
        public HttpResponseMessage GetModuleActionsById(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<ModuleActions> resultData = userManagementService.GetModuleActions(id, 0, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ModuleActions>>("ModuleActions not found", "Ensure that the ModuleActions Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ModuleActions>>("Get ModuleActions Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }
        /// <summary>
        /// Get ModuleActionsByModuleId
        /// </summary>
        /// <remarks>
        ///
        /// Get all module actions by providing module id 
        ///     
        /// </remarks>
        // GET: ModuleActionsByModuleId{moduleId}
        [HttpGet]
        [Route("api/ModuleActions/{moduleId}")]
        public HttpResponseMessage GetModuleActionsByModuleId(long moduleId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<ModuleActions> resultData = userManagementService.GetModuleActions(0, moduleId, tenentId);
            if (resultData.Count() == 0)
            {

                return ResponseMessages<IEnumerable<ModuleActions>>("ModuleActions not found", "Ensure that the Module Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ModuleActions>>("Get ModuleActions Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
