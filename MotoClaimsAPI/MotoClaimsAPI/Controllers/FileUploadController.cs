//using LinqToExcel;
using MotoClaims.Entities;
using MotoClaims.Entities.VehicleParts;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using MotoClaimsAPI.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using MotoClaims.Entities.Product;
using AutoMapper;
using MotoClaims.Entities.Benefits;
using Swashbuckle.Swagger.Annotations;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/File")]
    public class FileUploadController : ApiController
    {
        IPolicyService _policyService;
        IProductService _productService;
        IProductTypeService _productTypeService;

        public FileUploadController()
        {

        }

        public FileUploadController(IPolicyService policyService, IProductService productService, IProductTypeService productTypeService)
        {
            _policyService = policyService;
            _productService = productService;
            _productTypeService = productTypeService;
        }

        /// <summary>
        /// Upload Image
        /// </summary>
        /// <remarks>
        ///
        /// Upload Image
        ///     
        /// </remarks>

        [Route("Insert")]
        [HttpPost]
        public IHttpActionResult UploadFiles()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Fetch the File.
            //HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            long Id = Convert.ToInt64(HttpContext.Current.Request.Form[0]);
            ImagesUpload model = new ImagesUpload();
            string resultData = null;
            model.Id = Id;
            for (int i = 0; i < HttpContext.Current.Request.Files.Count ; i++)
            {
                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                //Fetch the File Name.
                string fileName = Path.GetFileName(postedFile.FileName);
                //Save the File.
                postedFile.SaveAs(path + Timestamp + fileName);
              
                var ImageUrl = "/Uploads/" + Timestamp + fileName;
               
                if (i == 0)
                { 
                    model.Image1 = ImageUrl;
                }
                else if (i == 1)
                {
                    model.Image2 = ImageUrl;
                }
                else if (i == 2)
                {
                    model.Image3 = ImageUrl;
                }
                else if (i == 3)
                {
                    model.Image4 = ImageUrl;
                }
                else if (i == 4)
                {
                    model.Image5 = ImageUrl;
                }
            }

            resultData = _policyService.InsertPolicyImage(model, tenentId, userId);
          

            if (resultData == null)
            {
                return BadRequest("error");
            }
            return Ok(resultData);
        }


        /// <summary>
        /// Update Image
        /// </summary>
        /// <remarks>
        ///
        ///  Update an existing Image By index number
        ///     
        /// </remarks>
        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateUploadFiles()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Fetch the File.
            //HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            long Id = Convert.ToInt64(HttpContext.Current.Request.Form[0]);
            ImagesUpload model = new ImagesUpload();
            string resultData = null;
            model.Id = Id;

            //image1 field name get from front-end
            long Fieldimage = Convert.ToInt64( HttpContext.Current.Request.Form[1]);


            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                //Fetch the File Name.
                string fileName = Path.GetFileName(postedFile.FileName);
                //Save the File.
                postedFile.SaveAs(path + Timestamp + fileName);

                var ImageUrl = "/Uploads/" + Timestamp + fileName;

                if (0 == Fieldimage)
                {
                    model.Image1 = ImageUrl;
                }
                else if (1 == Fieldimage)
                {
                    model.Image2 = ImageUrl;
                }
                else if (2 == Fieldimage)
                {
                    model.Image3 = ImageUrl;
                }
                else if (3 == Fieldimage)
                {
                    model.Image4 = ImageUrl;
                }
                else if (4 == Fieldimage)
                {
                    model.Image5 = ImageUrl;
                }
            }
            resultData = _policyService.UpdatePolicyImage(model, tenentId, userId);
            if (resultData == null)
            {
                return BadRequest("error");
            }
            return Ok(resultData);
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
