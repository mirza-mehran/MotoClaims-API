using MotoClaims.Entities.Surveyours;
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
    [RoutePrefix("api/Surveyour")]
    public class SurveyourController : ApiController
    {
         ISurveyorService _surveyorService;

        public SurveyourController()
        {

        }

        public SurveyourController(ISurveyorService surveyorService)
        {
            _surveyorService = surveyorService;
        }


        /// <summary>
        /// Add new SurveyourClaimsAssigned
        /// </summary>
        /// <remarks>
        ///
        /// Insert new SurveyourClaimsAssigned
        ///     
        /// </remarks>
        [Route("SurveyourClaimsAssigned")]
        [HttpPost]
        public HttpResponseMessage PostSurveyourClaimsAssigned([FromBody]ClaimsSurveyours obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            if (ModelState.IsValid)
            {
                string resultData = _surveyorService.InsertSurveyourClaimsAssigned(obj, tenantId, userId);
                if (resultData == null)
                {
                    return ResponseMessages<ClaimsSurveyours>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<ClaimsSurveyours>("ClaimsAssigned to Surveyour Successfully ", string.Empty, (int)HttpStatusCode.OK, null);
            }
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
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
