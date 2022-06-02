using AutoMapper;
using MotoClaims.Entities;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Product;
using MotoClaims.Services.Interfaces;
using MotoClaimsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using LinqToExcel;
using MotoClaims.Entities.VehicleParts;
using System.Data;
using System.Data.OleDb;
using MotoClaims.Entities.Benefits;
using Swashbuckle.Swagger.Annotations;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Policy")]
    public class PolicyController : ApiController
    {
        IPolicyService _policyService;
        IProductService _productService;
        IProductTypeService _productTypeService;

        public PolicyController()
        {

        }

        public PolicyController(IPolicyService policyService, IProductTypeService productTypeService, IProductService productService)
        {
            _policyService = policyService;
            _productService = productService;
            _productTypeService = productTypeService;

        }

        /// <summary>
        /// Get All Colour
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Colour
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Colour")]
        public HttpResponseMessage GetColour()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _policyService.GetColour(tenentId, userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<VehicleColour>>("Colour not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<VehicleColour>>("Get Colours list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }


        /// <summary>
        /// Get All PolicyMake
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all PolicyMake
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("PolicyMake")]
        public HttpResponseMessage GetPolicyMake()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _policyService.GetPolicyMake(tenentId, userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<PolicyMake>>("Make not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<PolicyMake>>("Get Makes list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get All PolicyModel
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all PolicyModel 
        ///     
        /// </remarks>
        [HttpGet]
        [Route("PolicyModel")]
        public HttpResponseMessage GetPolicyModel(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            
            IEnumerable <PolicyModel> resultData = _policyService.GetPolicyModel(Id, tenentId, userId);
            IEnumerable<ModelPolicyModel> obj = Mapper.Map<IEnumerable<ModelPolicyModel>>(resultData);

            if (obj.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ModelPolicyModel>>("Model not found", "Ensure that the Make Id included in the request are correct", (int)HttpStatusCode.NotFound, obj);
            }
            return ResponseMessages<IEnumerable<ModelPolicyModel>>("Get Models list Successfully", string.Empty, (int)HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Get PolicyType By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get signle PolicyType by providing PolicyType ID 
        ///     
        /// </remarks>
        [HttpGet]
        [Route("GetPolicyType/{Id:int}")]
        public HttpResponseMessage GetPolicyTypeByID(int Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _policyService.GetProductName(Id, tenentId, userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProductByID>>("Product Names not found", "Ensure that the PolicyType Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ProductByID>>("Get Product Names Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get ProductDetails By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get signle ProductDetails by providing ID 
        ///     
        /// </remarks>
        [HttpGet]
        [Route("GetProductDetails/{Id:int}")]
        public HttpResponseMessage GetProductDetails(int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _policyService.GetProductDetails(id, tenentId, userId);
            if (resultData.Id == 0)
            {
                return ResponseMessages<ProductByID>("Product Detail not found", "Ensure that the Product Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<ProductByID>("Get Product Detail Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Get All Policies
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Policies
        ///     
        /// </remarks>
        [HttpGet]
        [Route("Policies")]
        public HttpResponseMessage GetPolicies()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            var resultData = _policyService.GetPolicies( tenentId, userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<Policy>>("Policies not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<Policy>>("Get Policies list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get Policy By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get signle PolicyDetails by providing ID 
        ///     
        /// </remarks>
        [HttpGet]
        [Route("PolicyById")]
        public HttpResponseMessage GetPolicyById(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _policyService.GetPolicyByID( id, tenentId, userId);
            if (resultData.Id == 0)
            {
                return ResponseMessages<Policy>("Policy not found", "Ensure that the Policy Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<Policy>("Get Policy Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Get Policies Pagination
        /// </summary>
        /// <remarks>
        ///
        /// Get Policies Pagination by providing PageIndex and PageSize
        ///     
        /// </remarks>
        [HttpGet]
        [Route("Pagination")]
        public HttpResponseMessage GetAllPoliciesByPageIndex(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName)
        {

            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            long TotalRecord;
            IEnumerable<ModelPolicy> resultData = _policyService.GetAllPoliciesByPageIndex(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, userId);
            ModelPolicyWithTotalRecord obj = new ModelPolicyWithTotalRecord();
            obj.ModelPolicy = resultData;
            obj.TotalRecord = TotalRecord;

            if (obj.ModelPolicy.Count() == 0)
            {
                return ResponseMessages<ModelPolicyWithTotalRecord>("Policies not found", "Ensure that the PageIndex and PageSize included in the request are correct", (int)HttpStatusCode.OK, obj);
            }
            return ResponseMessages<ModelPolicyWithTotalRecord>("Get Policies list Successfully", string.Empty, (int)HttpStatusCode.OK, obj);

        }


        /// <summary>
        /// Delete Policy By ID
        /// </summary>
        /// <remarks>
        ///
        /// Delete signle PolicyDetails by providing ID 
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("PolicyDel")]
        public HttpResponseMessage GetPolicyDel(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            bool bit ;

            var resultData = _policyService.DeletePolicy(id, tenentId, userId, out bit);
            if (bit == false)
            {
                return ResponseMessages<Policy>(resultData, "Ensure that the User Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<Policy>("Policy Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);

        }

        /// <summary>
        /// Add new Policy
        /// </summary>
        /// <remarks>
        ///
        /// Insert new Policy
        ///     
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage PostPolicy(Policy model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _policyService.PostPolicy(model, tenentId, userId);
            Policy id = new Policy();
            id.Id = Convert.ToInt32(resultData);
            if (resultData == 0)
            {
                return ResponseMessages<Policy>("Error While Saving Policy!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<Policy>("Policy Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Update Policy
        /// </summary>
        /// <remarks>
        ///
        /// Update Existing Policy 
        ///     
        /// </remarks>
        [HttpPut]
        public HttpResponseMessage PutPolicy(Policy model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
          
            var resultData = _policyService.UpdatePolicy(model, tenentId, userId);
            Policy id = new Policy();
            id.Id =Convert.ToInt32(resultData);
            if (resultData == 0)
            {
                return ResponseMessages<Policy>("Error While Updating Policy!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<Policy>("Policy Updating Successfully.", string.Empty, (int)HttpStatusCode.OK, id);
        }


        /// <summary>
        /// Upload Policies
        /// </summary>
        /// <remarks>
        ///
        /// Upload Excel Files Product
        ///     
        /// </remarks>
        //[AllowAnonymous]
        //[SwaggerOperation("uploadsinglefile")]
        //[Route("ImportPolicy")]
        //[HttpPost]
        //public HttpResponseMessage UploadExcelPolicy()
        //{
        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
        //    tenentId = 2;

        //    var FileUpload = HttpContext.Current.Request.Files[0];
        //    List<PolicyImport> datalist = new List<PolicyImport>();
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
        //            var ValidateRows = (from a in excelFile.Worksheet<PolicyImport>(sheetName)
        //                                select a);
        //            try
        //            {

        //                foreach (var a in ValidateRows)
        //                {
        //                    PolicyImport TU = new PolicyImport();
        //                    TU.ErrorList = new List<ErrorList>();

        //                    var resultData = _productTypeService.GetProductTypeList(tenentId, userId);
        //                    var PType = resultData.Where(x => x.Id == a.PolicyType).ToList();

        //                    var resultDataPro = _productService.GetProducts(tenentId, userId);
        //                    var ProductsId = resultDataPro.Where(x => x.id == a.ProductId).ToList();

        //                    var resultDataMake = _policyService.GetPolicyMake(tenentId, userId);
        //                    var MakesId = resultDataMake.Where(x => x.Id == a.MakeId).ToList();

        //                    var makeid = MakesId.FirstOrDefault();
        //                    long Id = Convert.ToInt64(makeid.Id);

        //                    var resultDataModel = _policyService.GetPolicyModel(Id, tenentId, userId);
        //                    var ModelsId = resultDataModel.Where(x => x.Id == a.ModelId).ToList();

        //                    var resultDataColour = _policyService.GetColour(tenentId, userId);
        //                    var ColoursId = resultDataColour.Where(x => x.Id == a.ColourId).ToList();

        //                    TU.PolicyNo = a.PolicyHolderName;
        //                    TU.PolicyHolderName = a.PolicyHolderName;
        //                    TU.IdentityNo = a.IdentityNo;
        //                    TU.DOB = a.DOB;
        //                    TU.PolicyType = a.PolicyType;
        //                    TU.ProductId = a.ProductId;
        //                    TU.StartDate = a.StartDate;
        //                    TU.EndDate = a.EndDate;
        //                    TU.MakeId = a.MakeId;
        //                    TU.ModelId = a.ModelId;
        //                    TU.CarNumber = a.CarNumber;
        //                    TU.DrivingLicenseValidity = a.DrivingLicenseValidity;
        //                    TU.Address = a.Address;
        //                    TU.PlateNumber = a.PlateNumber;
        //                    TU.ChassisNumber = a.ChassisNumber;
        //                    TU.ColourId = a.ColourId;
        //                    TU.Year = a.Year;
        //                    TU.Capacity = a.Capacity;

        //                    if (a.PolicyNo != null && a.MakeId > 0 && a.ModelId > 0 && a.PolicyHolderName != string.Empty && a.PolicyHolderName != null && PType.Count() > 0 && ProductsId.Count() > 0 && MakesId.Count() > 0 && ModelsId.Count() > 0 && ColoursId.Count() > 0)
        //                    {
        //                        TU.HasError = false;
        //                        TU.ErrorList = null;
        //                        datalist.Add(TU);
        //                    }
        //                    else
        //                    {
        //                        InsertBit = true;
        //                        TU.HasError = true;

        //                        if (a.PolicyNo == string.Empty || a.PolicyNo == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "PolicyNo is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "PolicyNo"; TU.ErrorList.Add(errorList); }
        //                        if (a.PolicyHolderName == string.Empty || a.PolicyHolderName == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "PolicyHolderName is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "PolicyHolderName"; TU.ErrorList.Add(errorList); }
        //                        if (a.IdentityNo == string.Empty || a.IdentityNo == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "IdentityNo is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "IdentityNo"; TU.ErrorList.Add(errorList); }

        //                        if (a.DOB == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "DOB is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "DOB"; TU.ErrorList.Add(errorList); }
        //                        if (a.StartDate == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "StartDate is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "StartDate"; TU.ErrorList.Add(errorList); }
        //                        if (a.EndDate == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "EndDate is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "EndDate"; TU.ErrorList.Add(errorList); }


        //                        if (PType.Count() == 0 || a.PolicyType.ToString() == string.Empty) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid PolicyType"; errorList.ErrorType = "Validation"; errorList.FiledName = "PolicyType"; TU.ErrorList.Add(errorList); }
        //                        if (ProductsId.Count() == 0 || a.ProductId.ToString() == string.Empty) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ProductId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ProductId"; TU.ErrorList.Add(errorList); }

        //                        if (MakesId.Count() == 0 || a.MakeId.ToString() == string.Empty) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid MakeId"; errorList.ErrorType = "Validation"; errorList.FiledName = "MakeId"; TU.ErrorList.Add(errorList); }
        //                        if (ModelsId.Count() == 0 || a.ModelId.ToString() == string.Empty) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ModelId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ModelId"; TU.ErrorList.Add(errorList); }
        //                        if (ColoursId.Count() == 0 || a.ColourId.ToString() == string.Empty) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ColourId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ColourId"; TU.ErrorList.Add(errorList); }

        //                        if (a.CarNumber == string.Empty || a.CarNumber == null ) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "CarNumber is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "CarNumber"; TU.ErrorList.Add(errorList); }
        //                        if (a.DrivingLicenseValidity == null ) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "DrivingLicenseValidity is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "DrivingLicenseValidity"; TU.ErrorList.Add(errorList); }
        //                        if (a.Address == string.Empty || a.Address == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Address is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "Address"; TU.ErrorList.Add(errorList); }
        //                        if (a.PlateNumber == string.Empty || a.PlateNumber == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "PlateNumber is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "PlateNumber"; TU.ErrorList.Add(errorList); }
        //                        if (a.ChassisNumber == string.Empty || a.ChassisNumber == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "ChassisNumber is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "ChassisNumber"; TU.ErrorList.Add(errorList); }
        //                        if (a.Year == string.Empty || a.Year == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Year is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "Year"; TU.ErrorList.Add(errorList); }
        //                        if (a.Capacity == 0 || a.Capacity.ToString() == string.Empty) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Capacity is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "Capacity"; TU.ErrorList.Add(errorList); }

        //                        datalist.Add(TU);
        //                    }
        //                }

        //                if (InsertBit == false)
        //                {
        //                    foreach (var item in ValidateRows)
        //                    {
        //                        bool bit;
        //                        var resultDataPro = _policyService.GetProductDetails(item.ProductId, tenentId, userId);

        //                        item.Benefits = resultDataPro.Benefit;
        //                        item.CoPayPercentage = resultDataPro.CoPayPercentage;
        //                        item.Deductibles = resultDataPro.Deductibles;
        //                        item.IsAgencyRepair = resultDataPro.IsAgencyRepair;

        //                        var obj = Mapper.Map<Policy>(item);
        //                        var resultData = _policyService.PostPolicy(obj, tenentId, userId);

        //                    }

        //                    return ResponseMessages<List<PolicyImport>>("Polices Imported Successfully", string.Empty, (int)HttpStatusCode.OK, datalist);
        //                }

        //                //deleting excel file from folder
        //                if ((System.IO.File.Exists(pathToExcelFile)))
        //                {
        //                    System.IO.File.Delete(pathToExcelFile);
        //                }

        //                return ResponseMessages<List<PolicyImport>>("Validation Error", string.Empty, (int)HttpStatusCode.BadRequest, datalist);
        //            }
        //            catch (Exception ex)
        //            {
        //                return ResponseMessages<List<PolicyImport>>("Polices has not been Imported Successfully.", string.Empty, (int)HttpStatusCode.NotFound, null);
        //            }
        //        }
        //        else
        //        {
        //            return ResponseMessages<List<PolicyImport>>("Only Excel file format is allowed", string.Empty, (int)HttpStatusCode.BadRequest, null);
        //        }
        //    }
        //    else
        //    {
        //        return ResponseMessages<List<PolicyImport>>("Please choose Excel file", string.Empty, (int)HttpStatusCode.NotFound, null);
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
