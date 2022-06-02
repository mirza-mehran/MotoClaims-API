using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
  public  class ProviderServicesContractRepository
    {
        public ProviderServicesContractRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IUnitOfWork unitOfWork = null;

        public IEnumerable<ProviderServiceType> GetProviderServiceType(long ProviderTypeId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetServiceType");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("ProviderTypeId", ProviderTypeId);
            var result = unitOfWork.Connection.Query<ProviderServiceType>("usp_GetProvider_Services_Contract", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<ProviderServices> GetProviderServices(long ServiceTypeId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderService");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("ServiceTypeId", ServiceTypeId);
            var result = unitOfWork.Connection.Query<ProviderServices>("usp_GetProviderServices", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }


        public IEnumerable<ProviderServices> GetProviderServices(long ProviderId, long tenantId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderService");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("ProviderId", ProviderId);
            var result = unitOfWork.Connection.Query<ProviderServices>("usp_GetProvider_Services_Contract", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<PolicyMake> GetMake(long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetPolicyMake");
            dbParams.Add("TenantId", tenentId);
            var result = unitOfWork.Connection.Query<PolicyMake>("usp_PolicyMake", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<PolicyModel> GetModel(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetPolicyModel");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("MakeId", Id);
            dbParams.Add("tempPolicyId", 0);
            var result = unitOfWork.Connection.Query<PolicyModel>("usp_GetPolicies", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        long PSC_Id = 0;
        public string InsertProviderContract(Provider_Services_Contract obj, long tenantId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            bit = false;

            if (obj != null)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("ProviderService_Id",obj.ProviderService_Id);
                dbParams.Add("ServiceType_Id", obj.ServiceType_Id);
                dbParams.Add("PSC_Code",obj.PSC_Code);
                dbParams.Add("PSC_Description", obj.PSC_Description);
                dbParams.Add("Provider_Id", obj.Provider_Id);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                dbParams.Add("tempId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyProvider_Services_Contract", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                PSC_Id = dbParams.Get<long>("tempId");
                if (affectedrows != 0)
                {
                    ErrorMessage = "Provider Services Contract has been Save Successfully";
                    bit = true;
                }
                else
                {
                    ErrorMessage = "Provider Services Contract has not been Save Successfully";
                }
            }
            return ErrorMessage;
        }

        public string InsertPS_Prices(PS_Prices obj, long tenantId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            bit = false;

            if (obj != null)
            {
                if (obj.PSC_Id > 0)
                {
                    DynamicParameters dbParams = new DynamicParameters();
                    dbParams.Add("Operation", "Insert");
                    dbParams.Add("TenantId", tenantId);
                    dbParams.Add("PSC_Id", obj.PSC_Id);
                    dbParams.Add("Make", obj.Make);
                    dbParams.Add("Model", obj.Model);
                    dbParams.Add("Year", obj.Year);
                    dbParams.Add("Price", obj.Price);
                    dbParams.Add("Start_Date", obj.Start_Date);
                    dbParams.Add("End_Date", obj.End_Date);
                    dbParams.Add("Discount", obj.Discount);
                    dbParams.Add("Remark", obj.Remark);

                    var affectedrows = unitOfWork.Connection.Execute("usp_ModifyProvider_Services_Prices", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                    if (affectedrows != 0)
                    {
                        ErrorMessage = "Provider Services Contract Prices has been Save Successfully";
                        bit = true;
                    }
                    else
                    {
                        ErrorMessage = "Provider Services Contract Prices has not been Save Successfully";
                    }
                }
                else
                {
                    ErrorMessage = "Incorrect ProviderService Price Id";
                    bit = false;
                }
            }
            return ErrorMessage;
        }

        public string UpdateProviderContract(Provider_Services_Contract obj, long tenantId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            bit = false;

          
            if (obj != null)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Update");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("PSC_Id", obj.PSC_Id);
                dbParams.Add("ProviderService_Id", obj.ProviderService_Id);
                dbParams.Add("ServiceType_Id", obj.ServiceType_Id);
                dbParams.Add("PSC_Code", obj.PSC_Code);
                dbParams.Add("PSC_Description", obj.PSC_Description);
                dbParams.Add("Provider_Id", obj.Provider_Id);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                dbParams.Add("tempId", null);

                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyProvider_Services_Contract", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                if (affectedrows != 0)
                {
                    ErrorMessage = "Provider Services Contract has been Updated Successfully";
                    bit = true;
                }
                else
                {
                    ErrorMessage = "Provider Services Contract has not been Updated Successfully";
                }
            }

            return ErrorMessage;
        }

        public string UpdatePS_Prices(PS_Prices obj, long tenantId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            bit = false;

            if (obj != null)
            {
                if (obj.PSC_Id > 0 && obj.PS_Price_Id > 0)
                {
                    DynamicParameters dbParams = new DynamicParameters();
                    dbParams.Add("Operation", "Update");
                    dbParams.Add("TenantId", tenantId);
                    dbParams.Add("PS_Price_Id", obj.PS_Price_Id);
                    dbParams.Add("PSC_Id", obj.PSC_Id);
                    dbParams.Add("Make", obj.Make);
                    dbParams.Add("Model", obj.Model);
                    dbParams.Add("Year", obj.Year);
                    dbParams.Add("Price", obj.Price);
                    dbParams.Add("Start_Date", obj.Start_Date);
                    dbParams.Add("End_Date", obj.End_Date);
                    dbParams.Add("Discount", obj.Discount);
                    dbParams.Add("Remark", obj.Remark);

                    var affectedrows = unitOfWork.Connection.Execute("usp_ModifyProvider_Services_Prices", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                    if (affectedrows != 0)
                    {
                        ErrorMessage = "Provider Services Contract has been Updated Successfully";
                        bit = true;
                    }
                    else
                    {
                        ErrorMessage = "Provider Services Contract has not been Updated Successfully";
                    }
                }
            }
            return ErrorMessage;
        }

        public ModelProvider_Services_Contract GetProvider_Services_Contract(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderServiceById");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PSC_Id", Id);
            return unitOfWork.Connection.QueryFirstOrDefault<ModelProvider_Services_Contract>("usp_GetProvider_Services_Contract", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

  

        public PS_Prices GetDate(long PSC_Id)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetDate");
            dbParams.Add("PSC_Id", PSC_Id);
            return unitOfWork.Connection.QueryFirstOrDefault<PS_Prices>("usp_GetProvider_Services_Contract", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public bool DeleteProvider_Services_Contract(long Id, long TenantId, long userId)
        {
            bool bit = false;
            //string ErrorMessage = null;
            if (Id >= 0)
            {
                var param = new DynamicParameters();
                param.Add("Operation", "Delete");
                param.Add("PSC_Id", Id);
                param.Add("TenantId", TenantId);
                param.Add("tempId", null);
                var affectedRows = unitOfWork.Connection.Execute("usp_ModifyProvider_Services_Contract", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);
                if (affectedRows != 0)
                {
                    bit = true;
                    //ErrorMessage = "Record has been successfully deleted";
                }
                else
                {
                    bit = false;
                    //ErrorMessage = "Provider Id is not found";
                }
            }
            else
            {
                bit = false;
                //ErrorMessage = "Provider Id is not valid";
            }
            return bit;
        }

        public IEnumerable<ModelProviderContractServices> GetProvider_Services_Contrac_By_ProviderId(long ProviderId, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderContractServices_ByProviderId");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("ProviderId", ProviderId);
            return unitOfWork.Connection.Query<ModelProviderContractServices>("usp_GetProvider_Services_Contract", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }


        public IEnumerable<PS_Prices> GetProvider_Services_Contrac_Prices(long PSC_Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderServicePriceById");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PSC_Id", PSC_Id);
            return unitOfWork.Connection.Query<PS_Prices>("usp_GetProvider_Services_Contract", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public PS_Prices GetProvider_Services_Price(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetServicePriceById");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PSC_Id", Id);
            return unitOfWork.Connection.QueryFirstOrDefault<PS_Prices>("usp_GetProvider_Services_Contract", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }


        public bool DeleteProvider_Services_Contract_Price(long Id, long TenantId, long userId)
        {
            bool bit = false;
            //string ErrorMessage = null;
            if ( Id >= 0 )
            {
                var param = new DynamicParameters();
                param.Add("Operation", "Delete");
                param.Add("PS_Price_Id", Id);
                param.Add("TenantId", TenantId);
                
                var affectedRows = unitOfWork.Connection.Execute("usp_ModifyProvider_Services_Prices", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);
                if (affectedRows != 0)
                {
                    bit = true;
                    //ErrorMessage = "Record has been successfully deleted";
                }
                else
                {
                    bit = false;
                    //ErrorMessage = "Provider Id is not found";
                }
            }
            else
            {
                bit = false;
                //ErrorMessage = "Provider Id is not valid";
            }
            return bit;
        }




    }
}
