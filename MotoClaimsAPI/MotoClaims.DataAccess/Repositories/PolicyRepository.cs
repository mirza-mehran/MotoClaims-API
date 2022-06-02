using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MotoClaims.Entities.Product;
using MotoClaims.Entities.Benefits;
using MotoClaims.Entities.Policy;
using System.Data;
using MotoClaims.Entities;

namespace MotoClaims.DataAccess.Repositories
{
   public class PolicyRepository
    {
        public PolicyRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IUnitOfWork unitOfWork = null;

        public ProductByID GetProductByID(long id, long tenentId, long userId)
        {
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            var sql = "usp_GetPolicies";
            var param = new DynamicParameters();
            param.Add("@Id", id);
            param.Add("@TenantId", 2);
            param.Add("@Operation", "GetProductDetails");
            param.Add("@tempPolicyId", 1);

            var benefitDictionary = new Dictionary<int, ProductByID>();
            var list = unitOfWork.Connection.Query<ProductByID, Benefit, ProductByID>(
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
                },param, commandType: System.Data.CommandType.StoredProcedure,
                splitOn: "Id")
            .ToList();
            var result = list.Find(p => p.Id == id && p.Status == true );
            return result;
        }
        public IEnumerable<ProductByID> GetProductName(long id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("PolicyTypeId", id);
            dbParams.Add("Operation", "GetProductName");
            dbParams.Add("TenantId", 2);
            dbParams.Add("tempPolicyId", 1);
            var result = unitOfWork.Connection.Query<ProductByID>("usp_GetPolicies", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }
        public IEnumerable<VehicleColour> GetColour( long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetColour");
            dbParams.Add("TenantId", 2);
            var result = unitOfWork.Connection.Query<VehicleColour>("usp_Colour", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }
        public IEnumerable<PolicyMake> GetPolicyMake( long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetPolicyMake");
            dbParams.Add("TenantId", 2);
            var result = unitOfWork.Connection.Query<PolicyMake>("usp_PolicyMake", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }
        public IEnumerable<PolicyModel> GetPolicyModel(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetPolicyModel");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("MakeId", Id);
            dbParams.Add("tempPolicyId", 0);
            var result = unitOfWork.Connection.Query<PolicyModel>("usp_GetPolicies", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        long  PolicyId;
        public string InsertPolicy(Policy model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model != null && model.MakeId > 0 && model.ModelId > 0 && model.ColourId > 0)
            {
                //////////////Add Policy
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenentId);
                dbParams.Add("PolicyHolderName", model.PolicyHolderName);
                dbParams.Add("DOB",model.DOB);
                dbParams.Add("DrivingLicenseValidityExpiryDate",DateTime.Now);
                dbParams.Add("Address", model.Address);
                dbParams.Add("PolicyNo", model.PolicyNo);
                dbParams.Add("IdentityNo", model.IdentificationNumber);

                dbParams.Add("StartDate",model.StartDate);
                dbParams.Add("EndDate",model.EndDate);
                dbParams.Add("MakeId", model.MakeId);
                dbParams.Add("ModelId", model.ModelId);
                dbParams.Add("ColourId", model.ColourId);
                dbParams.Add("Status", 1);
                dbParams.Add("ProductId", model.ProductId);
                dbParams.Add("PolicyType", model.PolicyType);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);

                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                dbParams.Add("tempPolicyId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                
                var affectedrows = unitOfWork.Connection.Execute("usp_GetPolicies", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                PolicyId = dbParams.Get<int>("tempPolicyId");
                if (affectedrows != 0)
                {
                    return ErrorMessage = "Policy record has been successfully Inserted";
                }
                else
                {
                    return ErrorMessage = "Policy record has not been successfully Inserted";
                }
                
            }
            else
            {
                ErrorMessage = "Required Policy Details";
            }

            return ErrorMessage;
        }
        public string InsertPolicyCoverage(Policy model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model != null && model.MakeId > 0 && model.ModelId > 0 && model.ColourId > 0)
            {
                //////////////Add Policy Coverage
                DynamicParameters dbCovParams = new DynamicParameters();
                dbCovParams.Add("Operation", "Insert");
                dbCovParams.Add("TenantId", 2);
                dbCovParams.Add("CoverageName", model.CoverageName);
                dbCovParams.Add("CoPayPercentage", model.CoPayPercentage);
                dbCovParams.Add("Deductibles", model.Deductibles);

                dbCovParams.Add("IsAgencyRepair", model.IsAgencyRepair);
                dbCovParams.Add("PolicyID", PolicyId);
                dbCovParams.Add("ProductId", model.ProductId);

                dbCovParams.Add("CreatedBy", 1);
                dbCovParams.Add("UpdatedBy", 1);
                dbCovParams.Add("UpdatedDate", DateTime.Now);

                dbCovParams.Add("IsDeleted", false);
                dbCovParams.Add("IsActive", true);

                var affectedrows1 = unitOfWork.Connection.Execute("usp_PolicyCoverages", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                
                if (affectedrows1 != 0)
                {
                    return ErrorMessage = "PolicyCoverage record has been successfully Inserted";
                }
                else
                {
                    return ErrorMessage = "PolicyCoverage record has not been successfully Inserted";
                }
            }
            else
            {
                ErrorMessage = "Required Policy Coverage";
            }

            return ErrorMessage;
        }
        public string InsertPolicyBenefit(Policy model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model.Benefits != null && model.MakeId > 0 && model.ModelId > 0 && model.ColourId > 0)
            {
                ////////////Add Policy Benefit
                foreach (var item in model.Benefits)
                {
                    if (item !=null)
                    {
                        if (item.BenefitDetails != "" && item.BenefitDetails != " " && item.BenefitDetails != null)
                        {
                            DynamicParameters dbBenefitParams = new DynamicParameters();
                            dbBenefitParams.Add("Operation", "Insert");
                            dbBenefitParams.Add("TenantId", 2);
                            dbBenefitParams.Add("BenefitName", item.BenefitName);
                            dbBenefitParams.Add("BenefitDetails", item.BenefitDetails);
                            dbBenefitParams.Add("PolicyID", PolicyId);
                            dbBenefitParams.Add("ProductId", model.ProductId);
                            dbBenefitParams.Add("Status", true);

                            dbBenefitParams.Add("CreatedBy", 1);
                            dbBenefitParams.Add("UpdatedBy", 1);
                            dbBenefitParams.Add("UpdatedDate", DateTime.Now);

                            dbBenefitParams.Add("IsDeleted", false);
                            dbBenefitParams.Add("IsActive", true);

                            var affectedrows2 = unitOfWork.Connection.Execute("usp_PolicyBenefits", dbBenefitParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                            if (affectedrows2 != 0)
                            {
                                return ErrorMessage = "PolicyBenefit record has been successfully Inserted";
                            }
                            else
                            {
                                return ErrorMessage = "PolicyBenefit record has not been successfully Inserted";
                            }
                        }

                    }
                }
            }
            else
            {
                ErrorMessage = "Required Policy Benefits";
            }
            return ErrorMessage;
        }
        long PolicyVehicleId;
        public long InsertPolicyVehicle(Policy model, long tenentId, long userId)
        {
            string ErrorMessage = null;
           
            if (model != null && model.MakeId > 0 && model.ModelId > 0 && model.ColourId > 0)
            {
                //////////////Add Policy Vehicle
                DynamicParameters dbVehParams = new DynamicParameters();
                dbVehParams.Add("Operation", "Insert");
                dbVehParams.Add("TenantId", tenentId);
                dbVehParams.Add("RegistrationNumber", model.RegistrationNumber);
                dbVehParams.Add("ChassisNumber", model.ChassisNumber);
                dbVehParams.Add("DrivingLicenseValidity", model.DrivingLicenseValidity);
                dbVehParams.Add("IdentificationNumber", model.IdentificationNumber);
                dbVehParams.Add("Year", model.Year);
                dbVehParams.Add("ColourId", model.ColourId);
                dbVehParams.Add("Capacity", model.Capacity);
                dbVehParams.Add("CarNumber", model.CarNumber);
                dbVehParams.Add("PlateNumber", model.PlateNumber);
                dbVehParams.Add("ModelId", model.ModelId);
                dbVehParams.Add("Image1", model.Image1);
                dbVehParams.Add("Image2", model.Image2);
                dbVehParams.Add("Image3", model.Image3);
                dbVehParams.Add("Image4", model.Image4);
                dbVehParams.Add("Image5", model.Image5);
                dbVehParams.Add("PolicyId", PolicyId);
                dbVehParams.Add("MakeId", model.MakeId);

                dbVehParams.Add("CreatedBy", userId);
                dbVehParams.Add("UpdatedBy", userId);
                dbVehParams.Add("UpdatedDate", DateTime.Now);
                dbVehParams.Add("IsDeleted", false);
                dbVehParams.Add("IsActive", true);

                dbVehParams.Add("PolicyVehicleId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                var affectedrows3 = unitOfWork.Connection.Execute("usp_PolicyVehicle", dbVehParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                PolicyVehicleId = dbVehParams.Get<long>("PolicyVehicleId");

                if (PolicyId > 0)
                {
                    return PolicyId;
                }
                else
                {
                    return 0;
                }

                //if (affectedrows3 != 0)
                //{
                //     ErrorMessage = "PolicyVehicle record has been successfully Inserted";
                //}
                //else
                //{
                //     ErrorMessage = "PolicyVehicle record has not been successfully Inserted";
                //}
            }
            else
            {
                 ErrorMessage = "Required Policy Vehicle";
            }
            return PolicyId;
        }


        public string UpdatePolicyImage(ImagesUpload model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model.Id > 0)
            {
                DynamicParameters dbBenefitParams = new DynamicParameters();
                dbBenefitParams.Add("Operation", "UpdateimageUrl");
                dbBenefitParams.Add("TenantId", tenentId);
                dbBenefitParams.Add("Id", model.Id);
                dbBenefitParams.Add("Image1", model.Image1);
                dbBenefitParams.Add("Image2", model.Image2);
                dbBenefitParams.Add("Image3", model.Image3);
                dbBenefitParams.Add("Image4", model.Image4);
                dbBenefitParams.Add("Image5", model.Image5);
                dbBenefitParams.Add("PolicyVehicleId", null);
                var affectedrows2 = unitOfWork.Connection.Execute("usp_PolicyVehicle", dbBenefitParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows2 !=0)
                {
                     ErrorMessage = "Image Successfully Save";
                }
                else
                {
                     ErrorMessage = "Image not Successfully Save";
                }
            }
            else
            {
                return ErrorMessage = "Image not Successfully Save";
            }
            return ErrorMessage;
        }
        public string InsertPolicyImage(ImagesUpload model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model.Id > 0)
            {
                DynamicParameters dbBenefitParams = new DynamicParameters();
                dbBenefitParams.Add("Operation", "InsertimageUrl");
                dbBenefitParams.Add("TenantId", tenentId);
                dbBenefitParams.Add("Id", model.Id);
                dbBenefitParams.Add("Image1", model.Image1);
                dbBenefitParams.Add("Image2", model.Image2);
                dbBenefitParams.Add("Image3", model.Image3);
                dbBenefitParams.Add("Image4", model.Image4);
                dbBenefitParams.Add("Image5", model.Image5);
                dbBenefitParams.Add("PolicyVehicleId", null);
                var affectedrows2 = unitOfWork.Connection.Execute("usp_PolicyVehicle", dbBenefitParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows2 != 0)
                {
                    ErrorMessage = "Image Successfully Save";
                }
                else
                {
                    ErrorMessage = "Image not Successfully Save";
                }
            }
            else
            {
                return ErrorMessage = "Image not Successfully Save";
            }
            return ErrorMessage;
        }


        ///////Update Function
        public string UpdatePolicy(Policy model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model != null && model.MakeId > 0 && model.ModelId > 0 && model.ColourId > 0)
            {
                //////////////Update Policy
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Update");
                dbParams.Add("Id", model.Id);
                dbParams.Add("TenantId", 2);
                dbParams.Add("PolicyHolderName", model.PolicyHolderName);
                dbParams.Add("DOB", model.DOB);
                dbParams.Add("DrivingLicenseValidityExpiryDate", DateTime.Now);
                dbParams.Add("Address", model.Address);
                dbParams.Add("PolicyNo", model.PolicyNo);
                dbParams.Add("StartDate", model.StartDate);
                dbParams.Add("EndDate", model.EndDate);
                dbParams.Add("MakeId", model.MakeId);
                dbParams.Add("ModelId", model.ModelId);
                dbParams.Add("ColourId", model.ColourId);
                dbParams.Add("Status", 1);

                dbParams.Add("ProductId", model.ProductId);
                dbParams.Add("PolicyType", model.PolicyType);

                dbParams.Add("CreatedBy", 1);
                dbParams.Add("UpdatedBy", 1);
                dbParams.Add("UpdatedDate", DateTime.Now);

                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                dbParams.Add("tempPolicyId", null);

                var affectedrows = unitOfWork.Connection.Execute("usp_Policies", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows != 0)
                {
                    return ErrorMessage = "Policy record has been successfully Updated";
                }
                else
                {
                    return ErrorMessage = "Policy record has not been successfully Updated";
                }

            }
            else
            {
                ErrorMessage = "Required Policy Updated";
            }

            return ErrorMessage;
        }
        public string UpdatePolicyCoverage(Policy model, long tenentId, long userId)
        {
            string ErrorMessage = null;
            if (model != null && model.MakeId > 0 && model.ModelId > 0 && model.ColourId > 0)
            {
                //////////////Update Policy Coverage
                DynamicParameters dbCovParams = new DynamicParameters();
                dbCovParams.Add("Operation", "update");
                dbCovParams.Add("TenantId", 2);
                dbCovParams.Add("CoverageName", model.CoverageName);
                dbCovParams.Add("CoPayPercentage", model.CoPayPercentage);
                dbCovParams.Add("Deductibles", model.Deductibles);

                dbCovParams.Add("IsAgencyRepair", model.IsAgencyRepair);
                dbCovParams.Add("PolicyID", model.PolicyId);
                dbCovParams.Add("ProductId", model.ProductId);

                dbCovParams.Add("CreatedBy", 1);
                dbCovParams.Add("UpdatedBy", 1);
                dbCovParams.Add("UpdatedDate", DateTime.Now);
                dbCovParams.Add("IsDeleted", false);
                dbCovParams.Add("IsActive", true);

                var affectedrows1 = unitOfWork.Connection.Execute("usp_PolicyCoverages", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows1 != 0)
                {
                    return ErrorMessage = "PolicyCoverage record has been successfully Updated";
                }
                else
                {
                    return ErrorMessage = "PolicyCoverage record has not been successfully Updated";
                }
            }
            else
            {
                ErrorMessage = "Required Policy Coverage";
            }

            return ErrorMessage;
        }
        public string UpdatePolicyBenefit(Policy model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model.Benefits != null && model.MakeId > 0 && model.ModelId > 0 && model.ColourId > 0)
            {
                ////////////Update Policy Benefit
                foreach (var item in model.Benefits)
                {
                    if (item.BenefitDetails != "" && item.BenefitDetails != " " && item.BenefitDetails != null)
                    {
                        if (item.Id == 0 && item.BenefitDetails != null)
                        {
                            DynamicParameters dbBenefitParams = new DynamicParameters();
                            dbBenefitParams.Add("Operation", "Insert");
                            dbBenefitParams.Add("TenantId", 2);
                            dbBenefitParams.Add("BenefitName", item.BenefitName);
                            dbBenefitParams.Add("BenefitDetails", item.BenefitDetails);
                            dbBenefitParams.Add("PolicyID", PolicyId);
                            dbBenefitParams.Add("ProductId", model.ProductId);
                            dbBenefitParams.Add("Status", true);

                            dbBenefitParams.Add("CreatedBy", 1);
                            dbBenefitParams.Add("UpdatedBy", 1);
                            dbBenefitParams.Add("UpdatedDate", DateTime.Now);

                            dbBenefitParams.Add("IsDeleted", false);
                            dbBenefitParams.Add("IsActive", true);

                            var affectedrows2 = unitOfWork.Connection.Execute("usp_PolicyBenefits", dbBenefitParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                            if (affectedrows2 != 0)
                            {
                                return ErrorMessage = "PolicyBenefit record has been successfully Inserted";
                            }
                            else
                            {
                                return ErrorMessage = "PolicyBenefit record has not been successfully Inserted";
                            }
                        }
                        else
                        {
                            DynamicParameters dbBenefitParams = new DynamicParameters();
                            dbBenefitParams.Add("Operation", "update");
                            dbBenefitParams.Add("TenantId", 2);
                            dbBenefitParams.Add("BenefitName", item.BenefitName);
                            dbBenefitParams.Add("BenefitDetails", item.BenefitDetails);
                            dbBenefitParams.Add("PolicyID", model.PolicyId);
                            dbBenefitParams.Add("ProductId", model.ProductId);
                            dbBenefitParams.Add("Status", true);

                            dbBenefitParams.Add("CreatedBy", 1);
                            dbBenefitParams.Add("UpdatedBy", 1);
                            dbBenefitParams.Add("UpdatedDate", DateTime.Now);

                            dbBenefitParams.Add("IsDeleted", false);
                            dbBenefitParams.Add("IsActive", true);
                            var affectedrows2 = unitOfWork.Connection.Execute("usp_PolicyBenefits", dbBenefitParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                            if (affectedrows2 != 0)
                            {
                                return ErrorMessage = "PolicyBenefit record has been successfully Updated";
                            }
                            else
                            {
                                return ErrorMessage = "PolicyBenefit record has not been successfully Updated";
                            }
                        }
                       
                    }
                }
            }
            else
            {
                ErrorMessage = "Required Policy Benefits";
            }
            return ErrorMessage;
        }
        public long UpdatePolicyVehicle(Policy model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model != null && model.MakeId > 0 && model.ModelId > 0 && model.ColourId > 0)
            {
                //////////////Add Policy Vehicle
                DynamicParameters dbVehParams = new DynamicParameters();
                dbVehParams.Add("Operation", "update");
                dbVehParams.Add("TenantId", 2);
                dbVehParams.Add("RegistrationNumber", model.RegistrationNumber);
                dbVehParams.Add("ChassisNumber", model.ChassisNumber);
                dbVehParams.Add("DrivingLicenseValidity", model.DrivingLicenseValidity);
                dbVehParams.Add("IdentificationNumber", model.IdentificationNumber);
                dbVehParams.Add("Year", model.Year);
                dbVehParams.Add("ColourId", model.ColourId);
                dbVehParams.Add("Capacity", model.Capacity);
                dbVehParams.Add("CarNumber", model.CarNumber);
                dbVehParams.Add("PlateNumber", model.PlateNumber);
                dbVehParams.Add("ModelId", model.ModelId);
                dbVehParams.Add("Image1", model.Image1);
                dbVehParams.Add("Image2", model.Image2);
                dbVehParams.Add("Image3", model.Image3);
                dbVehParams.Add("Image4", model.Image4);
                dbVehParams.Add("Image5", model.Image5);
                dbVehParams.Add("PolicyId", model.PolicyId);
                dbVehParams.Add("MakeId", model.MakeId);

                dbVehParams.Add("PolicyVehicleId", null);


                dbVehParams.Add("CreatedBy", 1);
                dbVehParams.Add("UpdatedBy", 1);
                dbVehParams.Add("UpdatedDate", DateTime.Now);
                dbVehParams.Add("IsDeleted", false);
                dbVehParams.Add("IsActive", true);

                dbVehParams.Add("PolicyVehicleId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                var affectedrows3 = unitOfWork.Connection.Execute("usp_PolicyVehicle", dbVehParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                PolicyVehicleId = dbVehParams.Get<long>("PolicyVehicleId");

                if (PolicyVehicleId > 0)
                {
                    return PolicyVehicleId;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                ErrorMessage = "Required Policy Vehicle";
            }
            return PolicyVehicleId;
        }


        public IEnumerable<Policy> GetPolicies( long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetPolicies");
            dbParams.Add("TenantId", 2);
            dbParams.Add("tempPolicyId", null);
            var result = unitOfWork.Connection.Query<Policy>("usp_GetPolicies", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<ModelPolicy> GetAllPoliciesByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long userId)
        {

            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PageIndex", PageIndex);
            dbParams.Add("PageSize", PageSize);
            dbParams.Add("SearchText", SearchText);
            dbParams.Add("SearchOption", SearchOption);
            dbParams.Add("SortType", SortType);
            dbParams.Add("SortName", SortName);
            dbParams.Add("TotalRecord", dbType: DbType.Int64, direction: ParameterDirection.Output);

            var result = unitOfWork.Connection.Query<ModelPolicy>("usp_GetAllPoliciesByPageIndex", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            long TotalRecords = dbParams.Get<long>("TotalRecord");
            TotalRecord = TotalRecords;

            return result;
        }

        public Policy GetPolicyByID(long id, long tenentId, long userId)
        {
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            var sql = "usp_GetPolicies";
            var param = new DynamicParameters();
            param.Add("Id", id);
            param.Add("TenantId", tenentId);
            param.Add("Operation", "GetPolicyById");
            param.Add("tempPolicyId", null);

            var benefitDictionary = new Dictionary<int, Policy>();
            var list = unitOfWork.Connection.Query<Policy, Benefit, Policy>(
                sql,
                (productByID, benefits) =>
                {
                    Policy productEntry;
                    if (!benefitDictionary.TryGetValue(productByID.Id, out productEntry))
                    {
                        productEntry = productByID;
                        productEntry.Benefits = new List<Benefit>();
                        benefitDictionary.Add(productEntry.Id, productEntry);
                    }
                    productEntry.Benefits.Add(benefits);
                    return productEntry;
                }, param, commandType: System.Data.CommandType.StoredProcedure,
                splitOn: "Id")
            .ToList();

            var result = list.Find(p => p.Id == id);
            return result;
        }
        public string DeletePolicy(long Id, long tenentId, long userId,out bool bit)
        {
            string ErrorMessage = null;
            if (Id >= 0)
            {
                var param = new DynamicParameters();
                param.Add("Operation", "DeletePolicy");
                param.Add("Id", Id);
                param.Add("TenantId", tenentId);
                param.Add("tempPolicyId", null);


                //var affectedRows = unitOfWork.Connection.Execute("usp_Policies", param, commandType: CommandType.StoredProcedure);
                var affectedRows = unitOfWork.Connection.Execute("usp_Policies", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);


                if (affectedRows != 0)
                {
                    ErrorMessage = "Record has been successfully deleted";
                    bit = true;
                }
                else
                {
                    ErrorMessage = "Policy Id is not found";
                    bit = false;
                }
                return ErrorMessage;
            }
            else
            {
                ErrorMessage = "Policy Id is not valid";
                bit = false;
            }
            return ErrorMessage;
        }
    }
}
