using MotoClaims.Entities.CarReplacement;
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
using System.Web.Http.ModelBinding;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/CarReplacement")]
    public class CarReplacementController : ApiController
    {
        ICarReplacementService _carReplacementService;

        public CarReplacementController()
        {

        }

        public CarReplacementController(ICarReplacementService carReplacementService)
        {
            _carReplacementService = carReplacementService;
        }

        /// <summary>
        /// Add new ReplacementCarRequests
        /// </summary>
        /// <remarks>
        ///
        /// Insert new ReplacementCarRequests
        ///     
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage PostReplacementCarRequests([FromBody]ReplacementCarRequests obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            //if (ModelState.IsValid)
            //{
                string resultData = _carReplacementService.InsertReplacementCarRequests(obj, tenantId, userId);

                if (resultData == null)
                {
                    return ResponseMessages<ReplacementCarRequests>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<ReplacementCarRequests>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            //}
            //string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            //return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        /// <summary>
        /// Add new ReplacementCarAssigned
        /// </summary>
        /// <remarks>
        ///
        /// Insert new ReplacementCarAssigned
        ///     
        /// </remarks>
        [Route("ReplacementCarAssigned")]
        [HttpPost]
        public HttpResponseMessage PostReplacementCarAssigned([FromBody]ReplacementCarAssigned obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            //if (ModelState.IsValid)
            //{
                string resultData = _carReplacementService.InsertReplacementCarAssigned(obj, tenantId, userId);

                if (resultData == null)
                {
                    return ResponseMessages<ReplacementCarAssigned>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<ReplacementCarAssigned>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            //}
            //string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            //return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }


        /// <summary>
        /// Get CarReplacement Status By Claim Id
        /// </summary>
        /// <remarks>
        ///
        /// Get Array CarReplacement By Claim Id
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("CarReplacementStatus")]
        public HttpResponseMessage GetCarReplacementStatus_Master(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            var resultData = _carReplacementService.GetClaimStatus_Master(Id, tenantId, userId);

            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<Status_Master>>("ClaimStatus not found", "", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<Status_Master>>("Get list ClaimStatus Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
