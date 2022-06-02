using Microsoft.Owin;
using MotoClaims.Entities;
using MotoClaims.Entities.Claim;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.User;
using MotoClaims.Services.Interfaces;
using MotoClaimsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Claims")]
    public class ClaimsController : ApiController
    {
        IClaimsService _claimsService;
        IUserManagementService userManagementService;

        public ClaimsController()
        {

        }

        public ClaimsController(IClaimsService claimsService, IUserManagementService _userManagementService)
        {
            _claimsService = claimsService;
            userManagementService = _userManagementService;
        }

        /// <summary>
        /// Get All Policies By Civial Id
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Policies
        ///     
        /// </remarks>
        [HttpGet]
        [Route("Policies")]
        public HttpResponseMessage GetPolicies(string CivialId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _claimsService.GetPolicies(CivialId,tenentId, userId);
            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<Policy>>("Policies not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Policy>>("Get Policies list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get All Claims By PolicyId
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of Claims 
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Claims")]
        public HttpResponseMessage GetClaims(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            //tenentId = 2;
            var resultData = _claimsService.GetClaimsByPolicyId( Id,  tenentId,  userId);

            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Claims>>("Claims not found", "Ensure that the PolicyId included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Claims>>("Get Claims list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }


        /// <summary>
        /// Get All Claims By Civil Id
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of Claims 
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("MobileClaims")]
        public HttpResponseMessage GetMBClaims(long CivilId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            var resultData = _claimsService.GetMBClaims(CivilId, tenentId, userId);

            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<MBClaims>>("Claims not found", "Ensure that the CivilId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<MBClaims>>("Get Claims list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get Claim By Claim Id
        /// </summary>
        /// <remarks>
        ///
        /// Get an details of Claim
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("MobileClaim")]
        public HttpResponseMessage GetMBClaim(long ClaimId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            var resultData = _claimsService.GetMBClaim(ClaimId, tenentId, userId);

            if (resultData == null)
            {
                return ResponseMessages<MBClaim>("Claim not found", "Ensure that the ClaimId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<MBClaim>("Get Claim Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get Claims
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of Claims
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("AllClaims")]
        public HttpResponseMessage GetAllClaims(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _claimsService.GetAllClaim(PageIndex,  PageSize,  SearchText,  SearchOption,  SortType,  SortName, tenantId,  userId);
            if (resultData == null)
            {
                return ResponseMessages<AllClaimsWithCount>("Claims not found", "",(int)HttpStatusCode.NotFound,resultData);
            }

            return ResponseMessages<AllClaimsWithCount>("Get Claims Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get Claim By Id
        /// </summary>
        /// <remarks>
        ///
        /// Get Existing Single Claim By ID
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Claim")]
        public HttpResponseMessage GetClaimId(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            //tenantId = 2;
            //var resultData = _claimsService.GetClaimsById( Id,  userId,  tenantId);
            var resultData = _claimsService.GetClaimsById(Id, tenantId);
            if (resultData == null)
            {
                return ResponseMessages<Claims>("Claim not found", "Ensure that the ClaimId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<Claims>("Get Claim  Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get ClaimAction 
        /// </summary>
        /// <remarks>
        ///
        /// Get Existing ClaimAction By RoleId
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ClaimActions")]
        public HttpResponseMessage GetClaimActions(long ClaimId, long RoleId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _claimsService.GetClaimAction( ClaimId,  RoleId, tenantId, userId);
            if (resultData == null)
            {
                return ResponseMessages<ClaimActions>("ClaimActions not found", "Ensure that the ClaimId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<ClaimActions>("Get ClaimActions  Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get ClaimType
        /// </summary>
        /// <remarks>
        ///
        /// Get Array ClaimType
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ClaimType")]
        public HttpResponseMessage GetClaimType()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            var resultData = _claimsService.GetClaimsType(tenantId,  userId);

            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ClaimType>>("ClaimType not found", "Ensure that the ClaimId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ClaimType>>("Get ClaimType list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get ClaimRejectionReason 
        /// </summary>
        /// <remarks>
        ///
        /// Get Array ClaimRejectionReason
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ClaimRejectionReason")]
        public HttpResponseMessage GetClaimRejectionReason()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _claimsService.GetClaimRejectionReason(tenantId, userId);
            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<ClaimRejectionReason>>("ClaimRejectionReason not found", "", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ClaimRejectionReason>>("Get list ClaimRejectionReason Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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

            IEnumerable<UserProfiles> resultData = _claimsService.GetUserProfiles(0, 0, tenentId, SearchText);

            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<UserProfiles>>("UserProfiles not found", "Ensure that the SearchText parameter included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<UserProfiles>>("Get UserProfiles List Successfully.", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get ClaimStatus By Claim Id
        /// </summary>
        /// <remarks>
        ///
        /// Get Array ClaimStatus By Claim Id
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ClaimStatus")]
        public HttpResponseMessage GetClaimStatus_Master(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            var resultData = _claimsService.GetClaimStatus_Master(Id,tenantId,userId);

            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<Status_Master>>("ClaimStatus not found", "", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<Status_Master>>("Get list ClaimStatus Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get ClaimStatus
        /// </summary>
        /// <remarks>
        ///
        /// Get Array ClaimStatus
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("AllClaimStatus")]
        public HttpResponseMessage GetAllClaimStatus_Master()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            var resultData = _claimsService.GetClaimAllStatus_Master(tenantId, userId);

            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<Status_Master>>("ClaimStatus not found", "", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<Status_Master>>("Get list ClaimStatus Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Add new Claim
        /// </summary>
        /// <remarks>
        ///
        /// Insert new Claim
        ///     
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage PostClaim([FromBody]Claims obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            //tenentId = 2;
            bool bit = false;
            if (ModelState.IsValid)
            {
                var Roles = userManagementService.GetRoles(tenantId);
                string RoleName = Roles.Where(x => x.RoleId == obj.AddedById).Select(x => x.RoleName).FirstOrDefault().ToString();
                obj.AddedByType = RoleName;


                long resultData = _claimsService.InsertClaims(obj, tenantId, userId, out bit);

                Claims claim = new Claims();
                claim.ClaimId = resultData;
                if (bit == false)
                {
                    return ResponseMessages<Claims>("Ensure that the data included in the request are correct"," ", (int)HttpStatusCode.BadRequest, claim);
                } 
                return ResponseMessages<Claims>("Claim Save Successfully ", string.Empty, (int)HttpStatusCode.OK, claim);
            }
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }


        /// <summary>
        /// Add Claim Image
        /// </summary>
        /// <remarks>
        ///
        /// Add Claim Image
        ///     
        /// </remarks>
        [Route("ClaimImages")]
        [HttpPost]
        public HttpResponseMessage PostImage()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Fetch the File.
            string resultData = null;
          
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                ClaimAccidentCarPhotos obj = new ClaimAccidentCarPhotos();
                obj.ClaimId = Convert.ToInt64(HttpContext.Current.Request.Form[0]);
                obj.PolicyId = Convert.ToInt64(HttpContext.Current.Request.Form[1]);
                obj.MakeId = Convert.ToInt64(HttpContext.Current.Request.Form[2]);
                obj.ModelId = Convert.ToInt64(HttpContext.Current.Request.Form[3]);
                obj.AccidentCarPhotoId = Convert.ToInt64(HttpContext.Current.Request.Form[4]);
                obj.ClaimAttachmentId = Convert.ToInt64(HttpContext.Current.Request.Form[5]);
                obj.ClaimPhotoTypeId = Convert.ToInt64(HttpContext.Current.Request.Form[6]);

                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                string fileName = Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(path + Timestamp + fileName);

                var ImageUrl = "/Uploads/" + Timestamp + fileName;
                obj.Path = ImageUrl;
                
                resultData = _claimsService.InsertClaimsImage(obj,  tenantId,  userId);
            }

            if (resultData == null)
            {
                return ResponseMessages<ClaimAccidentCarPhotos>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<ClaimAccidentCarPhotos>("Claim Image Save Successfully ", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Add Mobile Claim Image
        /// </summary>
        /// <remarks>
        ///
        /// Add Mobile Claim Image
        ///     
        /// </remarks>
        //[Route("Mobile/ClaimImages")]
        //[HttpPost]
        //public HttpResponseMessage PostMBImage([FromBody]HttpPostedFileBase file)
        //{
        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

        //    if (file.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);
        //        var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), fileName);
        //        file.SaveAs(path);
        //        return ResponseMessages<ClaimAccidentCarPhotos>("Claim Image Save Successfully ", string.Empty, (int)HttpStatusCode.OK, null);
        //    }

        //    using (WebClient webClient = new WebClient())
        //    {
        //        byte[] data = webClient.DownloadData(obj.uri);
        //        string resultData = null;
        //        using (MemoryStream mem = new MemoryStream(data))
        //        {
        //            using (var yourImage = Image.FromStream(mem))
        //            {
        //                string path = HttpContext.Current.Server.MapPath("~/Uploads/");
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }
        //                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        //                if (obj.type == "image/jpeg" || obj.type == "image/jpg")
        //                {
        //                    yourImage.Save(path + Timestamp + obj.fileName, ImageFormat.Jpeg);
        //                }
        //                else if (obj.type == "image/png")
        //                {
        //                    yourImage.Save(path + Timestamp + obj.fileName, ImageFormat.Png);
        //                }

        //                var ImageUrl = "/Uploads/" + Timestamp + obj.fileName;
        //                obj.Path = ImageUrl;
        //                resultData = _claimsService.InsertClaimsImage(obj, tenantId, userId);
        //            }
        //        }
        //    }

        //    return ResponseMessages<ClaimAccidentCarPhotos>("Error", string.Empty, (int)HttpStatusCode.OK, null);
        //}

        /// <summary>
        /// Add Mobile Claim Attachments
        /// </summary>
        /// <remarks>
        ///
        /// Add Mobile Claim Attachments
        ///     
        /// </remarks>
        //[Route("Mobile/ClaimAttachment")]
        //[HttpPost]
        //public HttpResponseMessage PostAttachment([FromBody]ClaimDocuments obj)
        //{
        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

        //    using (WebClient webClient = new WebClient())
        //    {
        //        byte[] data = webClient.DownloadData(obj.uri);
        //        string resultData = null;
        //        using (MemoryStream mem = new MemoryStream(data))
        //        {
        //            using (var yourImage = Image.FromStream(mem))
        //            {
        //                string path = HttpContext.Current.Server.MapPath("~/Uploads/");
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }
        //                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                        
        //                yourImage.Save(path + Timestamp + obj.fileName + obj.type);
                        
        //                var ImageUrl = "/Uploads/" + Timestamp + obj.fileName;
        //                obj.Path = ImageUrl;
        //                resultData = _claimsService.InsertClaimsDocument(obj, tenantId, userId);

        //            }
        //        }
        //    }
        //    return ResponseMessages<ClaimDocuments>("Claim Attachment Save Successfully ", string.Empty, (int)HttpStatusCode.OK, null);
        //}

        /// <summary>
        /// Add Claim Attachments
        /// </summary>
        /// <remarks>
        ///
        /// Add Claim Attachments
        ///     
        /// </remarks>
        [Route("ClaimAttachment")]
        [HttpPost]
        public HttpResponseMessage PostAttachment()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            tenantId = 2;

            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            string resultData = null;
            string AlreadyPath = HttpContext.Current.Request.Form[6];

            if (AlreadyPath != "")
            {
                ClaimDocuments obj = new ClaimDocuments();
                obj.ClaimId     = Convert.ToInt64(HttpContext.Current.Request.Form[0]);
                obj.PolicyId    = Convert.ToInt64(HttpContext.Current.Request.Form[1]);
                obj.MakeId      = Convert.ToInt64(HttpContext.Current.Request.Form[2]);
                obj.ModelId     = Convert.ToInt64(HttpContext.Current.Request.Form[3]);
                obj.DocumentTypeId    = Convert.ToInt64(HttpContext.Current.Request.Form[4]);
                obj.ClaimAttachmentId = Convert.ToInt64(HttpContext.Current.Request.Form[5]);
                obj.Path = AlreadyPath;
                resultData = _claimsService.InsertClaimsDocument(obj, tenantId, userId);
            }
            else
            {
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    ClaimDocuments obj = new ClaimDocuments();
                    obj.ClaimId     = Convert.ToInt64(HttpContext.Current.Request.Form[0]);
                    obj.PolicyId = Convert.ToInt64(HttpContext.Current.Request.Form[1]);
                    obj.MakeId = Convert.ToInt64(HttpContext.Current.Request.Form[2]);
                    obj.ModelId = Convert.ToInt64(HttpContext.Current.Request.Form[3]);
                    obj.DocumentTypeId = Convert.ToInt64(HttpContext.Current.Request.Form[4]);
                    obj.ClaimAttachmentId = Convert.ToInt64(HttpContext.Current.Request.Form[5]);

                    var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                    HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(path + Timestamp + fileName);

                    var ImageUrl = "/Uploads/" + Timestamp + fileName;
                    obj.Path = ImageUrl;
                    
                    resultData = _claimsService.InsertClaimsDocument(obj, tenantId, userId);
                }
            }

            if (resultData == null)
            {
                return ResponseMessages<ClaimDocuments>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<ClaimDocuments>("Claim Attachment Save Successfully ", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Add Claim Appeal
        /// </summary>
        /// <remarks>
        ///
        /// Add Claim Appeal
        ///     
        /// </remarks>
        [Route("ClaimAppeal")]
        [HttpPost]

        public HttpResponseMessage PostClaimAppeal(ClaimAppeal obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var tenantId =Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            var userId =Convert.ToInt64( claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _claimsService.InsertClaimAppeal(obj,tenantId,userId);
            if (resultData == null)
            {
                return ResponseMessages<ClaimAppeal>(resultData, " ", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<ClaimAppeal>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Update Claim
        /// </summary>
        /// <remarks>
        ///
        /// Update Existing Claim
        ///     
        /// </remarks>
        [HttpPut] 
        public HttpResponseMessage PutClaim([FromBody]Claims obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            tenantId = 2;
            bool bit = false;
            if (ModelState.IsValid)
            {
                var Roles = userManagementService.GetRoles(tenantId);
                string RoleName = Roles.Where(x => x.RoleId == obj.AddedById).Select(x => x.RoleName).FirstOrDefault().ToString();
                obj.AddedByType = RoleName;

                long resultData = _claimsService.UpdateClaims(obj, tenantId, userId, out bit);
                Claims claim = new Claims();
                claim.ClaimId = resultData;

                if (bit == false)
                {
                    return ResponseMessages<Claims>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, claim);
                }
                return ResponseMessages<Claims>("Claim Update Successfully ", string.Empty, (int)HttpStatusCode.OK, claim);
            }
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        /// <summary>
        /// Upload Claim Image
        /// </summary>
        /// <remarks>
        ///
        /// Upload Claim Image
        ///     
        /// </remarks>
        [Route("ClaimImages")]
        [HttpPut]
        public HttpResponseMessage PutImage()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Fetch the File.
            string resultData = null;

            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                ClaimAccidentCarPhotos obj = new ClaimAccidentCarPhotos();
                obj.ClaimId = Convert.ToInt64(HttpContext.Current.Request.Form[0]);
                obj.PolicyId = Convert.ToInt64(HttpContext.Current.Request.Form[1]);
                obj.MakeId = Convert.ToInt64(HttpContext.Current.Request.Form[2]);
                obj.ModelId = Convert.ToInt64(HttpContext.Current.Request.Form[3]);
                obj.AccidentCarPhotoId = Convert.ToInt64(HttpContext.Current.Request.Form[4]);
                obj.ClaimAttachmentId = Convert.ToInt64(HttpContext.Current.Request.Form[5]);
                obj.ClaimPhotoTypeId = Convert.ToInt64(HttpContext.Current.Request.Form[6]);

                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                string fileName = Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(path + Timestamp + fileName);

                var ImageUrl = "/Uploads/" + Timestamp + fileName;
                obj.Path = ImageUrl;

                resultData = _claimsService.UpdateClaimsImage(obj, tenantId, userId);
            }

            if (resultData == null)
            {
                return ResponseMessages<ClaimAccidentCarPhotos>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<ClaimAccidentCarPhotos>("Claim Image Update Successfully ", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Upload Claim Attachments
        /// </summary>
        /// <remarks>
        ///
        /// Upload Claim Attachments
        ///     
        /// </remarks>
        [Route("ClaimAttachment")]
        [HttpPut]
        public HttpResponseMessage PutAttachment()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            

            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            string resultData = null;
            string AlreadyPath = HttpContext.Current.Request.Form[6];

            if (AlreadyPath != "")
            {
                ClaimDocuments obj = new ClaimDocuments();
                obj.ClaimId = Convert.ToInt64(HttpContext.Current.Request.Form[0]);
                obj.PolicyId = Convert.ToInt64(HttpContext.Current.Request.Form[1]);
                obj.MakeId = Convert.ToInt64(HttpContext.Current.Request.Form[2]);
                obj.ModelId = Convert.ToInt64(HttpContext.Current.Request.Form[3]);
                obj.DocumentTypeId = Convert.ToInt64(HttpContext.Current.Request.Form[4]);
                obj.ClaimAttachmentId = Convert.ToInt64(HttpContext.Current.Request.Form[5]);
                obj.Path = AlreadyPath;
                resultData = _claimsService.UpdateClaimsDocument(obj, tenantId, userId);
            }
            else
            {
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    ClaimDocuments obj = new ClaimDocuments();
                    obj.ClaimId             =   Convert.ToInt64(HttpContext.Current.Request.Form[0]);
                    obj.PolicyId            =   Convert.ToInt64(HttpContext.Current.Request.Form[1]);
                    obj.MakeId              =   Convert.ToInt64(HttpContext.Current.Request.Form[2]);
                    obj.ModelId             =   Convert.ToInt64(HttpContext.Current.Request.Form[3]);
                    obj.DocumentTypeId      =   Convert.ToInt64(HttpContext.Current.Request.Form[4]);
                    obj.ClaimAttachmentId   =   Convert.ToInt64(HttpContext.Current.Request.Form[5]);
                    obj.CD_Id               = Convert.ToInt64(HttpContext.Current.Request.Form[7]);

                    var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                    HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(path + Timestamp + fileName);

                    var ImageUrl = "/Uploads/" + Timestamp + fileName;
                    obj.Path = ImageUrl;

                    resultData = _claimsService.UpdateClaimsDocument(obj, tenantId, userId);
                }
            }

            if (resultData == null)
            {
                return ResponseMessages<ClaimDocuments>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<ClaimDocuments>("Claim Attachment Update Successfully ", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Update Status RejectApprove
        /// </summary>
        /// <remarks>
        ///
        /// Update Existing Claim Status
        ///     
        /// </remarks>
        [Route("UpdateStatusRejectApprove")]
        [HttpPut]
        public HttpResponseMessage PutUpdateStatusRejectApprove([FromBody]Claims obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            if (ModelState.IsValid)
            {
                string resultData = _claimsService.UpdateStatusRejectApprove( obj,tenantId,  userId);

                if (resultData == null)
                {
                    return ResponseMessages<Claims>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<Claims>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            }
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        /// <summary>
        /// Add new ClaimLeaveMessage
        /// </summary>
        /// <remarks>
        ///
        /// Insert new ClaimLeaveMessage
        ///     
        /// </remarks>
        [Route("ClaimLeaveMessage")]
        [HttpPost]
        public HttpResponseMessage PostClaimLeaveMessage([FromBody]ClaimLeaveMessage obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            bool bit = false;
            if (ModelState.IsValid)
            {
                string resultData = _claimsService.InsertClaimsLeaveMessage(obj, tenantId,  userId, out bit);

                if (bit == false)
                {
                    return ResponseMessages<ClaimLeaveMessage>(resultData,"Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }

                return ResponseMessages<ClaimLeaveMessage>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            }
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }


        /// <summary>
        /// Get Claim Image By Claim Id
        /// </summary>
        /// <remarks>
        ///
        /// Get Claim Image By Claim Id
        ///     
        /// </remarks>
        [Route("Mobile/ClaimImages")]
        [HttpGet]
        public HttpResponseMessage GetClaimImages(long ClaimId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<ClaimAccidentCarPhotos> resultData = _claimsService.GetMBClaimAccidentCarPhotosByClaimId(ClaimId, tenantId);

            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<ClaimAccidentCarPhotos>>("Ensure that the Claim Id in the request are correct", " ", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ClaimAccidentCarPhotos>>("Get Claim list Successfully ", string.Empty, (int)HttpStatusCode.OK, resultData);
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
