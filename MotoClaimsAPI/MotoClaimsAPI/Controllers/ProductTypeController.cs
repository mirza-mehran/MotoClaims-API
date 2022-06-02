using MotoClaims.Entities.Product;
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
using System.Web.Http.Cors;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("ProductType")]
    public class ProductTypeController : ApiController
    {
        IProductTypeService _productTypeService;

        public ProductTypeController()
        {

        }

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        /// <summary>
        /// Get All ProductType
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all ProductType
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        public HttpResponseMessage GetProductTypeList()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData =  _productTypeService.GetProductTypeList(tenentId, userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProductType>>("ProductTypes not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ProductType>>("Get ProductTypes list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
