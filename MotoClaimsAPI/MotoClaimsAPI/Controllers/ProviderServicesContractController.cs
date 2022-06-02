using AutoMapper;
using MotoClaims.Entities.Policy;
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
using System.Web;
//using LinqToExcel;
using MotoClaims.Entities.VehicleParts;
using System.Data;
using System.Data.OleDb;
using Swashbuckle.Swagger.Annotations;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/ProviderServicesContract")]
    public class ProviderServicesContractController : ApiController
    {
        IProviderServicesContractService _providerServicesContractService;

        public ProviderServicesContractController()
        {

        }

        public ProviderServicesContractController(IProviderServicesContractService providerServicesContractService)
        {
            _providerServicesContractService = providerServicesContractService;
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
        public HttpResponseMessage GetMake()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _providerServicesContractService.GetMake( tenentId,  userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<PolicyMake>>("Make not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<PolicyMake>>("Get Makes list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
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
        public HttpResponseMessage GetPolicyModel(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);


            IEnumerable<PolicyModel> resultData = _providerServicesContractService.GetModel(Id, tenentId, userId);
            IEnumerable<ModelPolicyModel> obj = Mapper.Map<IEnumerable<ModelPolicyModel>>(resultData);

            if (obj.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ModelPolicyModel>>("Model not found", "Ensure that the Make Id included in the request are correct", (int)HttpStatusCode.NotFound, obj);
            }
            return ResponseMessages<IEnumerable<ModelPolicyModel>>("Get Models list Successfully", string.Empty, (int)HttpStatusCode.OK, obj);
        }


        /// <summary>
        /// Get All ServiceType By ProviderType Id 
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of ServiceType
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ServiceType")]
        public HttpResponseMessage GetServiceType(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _providerServicesContractService.GetProviderServiceType(Id, tenantId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProviderServiceType>>("ServiceType not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ProviderServiceType>>("Get ServiceType list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

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
        [Route("Service")]
        public HttpResponseMessage GetProviderService(long ServiceTypeId)
        {

            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            var resultData = _providerServicesContractService.GetProviderServices(ServiceTypeId, tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProviderServices>>("ProviderService not found", "Ensure that the ServiceTypeId included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<IEnumerable<ProviderServices>>("Get ProviderService list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get Services By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get Existing Services By Provider ID
        ///     
        /// </remarks>
        /// 
        //[HttpGet]
        //[Route("Services")]
        //public HttpResponseMessage GetService(long ProviderId)
        //{
        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

        //    var resultData = _providerServicesContractService.GetProviderServices( ProviderId,  tenantId,  userId);
        //    if (resultData == null)
        //    {
        //        return ResponseMessages<IEnumerable<ProviderServices>>("ProviderService not found", "Ensure that the Provider Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
        //    }
        //    return ResponseMessages<IEnumerable<ProviderServices>>("Get ProviderService list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        //}

      

        /// <summary>
        /// Get ProviderServicesContract By Provider Id
        /// </summary>
        /// <remarks>
        ///
        /// Get Existing list ProviderServicesContract By Provider Id
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ProviderServicesContracts")]
        public HttpResponseMessage GetProvider_Services_Contrac_By_ProviderId(long ProviderId)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            tenentId = 2;
            IEnumerable<ModelProviderContractServices> resultData = _providerServicesContractService.GetProvider_Services_Contrac_By_ProviderId(ProviderId, tenentId, userId);

            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<ModelProviderContractServices>>("Provider_Services_Contracts not found", "Ensure that the Provider_Services_Contract Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ModelProviderContractServices>>("Get Provider_Services_Contracts list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get ProviderServicesContract By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get Existing Single ProviderServicesContract By ID
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ProviderServicesContract")]
        public HttpResponseMessage GetProvider_Services_Contract(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            tenentId = 2;
            ModelProvider_Services_Contract resultData = _providerServicesContractService.GetProvider_Services_Contract(Id,tenentId,userId);
            
            if (resultData == null)
            {
                return ResponseMessages<ModelProvider_Services_Contract>("Provider_Services_Contract not found", "Ensure that the Provider_Services_Contract Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<ModelProvider_Services_Contract>("Get Provider_Services_Contract Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Delete ProviderServicesContract By ID
        /// </summary>
        /// <remarks>
        ///
        /// Delete signle ProviderServicesContract by providing ID 
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("ProviderServicesContract")]
        public HttpResponseMessage DeleteProviderServicesContract(long id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            tenentId = 2;
            var resultData = _providerServicesContractService.DeleteProvider_Services_Contract(id, tenentId, userId);

            if (resultData == false)
            {
                return ResponseMessages<Provider_Services_Contract>("Error While Delete Provider_Services_Contract!", "Ensure that the Provider_Services_Contract Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<Provider_Services_Contract>("ProviderServicesContract Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Add new ProviderServicesContract
        /// </summary>
        /// <remarks>
        ///
        /// Insert new ProviderServicesContract
        ///     
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage PostProviderServicesContract([FromBody]Provider_Services_Contract obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            //tenentId = 2;
            bool bit = false;
            if (ModelState.IsValid)
            {
                string resultData = _providerServicesContractService.InsertProviderContract(obj, tenentId, userId, out bit);

                if (bit == false)
                {
                    return ResponseMessages<Provider_Services_Contract>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<Provider_Services_Contract>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            }
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }


        /// <summary>
        /// Update ProviderServicesContract
        /// </summary>
        /// <remarks>
        ///
        /// Update Existing ProviderServicesContract
        ///     
        /// </remarks>
        [HttpPut]
        public HttpResponseMessage PutProviderServicesContract([FromBody]Provider_Services_Contract obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            tenentId = 2;
            bool bit = false;
            if (ModelState.IsValid)
            {
                string resultData = _providerServicesContractService.UpdateProviderContract(obj, tenentId, userId, out bit);
                if (bit == false)
                {
                    return ResponseMessages<Provider_Services_Contract>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<Provider_Services_Contract>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            }

            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }


       
        /// <summary>
        /// Add new ProviderServicesContract_Price
        /// </summary>
        /// <remarks>
        ///
        /// Insert new ProviderServicesContract_Price
        ///     
        /// </remarks>
        [HttpPost]
        [Route("ProviderServicesContractPrice")]
        public HttpResponseMessage PostProviderServicesContractPrice([FromBody]PS_Prices obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            //tenentId = 2;
            bool bit = false;
            if (ModelState.IsValid)
            {
                string resultData = _providerServicesContractService.InsertProviderContractPrice(obj, tenentId, userId, out bit);

                if (bit == false)
                {
                    return ResponseMessages<PS_Prices>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<PS_Prices>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            }
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        /// <summary>
        /// Update ProviderServicesContract_Price
        /// </summary>
        /// <remarks>
        ///
        /// Update Existing ProviderServicesContract_Price
        ///     
        /// </remarks>
        [HttpPut]
        [Route("ProviderServicesContractPrice")]
        public HttpResponseMessage PutProviderServicesContractPrice([FromBody]PS_Prices obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            tenentId = 2;
            bool bit = false;
            if (ModelState.IsValid)
            {
                string resultData = _providerServicesContractService.UpdateProviderContractPrice(obj, tenentId, userId, out bit);
                if (bit == false)
                {
                    return ResponseMessages<Provider_Services_Contract>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<Provider_Services_Contract>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            }

            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }


        /// <summary>
        /// Get ProviderServicesContractPrice By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get Existing Single ProviderServicesContractPrice By ID
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ProviderServicesContractPrices")]
        public HttpResponseMessage GetProvider_Services_Contract_Prices(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            tenentId = 2;
            IEnumerable<PS_Prices> resultData = _providerServicesContractService.GetProvider_Services_Contrac_Prices(Id, tenentId, userId);

            if (resultData == null)
            {
                return ResponseMessages<IEnumerable<PS_Prices>>("Provider_Services_Contract_Price not found", "Ensure that the Provider_Services_Contract Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<PS_Prices>>("Get Provider_Services_Contract_Price Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }




        /// <summary>
        /// Delete ProviderServicesContract_Price By ID
        /// </summary>
        /// <remarks>
        ///
        /// Delete signle ProviderServicesContract_Price by providing ID 
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("ProviderServicesContractPrice")]
        public HttpResponseMessage DeleteProviderServicesContract_Price(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            tenentId = 2;
            var resultData = _providerServicesContractService.DeleteProvider_Services_Contract_Price( Id, tenentId,  userId);

            if (resultData == false)
            {
                return ResponseMessages<PS_Prices>("Error While Delete Provider_Services_Contract_Price!", "Ensure that the Provider_Services_Contract Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<PS_Prices>("ProviderServicesContract_Price Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Get ProviderServicesContractPrice By ID
        /// </summary>
        /// <remarks>
        ///
        /// Get Existing Single ProviderServicesContractPrice By ID
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ProviderServicesContractPrice")]
        public HttpResponseMessage GetProvider_Services_Contract_Price(long Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            tenentId = 2;
            PS_Prices resultData = _providerServicesContractService.GetProvider_Services_Price( Id, tenentId, userId);

            if (resultData == null)
            {
                return ResponseMessages<PS_Prices>("Provider_Services_Contract_Price not found", "Ensure that the Provider_Services_Contract_Price Id included in the request are correct", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<PS_Prices>("Get Provider_Services_Contract_Price Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }


        /// <summary>
        /// Upload Provider ServicesContarct
        /// </summary>
        /// <remarks>
        ///
        /// Upload Excel Files Provider ServicesContarct
        ///     
        /// </remarks>
        //[AllowAnonymous]
        //[SwaggerOperation("uploadsinglefile")]
        //[Route("ImportProviderServicesContarct")]
        //[HttpPost]
        //public HttpResponseMessage UploadExcelProviderServices()
        //{

        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
        //    tenantId = 2;

        //    var FileUpload = HttpContext.Current.Request.Files[0];
        //    List<Provider_Services_ContractImport> datalist = new List<Provider_Services_ContractImport>();
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
        //            var ValidateRows = (from a in excelFile.Worksheet<Provider_Services_ContractImport>(sheetName)
        //                                select a);

        //            try
        //            {
        //                foreach (var a in ValidateRows)
        //                {
        //                    Provider_Services_ContractImport TU = new Provider_Services_ContractImport();
        //                    TU.ErrorList = new List<ErrorList>();

        //                    var resultDataServicesType = _providerServicesContractService.GetProviderServiceType(TU.Provider_Id, tenantId);
        //                    var PServiceTypeId = resultDataServicesType.Where(x => x.Id == a.ServiceType_Id).FirstOrDefault();
        //                    long Id = Convert.ToInt64(PServiceTypeId.Id);

        //                    var resultDataServices = _providerServicesContractService.GetProviderServices(Id, tenantId);
        //                    var PServiceId = resultDataServices.Where(x => x.Id == a.ProviderService_Id).ToList();

        //                    TU.PSC_Code = a.PSC_Code;
        //                    TU.PSC_Description = a.PSC_Description;
        //                    TU.ServiceType_Id = a.ServiceType_Id;
        //                    TU.ProviderService_Id = a.ProviderService_Id;
        //                    TU.Provider_Id = a.Provider_Id;
                        

        //                    if (a.PSC_Code != null && a.PSC_Code != string.Empty && PServiceTypeId.Id > 0 && PServiceId.Count() > 0 && a.Provider_Id > 0)
        //                    {
        //                        TU.HasError = false;
        //                        TU.ErrorList = null;
        //                        datalist.Add(TU);
        //                    }
        //                    else
        //                    {
        //                        InsertBit = true;
        //                        TU.HasError = true;

        //                        if (a.PSC_Code == string.Empty || a.PSC_Code == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "PSC_Code is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "PSC_Code"; TU.ErrorList.Add(errorList); }
        //                        if (PServiceTypeId.Id == 0) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ServiceTypeId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ServiceTypeId"; TU.ErrorList.Add(errorList); }
        //                        if (PServiceId.Count() == 0) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ServiceId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ServiceId"; TU.ErrorList.Add(errorList); }
        //                        datalist.Add(TU);
        //                    }
        //                }

        //                if (InsertBit == false)
        //                {
        //                    foreach (var item in ValidateRows)
        //                    {
        //                        var obj = Mapper.Map<Provider_Services_Contract>(item);
        //                        bool bit;
        //                        string resultData = _providerServicesContractService.InsertProviderContract(obj, tenantId, userId, out bit);
        //                    }

        //                    return ResponseMessages<List<Provider_Services_ContractImport>>("ServicesContract Imported Successfully", string.Empty, (int)HttpStatusCode.OK, datalist);
        //                }

        //                //deleting excel file from folder
        //                if ((System.IO.File.Exists(pathToExcelFile)))
        //                {
        //                    System.IO.File.Delete(pathToExcelFile);
        //                }

        //                return ResponseMessages<List<Provider_Services_ContractImport>>("Validation Error", string.Empty, (int)HttpStatusCode.BadRequest, datalist);
        //            }
        //            catch (Exception ex)
        //            {
        //                return ResponseMessages<List<Provider_Services_ContractImport>>("ServicesContract has not been Imported Successfully.", string.Empty, (int)HttpStatusCode.NotFound, null);
        //            }
        //        }
        //        else
        //        {
        //            return ResponseMessages<List<Provider_Services_ContractImport>>("Only Excel file format is allowed", string.Empty, (int)HttpStatusCode.BadRequest, null);
        //        }
        //    }
        //    else
        //    {
        //        return ResponseMessages<List<Provider_Services_ContractImport>>("Please choose Excel file", string.Empty, (int)HttpStatusCode.NotFound, null);
        //    }
        //}


        ///// <summary>
        ///// Upload Provider ServicesContarct
        ///// </summary>
        ///// <remarks>
        /////
        ///// Upload Excel Files Provider ServicesContarct
        /////     
        ///// </remarks>
        //[AllowAnonymous]
        //[SwaggerOperation("uploadsinglefile")]
        //[Route("ImportProviderServicesPrices")]
        //[HttpPost]
        //public HttpResponseMessage UploadExcelProviderServicesPrices()
        //{

        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
        //    tenantId = 2;

        //    var FileUpload = HttpContext.Current.Request.Files[0];
        //    List<Provider_ServicesPrices_Import> datalist = new List<Provider_ServicesPrices_Import>();
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
        //            var ValidateRows = (from a in excelFile.Worksheet<Provider_ServicesPrices_Import>(sheetName)
        //                                select a);

        //            try
        //            {
        //                foreach (var a in ValidateRows)
        //                {
        //                    Provider_ServicesPrices_Import TU = new Provider_ServicesPrices_Import();
        //                    TU.ErrorList = new List<ErrorList>();

        //                    var resultDataMake = _providerServicesContractService.GetMake(tenantId, userId);
        //                    var MakesId = resultDataMake.Where(x => x.Id == a.MakeId).FirstOrDefault();
        //                    long makeId = Convert.ToInt64(MakesId.Id);

        //                    var resultDataModel = _providerServicesContractService.GetModel(makeId, tenantId, userId);
        //                    var modelId = resultDataModel.Where(x => x.Id == a.ModelId).ToList();

        //                    var resultData = _providerServicesContractService.GetDate(a.PSC_Id);

        //                    TU.PSC_Id = a.PSC_Id;
        //                    TU.MakeId = a.MakeId;
        //                    TU.ModelId = a.ModelId;
        //                    TU.Year = a.Year;
        //                    TU.Price = a.Price;
        //                    TU.Discount = a.Discount;
        //                    TU.Start_Date = a.Start_Date;
        //                    TU.End_Date = a.End_Date;
        //                    TU.Remark = a.Remark;

        //                    if (resultData.End_Date == DateTime.MinValue && resultData.End_Date < resultData.End_Date && a.PSC_Id > 0 && makeId > 0 && modelId.Count() > 0)
        //                    {
        //                        TU.HasError = false;
        //                        TU.ErrorList = null;
        //                        datalist.Add(TU);
        //                    }
        //                    else
        //                    {
        //                        InsertBit = true;
        //                        TU.HasError = true;

        //                        if (a.End_Date > resultData.End_Date ) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "End Date should be greater then Start date"; errorList.ErrorType = "Validation"; errorList.FiledName = "End_Date"; TU.ErrorList.Add(errorList); }
        //                        if (makeId == 0) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid MakeId"; errorList.ErrorType = "Validation"; errorList.FiledName = "MakeId"; TU.ErrorList.Add(errorList); }
        //                        if (modelId.Count() == 0) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ModelId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ModelId"; TU.ErrorList.Add(errorList); }
        //                        datalist.Add(TU);
        //                    }
        //                }

        //                if (InsertBit == false)
        //                {
        //                    foreach (var item in ValidateRows)
        //                    {
        //                        PS_Prices obj = new PS_Prices();
        //                        obj.PSC_Id    = item.PSC_Id;
        //                        obj.Make      = item.MakeId;
        //                        obj.Model     = item.ModelId;
        //                        obj.Year      = item.Year;
        //                        obj.Price     = item.Price;
        //                        obj.Discount  = item.Discount;
        //                        obj.Start_Date= item.Start_Date;
        //                        obj.End_Date  = item.End_Date;
        //                        obj.Remark    = item.Remark;

        //                        bool bit;
        //                        string resultData = _providerServicesContractService.InsertProviderContractPrice(obj, tenantId, userId, out bit);
        //                    }

        //                    return ResponseMessages<List<Provider_ServicesPrices_Import>>("ServicesContractPrices Imported Successfully", string.Empty, (int)HttpStatusCode.OK, datalist);
        //                }

        //                //deleting excel file from folder
        //                if ((System.IO.File.Exists(pathToExcelFile)))
        //                {
        //                    System.IO.File.Delete(pathToExcelFile);
        //                }

        //                return ResponseMessages<List<Provider_ServicesPrices_Import>>("Validation Error", string.Empty, (int)HttpStatusCode.BadRequest, datalist);
        //            }
        //            catch (Exception ex)
        //            {
        //                return ResponseMessages<List<Provider_ServicesPrices_Import>>("ServicesContractPrices has not been Imported Successfully.", string.Empty, (int)HttpStatusCode.NotFound, null);
        //            }
        //        }
        //        else
        //        {
        //            return ResponseMessages<List<Provider_ServicesPrices_Import>>("Only Excel file format is allowed", string.Empty, (int)HttpStatusCode.BadRequest, null);
        //        }
        //    }
        //    else
        //    {
        //        return ResponseMessages<List<Provider_ServicesPrices_Import>>("Please choose Excel file", string.Empty, (int)HttpStatusCode.NotFound, null);
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
