using MotoClaims.Entities.AgencyGarage;
using MotoClaims.Entities.Provider;
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
    [RoutePrefix("api/AgencyGarage")]
    public class AgencyGarageController : ApiController
    {
        IAgenciesService _agenciesService;

        public AgencyGarageController()
        {

        }

        public AgencyGarageController(IAgenciesService agenciesService)
        {
            _agenciesService = agenciesService;
        }


        /// <summary>
        /// Get All Agencies and Garages
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Agencies and Garages
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("AgencyGarage")]
        public HttpResponseMessage GetAgencyGarage(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long ProviderTypeId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _agenciesService.GetAgencyGarage( PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, ProviderTypeId);
            if (resultData == null)
            {
                return ResponseMessages<AgencyGarageWithCount>("AgencyGarage not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<AgencyGarageWithCount>("Get AgencyGarage list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get All Branches By ProviderId
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of Branches By ProviderId
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ProviderBranches")]
        public HttpResponseMessage GetProviderBranches(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            tenentId = 2;

            var resultData = _agenciesService.GetAllLocationByProviderId( Id,  tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProviderLocation>>("Branches not found", "Empty Record", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ProviderLocation>>("Get Branches list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Add new AgencyGarageClaimsAssigned
        /// </summary>
        /// <remarks>
        ///
        /// Insert new AgencyGarageClaimsAssigned
        ///     
        /// </remarks>
        [Route("AgencyGarage")]
        [HttpPost]
        public HttpResponseMessage PostAgencyGarageClaimsAssigned([FromBody]AgencyGarageClaimsAssigned obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            //if (ModelState.IsValid)
            //{
                string resultData = _agenciesService.InsertAgencyGarageClaimsAssigned(obj,tenantId,userId);
                if (resultData == null)
                {
                    return ResponseMessages<AgencyGarageClaimsAssigned>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<AgencyGarageClaimsAssigned>("AgencyGarageClaimsAssigned Save Successfully ", string.Empty, (int)HttpStatusCode.OK, null);
            //}
            //string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            //return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
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
