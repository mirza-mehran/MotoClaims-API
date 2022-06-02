using AutoMapper;
using MotoClaims.Entities;
using MotoClaims.Entities.User;
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
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    public class UserProfileController : ApiController
    {
        IUserManagementService userManagementService;
        public UserProfileController()
        {
        }
        public UserProfileController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
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
        public HttpResponseMessage GetUserProfiles()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<UserProfile> resultData = userManagementService.GetUserProfiles(0,0,tenentId);

            if (resultData.Count() == 0)
            {
                var response = ResponseMessages<UserProfile>("UserProfiles not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
                return response;
            }
            return ResponseMessages<UserProfile>("Get UserProfiles Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get UserProfiles By Id
        /// </summary>
        /// <remarks>
        ///
        /// Get signle UserProfile by providing Profile ID
        ///     
        /// </remarks>
        // GET: UserProfiles{id}
        [HttpGet]
        public HttpResponseMessage GetUserProfilesById(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<UserProfile> resultData = userManagementService.GetUserProfiles(id, 0, tenentId);
     

            if (resultData.Count() == 0)
            {
                var response = ResponseMessages<IEnumerable<UserProfile>>("UserProfiles not found", "Ensure that the UserProfile Id included in the request are correct", (int)HttpStatusCode.BadRequest, resultData);
                return response;
            }
            return ResponseMessages<IEnumerable<UserProfile>>("Get UserProfile By Id Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get UserProfiles By Role Id
        /// </summary>
        /// <remarks>
        ///
        /// Get signle UserProfile by providing Role ID
        ///     
        /// </remarks>
        // GET: UserProfiles{roleId}
        //[HttpGet]
        //[Route("api/UserProfiles/{roleId}")]
        //public IHttpActionResult GetUserProfilesByRoleId(long roleId)
        //{
        //    long tenentId = 2;
        //    IEnumerable<UserProfile> resultData = userManagementService.GetUserProfiles(0, roleId, tenentId);
        //    if (resultData == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(resultData);
        //}

        /// <summary>
        /// Get UserProfiles By Id
        /// </summary>
        /// <remarks>
        ///
        /// Get signle UserProfile by providing Profile ID
        ///     
        /// </remarks>
        [HttpGet]
        [Route("api/UserProfile/ByID")]
        public HttpResponseMessage GetUserProfileByID(long Id)
        {

            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<UserProfile> resultData = userManagementService.GetUserProfileByID(Id, tenentId);

            if (resultData.Count() == 0)
            {
                var response = ResponseMessages<IEnumerable<UserProfile>>("UserProfiles not found", "Ensure that the UserProfile Id included in the request are correct", (int)HttpStatusCode.BadRequest, resultData);
                return response;
            }
            return ResponseMessages<IEnumerable<UserProfile>>("Get UserProfile By Id Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get UserProfile Already Exists
        /// </summary>
        /// <remarks>
        ///
        /// Get UserProfile Already Exists
        ///     
        /// </remarks>
        // GET: UserProfilesAlreadyExist
        [AllowAnonymous]
        [HttpGet]
        [Route("api/UserProfilesAlreadyExist")]
        
        public HttpResponseMessage GetUserProfilesAlreadyExist(string Username, string Email)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            UserProfile resultData = userManagementService.GetUserAlreadyExist(tenentId, Username, Email);

            if (resultData == null)
            {
                return ResponseMessages<UserProfile>("UserProfiles not exist", "Empty Record", (int)HttpStatusCode.NotFound, null);
            }
            return ResponseMessages<UserProfile>("UserProfiles already exist", string.Empty, (int)HttpStatusCode.OK, null);
        }
        /// <summary>
        /// Get UserProfiles By Id
        /// </summary>
        /// <remarks>
        ///
        /// Get signle UserProfile by providing Profile ID
        ///     
        /// </remarks>
        [HttpGet]
        [Route("api/UserProfile")]
        public HttpResponseMessage GetUserProfile()
        {

            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<UserProfile> resultData = userManagementService.GetUserProfileByID(userId, tenentId);
            if (resultData.Count() == 0)
            {
                var response = ResponseMessages< IEnumerable<UserProfile>>("UserProfiles not found", "Ensure that the UserProfile Id included in the request are correct", (int)HttpStatusCode.BadRequest, resultData);
                return response;
            }
            return ResponseMessages<IEnumerable<UserProfile>>("Get UserProfile By Id Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get UserProfiles Pagination
        /// </summary>
        /// <remarks>
        ///
        /// Get UserProfile Pagination by providing PageIndex and PageSize
        ///     
        /// </remarks>
        [HttpGet]
        [Route("api/UserProfile/Paging")]
        public HttpResponseMessage GetGenericSearch(long PageIndex,long PageSize,string SearchText,string SearchOption,string SortType,string SortName)
        {

            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            long TotalRecord;
            IEnumerable<UserProfile> resultData = userManagementService.GenericSearch(out TotalRecord,PageIndex, PageSize,SearchText,SearchOption,SortType,SortName, tenentId);

            IEnumerable<ModelUserProfile> list = Mapper.Map<IEnumerable<ModelUserProfile>>(resultData);
            ModelUserProfileWithTotalRecord obj = new ModelUserProfileWithTotalRecord();
            obj.ModelUserProfileWithTotalRecords = list;
            obj.TotalRecord = TotalRecord;

            if (obj.ModelUserProfileWithTotalRecords.Count() == 0)
            {
                var response = ResponseMessages<ModelUserProfileWithTotalRecord>("UserProfiles not found", "Ensure that the PageIndex and PageSize included in the request are correct", (int)HttpStatusCode.OK, obj);
                return response;
            }
            return ResponseMessages<ModelUserProfileWithTotalRecord>("Get UserProfiles Successfully", string.Empty, (int)HttpStatusCode.OK, obj);
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
        /// Add UserProfile
        /// </summary>
        /// <remarks>
        ///
        /// Insert new UserProfile
        ///     
        /// </remarks>
        [Route("api/UserProfile")]
        [HttpPost]
        public HttpResponseMessage PostUserProfile([FromBody]ModelUserProfile model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            //tenentId = 2;
            if (model.ImageModel != null)
            {
                if (model.ImageModel.Base64 != string.Empty && model.ImageModel.ImageName != string.Empty )
                {
                    if (model.ImageModel.Base64 != null && model.ImageModel.ImageName != null )
                    {
                        model.ImageUrl = SaveImage(model.ImageModel);
                    }
                    else
                    {
                        model.ImageUrl = null;
                    }
                }
                else
                {
                    model.ImageUrl = null;
                }
            }
            else
            {
                model.ImageUrl = null;
            }
            
          
            UserProfile obj = Mapper.Map<UserProfile>(model);
            obj.CreatedBy = userId;
            if(!string.IsNullOrEmpty(obj.Password)){
                obj.Password = Common.GetSHA256Hash(obj.Password);
            }

            string errMessage = string.Empty;
            bool resultData = userManagementService.InsertUserProfile(obj, tenentId, out errMessage);
            if (!string.IsNullOrEmpty(errMessage))
            {
                return ResponseMessages<ModelUserProfile>(errMessage, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            else
            {
                if (resultData == false)
                {
                    return ResponseMessages<ModelUserProfile>("Error While Saving User Profile!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }
            }

            return ResponseMessages<ModelUserProfile>("User Profile Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Update UserProfile
        /// </summary>
        /// <remarks>
        ///
        /// Update an existing UserProfile
        ///     
        /// </remarks>
        [HttpPut]
        public HttpResponseMessage PutUserProfile([FromBody]ModelUserProfile model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            if (model.ImageModel != null)
            {
                if (model.ImageModel.Base64 != string.Empty && model.ImageModel.ImageName != string.Empty )
                {
                    if (model.ImageModel.Base64 != null && model.ImageModel.ImageName != null )
                    {
                        model.ImageUrl = SaveImage(model.ImageModel);
                    }
                    else
                    {
                        model.ImageUrl = null;
                    }
                }
                else
                {
                    model.ImageUrl = null;
                }
            }
            else
            {
                model.ImageUrl = null;
            }

            UserProfile obj = Mapper.Map<UserProfile>(model);
            obj.UpdatedBy = userId;
            if (!string.IsNullOrEmpty(obj.Password))
            {
                obj.Password = Common.GetSHA256Hash(obj.Password);
            }
            string errMessage = string.Empty;
            bool resultData = userManagementService.UpdateUserProfile(obj, tenentId,out errMessage);
            if(!string.IsNullOrEmpty(errMessage))
            {
                // return BadRequest(errMessage);
                return ResponseMessages<ModelUserProfile>(errMessage, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            else
            {
                if (resultData == false)
                {
                    return ResponseMessages<ModelUserProfile>("Error While Updating User Profile!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }
            }
            return ResponseMessages<ModelUserProfile>("User Profile Updating Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Update UserAccountProfile
        /// </summary>
        /// <remarks>
        ///
        /// Update an existing UserAccountProfile
        ///     
        /// </remarks>
        [Route("api/UserAccount")]
        [HttpPut]
        public HttpResponseMessage PutUserAcProfile([FromBody]ModelUserProfile model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            if (model.CurrentPassword != null &&  !string.IsNullOrEmpty(model.CurrentPassword))
            {
                UserProfile user = userManagementService.GetUserLoginDetail(userId);
                bool passwordMatched;
                passwordMatched = Common.VerifySHA256Hash(model.CurrentPassword, user.Password);
                if (!passwordMatched)
                {
                    return ResponseMessages<ModelUserProfile>("Incorrect current Password!", string.Empty, (int)HttpStatusCode.OK, null);
                }
            }

            if (model.ImageModel != null)
            {
                if (model.ImageModel.Base64 != string.Empty && model.ImageModel.ImageName != string.Empty)
                {
                    if (model.ImageModel.Base64 != null && model.ImageModel.ImageName != null)
                    {
                        model.ImageUrl = SaveImage(model.ImageModel);
                    }
                    else
                    {
                        model.ImageUrl = null;
                    }
                }
                else
                {
                    model.ImageUrl = null;
                }
            }
            else
            {
                model.ImageUrl = null;
            }

            UserProfile obj = Mapper.Map<UserProfile>(model);
            obj.UpdatedBy = userId;

            if (!string.IsNullOrEmpty(obj.Password))
            {
                obj.Password = Common.GetSHA256Hash(obj.Password);
            }
            string errMessage = string.Empty;
            bool resultData = userManagementService.UpdateUserAccount(obj, tenentId, out errMessage);
            if (!string.IsNullOrEmpty(errMessage))
            {
                return ResponseMessages<ModelUserProfile>(errMessage, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            else
            {
                if (resultData == false)
                {
                    return ResponseMessages<ModelUserProfile>("Error While Updating User Profile!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }
            }
            return ResponseMessages<ModelUserProfile>("User Profile Updating Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Delete UserProfile
        /// </summary>
        /// <remarks>
        ///
        /// Delete Existing UserProfile
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("api/UserProfile/Del")]
        public HttpResponseMessage DeleteUserProfile(int Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            bool resultData = userManagementService.DeleteUserProfile(Id, tenentId);
            if (resultData == false)
            {                
                return ResponseMessages<ModelUserProfile>("Error While Delete User Profile!", "Ensure that the User Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<ModelUserProfile>("User Profile Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Add UserProfile For Mobile
        /// </summary>
        /// <remarks>
        ///
        /// Insert new UserProfile
        ///     
        /// </remarks>
        [AllowAnonymous]
        [Route("api/Mobile/UserProfile")]
        [HttpPost]
        public HttpResponseMessage PostMBUserProfile()
        {
            var tenentId = 2;
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Fetch the File.

            ModelUserProfile model = new ModelUserProfile();
            model.FirstName = HttpContext.Current.Request.Form[0];
            model.UserName  = HttpContext.Current.Request.Form[1];
            model.Password  = HttpContext.Current.Request.Form[2];
            model.Email     =  HttpContext.Current.Request.Form[3];
            model.MobileNo  = HttpContext.Current.Request.Form[4];
            model.RoleId    = Convert.ToInt64(HttpContext.Current.Request.Form[5]);

            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                string fileName = Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(path + Timestamp + fileName);
                var ImageUrl = "/Uploads/" + Timestamp + fileName;

                if (i == 0)
                {
                    model.Civil_IdFront = ImageUrl;
                }
                if (i == 1)
                {
                    model.Civil_IdBack = ImageUrl;
                }
            }

            UserProfile obj = Mapper.Map<UserProfile>(model);
            if (!string.IsNullOrEmpty(obj.Password))
            {
                obj.Password = Common.GetSHA256Hash(obj.Password);
            }

            string errMessage = string.Empty;
            bool resultData = userManagementService.MBInsertUserProfile(obj, tenentId, out errMessage);
            if (!string.IsNullOrEmpty(errMessage))
            {
                return ResponseMessages<ModelUserProfile>(errMessage, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            else
            {
                if (resultData == false)
                {
                    return ResponseMessages<ModelUserProfile>("Error While Saving User Profile!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }
            }

            return ResponseMessages<ModelUserProfile>("User Profile Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }
        

        /// <summary>
        /// Get UserProfile For Mobile OTP
        /// </summary>
        /// <remarks>
        ///
        /// Verify UserProfile OTP
        ///     
        /// </remarks>
        [AllowAnonymous]
        [Route("api/Mobile/OTP")]
        [HttpGet]
        public HttpResponseMessage GetOTP(long OTP)
        {

            if (OTP != 1234 )
            {
                return ResponseMessages<ModelUserProfile>("Invalid OTP!", "", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<ModelUserProfile>("OTP authenticated.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Add UserProfileFCMToken
        /// </summary>
        /// <remarks>
        ///
        /// Insert new UserProfileFCMToken
        ///     
        /// </remarks>
        [Route("api/Mobile/FCMToken")]
        [HttpPost]
        public HttpResponseMessage PostUserProfileFCMToken(string FCMToken)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
   
            string resultData = userManagementService.InsertUserProfileFCMToken( tenentId, userId, FCMToken);
            if (string.IsNullOrEmpty(resultData))
            {
                return ResponseMessages<ModelUserProfile>(resultData, "", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<ModelUserProfile>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
        }

        [NonAction]
        public HttpResponseMessage ResponseMessages<T>(string Message, string Detail, int Status,dynamic resultData)
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
