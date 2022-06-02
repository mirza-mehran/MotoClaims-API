using AutoMapper;
using MotoClaims.Entities.AuthorityMatrix;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Product;
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
using System.Web.Http;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/AuthorityMatrix")]
    public class AuthorityMatrixController : ApiController
    {
        IAuthorityMatrixService _iAuthorityMatrixService;

        public AuthorityMatrixController()
        {
            
        }
        public AuthorityMatrixController(IAuthorityMatrixService iAuthorityMatrixService)
        {
            _iAuthorityMatrixService = iAuthorityMatrixService;
        }

        /// <summary>
        /// Get All Make
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Make
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Make")]
        public HttpResponseMessage GetMake(string SearchText)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _iAuthorityMatrixService.GetMake(tenentId, userId, SearchText);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<PolicyMake>>("Make not found", "Ensure that the SearchText parameter included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<PolicyMake>>("Get Make List Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Get All Model
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Model 
        ///     
        /// </remarks>
        [HttpGet]
        [Route("Model")]
        public HttpResponseMessage GetModel(long Id,string SearchText)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<PolicyModel> resultData = _iAuthorityMatrixService.GetModel(Id, tenentId, userId, SearchText);
            IEnumerable<ModelPolicyModel> obj = Mapper.Map<IEnumerable<ModelPolicyModel>>(resultData);

            if (obj.Count() == 0)
            {
                return ResponseMessages<ModelPolicyModel>("Model not found", "Ensure that the Make Id and SearchText parameter included in the request are correct", (int)HttpStatusCode.NotFound, obj);
            }

            return ResponseMessages<ModelPolicyModel>("Get Modle List Successfully.", string.Empty, (int)HttpStatusCode.OK, obj);

        }

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Products
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Products")]
        public HttpResponseMessage GetProducts(string SearchText)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _iAuthorityMatrixService.GetProducts(tenentId, userId, SearchText);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProductList>>("Products not found", "Ensure that the SearchText parameter included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ProductList>>("Get Products List Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get UserProfiles
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all UserProfiles
        ///     
        /// </remarks>
        // GET: UserProfiles
        [HttpGet]
        [Route("UserProfiles")]
        public HttpResponseMessage GetUserProfiles(string SearchText)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<UserProfiles> resultData = _iAuthorityMatrixService.GetUserProfiles(0, 0, tenentId, SearchText);

            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<UserProfiles>>("UserProfiles not found", "Ensure that the SearchText parameter included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<UserProfiles>>("Get UserProfiles List Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get All Services
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of Services
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Services")]
        public HttpResponseMessage GetServices(string SearchText)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _iAuthorityMatrixService.GetServices(tenentId,  SearchText);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AuthorityMatrixAssess_Services>>("Services not found", "Ensure that the SearchText parameter included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AuthorityMatrixAssess_Services>>("Get Services List Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);

        }


        /// <summary>
        /// Get All AuthorityMatrixs
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of AuthorityMatrixs
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("AuthorityMatrixs")]
        public HttpResponseMessage GetAuthoritysMatrixs()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            var resultData = _iAuthorityMatrixService.GetAuthoritysMatrixs(0, tenentId,  userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AuthorityMatrixs>>("AuthorityMatrixs not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AuthorityMatrixs>>("Get AuthorityMatrixs List Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get AuthorityMatrixs By Id
        /// </summary>
        /// <remarks>
        ///
        /// Get signle  AuthorityMatrixs by providing Id
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("AuthorityMatrixId")]
        public HttpResponseMessage GetAuthorityMatrix(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
           
            var resultData = _iAuthorityMatrixService.GetAuthorityMatrix( Id,  tenentId,  userId);
            if (resultData.AM_Assign_ID == 0)
            {
                return ResponseMessages<AuthorityMatrixs>("AuthorityMatrix not found", "Ensure that the AuthorityMatrix Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<AuthorityMatrixs>("Get AuthorityMatrix Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);
        }


        /// <summary>
        /// Get All AuthorityMatrixsAssess
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of AuthorityMatrixsAssess
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("AuthorityMatrixsAssess")]
        public HttpResponseMessage GetAuthoritysMatrixsAssess()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
          
            var resultData = _iAuthorityMatrixService.GetAuthoritysMatrixsAssess( 0,  tenentId,  userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<AuthorityMatrixsAssessment>>("AuthorityMatrixsAssess not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<AuthorityMatrixsAssessment>>("Get AuthorityMatrixsAssess list Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Get AuthorityMatrixAssess By Id
        /// </summary>
        /// <remarks>
        ///
        /// Get signle  AuthorityMatrixAssess by providing Id
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("AuthorityMatrixAssessId")]
        public HttpResponseMessage GetAuthorityMatrixAssess(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            var resultData = _iAuthorityMatrixService.GetAuthorityMatrixAssess(Id, tenentId, userId);
            if (resultData.AM_Assess_ID == 0)
            {
                return ResponseMessages<AuthorityMatrixAssessment>("AuthorityMatrixsAssess not found", "Ensure that the AuthorityMatrixAssess Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<AuthorityMatrixAssessment>("Get AuthorityMatrixAssess Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Add AuthorityMatrix
        /// </summary>
        /// <remarks>
        ///
        /// Insert new AuthorityMatrix
        ///     
        /// </remarks>
        [HttpPost]
        [Route("AuthorityMatrix")]
        public HttpResponseMessage PostAuthorityMatrix(AuthorityMatrixs obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            bool bit ;
            var resultData = _iAuthorityMatrixService.InsertAuthorityMatrix( obj,  userId,  tenantId, out  bit);
            if (bit == false)
            {
                return ResponseMessages<AuthorityMatrixs>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AuthorityMatrixs>("AuthorityMatrix Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);

        }

        /// <summary>
        /// Update AuthorityMatrix
        /// </summary>
        /// <remarks>
        ///
        /// Update an existing AuthorityMatrix
        ///     
        /// </remarks>

        [HttpPut]
        [Route("AuthorityMatrix")]
        public HttpResponseMessage PutAuthorityMatrix(AuthorityMatrixs obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            bool bit;
            var resultData = _iAuthorityMatrixService.UpdateAuthorityMatrix( obj,  userId,  tenantId, out bit);
            if (bit == false)
            {
                return ResponseMessages<AuthorityMatrixs>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AuthorityMatrixs>("AuthorityMatrixs Updating Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Add AuthorityMatrixAssessment
        /// </summary>
        /// <remarks>
        ///
        /// Insert new AuthorityMatrixAssessment
        ///     
        /// </remarks>
        [HttpPost]
        [Route("InitialAssessment")]
        public HttpResponseMessage PostAuthorityMatrixAssessment(AuthorityMatrixAssessment obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            bool bit;
            var resultData = _iAuthorityMatrixService.InsertAuthorityMatrixAssessment( obj,  userId,  tenantId, out bit);
            if (bit == false)
            {
                return ResponseMessages<AuthorityMatrixAssessment>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AuthorityMatrixAssessment>("AuthorityMatrixAssessment Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);

        }

        /// <summary>
        /// Update AuthorityMatrixAssessment
        /// </summary>
        /// <remarks>
        ///
        /// Update an existing AuthorityMatrixAssessment
        ///     
        /// </remarks>

        [HttpPut]
        [Route("AfterInitialAssessment")]
        public HttpResponseMessage PutAuthorityMatrixAssessment(AuthorityMatrixAssessment obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            bool bit;
            var resultData = _iAuthorityMatrixService.UpdateAuthorityMatrixAssessment( obj,  userId,  tenantId, out bit);
            if (bit == false)
            {
                return ResponseMessages<AuthorityMatrixAssessment>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AuthorityMatrixAssessment>("AfterInitialAssessment Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Delete AuthorityMatrix
        /// </summary>
        /// <remarks>
        ///
        /// Delete Existing AuthorityMatrix
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("AuthorityMatrix")]
        public HttpResponseMessage DeleteAuthorityMatrix(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            bool bit;
            var resultData = _iAuthorityMatrixService.DeleteAuthorityMatrix( Id,  userId,  tenantId, out bit);
            if (bit == false)
            {
                return ResponseMessages<AuthorityMatrixs>("Error While Delete AuthorityMatrix!", "Ensure that the AuthorityMatrix Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AuthorityMatrixs>("AuthorityMatrix Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Delete AuthorityMatrixAssess
        /// </summary>
        /// <remarks>
        ///
        /// Delete Existing AuthorityMatrixAssess
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("AuthorityMatrixAssess")]
        public HttpResponseMessage DeleteAuthorityMatrixAssess(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            bool bit;
            var resultData = _iAuthorityMatrixService.DeleteAuthorityMatrixAssess( Id,  userId,  tenantId, out bit);
            if (bit == false)
            {
                return ResponseMessages<AuthorityMatrixAssessment>(resultData, "Ensure that the AuthorityMatrixAssess Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<AuthorityMatrixAssessment>("AuthorityMatrixAssess Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);

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
