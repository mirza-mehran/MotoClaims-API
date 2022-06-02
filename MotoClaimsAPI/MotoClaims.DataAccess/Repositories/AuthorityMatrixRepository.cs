using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Product;
using MotoClaims.Entities.Provider;
using MotoClaims.Entities.User;
using MotoClaims.Entities.AuthorityMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MotoClaims.DataAccess.Repositories
{
   public class AuthorityMatrixRepository
    {
        public AuthorityMatrixRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;

        public IEnumerable<PolicyMake> GetMake(long tenentId, long userId, string SearchText)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetMake");
            dbParams.Add("TenantId", 2);
            dbParams.Add("SearchText", SearchText);
            var result = unitOfWork.Connection.Query<PolicyMake>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<PolicyModel> GetModel(long Id, long tenentId, long userId, string SearchText)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetModel");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("MakeId", Id);
            dbParams.Add("SearchText", SearchText);
            var result = unitOfWork.Connection.Query<PolicyModel>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<ProductList> GetProducts(long tenentId, long userId, string SearchText)
        {
            var param = new DynamicParameters();
            param.Add("Operation", "GetProducts");
            param.Add("TenantId", tenentId);
            param.Add("SearchText", SearchText);

            return unitOfWork.Connection.Query<ProductList>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);
        }

        public IEnumerable<UserProfiles> GetUserProfiles(long id, long roleId, long tenantId, string SearchText)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetUserProfile");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("SearchText", SearchText);

            return unitOfWork.Connection.Query<UserProfiles>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<AuthorityMatrixAssess_Services> GetServices(long tenantId, string SearchText)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetServices");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("SearchText", SearchText);
            var result = unitOfWork.Connection.Query<AuthorityMatrixAssess_Services>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public string InsertAuthorityMatrix(AuthorityMatrixs obj, long userId, long tenantId,out bool bit)
        {
            string Errormessage = null;
            bit = false;
            if (obj != null)
            {
                if (string.IsNullOrEmpty(obj.AM_Assign_MakeID))
                {
                    Errormessage = " Make Id is not null and empty";
                    bit = false;
                }
                else
                {
                    if (string.IsNullOrEmpty(obj.AM_Assign_ModelID))
                    {
                        Errormessage = "Model Id is not null and empty";
                        bit = false;
                    }
                    else
                    {
                        DynamicParameters dbParams = new DynamicParameters();
                        dbParams.Add("Operation", "Insert");
                        dbParams.Add("TenantId", tenantId);
                        dbParams.Add("AM_Assign_Name", obj.AM_Assign_Name);
                        dbParams.Add("AM_Assign_MakeID", obj.AM_Assign_MakeID);
                        dbParams.Add("AM_Assign_ModelID", obj.AM_Assign_ModelID);
                        dbParams.Add("AM_Assign_YearFrom", obj.AM_Assign_YearFrom);
                        dbParams.Add("AM_Assign_YearTo", obj.AM_Assign_YearTo);
                        dbParams.Add("AM_Assign_RepairOption", obj.AM_Assign_RepairOption);
                        dbParams.Add("AM_Assign_Product", obj.AM_Assign_Product);
                        dbParams.Add("AM_Assign_ToUser", obj.AM_Assign_ToUser);
                        dbParams.Add("AM_Assign_Details", obj.AM_Assign_Details);

                        dbParams.Add("CreatedBy", userId);
                        dbParams.Add("UpdatedBy", userId);
                        dbParams.Add("UpdatedDate", DateTime.Now);
                        dbParams.Add("IsDeleted", false);
                        dbParams.Add("IsActive", true);

                        var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                        if (AffectedRow != 0)
                        {
                            Errormessage = "Initial AuthorityMatrix has been successfully Inserted";
                            bit = true;
                        }
                        else
                        {
                            Errormessage = "Initial AuthorityMatrix has not been successfully Inserted";
                            bit = false;
                        }
                    }
                }
            }
            else
            {
                Errormessage = "Model is not null and empty";
                bit = false;
            }
          
            return Errormessage;
        }

        public string UpdateAuthorityMatrix(AuthorityMatrixs obj, long userId, long tenantId, out bool bit)
        {
            string Errormessage = null;
            bit = false;
            if (obj != null && obj.AM_Assign_ID > 0)
            {
                if (string.IsNullOrEmpty(obj.AM_Assign_MakeID))
                {
                    Errormessage = "Make Id is not null and empty";
                    bit = false;
                }
                else
                {
                    if (string.IsNullOrEmpty(obj.AM_Assign_ModelID))
                    {
                        Errormessage = "Model Id is not null and empty";
                        bit = false;
                    }
                    else
                    {
                        DynamicParameters dbParams = new DynamicParameters();
                        dbParams.Add("Operation", "Update");
                        dbParams.Add("TenantId", tenantId);
                        dbParams.Add("AM_Assign_ID", obj.AM_Assign_ID);
                        dbParams.Add("AM_Assign_Name", obj.AM_Assign_Name);
                        dbParams.Add("AM_Assign_MakeID", obj.AM_Assign_MakeID);
                        dbParams.Add("AM_Assign_ModelID", obj.AM_Assign_ModelID);
                        dbParams.Add("AM_Assign_YearFrom", obj.AM_Assign_YearFrom);
                        dbParams.Add("AM_Assign_YearTo", obj.AM_Assign_YearTo);
                        dbParams.Add("AM_Assign_RepairOption", obj.AM_Assign_RepairOption);
                        dbParams.Add("AM_Assign_Product", obj.AM_Assign_Product);
                        dbParams.Add("AM_Assign_ToUser", obj.AM_Assign_ToUser);
                        dbParams.Add("AM_Assign_Details", obj.AM_Assign_Details);

                        dbParams.Add("CreatedBy", userId);
                        dbParams.Add("UpdatedBy", userId);
                        dbParams.Add("UpdatedDate", DateTime.Now);
                        dbParams.Add("IsDeleted", false);
                        dbParams.Add("IsActive", true);

                        var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                        if (AffectedRow != 0)
                        {
                            Errormessage = "Initial AuthorityMatrix has been successfully Updated";
                            bit = true;
                        }
                        else
                        {
                            Errormessage = "Initial AuthorityMatrix has not been successfully Updated";
                            bit = false;
                        }
                    }
                }
            }
            else
            {
                Errormessage = "Model is not null and empty";
                bit = false;
            }

           
            return Errormessage;
        }

        long AMS_Id = 0;
        public string InsertAuthorityMatrixAssessment(AuthorityMatrixAssessment obj, long userId, long tenantId, out bool bit)
        {
            string Errormessage = null;
            bit = false;
            if (obj != null)
            {
                //if (obj.AM_Assess_AssignUser > 0)
                //{
                    DynamicParameters dbParams = new DynamicParameters();
                    dbParams.Add("Operation", "Insert");
                    dbParams.Add("TenantId", tenantId);
                    dbParams.Add("AM_Assess_Name", obj.AM_Assess_Name);
                    dbParams.Add("AM_Assess_Details", obj.AM_Assess_Details);
                    dbParams.Add("AM_Assess_Type", obj.AM_Assess_Type);
                    dbParams.Add("AM_Assess_AmountFrom", obj.AM_Assess_AmountFrom);
                    dbParams.Add("AM_Assess_AmountTo", obj.AM_Assess_AmountTo);
                    dbParams.Add("AM_Assess_AssignType", obj.AM_Assess_AssignType);
                    dbParams.Add("AM_Assess_AssignUser", obj.AM_Assess_AssignUser);

                    dbParams.Add("CreatedBy", userId);
                    dbParams.Add("UpdatedBy", userId);
                    dbParams.Add("UpdatedDate", DateTime.Now);
                    dbParams.Add("IsDeleted", false);
                    dbParams.Add("IsActive", true);
                    dbParams.Add("tempId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                    var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyAuthorityMatrixAssessment", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    AMS_Id = dbParams.Get<long>("tempId");

                    if (AffectedRow != 0)
                    {
                        Errormessage = "Initial AuthorityMatrixAssessment has been successfully Inserted";
                        bit = true;
                    }
                    else
                    {
                        Errormessage = "Initial AuthorityMatrixAssessment has not been successfully Inserted";
                        bit = false;
                    }
                   
                //}
                //else
                //{
                //    Errormessage = "User Id not null or empty";
                //}
            }
            else
            {
                Errormessage = "Model is not null and empty";
                bit = false;
            }
         
            return Errormessage;
        }

        public string InsertAuthorityMatrixAssess_Services(AuthorityMatrixAssessment obj, long userId, long tenantId)
        {
            string Errormessage = null;
            if (obj.AMatrixAssess_Service != null && obj.AMatrixAssess_Service.Count() > 0)
            {
                if (AMS_Id < 0)
                {
                    Errormessage = "MatrixAssess Id not null or empty";
                }
                else
                {
                    foreach (var item in obj.AMatrixAssess_Service)
                    {
                        DynamicParameters dbParams = new DynamicParameters();
                        dbParams.Add("Operation", "Insert");
                        dbParams.Add("TenantId", tenantId);
                        dbParams.Add("AM_Assess_ID", obj.AM_Assess_ID);
                        dbParams.Add("AMA_Service_Type", item.AMA_Service_Type);
                        dbParams.Add("AMA_Service_Code", item.AMA_Service_Code);

                        dbParams.Add("CreatedBy", userId);
                        dbParams.Add("UpdatedBy", userId);
                        dbParams.Add("UpdatedDate", DateTime.Now);
                        dbParams.Add("IsDeleted", false);
                        dbParams.Add("IsActive", true);
                        var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyAuthorityMatrixAssess_Services", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                        if (AffectedRow != 0)
                        {
                            Errormessage = "Initial AuthorityMatrixAssessment_Services has been successfully Inserted";
                        }
                        else
                        {
                            Errormessage = "Initial AuthorityMatrixAssessment_Services has not been successfully Inserted";
                        }
                    }
                }
            }
            else
            {
                Errormessage = "Model is not null and empty";
            }
            
            return Errormessage;
        }
        
        public string UpdateAuthorityMatrixAssessment(AuthorityMatrixAssessment obj, long userId, long tenantId, out bool bit)
        {
            string Errormessage = null;
            bit = false;
            if (obj != null)
            {
                //if (obj.AM_Assess_AssignUser > 0)
                //{
                    if (obj.AM_Assess_ID > 0)
                    {
                        DynamicParameters dbParams = new DynamicParameters();
                        dbParams.Add("Operation", "Update");
                        dbParams.Add("TenantId", tenantId);
                        dbParams.Add("AM_Assess_ID", obj.AM_Assess_ID);
                        dbParams.Add("AM_Assess_Name", obj.AM_Assess_Name);
                        dbParams.Add("AM_Assess_Details", obj.AM_Assess_Details);
                        dbParams.Add("AM_Assess_Type", obj.AM_Assess_Type);
                        dbParams.Add("AM_Assess_AmountFrom", obj.AM_Assess_AmountFrom);
                        dbParams.Add("AM_Assess_AmountTo", obj.AM_Assess_AmountTo);
                        dbParams.Add("AM_Assess_AssignType", obj.AM_Assess_AssignType);
                        dbParams.Add("AM_Assess_AssignUser", obj.AM_Assess_AssignUser);
                        dbParams.Add("tempId", null);
                        dbParams.Add("CreatedBy", userId);
                        dbParams.Add("UpdatedBy", userId);
                        dbParams.Add("UpdatedDate", DateTime.Now);
                        dbParams.Add("IsDeleted", false);
                        dbParams.Add("IsActive", true);

                        var AffectedRow = unitOfWork.Connection.Execute("usp_ModifyAuthorityMatrixAssessment", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                        if (AffectedRow != 0)
                        {
                            Errormessage = "Initial AuthorityMatrixAssessment has been successfully Inserted";
                            bit = true;
                        }
                        else
                        {
                            Errormessage = "Initial AuthorityMatrixAssessment has not been successfully Inserted";
                            bit = false;
                        }
                    }
                    else
                    {
                        Errormessage = "AM_Assess_ID is not null or empty";
                        bit = false;
                    }
                //}
                //else
                //{
                //    Errormessage = "User Id not is null or empty";
                //}
            }
            else
            {
                Errormessage = "Model is not null and empty";
                bit = false;
            }

            return Errormessage;
        }

        public string DeleteAuthorityMatrixAssess_Services(AuthorityMatrixAssessment obj, long userId, long tenantId)
        {
            string Errormessage = null;
            if (obj.AM_Assess_ID > 0)
            {
                DynamicParameters Params = new DynamicParameters();
                Params.Add("Operation", "Delete");
                Params.Add("AMA_ID", obj.AM_Assess_ID);
                var affectedrow = unitOfWork.Connection.Execute("usp_ModifyAuthorityMatrixAssess_Services", Params, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                if (affectedrow != 0)
                {
                    Errormessage = "Record has been successfully deleted";
                }
            }
            else
            {
                Errormessage = "AM_Assess_ID is not null and empty";
            }
           
            return Errormessage;
        }

        public string DeleteAuthorityMatrixAssess(long Id, long userId, long tenantId, out bool bit)
        {
            string ErrorMessage = null;
            bit = false;
            if (Id > 0)
            {
                DynamicParameters Params = new DynamicParameters();
                Params.Add("Operation", "Delete");
                Params.Add("TenantId", tenantId);
                Params.Add("AM_Assess_ID", Id);
                Params.Add("tempId", null);
                var affectedrow = unitOfWork.Connection.Execute("usp_ModifyAuthorityMatrixAssessment", Params, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                if (affectedrow != 0)
                {
                    ErrorMessage = "Record has been successfully deleted";
                    bit = true;
                }
                else
                {
                    ErrorMessage = "Record has not been successfully deleted";
                    bit = false;
                }
            }
            else
            {
                ErrorMessage = "Id is not null and empty";
                bit = false;
            }
           
            return ErrorMessage;
        }

        public string DeleteAuthorityMatrix(long Id, long userId, long tenantId, out bool bit)
        {
            string ErrorMessage = null;
            bit = false;
            if (Id > 0)
            {
                DynamicParameters Params = new DynamicParameters();
                Params.Add("Operation", "Delete");
                Params.Add("AM_Assign_ID", Id);
                Params.Add("TenantId", tenantId);
                var affectedrow = unitOfWork.Connection.Execute("usp_ModifyAuthorityMatrix", Params, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                if (affectedrow != 0)
                {
                    ErrorMessage = "Record has been successfully deleted";
                    bit = true;
                }
                else
                {
                    ErrorMessage = "Record has not been successfully deleted";
                    bit = false;
                }
            }
            else
            {
                ErrorMessage = "Id is not null and empty";
                bit = false;
            }
            return ErrorMessage;
        }


        public IEnumerable<AuthorityMatrixs> GetAuthoritysMatrixs(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetAuthoritysMatrixs");
            dbParams.Add("TenantId", tenentId);
            return unitOfWork.Connection.Query<AuthorityMatrixs>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);        
        }

        public AuthorityMatrixs GetAuthorityMatrix(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetAuthorityMatrix");
            dbParams.Add("AM_Assign_ID", Id);
            dbParams.Add("TenantId", tenentId);
            return unitOfWork.Connection.QuerySingleOrDefault<AuthorityMatrixs>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<AuthorityMatrixsAssessment> GetAuthoritysMatrixsAssess(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetAuthoritysMatrixsAssess");
            dbParams.Add("TenantId", tenentId);
            return unitOfWork.Connection.Query<AuthorityMatrixsAssessment>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }


        public AuthorityMatrixAssessment GetAuthorityMatrixAssess(long Id, long tenentId, long userId)
        {
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            var sql = "usp_GetAuthorityMatrix";
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetAuthorityMatrixAssess");
            dbParams.Add("AM_Assign_ID", Id);
            dbParams.Add("TenantId", tenentId);

            var benefitDictionary = new Dictionary<int, AuthorityMatrixAssessment>();
            var list = unitOfWork.Connection.Query<AuthorityMatrixAssessment, AMatrixAssess_Services, AuthorityMatrixAssessment>(
                sql,
                (productByID, benefits) =>
                {
                    AuthorityMatrixAssessment productEntry;
                    if (!benefitDictionary.TryGetValue(Convert.ToInt32( productByID.AM_Assess_ID), out productEntry))
                    {
                        productEntry = productByID;
                        productEntry.AMatrixAssess_Service = new List<AMatrixAssess_Services>();
                        benefitDictionary.Add(Convert.ToInt32(productEntry.AM_Assess_ID), productEntry);
                    }
                    productEntry.AMatrixAssess_Service.Add(benefits);
                    return productEntry;
                }, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams,
                splitOn: "AM_Assign_ID,AMA_ID").Distinct().ToList();
            var result = list.Where(p => p.AM_Assess_ID == Id).FirstOrDefault();
            return result;
        }



    }
}
