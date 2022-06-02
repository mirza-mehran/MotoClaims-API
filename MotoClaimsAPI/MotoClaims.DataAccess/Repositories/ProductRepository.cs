using Dapper;
using MotoClaims.Entities.Benefits;
using MotoClaims.Entities.Product;
using MotoClaims.DataAccess.UOW;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
    public class ProductRepository 
    {
        public ProductRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;

        public string DeleteProduct(long Id,long tenentId, long userId, out bool bit)
        {
            bit = false;
            string ErrorMessage = null;
            if (Id >= 0)
            {
                var param = new DynamicParameters();
                param.Add("Id", Id);
                param.Add("TenantId", tenentId);
                param.Add("CreatedBy", userId);
                param.Add("UpdatedBy", userId);
                param.Add("UpdatedDate", DateTime.Now);
                param.Add("IsDeleted", false);
                param.Add("IsActive", true);
                var affectedRows = unitOfWork.Connection.Execute("usp_DeleteProduct", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);

                if (affectedRows != 0)
                {
                    ErrorMessage = "Record has been successfully deleted";
                    bit = true;
                }
                else
                {
                    ErrorMessage = "Please activate product";
                    bit = false;
                }
                return ErrorMessage;
            }
            else
            {
                ErrorMessage = "Product Id is not valid";
                bit = false;
            }
            return ErrorMessage;
        }
        
        public ProductByID GetProduct(long Id, long tenentId, long userId)
        {
            using (var con = unitOfWork.Connection)
            {
                Dictionary<int, object> dictionary = new Dictionary<int, object>();
                var sql = "usp_GetProductById";
                var benefitDictionary = new Dictionary<int, ProductByID>();
                var list = con.Query<ProductByID, Benefit, ProductByID>(
                    sql,
                    (productByID, benefits) =>
                    {
                        ProductByID productEntry;
                        if (!benefitDictionary.TryGetValue(productByID.Id, out productEntry))
                        {
                            productEntry = productByID;
                            productEntry.Benefit = new List<Benefit>();
                            benefitDictionary.Add(productEntry.Id, productEntry);
                        }
                        productEntry.Benefit.Add(benefits);
                        return productEntry;
                    },
                    splitOn: "Id")
                .ToList();
                var result = list.Find(p => p.Id == Id && p.TenantId == tenentId);
                return result;
            }
        }

        public IEnumerable<ProductList> GetProducts(long tenentId, long userId)
        {
            var param = new DynamicParameters();
            param.Add("TenantId", tenentId);
            return unitOfWork.Connection.Query<ProductList>("usp_GetProductList", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param); 
        }

        public string InsertProduct(ProductAdd model, long tenentId, long userId, out bool bit)
        {
            bit = false;
            bool exists = false;
            var paramss = new DynamicParameters();
            paramss.Add("TenantId", tenentId);
            paramss.Add("ProductNameExist", model.ProductName);
            paramss.Add("PtID", model.ProductType);
            paramss.Add("ProductName", null);
            paramss.Add("ProductType", 0);
            paramss.Add("exists", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            var affectedrows = unitOfWork.Connection.Execute("usp_GetProductDetailsExist", transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure,param: paramss);
            exists = paramss.Get<bool>("exists");

            string ErrorMessage = null;

            if (exists == false)
            {
                if (model != null && model.ProductType > 0)
                {
                    var param = new DynamicParameters();
                    param.Add("ProductName", model.ProductName);
                    param.Add("ProductType", model.ProductType);
                    param.Add("ProductDetails", model.ProductDetails);
                    param.Add("AnnualPremium", model.AnnualPremium);
                    param.Add("Status", model.Status);
                    param.Add("CreatedBy", userId);
                   // param.Add("CreatedDate", DateTime.Now);
                    param.Add("UpdatedBy", userId);
                    param.Add("UpdatedDate", DateTime.Now);
                    param.Add("IsDeleted", false);
                    param.Add("IsActive", true);
                    param.Add("TenantId", tenentId);
                    param.Add("CoverageName", "");
                    param.Add("CoPayPercentage", model.Coverage.CoPayPercentage);
                    param.Add("Deductibles", model.Coverage.Deductibles);
                    param.Add("Ceiling", true);
                    param.Add("IsAgencyRepair", model.Coverage.IsAgencyRepair);
                    param.Add("tempProID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                     var affectedRows = unitOfWork.Connection.Execute("usp_PostProductInfoAndCoverageSave", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);

                    var ComplaintId = param.Get<int>("tempProID");
                    if (model.Benefit != null)
                    {
                        foreach (var item in model.Benefit)
                        {
                            if (item.BenefitDetails != "" && item.BenefitDetails != " " && item.BenefitDetails != null)
                            {
                                var parambenefit = new DynamicParameters();
                                parambenefit.Add("BenefitName", " ");
                                parambenefit.Add("BenefitDetails", item.BenefitDetails);
                                parambenefit.Add("Status", true);
                                parambenefit.Add("ProductId", ComplaintId);
                                parambenefit.Add("CreatedBy", 1);
                                parambenefit.Add("CreatedDate", DateTime.Now);
                                parambenefit.Add("UpdatedBy", 1);
                                parambenefit.Add("UpdatedDate", DateTime.Now);
                                parambenefit.Add("IsDeleted", false);
                                parambenefit.Add("IsActive", true);
                                unitOfWork.Connection.Execute("usp_PostBenefits", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: parambenefit);
                            }
                        }
                    }

                    if (affectedRows != 0)
                    {
                        ErrorMessage = "Record has been successfully Inserted";
                        bit = true;
                    }
                    else
                    {
                        ErrorMessage = "Record has not been successfully Inserted";
                        bit = false;
                    }
                    return ErrorMessage;
                }
                else
                {
                    ErrorMessage = "ProductType Required parameter is missing";
                    bit = false;
                }
            }
            else
            {
                return ErrorMessage = "Product Already Exist";
                bit = false;
            }

            return ErrorMessage;
        }

        public string UpdateProduct(ProductAdd model, long tenentId, long userId, out bool bit)
        {
            bit = false;
            bool exists = false;
            var paramss = new DynamicParameters();
            paramss.Add("TenantId", tenentId);
            paramss.Add("ProductNameExist", model.ProductName);
            paramss.Add("PtID", model.ProductType);
            paramss.Add("ProductName", null);
            paramss.Add("ProductType", 0);
            paramss.Add("exists", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            var affectedrows = unitOfWork.Connection.Execute("usp_GetProductDetailsExist", transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure, param: paramss);
            exists = paramss.Get<bool>("exists");


            string ErrorMessage = null;
            if (exists == false)
            {
                if (model != null && model.Id > 0 && model.Coverage.ProductId > 0)
                {
                    var param = new DynamicParameters();
                    param.Add("ProductId", model.Id);
                    param.Add("ProductName", model.ProductName);
                    param.Add("ProductType", model.ProductType);
                    param.Add("ProductDetails", model.ProductDetails);
                    param.Add("AnnualPremium", model.AnnualPremium);
                    param.Add("Status", model.Status);
                    param.Add("CreatedBy", userId);
                   // param.Add("CreatedDate", DateTime.Now);
                    param.Add("UpdatedBy", userId);
                    param.Add("UpdatedDate", DateTime.Now);
                    param.Add("IsDeleted", false);
                    param.Add("IsActive", true);
                    param.Add("TenantId", tenentId);
                    var affectedRows = unitOfWork.Connection.Execute("usp_PutProduct", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);

                    var paramCov = new DynamicParameters();
                    paramCov.Add("@CoverageId", model.Coverage.Id);
                    paramCov.Add("@CoPayPercentage", model.Coverage.CoPayPercentage);
                    paramCov.Add("@Deductibles", model.Coverage.Deductibles);
                    paramCov.Add("@IsAgencyRepair", model.Coverage.IsAgencyRepair);
                    paramCov.Add("@ProductId", model.Id);
                    paramCov.Add("@CreatedBy", userId);
                    //paramCov.Add("@CreatedDate", DateTime.Now);
                    paramCov.Add("@UpdatedBy", userId);
                    paramCov.Add("@UpdatedDate", DateTime.Now);
                    paramCov.Add("@IsDeleted", false);
                    paramCov.Add("@IsActive", true);
                    var affectedRowsCov = unitOfWork.Connection.Execute("usp_PutCoverage", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: paramCov);


                    DynamicParameters Params = new DynamicParameters();
                    Params.Add("Id", model.Id);
                    Params.Add("TenantId", tenentId);
                    var affectedrow = unitOfWork.Connection.Execute("usp_DeleteBenefit", Params, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);


                    foreach (var item in model.Benefit)
                    {
                        if ( item.BenefitDetails != null)
                        {
                            var parambenefit = new DynamicParameters();
                            parambenefit.Add("BenefitName", " ");
                            parambenefit.Add("BenefitDetails", item.BenefitDetails);
                            parambenefit.Add("Status", true);
                            parambenefit.Add("ProductId", model.Id);
                            parambenefit.Add("CreatedBy", userId);
                            //parambenefit.Add("CreatedDate", DateTime.Now);
                            parambenefit.Add("UpdatedBy", userId);
                            parambenefit.Add("UpdatedDate", DateTime.Now);
                            parambenefit.Add("IsDeleted", false);
                            parambenefit.Add("IsActive", true);
                            var result = unitOfWork.Connection.Execute("usp_PostBenefits", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: parambenefit);

                        }
                        //else
                        //{
                        //    var parambenefit = new DynamicParameters();
                        //    parambenefit.Add("BenefitId", item.Id);
                        //    parambenefit.Add("BenefitDetails", item.BenefitDetails);
                        //    parambenefit.Add("ProductId", model.Id);
                        //    parambenefit.Add("CreatedBy", userId);
                        //    //parambenefit.Add"@CreatedDate", DateTime.Now);
                        //    parambenefit.Add("UpdatedBy", userId);
                        //    parambenefit.Add("UpdatedDate", DateTime.Now);
                        //    parambenefit.Add("IsDeleted", false);
                        //    parambenefit.Add("IsActive", true);
                        //    var result = unitOfWork.Connection.Execute("usp_PutBenefit", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: parambenefit);

                        //}
                    }
                    return ErrorMessage = "Record has been successfully Updated";
                    bit = true;

                }
                else
                {
                    ErrorMessage = "Required parameter is missing";
                    bit = false;
                }
            }
            else
            {
                var param1 = new DynamicParameters();
                param1.Add("Id", model.Id);
                param1.Add("TenantId", model.TenantId);
                param1.Add("exists", 0);
                param1.Add("ProductType", dbType: DbType.Int64, direction: ParameterDirection.Output);
                param1.Add("ProductName", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                var affectedRow = unitOfWork.Connection.Execute("usp_GetProductDetailsExist", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param1);

                var ProductType = param1.Get<long>("ProductType");
                var ProductName = param1.Get<string>("ProductName");

                if (ProductType == model.ProductType && ProductName == model.ProductName)
                {
                    if (model != null && model.Id > 0 && model.Coverage.ProductId > 0)
                    {
                        var param = new DynamicParameters();
                        param.Add("ProductId", model.Id);
                        param.Add("ProductName", model.ProductName);
                        param.Add("ProductType", model.ProductType);
                        param.Add("ProductDetails", model.ProductDetails);
                        param.Add("AnnualPremium", model.AnnualPremium);
                        param.Add("Status", model.Status);
                        param.Add("CreatedBy", userId);
                       // param.Add("CreatedDate", DateTime.Now);
                        param.Add("UpdatedBy", userId);
                        param.Add("UpdatedDate", DateTime.Now);
                        param.Add("IsDeleted", false);
                        param.Add("IsActive", true);
                        param.Add("TenantId", model.TenantId);
                        var affectedRows = unitOfWork.Connection.Execute("usp_PutProduct", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);

                        var paramCov = new DynamicParameters();
                        paramCov.Add("CoverageId", model.Coverage.Id);
                        paramCov.Add("CoPayPercentage", model.Coverage.CoPayPercentage);
                        paramCov.Add("Deductibles", model.Coverage.Deductibles);
                        paramCov.Add("IsAgencyRepair", model.Coverage.IsAgencyRepair);
                        paramCov.Add("ProductId", model.Id);
                        paramCov.Add("CreatedBy", userId);
                        //paramCov.Add("CreatedDate", DateTime.Now);
                        paramCov.Add("UpdatedBy", userId);
                        paramCov.Add("UpdatedDate", DateTime.Now);
                        paramCov.Add("IsDeleted", false);
                        paramCov.Add("IsActive", true);
                        var affectedRowsCov = unitOfWork.Connection.Execute("usp_PutCoverage", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: paramCov);

                        DynamicParameters Params = new DynamicParameters();
                        Params.Add("Id", model.Id);
                        Params.Add("TenantId", tenentId);
                        var affectedrow = unitOfWork.Connection.Execute("usp_DeleteBenefit", Params, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);


                        foreach (var item in model.Benefit)
                        {
                            if ( item.BenefitDetails != null)
                            {
                                var parambenefit = new DynamicParameters();
                                parambenefit.Add("BenefitName", " ");
                                parambenefit.Add("BenefitDetails", item.BenefitDetails);
                                parambenefit.Add("Status", true);
                                parambenefit.Add("ProductId", model.Id);
                                parambenefit.Add("CreatedBy", userId);
                                //parambenefit.Add("CreatedDate", DateTime.Now);
                                parambenefit.Add("UpdatedBy", userId);
                                parambenefit.Add("UpdatedDate", DateTime.Now);
                                parambenefit.Add("IsDeleted", false);
                                parambenefit.Add("IsActive", true);
                                var result = unitOfWork.Connection.Execute("usp_PostBenefits", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: parambenefit);

                            }
                            //else
                            //{
                            //    var parambenefit = new DynamicParameters();
                            //    parambenefit.Add("BenefitId", item.Id);
                            //    parambenefit.Add("BenefitDetails", item.BenefitDetails);
                            //    parambenefit.Add("ProductId", model.Id);
                            //    parambenefit.Add("CreatedBy", userId);
                            //    //parambenefit.Add("CreatedDate", DateTime.Now);
                            //    parambenefit.Add("UpdatedBy", userId);
                            //    parambenefit.Add("UpdatedDate", DateTime.Now);
                            //    parambenefit.Add("IsDeleted", false);
                            //    parambenefit.Add("IsActive", true);
                            //    var result = unitOfWork.Connection.Execute("usp_PutBenefit", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: parambenefit);

                            //}
                        }
                         ErrorMessage = "Record has been successfully Updated";
                        bit = true;

                    }
                    else
                    {
                        ErrorMessage = "Required parameter is missing";
                        bit = false;

                    }
                }
                else
                {
                    ErrorMessage = "Product already Exist";
                    bit = false;
                }
            }
            
            return ErrorMessage;
        }

    }
}
