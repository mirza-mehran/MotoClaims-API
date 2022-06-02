using AutoMapper;
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
using System.Web.Http;
//using LinqToExcel;
using MotoClaims.Entities.VehicleParts;
using System.Data;
using System.Data.OleDb;
using System.Web;
using Microsoft.Office.Interop.Excel;
using MotoClaims.Entities.Benefits;
using Swashbuckle.Swagger.Annotations;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        IProductService _productService;
        IProductTypeService _productTypeService;

        public ProductController()
        {
          
        }

        public ProductController(IProductService productService, IProductTypeService productTypeService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
        }

        /// <summary>
        /// Get All Product
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all Product
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        public HttpResponseMessage GetProductList()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData =  _productService.GetProducts(tenentId,userId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ProductList>>("Products not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ProductList>>("Get Products list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get Product
        /// </summary>
        /// <remarks>
        ///
        /// Get signle Product by providing Product ID 
        ///     
        /// </remarks>

        [HttpGet]
        [Route("{Id:int}")]
        public HttpResponseMessage GetProductByID(int Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData =  _productService.GetProduct(Id, tenentId, userId);
            if (resultData.Id == 0)
            {
                return ResponseMessages<ProductByID>("Product not found", "Ensure that the Product Id included in the request are correct", (int)HttpStatusCode.NotFound, resultData);
            }
            return ResponseMessages<ProductByID>("Get Product Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Add new Product
        /// </summary>
        /// <remarks>
        ///
        /// Insert new Product
        ///     
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage PostProduct([FromBody]ProductAdd model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            bool bit;
            var resultData =  _productService.InsertProduct(model, tenentId, userId, out bit);

            if (bit == false)
            {
                return ResponseMessages<ProductAdd>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }

            return ResponseMessages<ProductAdd>("Product Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <remarks>
        ///
        /// Update an existing Product
        ///     
        /// </remarks>
        [HttpPut]
        public HttpResponseMessage PutProduct([FromBody]ProductAdd model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            bool bit;
            var resultData =  _productService.UpdateProduct(model, tenentId, userId, out bit);

            if (bit == false)
            {
                return ResponseMessages<ProductAdd>("Product has not been Updated Successfully.", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }

            return ResponseMessages<ProductAdd>(resultData , string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Delete Product
        /// </summary>
        /// <remarks>
        ///
        /// Delete Existing Product
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("{Id:int}")]
        public HttpResponseMessage DeleteProduct(int Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            bool bit;

            var resultData =  _productService.DeleteProduct(Id,  tenentId, userId, out bit);

            if (bit == false)
            {
                return ResponseMessages<ProductAdd>(resultData, "Ensure that the User Id included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }

            return ResponseMessages<ProductAdd>("Product Delete Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }


        /// <summary>
        /// Upload Products
        /// </summary>
        /// <remarks>
        ///
        /// Upload Excel Files Products
        ///     
        /// </remarks>
        //[AllowAnonymous]
        //[SwaggerOperation("uploadsinglefile")]
        //[Route("ImportProduct")]
        //[HttpPost]
        //public HttpResponseMessage UploadExcelProduct()
        //{
        //    var FileUpload = HttpContext.Current.Request.Files[0];
        //    List<Product> datalist = new List<Product>();
        //    bool InsertBit = false;

        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
        //    tenentId = 2;

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
        //            var ValidateRows = (from a in excelFile.Worksheet<Product>(sheetName)
        //                                select a);

        //            var data = excelFile.Worksheet<Product>(sheetName)
        //                .ToList()
        //                .GroupBy(q => new { q.ProductName, q.ProductType, q.ProductDetails, q.AnnualPremium, q.Status, q.CoPayPercentage, q.Deductibles })
        //                .Select(g => new Product
        //                {
        //                    ProductName = g.Key.ProductName,
        //                    ProductType = g.Key.ProductType,
        //                    ProductDetails = g.Key.ProductDetails,
        //                    AnnualPremium = g.Key.AnnualPremium,
        //                    Status = g.Key.Status,
        //                    Coverage = g.Select(c => new Coverage { CoPayPercentage = c.CoPayPercentage, Deductibles = c.Deductibles, IsAgencyRepair = c.IsAgencyRepair }).FirstOrDefault(),
        //                    Benefit = g.Select(b => new Benefit { BenefitDetails = b.BenefitDetails }).ToList()
        //                })
        //                .ToList();

        //            try
        //            {


        //                foreach (var a in ValidateRows)
        //                {
        //                    Product TU = new Product();
        //                    TU.ErrorList = new List<ErrorList>();

        //                    var resultData = _productTypeService.GetProductTypeList(tenentId, userId);
        //                    var abc = resultData.Where(x => x.Id == a.ProductType).ToList();

        //                    if (a.ProductType > 0 && a.IsAgencyRepair > 0 && a.ProductName != string.Empty && a.ProductName != null && abc.Count() > 0)
        //                    {
        //                        TU.ProductName = a.ProductName;
        //                        TU.ProductType = a.ProductType;
        //                        TU.ProductDetails = a.ProductDetails;
        //                        TU.AnnualPremium = a.AnnualPremium;
        //                        TU.Status = a.Status;
        //                        TU.CoPayPercentage = a.CoPayPercentage;
        //                        TU.Deductibles = a.Deductibles;
        //                        TU.IsAgencyRepair = a.IsAgencyRepair;
        //                        TU.BenefitDetails = a.BenefitDetails;

        //                        TU.HasError = false;
        //                        TU.ErrorList = null;
        //                        datalist.Add(TU);
        //                    }
        //                    else
        //                    {
        //                        InsertBit = true;

        //                        TU.ProductName = a.ProductName;
        //                        TU.ProductType = a.ProductType;
        //                        TU.ProductDetails = a.ProductDetails;
        //                        TU.AnnualPremium = a.AnnualPremium;
        //                        TU.Status = a.Status;
        //                        TU.CoPayPercentage = a.CoPayPercentage;
        //                        TU.Deductibles = a.Deductibles;
        //                        TU.IsAgencyRepair = a.IsAgencyRepair;
        //                        TU.BenefitDetails = a.BenefitDetails;

        //                        TU.HasError = true;

        //                        if (a.ProductName == string.Empty || a.ProductName == null) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "ProductName is required"; errorList.ErrorType = "Validation"; errorList.FiledName = "ProductName"; TU.ErrorList.Add(errorList); }
        //                        if (a.ProductType == 0 || a.ProductType.ToString() == string.Empty) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "ProductTypeId is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "ProductTypeId"; TU.ErrorList.Add(errorList); }
        //                        if (a.AnnualPremium == 0 || a.AnnualPremium.ToString() == string.Empty) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "AnnualPremium is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "AnnualPremium"; TU.ErrorList.Add(errorList); }
        //                        if (a.CoPayPercentage == string.Empty || a.CoPayPercentage == null || a.CoPayPercentage == "0") { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "CoPayPercentage is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "CoPayPercentage"; TU.ErrorList.Add(errorList); }
        //                        if (a.Deductibles == string.Empty || a.Deductibles == null || a.Deductibles == "0") { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Deductibles is required and the 0 isn't valid"; errorList.ErrorType = "Validation"; errorList.FiledName = "Deductibles"; TU.ErrorList.Add(errorList); }
        //                        if (abc.Count() == 0) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid ProductTypeId"; errorList.ErrorType = "Validation"; errorList.FiledName = "ProductTypeId"; TU.ErrorList.Add(errorList); }
        //                        if (a.IsAgencyRepair != 1 && a.IsAgencyRepair != 2 && a.IsAgencyRepair != 3) { ErrorList errorList = new ErrorList(); errorList.ErrorMessage = "Invalid IsAgencyRepairId"; errorList.ErrorType = "Validation"; errorList.FiledName = "IsAgencyRepairId"; TU.ErrorList.Add(errorList); }

        //                        datalist.Add(TU);
        //                    }
        //                }

        //                if (InsertBit == false)
        //                {
        //                    foreach (var item in data)
        //                    {
        //                        //Sql Procedure 

        //                        bool bit;
        //                        var obj = Mapper.Map<ProductAdd>(item);

        //                        var resultData = _productService.InsertProduct(obj, tenentId, userId, out bit);
        //                    }

        //                    return ResponseMessages<List<Product>>("Products Imported Successfully", string.Empty, (int)HttpStatusCode.OK, datalist);
        //                }

        //                //deleting excel file from folder
        //                if ((System.IO.File.Exists(pathToExcelFile)))
        //                {
        //                    System.IO.File.Delete(pathToExcelFile);
        //                }

        //                return ResponseMessages<List<Product>>("Validation Error", string.Empty, (int )HttpStatusCode.BadRequest, datalist);
        //            }
        //            catch (Exception ex)
        //            {
        //                return ResponseMessages<List<Product>>("Products has not been Imported Successfully.", string.Empty, (int)HttpStatusCode.NotFound, null);
        //            }
        //        }
        //        else
        //        {
        //            return ResponseMessages<List<Product>>("Only Excel file format is allowed", string.Empty, (int)HttpStatusCode.BadRequest, null);
        //        }
        //    }
        //    else
        //    {
        //        return ResponseMessages<List<Product>>("Please choose Excel file", string.Empty, (int)HttpStatusCode.NotFound, null);
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
