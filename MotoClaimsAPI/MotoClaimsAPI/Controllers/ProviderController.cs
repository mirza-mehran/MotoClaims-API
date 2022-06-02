using AutoMapper;
using MotoClaims.Entities;
using MotoClaims.Entities.Provider;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using Newtonsoft.Json;
using MotoClaimsAPI.Models;
using MotoClaims.Entities.Policy;
using System.Web;
//using LinqToExcel;
using MotoClaims.Entities.VehicleParts;
using System.Data;
using System.Data.OleDb;
using Swashbuckle.Swagger.Annotations;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Provider")]
    public class ProviderController : ApiController
    {
        IProviderService _providerService ;

        public ProviderController()
        {

        }

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        /// <summary>
        /// Get All Garage
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Garage
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Garage")]
        public HttpResponseMessage GetGarage()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            
            var resultData = _providerService.GetGarage(tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Provider>>("Garage not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Provider>>("Get Garage list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get All Agency
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Agency
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Agency")]
        public HttpResponseMessage GetAgency()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

       
            var resultData = _providerService.GetAgency(tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Provider>>("Agency not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Provider>>("Get Agency list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get All CarAgency
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all CarAgency
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("CarAgency")]
        public HttpResponseMessage GetCarAgency()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _providerService.GetCarAgency(tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Provider>>("CarAgency not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Provider>>("CarAgency list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }


        /// <summary>
        /// Get All Surveyor
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Surveyor
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Surveyor")]
        public HttpResponseMessage GetSurveyor()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

           
            var resultData = _providerService.GetSurveyor(tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Provider>>("Surveyor not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Provider>>("Surveyor list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
        [HttpGet]
        [Route("Country")]
        public HttpResponseMessage GetCountry()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

          
            var resultData = _providerService.GetProviderCountry(tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProviderCountry>>("Country not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
          
            }
            return ResponseMessages<IEnumerable<ProviderCountry>>("Get Country list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get All City
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of Country City
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("City")]
        public HttpResponseMessage GetCity(long CountryId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

         
            var resultData = _providerService.GetProviderCity(CountryId, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProviderCity>>("City not found", "Empty Record", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ProviderCity>>("Get City list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get All Areas
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of City Areas
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Area")]
        public HttpResponseMessage GetArea(long CityId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _providerService.GetProviderArea(CityId, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<MotoClaims.Entities.Provider.Areas>>("Areas not found", "Ensure that the CityId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<MotoClaims.Entities.Provider.Areas>> ("Get Areas list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Get All ServiceType
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of ServiceType
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ServiceType")]
        public HttpResponseMessage GetServiceType()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _providerService.GetProviderServiceType(tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProviderServiceType>>("ServiceType not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ProviderServiceType>>("Get City list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Get All ProviderService
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of ProviderService
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ProviderService")]
        public HttpResponseMessage GetProviderService(long ServiceTypeId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _providerService.GetProviderServices(ServiceTypeId, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProviderServices>>("ProviderService not found", "Ensure that the ServiceTypeId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ProviderServices>>("Get ProviderService list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }


        /// <summary>
        /// Get Provider By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get Existing Single Provider By ID
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ProviderByID")]
        public HttpResponseMessage GetProviderByID(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _providerService.GetProviderByID(Id, tenentId);
            if (resultData.Id == 0)
            {
                return ResponseMessages<Providers>("Provider not found", "Ensure that the ProviderId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<Providers>("Get Provider list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }


        /// <summary>
        /// Check ProviderName 
        /// </summary>
        /// <remarks>
        ///
        /// Check provider name already existing 
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ProviderName")]
        public HttpResponseMessage GetProviderName(long ProviderTypeId, string name)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            var resultData = _providerService.GetProviderName(ProviderTypeId,name, tenentId);

            if (resultData == null)
            {
                return ResponseMessages<Providers>("Incorrect ProviderTypeId or name.", "Ensure that the ProviderTypeId and name included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<Providers>("Check Provider already exists", string.Empty, (int)HttpStatusCode.OK, resultData);
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
                //string base64Img = image.Base64.Replace("data:image/jpeg;base64,", "");
                byte[] bitmapData = new byte[base64Img.Length];
                bitmapData = Convert.FromBase64String(base64Img);
                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                System.Drawing.Image images;
                var path = HostingEnvironment.MapPath("/Uploads/" + Timestamp + image.ImageName);

                using (var streamBitmap = new MemoryStream(bitmapData))
                {
                    using (images = System.Drawing.Image.FromStream(streamBitmap))
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
        /// Add new Provider
        /// </summary>
        /// <remarks>
        ///
        /// Insert new Provider
        ///     
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage PostProvider([FromBody]Providers model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            if (model.ImageModel != null)
            {
                if (model.ImageModel.Base64 != string.Empty && model.ImageModel.ImageName != string.Empty)
                {
                    if (model.ImageModel.Base64 != null && model.ImageModel.ImageName != null)
                    {
                        model.Image = SaveImage(model.ImageModel);
                    }
                    else
                    {
                        model.Image = null;
                    }
                }
                else
                {
                    model.Image = null;
                }
            }
            else
            {
                model.Image = null;
            }

            long resultData = _providerService.PostProvider(model, tenentId,  userId);
            if (resultData == 0)
            {
                return ResponseMessages<Providers>("Error While Saving Provider!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<Providers>("Provider Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Update Provider
        /// </summary>
        /// <remarks>
        ///
        /// Update Existing Provider
        ///     
        /// </remarks>
        [HttpPut]
        public HttpResponseMessage PutProvider([FromBody] Providers model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            if (model.ImageModel != null)
            {
                if (model.ImageModel.Base64 != string.Empty && model.ImageModel.ImageName != string.Empty)
                {
                    if (model.ImageModel.Base64 != null && model.ImageModel.ImageName != null)
                    {
                        model.Image = SaveImage(model.ImageModel);
                    }
                    else
                    {
                        model.Image = null;
                    }
                }
                else
                {
                    model.Image = null;
                }
            }
            else
            {
                model.Image = null;
            }

            long resultData = _providerService.UpdateProvider(model, tenentId, userId);
            if (resultData == 0)
            {
                return ResponseMessages<Providers>("Error While Updated Provider!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<Providers>("Provider Updated Successfully.", string.Empty, (int)HttpStatusCode.OK, null);

        }

        /// <summary>
        /// Delete Provider By ID
        /// </summary>
        /// <remarks>
        ///
        /// Delete signle ProviderDetails by providing ID 
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("ProviderDel")]
        public HttpResponseMessage GetProviderDel(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _providerService.DeleteProvider(id, tenentId, userId);

            if (resultData == false)
            {
                return ResponseMessages<Providers>("Error While Delete Provider!", "Ensure that the Provider Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<Providers>("Provider Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Get Provider Pagination
        /// </summary>
        /// <remarks>
        ///
        /// Get Provider Pagination by providing PageIndex and PageSize
        ///     
        /// </remarks>
        [HttpGet]
        [Route("Pagination")]
        public HttpResponseMessage GetAllProviderByPageIndex(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName,long ProviderTypeId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            long TotalRecord;
            IEnumerable<ModelProvider> resultData = _providerService.GetAllProviderByPageIndex(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, ProviderTypeId);
            ModelProviderWithTotalRecord obj = new ModelProviderWithTotalRecord();
            obj.ModelProvider = resultData;
            obj.TotalRecord = TotalRecord;

            if (resultData.Count() == 0)
            {
                return ResponseMessages<ModelProviderWithTotalRecord>("Provider not found", "Ensure that the ProviderTypeId, PageIndex and PageSize included in the request are correct", (int)HttpStatusCode.OK, obj);
            }
            return ResponseMessages<ModelProviderWithTotalRecord>("Get Provider list Successfully", string.Empty, (int)HttpStatusCode.OK, obj);

        }



        /// <summary>
        /// Upload Provider
        /// </summary>
        /// <remarks>
        ///
        /// Upload Excel Files Provider
        ///     
        /// </remarks>
        //[AllowAnonymous]
        //[SwaggerOperation("uploadsinglefile")]
        //[Route("ImportProvider")]
        //[HttpPost]
        //public HttpResponseMessage UploadExcelProvider()
        //{

        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
        //    tenentId = 2;

        //    var FileUpload = HttpContext.Current.Request.Files[0];
        //    List<ProviderImport> datalist = new List<ProviderImport>();
        //    bool InsertBit = false;

        //    if (FileUpload.ContentType != null && FileUpload.ContentLength > 0)
        //    {
        //        if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //        {
        //            string filename = FileUpload.FileName;
        //            string targetpath = HttpContext.Current.Server.MapPath("~/Content/Document/");
        //            FileUpload.SaveAs(targetpath + filename);
        //            string pathToExcelFile = targetpath + filename;
        //            var connectionString = "";
        //            if (filename.EndsWith(".xls"))
        //            {
        //                connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
        //            }
        //            else if (filename.EndsWith(".xlsx"))
        //            {
        //                connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
        //            }

        //            var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
        //            var ds = new DataSet();
        //            adapter.Fill(ds, "ExcelTable");
        //            System.Data.DataTable dtable = ds.Tables["ExcelTable"];
        //            string sheetName = "Sheet1";
        //            var excelFile = new ExcelQueryFactory(pathToExcelFile);
        //            var ValidateRows = (from a in excelFile.Worksheet<ProviderImport>(sheetName)
        //                                select a);

        //            var data = excelFile.Worksheet<ProviderImport>(sheetName)
        //             .ToList()
        //             .GroupBy(q => new { q.ProviderTypeId, q.Name })
        //             .Select(g => new ProviderImport
        //             {
        //                 ProviderTypeId = g.Key.ProviderTypeId,
        //                 Name = g.Key.Name,

        //                 ProviderContacts = g.Select(c => new ProviderContact { FullName = c.FullName, PhoneNumber = c.PhoneNumber, Email = c.Email }).ToList(),
        //                 ProviderServiceMapping = g.Select(b => new ProviderServiceMapping { ServiceTypeId = b.ServiceTypeId, ProviderServiceId = b.ProviderServiceId }).ToList(),
        //                 ProviderLocations = g.Select(c => new ProviderLocation { BranchName = c.BranchName, StreetAddress = c.StreetAddress, CountryId = c.CountryId, CityId = c.CountryId, AreaId = c.CountryId, Url = c.Url, latitude = c.latitude, longitude= c.longitude }).ToList(),

        //             })
        //             .ToList();

        //            try
        //            {
        //                foreach (var a in ValidateRows)
        //                {
        //                    ProviderImport TU = new ProviderImport();
        //                    TU.ErrorList = new List<ErrorList>();

        //                    var resultDataServicesType = _providerService.GetProviderServiceType(tenentId);
        //                    var PServiceTypeId = resultDataServicesType.Where(x => x.Id == a.ServiceTypeId).FirstOrDefault();
        //                    long Id =Convert.ToInt64(PServiceTypeId.Id);

        //                    var resultDataServices = _providerService.GetProviderServices(Id, tenentId);
        //                    var PServiceId = resultDataServices.Where(x => x.Id == a.ProviderServiceId).ToList();

        //                    var resultDataCountry = _providerService.GetProviderCountry(tenentId);
        //                    var CountriesId = resultDataCountry.Where(x => x.Id == a.CountryId).FirstOrDefault();
        //                    long CountryId = Convert.ToInt64(CountriesId.Id);

        //                    var resultDataCity = _providerService.GetProviderCity(CountryId, tenentId);
        //                    var CitiesId = resultDataCity.Where(x => x.Id == a.CityId).FirstOrDefault();
        //                    long CityId = Convert.ToInt64(CitiesId.Id);

        //                    var resultDataArea = _providerService.GetProviderArea(CityId, tenentId);
        //                    var AreaId = resultDataArea.Where(x => x.Id == a.AreaId).FirstOrDefault();

        //                    TU.ProviderTypeId = a.ProviderTypeId;
        //                    TU.Name = a.Name;
        //                    TU.FullName = a.FullName;
        //                    TU.PhoneNumber = a.PhoneNumber;
        //                    TU.Email = a.Email;
        //                    TU.ServiceTypeId = a.ServiceTypeId;
        //                    TU.ProviderServiceId = a.ProviderServiceId;
        //                    TU.BranchName = a.BranchName;
        //                    TU.StreetAddress = a.StreetAddress;
        //                    TU.CountryId = a.CountryId;
        //                    TU.CityId = a.CityId;
        //                    TU.AreaId = a.AreaId;
        //                    TU.Url = a.Url;
        //                    TU.latitude = a.latitude;
        //                    TU.longitude = a.longitude;


        //                    if (  a.Name != null && a.FullName != null  && PServiceTypeId.Id > 0 && PServiceId.Count() > 0 && CountryId > 0 && CityId > 0 && AreaId.Id > 0)
        //                    {
        //                        if (a.ProviderTypeId == 1 || a.ProviderTypeId == 2 || a.ProviderTypeId == 3 || a.ProviderTypeId == 4)
        //                        {
        //                            TU.HasError = false;
        //                            TU.ErrorList = null;
        //                            datalist.Add(TU);
        //                        }
                               
        //                    }
        //                    else
        //                    {
        //                        InsertBit = true;
        //                        TU.HasError = true;

        //                        if (a.Name == string.Empty || a.Name == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Name is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "Name"; TU.ErrorList.Add(errorList); }
        //                        if (a.FullName == string.Empty || a.FullName == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "FullName is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "FullName"; TU.ErrorList.Add(errorList); }

        //                        if (PServiceTypeId.Id == 0) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ServiceTypeId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ServiceTypeId"; TU.ErrorList.Add(errorList); }
        //                        if (PServiceId.Count() == 0 ) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ServiceId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ServiceId"; TU.ErrorList.Add(errorList); }

        //                        if (CountryId == 0) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid CountryId";  errorList.ErrorType = "Validation"; errorList.FiledName = "CountryId";  TU.ErrorList.Add(errorList); }
        //                        if (CityId == 0)    { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid CityId";     errorList.ErrorType = "Validation"; errorList.FiledName = "CityId";     TU.ErrorList.Add(errorList); }
        //                        if (AreaId.Id == 0) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid AreaId";     errorList.ErrorType = "Validation"; errorList.FiledName = "AreaId";     TU.ErrorList.Add(errorList); }
        //                        if (a.ProviderTypeId != 1 && a.ProviderTypeId != 2 && a.ProviderTypeId != 3 && a.ProviderTypeId != 4 ) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid IsAgencyRepairId"; errorList.ErrorType = "Validation"; errorList.FiledName = "IsAgencyRepairId"; TU.ErrorList.Add(errorList); }

        //                        datalist.Add(TU);
        //                    }
        //                }

        //                if (InsertBit == false)
        //                {
        //                    foreach (var item in data)
        //                    {
        //                        var obj = Mapper.Map<Providers>(item);
        //                        long resultData = _providerService.PostProvider(obj, tenentId, userId);
        //                    }

        //                    return ResponseMessages<List<ProviderImport>>("Providers Imported Successfully", string.Empty, (int)HttpStatusCode.OK, datalist);
        //                }

        //                //deleting excel file from folder
        //                if ((System.IO.File.Exists(pathToExcelFile)))
        //                {
        //                    System.IO.File.Delete(pathToExcelFile);
        //                }

        //                return ResponseMessages<List<ProviderImport>>("Validation Error", string.Empty, (int)HttpStatusCode.BadRequest, datalist);
        //            }
        //            catch (Exception ex)
        //            {
        //                return ResponseMessages<List<ProviderImport>>("Providers has not been Imported Successfully.", string.Empty, (int)HttpStatusCode.NotFound, null);
        //            }
        //        }
        //        else
        //        {
        //            return ResponseMessages<List<ProviderImport>>("Only Excel file format is allowed", string.Empty, (int)HttpStatusCode.BadRequest, null);
        //        }
        //    }
        //    else
        //    {
        //        return ResponseMessages<List<ProviderImport>>("Please choose Excel file", string.Empty, (int)HttpStatusCode.NotFound, null);
        //    }
        //}

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
