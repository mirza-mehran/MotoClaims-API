using MotoClaims.Entities;
using MotoClaims.Entities.Provider;
using MotoClaims.Services.Interfaces;
using MotoClaimsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class TenantController : ApiController
    {
        ITenantService _tenant;

        public TenantController()
        {

        }

        public TenantController(ITenantService tenant)
        {
            _tenant = tenant;
        }

        /// <summary>
        /// Get All Country
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Country
        ///     
        /// </remarks>
        ///
        [Route("Country")]
        [HttpGet]
        public HttpResponseMessage GetCountry()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
           
            IEnumerable<ProviderCountry> resultData = _tenant.GetCountry(tenentId);

            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProviderCountry>>("Country not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ProviderCountry>>("Get Country list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get UserAccount
        /// </summary>
        /// <remarks>
        ///
        /// Get signle UserAccount by providing Tenant ID
        ///     
        /// </remarks>
        /// 
        [Route("UserAccount")]
        [HttpGet]
        public HttpResponseMessage GetUserAccount()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);

            var resultData = _tenant.GetTenantsByID(tenentId);
            if (resultData.Id == 0)
            {
                var response = ResponseMessages<Tenants>("UserAccount not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
                return response;
            }
            return ResponseMessages<Tenants>("Get UserAccount Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }


        [NonAction]
        public string SaveImage(UploadImage image)
        {
            try
            {
                string base64Img;
                if (image.Type == "png")
                {
                     base64Img = image.Base64.Replace("data:image/png;base64,", "");
                }
                else
                {
                     base64Img = image.Base64.Replace("data:image/jpeg;base64,", "");
                }
                byte[] bitmapData = new byte[base64Img.Length];
                bitmapData = Convert.FromBase64String(base64Img);
                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                Image images;
                var path = HostingEnvironment.MapPath("/Uploads/" + Timestamp + image.ImageName);

                using (var streamBitmap = new MemoryStream(bitmapData))
                {
                    using (images = Image.FromStream(streamBitmap))
                    {
                        images.Save(path);
                        var dbimageurl = "/Uploads/" + Timestamp + image.ImageName;
                        return dbimageurl;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Update UserAccount
        /// </summary>
        /// <remarks>
        ///
        /// Update an existing UserAccount
        ///     
        /// </remarks>
        [Route("UserAccount")]
        [HttpPut]
        public HttpResponseMessage PutUserAccount(Tenants model)
        {

            if (model.ImageModel != null)
            {
                if (model.ImageModel.Base64 != string.Empty && model.ImageModel.ImageName != string.Empty)
                {
                    if (model.ImageModel.Base64 != null && model.ImageModel.ImageName != null)
                    {
                        model.TenantLogoPath = SaveImage(model.ImageModel);
                    }
                    else
                    {
                        model.TenantLogoPath = null;
                    }
                }
                else
                {
                    model.TenantLogoPath = null;
                }
            }
            else
            {
                model.TenantLogoPath = null;
            }

            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _tenant.Update_Tenant(model, tenentId, userId);
            if (resultData == false)
            {
                return ResponseMessages<Tenants>("Error While Updating User Account!", "Ensure that the User Account Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<Tenants>("User Account Updating Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
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
