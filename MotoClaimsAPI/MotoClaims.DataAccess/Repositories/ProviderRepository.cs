using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
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
    
    public class ProviderRepository
    {
        public ProviderRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IUnitOfWork unitOfWork = null;


        /// <summary>
        /// Get Function
        /// </summary>
        ///
        public IEnumerable<Provider> GetGarage(long tenantId)
        {
            IEnumerable<Provider> Providers = null;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenantId);

            dbParams.Add("Operation", "GetGarage");
            var result = unitOfWork.Connection.Query<Provider>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            Providers = result.ToList();

            DynamicParameters dbParam = new DynamicParameters();
            dbParam.Add("TenantId", tenantId);
            dbParam.Add("Operation", "GetAllLocation");
            var location = unitOfWork.Connection.Query<ProviderLocation>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParam);

            foreach (var item in Providers)
            {
                for (int i = 0; i < 1; i++)
                {
                    item.ProviderLocations = location.Where(p => p.ProviderId == item.Id).ToList();
                }
            }

            DynamicParameters dbParamc = new DynamicParameters();
            dbParamc.Add("TenantId", tenantId);
            dbParamc.Add("Operation", "GetAllContact");
            var contact = unitOfWork.Connection.Query<ProviderContact>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParamc);

            foreach (var item in Providers)
            {
                for (int i = 0; i < 1; i++)
                {
                    item.ProviderContacts = contact.Where(p => p.ProviderId == item.Id).ToList();
                }
            }

            return Providers;
        }

        public IEnumerable<Provider> GetAgency(long tenantId)
        {
            IEnumerable<Provider> Providers = null;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenantId);

            dbParams.Add("Operation", "GetAgency");
            var result = unitOfWork.Connection.Query<Provider>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            Providers = result.ToList();

            DynamicParameters dbParam = new DynamicParameters();
            dbParam.Add("TenantId", tenantId);
            dbParam.Add("Operation", "GetAllLocation");
            var location = unitOfWork.Connection.Query<ProviderLocation>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParam);

            foreach (var item in Providers)
            {
                for (int i = 0; i < 1; i++)
                {
                    item.ProviderLocations = location.Where(p => p.ProviderId == item.Id).ToList();
                }
            }

            DynamicParameters dbParamc = new DynamicParameters();
            dbParamc.Add("TenantId", tenantId);
            dbParamc.Add("Operation", "GetAllContact");
            var contact = unitOfWork.Connection.Query<ProviderContact>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParamc);

            foreach (var item in Providers)
            {
                for (int i = 0; i < 1; i++)
                {
                    item.ProviderContacts = contact.Where(p => p.ProviderId == item.Id).ToList();
                }
            }

            return Providers;
        }

        public IEnumerable<Provider> GetCarAgency(long tenantId)
        {
            IEnumerable<Provider> Providers = null;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenantId);

            dbParams.Add("Operation", "GetCarAgency");
            var result = unitOfWork.Connection.Query<Provider>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            Providers = result.ToList();

            DynamicParameters dbParam = new DynamicParameters();
            dbParam.Add("TenantId", tenantId);
            dbParam.Add("Operation", "GetAllLocation");
            var location = unitOfWork.Connection.Query<ProviderLocation>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParam);

            foreach (var item in Providers)
            {
                for (int i = 0; i < 1; i++)
                {
                    item.ProviderLocations = location.Where(p => p.ProviderId == item.Id).ToList();
                }
            }

            DynamicParameters dbParamc = new DynamicParameters();
            dbParamc.Add("TenantId", tenantId);
            dbParamc.Add("Operation", "GetAllContact");
            var contact = unitOfWork.Connection.Query<ProviderContact>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParamc);

            foreach (var item in Providers)
            {
                for (int i = 0; i < 1; i++)
                {
                    item.ProviderContacts = contact.Where(p => p.ProviderId == item.Id).ToList();
                }
            }

            return Providers;
        }

        public IEnumerable<Provider> GetSurveyor(long tenantId)
        {
            IEnumerable<Provider> Providers = null;
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenantId);

            dbParams.Add("Operation", "GetSurveyor");
            var result = unitOfWork.Connection.Query<Provider>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            Providers = result.ToList();

            DynamicParameters dbParam = new DynamicParameters();
            dbParam.Add("TenantId", tenantId);
            dbParam.Add("Operation", "GetAllLocation");
            var location = unitOfWork.Connection.Query<ProviderLocation>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParam);

            foreach (var item in Providers)
            {
                for (int i = 0; i < 1; i++)
                {
                    item.ProviderLocations = location.Where(p => p.ProviderId == item.Id).ToList();
                }
            }

            DynamicParameters dbParamc = new DynamicParameters();
            dbParamc.Add("TenantId", tenantId);
            dbParamc.Add("Operation", "GetAllContact");
            var contact = unitOfWork.Connection.Query<ProviderContact>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParamc);

            foreach (var item in Providers)
            {
                for (int i = 0; i < 1; i++)
                {
                    item.ProviderContacts = contact.Where(p => p.ProviderId == item.Id).ToList();
                }
            }

            return Providers;
        }


        public IEnumerable<ProviderCountry> GetProviderCountry(long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderCountry");
            dbParams.Add("TenantId", tenantId);
            var result = unitOfWork.Connection.Query<ProviderCountry>("usp_GetProviderCountry", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<ProviderCity> GetProviderCity(long CountryId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderCity");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("CountryId", CountryId);
            var result = unitOfWork.Connection.Query<ProviderCity>("usp_GetProviderCountry", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<Areas> GetProviderArea(long CityId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderArea");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("CityId", CityId);
            var result = unitOfWork.Connection.Query<Areas>("usp_GetProviderCountry", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<ProviderServiceType> GetProviderServiceType(long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetProviderServiceType");
            dbParams.Add("TenantId", tenantId);
            var result = unitOfWork.Connection.Query<ProviderServiceType>("usp_GetProviderServices", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
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

        public string GetProviderName(long ProviderTypeId, string name, long tenantId)
        {
            const string sql = "SELECT CAST(CASE WHEN EXISTS (SELECT 1 FROM Providers WHERE Name = @Name and ProviderTypeId = @Id and TenantId=@TenantId) THEN 1 ELSE 0 END as BIT)";
            bool exists = unitOfWork.Connection.ExecuteScalar<bool>(sql, new { Id = ProviderTypeId, Name = name, TenantId=tenantId }, transaction: unitOfWork.Transaction);
            string ErrorMessage = null;

            if (exists == true)
            {
                ErrorMessage = "Provider already existing ";
            }
            else
            {
                ErrorMessage = "Provider not exist";
            }
            return ErrorMessage;
        }

        /// <summary>
        /// Insert Function
        /// </summary>
        long ProviderId = 0, ProviderServiceId=0;
        public long InsertProvider(Providers model, long tenantId, long userId)
        {
            if (model != null )
            {
                //////////////Add Provider
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("name", model.Name);
                dbParams.Add("Status", 3);
                dbParams.Add("ProviderTypeId", model.ProviderTypeId);
                dbParams.Add("Image", model.Image);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                dbParams.Add("tempProviderId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                var affectedrows = unitOfWork.Connection.Execute("usp_Providers", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                ProviderId = dbParams.Get<long>("tempProviderId");
                if (affectedrows != 0)
                {
                    return ProviderId;
                }
                else
                {
                    return 0;
                }
            }
            return ProviderId;
        }

        public string InsertProviderContact(Providers model, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (model.ProviderContacts != null && ProviderId > 0 && model.ProviderContacts.Count > 0 )
            {
                foreach (var item in model.ProviderContacts)
                {
                    if (item != null)
                    {
                        //////////////Add ProviderContact
                        DynamicParameters dbCovParams = new DynamicParameters();
                        dbCovParams.Add("Operation", "Insert");
                        dbCovParams.Add("TenantId", tenantId);
                        dbCovParams.Add("FullName", item.FullName);
                        dbCovParams.Add("PhoneNumber", item.PhoneNumber);
                        dbCovParams.Add("Email", item.Email);
                        dbCovParams.Add("ProviderId", ProviderId);
                        dbCovParams.Add("CreatedBy", userId);
                        dbCovParams.Add("UpdatedBy", userId);
                        dbCovParams.Add("UpdatedDate", DateTime.Now);
                        dbCovParams.Add("IsDeleted", false);
                        dbCovParams.Add("IsActive", true);
                        var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderContact", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                    }
                }                       
            }
            else
            {
                ErrorMessage = "Required ProviderId";
            }

            return ErrorMessage;
        }

        //public string InsertProviderservices(Providers model, long tenantId)
        //{
        //    string ErrorMessage = null;

        //    if (model.ProviderServices != null && ProviderId > 0 && model.ProviderServices.Count > 0)
        //    {
        //        foreach (var item in model.ProviderServices)
        //        {
        //            if (item != null)
        //            {
        //                //////////////Add ProviderService
        //                DynamicParameters dbCovParams = new DynamicParameters();
        //                dbCovParams.Add("Operation", "Insert");
        //                dbCovParams.Add("TenantId", tenantId);
        //                dbCovParams.Add("Service", item.Service);
        //                dbCovParams.Add("ServiceTypeId", item.ServiceTypeId);

        //                dbCovParams.Add("CreatedBy", 1);
        //                dbCovParams.Add("UpdatedBy", 1);
        //                dbCovParams.Add("UpdatedDate", DateTime.Now);
        //                dbCovParams.Add("IsDeleted", false);
        //                dbCovParams.Add("IsActive", true);
        //                dbCovParams.Add("ProviderServiceId", dbType: DbType.Int64, direction: ParameterDirection.Output);
        //                var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderServices", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        //                ProviderServiceId = dbCovParams.Get<long>("ProviderServiceId");
        //                var message = InsertProviderservicesMapping(tenantId);

        //                if (affectedrows1 != 0 && message!=null)
        //                {
        //                    return ErrorMessage = "ProviderServices record has been successfully Inserted";
        //                }
        //                else
        //                {
        //                    return ErrorMessage = "ProviderServices record has not been successfully Inserted";
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ErrorMessage = "ProviderServices ProviderId";
        //    }

        //    return ErrorMessage;
        //}
        public string InsertProviderservicesMapping(Providers model, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (ProviderId > 0 && model.ProviderServiceMapping.Count > 0 )
            {
                foreach (var item in model.ProviderServiceMapping)
                {
                    if (item != null)
                    {
                        //////////////Add ProviderServiceMapping
                        DynamicParameters dbCovParams = new DynamicParameters();
                        dbCovParams.Add("Operation", "Insert");
                        dbCovParams.Add("TenantId", tenantId);
                        dbCovParams.Add("ProviderId", ProviderId);
                        dbCovParams.Add("ProviderServiceId", item.ProviderServiceId);
                        dbCovParams.Add("ServiceTypeId", item.ServiceTypeId);

                        dbCovParams.Add("CreatedBy", userId);
                        dbCovParams.Add("UpdatedBy", userId);
                        dbCovParams.Add("UpdatedDate", DateTime.Now);
                        dbCovParams.Add("IsDeleted", false);
                        dbCovParams.Add("IsActive", true);
                        var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderServicesMapping", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                
                    }
                    else
                    {
                        ErrorMessage = "ProviderServicesMapping ProviderId";
                    }
                }
            }
            return ErrorMessage;
        }

        public string InsertProviderLocation(Providers model, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (model.ProviderLocations != null && ProviderId > 0 && model.ProviderLocations.Count > 0)
            {
                foreach (var item in model.ProviderLocations)
                {
                    if (item != null)
                    {
                        //////////////  Add ProviderLocations
                        DynamicParameters dbCovParams = new DynamicParameters();
                        dbCovParams.Add("Operation", "Insert");
                        dbCovParams.Add("TenantId", tenantId);
                        dbCovParams.Add("BranchName", item.BranchName);
                        dbCovParams.Add("CountryId", item.CountryId);
                        dbCovParams.Add("StreetAddress", item.StreetAddress);
                        dbCovParams.Add("CityId", item.CityId);
                        dbCovParams.Add("Url", item.Url);
                        dbCovParams.Add("latitude", item.latitude);
                        dbCovParams.Add("longitude", item.longitude);
                        dbCovParams.Add("ProviderId", ProviderId);
                        dbCovParams.Add("AreaId", item.AreaId);

                        dbCovParams.Add("CreatedBy", userId);
                        dbCovParams.Add("UpdatedBy", userId);
                        dbCovParams.Add("UpdatedDate", DateTime.Now);
                        dbCovParams.Add("IsDeleted", false);
                        dbCovParams.Add("IsActive", true);
                        var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderLocation", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                    
                    }
                }
            }
            else
            {
                ErrorMessage = "Empty ProviderLocation";
            }

            return ErrorMessage;
        }

        public string InsertProviderImage(ImagesUpload model, long tenentId, long userId)
        {
            string ErrorMessage = null;

            if (model.Id > 0)
            {
                DynamicParameters dbBenefitParams = new DynamicParameters();
                dbBenefitParams.Add("Operation", "InsertimageUrl");
                dbBenefitParams.Add("TenantId", tenentId);
                dbBenefitParams.Add("Id", model.Id);
                dbBenefitParams.Add("Image", model.Image1);
                dbBenefitParams.Add("tempProviderId", null);
                var affectedrows2 = unitOfWork.Connection.Execute("usp_Providers", dbBenefitParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

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

        /// <summary>
        /// Update Function
        /// </summary>
        public long UpdateProvider(Providers model, long tenantId, long userId)
        {
            if (model != null)
            {
                //////////////Add Provider
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Update");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("Id", model.Id);
                dbParams.Add("name", model.Name);
                dbParams.Add("Status", 3);
                dbParams.Add("ProviderTypeId", model.ProviderTypeId);
                dbParams.Add("Image", model.Image);
                dbParams.Add("CreatedBy", 1);
                dbParams.Add("UpdatedBy", 1);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                dbParams.Add("tempProviderId", null);
                dbParams.Add("tempProviderId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                var affectedrows = unitOfWork.Connection.Execute("usp_Providers", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                ProviderId = dbParams.Get<long>("tempProviderId");

                if (affectedrows != 0)
                {
                    return ProviderId;
                }
                else
                {
                    return 0;
                }
            }
            return ProviderId;
        }
        public string UpdateProviderContact(Providers model, long tenantId, long userId)
        {
            string ErrorMessage = null;
            if (model.ProviderContacts != null && model.Id > 0 && model.ProviderContacts.Count > 0)
            {
                DynamicParameters Params = new DynamicParameters();
                Params.Add("Operation", "DeleteProviderContact");
                Params.Add("@Id", model.Id);
                Params.Add("@TenantId", tenantId);
                var affectedrow = unitOfWork.Connection.Execute("usp_ProviderContact", Params, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                foreach (var item in model.ProviderContacts)
                {
                    //if (item.Id != 0 && item != null)
                    //{
                    //    //////////////Update ProviderContact
                    //    DynamicParameters dbCovParams = new DynamicParameters();
                    //    dbCovParams.Add("Operation", "Update");
                    //    dbCovParams.Add("TenantId", tenantId);
                    //    dbCovParams.Add("Id", item.Id);
                    //    dbCovParams.Add("FullName", item.FullName);
                    //    dbCovParams.Add("PhoneNumber", item.PhoneNumber);
                    //    dbCovParams.Add("Email", item.Email);
                    //    dbCovParams.Add("ProviderId", ProviderId);
                    //    dbCovParams.Add("CreatedBy", 1);
                    //    dbCovParams.Add("UpdatedBy", 1);
                    //    dbCovParams.Add("UpdatedDate", DateTime.Now);
                    //    dbCovParams.Add("IsDeleted", false);
                    //    dbCovParams.Add("IsActive", true);
                    //    var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderContact", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                    //}
                    //else
                    //{
                        //////////////Add ProviderContact
                        DynamicParameters dbCovParams = new DynamicParameters();
                        dbCovParams.Add("Operation", "Insert");
                        dbCovParams.Add("TenantId", tenantId);
                        dbCovParams.Add("FullName", item.FullName);
                        dbCovParams.Add("PhoneNumber", item.PhoneNumber);
                        dbCovParams.Add("Email", item.Email);
                        dbCovParams.Add("ProviderId", ProviderId);
                        dbCovParams.Add("CreatedBy", 1);
                        dbCovParams.Add("UpdatedBy", 1);
                        dbCovParams.Add("UpdatedDate", DateTime.Now);
                        dbCovParams.Add("IsDeleted", false);
                        dbCovParams.Add("IsActive", true);
                        var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderContact", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                   // }

                }
            }
            else
            {
                ErrorMessage = "Required ProviderId";
            }

            return ErrorMessage;
        }
        public string UpdateProviderservicesMapping(Providers model, long tenantId, long userId)
        {
            string ErrorMessage = null;
            DynamicParameters Params = new DynamicParameters();
            Params.Add("Operation", "DeleteProviderMapping");
            Params.Add("Id", model.Id);
            Params.Add("TenantId", tenantId);
            var affectedrow = unitOfWork.Connection.Execute("usp_ProviderServicesMapping", Params, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            if (ProviderId > 0 && model.ProviderServiceMapping.Count > 0)
            {
                foreach (var item in model.ProviderServiceMapping)
                {
                    //if (item.Id != 0 && item != null)
                    //{
                    //    //////////////Update ProviderServiceMapping
                    //    DynamicParameters dbCovParams = new DynamicParameters();
                    //    dbCovParams.Add("Operation", "Update");
                    //    dbCovParams.Add("TenantId", tenantId);
                    //    dbCovParams.Add("ProviderId", ProviderId);
                    //    dbCovParams.Add("ProviderServiceId", item.ProviderServiceId);
                    //    dbCovParams.Add("ServiceTypeId", item.ServiceTypeId);

                    //    dbCovParams.Add("CreatedBy", 1);
                    //    dbCovParams.Add("UpdatedBy", 1);
                    //    dbCovParams.Add("UpdatedDate", DateTime.Now);
                    //    dbCovParams.Add("IsDeleted", false);
                    //    dbCovParams.Add("IsActive", true);
                    //    var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderServicesMapping", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                    //}
                    //else
                    //{
                        //////////////Add ProviderServiceMapping
                        DynamicParameters dbCovParams = new DynamicParameters();
                        dbCovParams.Add("Operation", "Insert");
                        dbCovParams.Add("TenantId", tenantId);
                        dbCovParams.Add("ProviderId", ProviderId);
                        dbCovParams.Add("ProviderServiceId", item.ProviderServiceId);
                        dbCovParams.Add("ServiceTypeId", item.ServiceTypeId);

                        dbCovParams.Add("CreatedBy", 1);
                        dbCovParams.Add("UpdatedBy", 1);
                        dbCovParams.Add("UpdatedDate", DateTime.Now);
                        dbCovParams.Add("IsDeleted", false);
                        dbCovParams.Add("IsActive", true);
                        var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderServicesMapping", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                   // }
                }
            }

            return ErrorMessage;
        }
        //public string UpdateProviderservices(Providers model, long tenantId)
        //{
        //    string ErrorMessage = null;

        //    if (model.ProviderServices != null && model.Id > 0 && model.ProviderServices.Count > 0)
        //    {
        //        foreach (var item in model.ProviderServices)
        //        {
        //            if (item != null)
        //            {
        //                //////////////Add ProviderService
        //                DynamicParameters dbCovParams = new DynamicParameters();
        //                dbCovParams.Add("Operation", "Update");
        //                dbCovParams.Add("TenantId", tenantId);
        //                dbCovParams.Add("Id", item.Id);
        //                dbCovParams.Add("Service", item.Service);
        //                dbCovParams.Add("ServiceTypeId", item.ServiceTypeId);

        //                dbCovParams.Add("CreatedBy", 1);
        //                dbCovParams.Add("UpdatedBy", 1);
        //                dbCovParams.Add("UpdatedDate", DateTime.Now);
        //                dbCovParams.Add("IsDeleted", false);
        //                dbCovParams.Add("IsActive", true);
        //                dbCovParams.Add("ProviderServiceId", null);


        //                dbCovParams.Add("ProviderServiceId", dbType: DbType.Int64, direction: ParameterDirection.Output);
        //                var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderServices", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        //                ProviderServiceId = dbCovParams.Get<long>("ProviderServiceId");
        //                var message = InsertProviderservicesMapping(tenantId);

        //                if (affectedrows1 != 0 && message != null)
        //                {
        //                    return ErrorMessage = "ProviderServices record has been successfully Inserted";
        //                }
        //                else
        //                {
        //                    return ErrorMessage = "ProviderServices record has not been successfully Inserted";
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ErrorMessage = "ProviderServices ProviderId";
        //    }

        //    return ErrorMessage;
        //}
        public string UpdateProviderLocation(Providers model, long tenantId, long userId)
        {
            string ErrorMessage = null;
            DynamicParameters Params = new DynamicParameters();
            Params.Add("Operation", "DeleteProviderLocation");
            Params.Add("Id", model.Id);
            Params.Add("TenantId", tenantId);
            var affectedrow = unitOfWork.Connection.Execute("usp_ProviderLocation", Params, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            if (model.ProviderLocations != null && ProviderId > 0 && model.ProviderLocations.Count > 0)
            {
                foreach (var item in model.ProviderLocations)
                {
                    //if (item.Id != 0 && item != null)
                    //{
                    //    //////////////  Update ProviderLocations
                    //    DynamicParameters dbCovParams = new DynamicParameters();
                    //    dbCovParams.Add("Operation", "Update");
                    //    dbCovParams.Add("TenantId", tenantId);
                    //    dbCovParams.Add("Id", item.Id);
                    //    dbCovParams.Add("BranchName", item.BranchName);
                    //    dbCovParams.Add("CountryId", item.CountryId);
                    //    dbCovParams.Add("StreetAddress", item.StreetAddress);
                    //    dbCovParams.Add("CityId", item.CityId);
                    //    dbCovParams.Add("Url", item.Url);
                    //    dbCovParams.Add("latitude", item.latitude);
                    //    dbCovParams.Add("longitude", item.longitude);
                    //    dbCovParams.Add("ProviderId", ProviderId);
                    //    dbCovParams.Add("AreaId", item.AreaId);

                    //    dbCovParams.Add("CreatedBy", 1);
                    //    dbCovParams.Add("UpdatedBy", 1);
                    //    dbCovParams.Add("UpdatedDate", DateTime.Now);
                    //    dbCovParams.Add("IsDeleted", false);
                    //    dbCovParams.Add("IsActive", true);
                    //    var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderLocation", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                    //}
                    //else
                    //{
                        //////////////  Add ProviderLocations
                        DynamicParameters dbCovParams = new DynamicParameters();
                        dbCovParams.Add("Operation", "Insert");
                        dbCovParams.Add("TenantId", tenantId);
                        dbCovParams.Add("BranchName", item.BranchName);
                        dbCovParams.Add("CountryId", item.CountryId);
                        dbCovParams.Add("StreetAddress", item.StreetAddress);
                        dbCovParams.Add("CityId", item.CityId);
                        dbCovParams.Add("Url", item.Url);
                        dbCovParams.Add("latitude", item.latitude);
                        dbCovParams.Add("longitude", item.longitude);
                        dbCovParams.Add("ProviderId", ProviderId);
                        dbCovParams.Add("AreaId", item.AreaId);

                        dbCovParams.Add("CreatedBy", userId);
                        dbCovParams.Add("UpdatedBy", userId);
                        dbCovParams.Add("UpdatedDate", DateTime.Now);
                        dbCovParams.Add("IsDeleted", false);
                        dbCovParams.Add("IsActive", true);
                        var affectedrows1 = unitOfWork.Connection.Execute("usp_ProviderLocation", dbCovParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                   // }
                }
            }
            else
            {
                ErrorMessage = "Required ProviderLocation and ProviderId";
            }

            return ErrorMessage;
        }
        public bool DeleteProvider(long Id, long TenantId, long userId)
        {
            bool bit = false;
            //string ErrorMessage = null;
            if (Id >= 0)
            {
                var param = new DynamicParameters();
                param.Add("Operation", "Delete");
                param.Add("Id", Id);
                param.Add("TenantId", TenantId);
                param.Add("tempProviderId", null);
                var affectedRows = unitOfWork.Connection.Execute("usp_Providers", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: param);
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

        public Providers GetProviderByID(long id, long tenantId)
        {
            Providers Provider = new Providers();
            var dbParams = new DynamicParameters();
            dbParams.Add("@Id", id);
            dbParams.Add("@TenantId", tenantId);
           
            for (int i = 0; i <= 3; i++)
            {
                if (i == 0)
                {
                    dbParams.Add("@Operation", "GetProvider");
                    var result = unitOfWork.Connection.Query<Providers>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    Provider = result.First();
                }
                else if (i==1)
                {
                    dbParams.Add("@Operation", "GetProviderById");
                    var result = unitOfWork.Connection.Query<ProviderServiceMapping>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    Provider.ProviderServiceMapping = result.ToList();
                }
                else if (i == 2)
                {
                    dbParams.Add("@Operation", "GetProviderContact");
                    var result = unitOfWork.Connection.Query<ProviderContact>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    Provider.ProviderContacts = result.ToList();
                }
                else if (i == 3)
                {
                    dbParams.Add("@Operation", "GetProviderLocation");
                    var result = unitOfWork.Connection.Query<ProviderLocation>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    Provider.ProviderLocations = result.ToList();
                }
            }
            return Provider;
        }
        //public Providers GetProviderByID(long id,long tenantId)
        //{
        //    Dictionary<int, object> dictionary = new Dictionary<int, object>();
        //    var sql = "usp_GetProvider";
        //    var param = new DynamicParameters();
        //    param.Add("@Id", id);
        //    param.Add("@TenantId", tenantId);
        //    param.Add("@Operation", "GetProviderById");

        //    var benefitDictionary = new Dictionary<int, Providers>();
        //    var list = unitOfWork.Connection.Query<Providers, ProviderContact, ProviderServices, ProviderLocation, ProviderServiceMapping, Providers>(
        //        sql,
        //        (P,PC,PS,PL,PSM) =>
        //        {
        //            Providers ProviderEntry;
        //            if (!benefitDictionary.TryGetValue(Convert.ToInt32(P.Id), out ProviderEntry))
        //            {
        //                ProviderEntry = P;
        //                ProviderEntry.ProviderContacts = new List<ProviderContact>() ;
        //                ProviderEntry.ProviderServices = new List<ProviderServices>();
        //                ProviderEntry.ProviderLocations = new List<ProviderLocation>();
        //                ProviderEntry.ProviderServiceMapping = new List<ProviderServiceMapping>();
        //                benefitDictionary.Add(Convert.ToInt32(ProviderEntry.Id), ProviderEntry);
        //            }
        //            ProviderEntry.ProviderContacts.Add(PC);
        //            ProviderEntry.ProviderServices.Add(PS);
        //            ProviderEntry.ProviderLocations.Add(PL);
        //            ProviderEntry.ProviderServiceMapping.Add(PSM);

        //            return ProviderEntry;
        //        }, param, commandType: System.Data.CommandType.StoredProcedure,
        //        splitOn: "Id,Id,,Id,Id,Id,Id").Distinct().ToList();

        //    var abc = list.First();
        //    IEnumerable<ProviderContact> ProviderContact = list.First().ProviderContacts.GroupBy(x => x.Id).Select(g => g.First());
        //    IEnumerable<ProviderServices> ProviderService = list.First().ProviderServices.GroupBy(x => x.Id).Select(g => g.First());
        //    IEnumerable<ProviderLocation> ProviderLocation = list.First().ProviderLocations.GroupBy(x => x.Id).Select(g => g.First());
        //    IEnumerable<ProviderServiceMapping> ProviderServiceMapping = list.First().ProviderServiceMapping.GroupBy(x => x.Id).Select(g => g.First());

        //    Providers pro = new Providers();
        //    pro.ProviderContacts = ProviderContact.ToList();
        //   // pro.ProviderServices = ProviderService.ToList();
        //    pro.ProviderLocations = ProviderLocation.ToList();
        //    pro.ProviderServiceMapping = ProviderServiceMapping.ToList();

        //    pro.Id = abc.Id;
        //    pro.Name = abc.Name;
        //    pro.Image = abc.Image;
        //    return pro;
        //}

        //public IEnumerable<ProviderServiceMapping> GetProviderByID(long id, long tenantId)
        //{
        //    Dictionary<int, object> dictionary = new Dictionary<int, object>();
        //    //var sql = "usp_GetProvider";
        //    var dbParams = new DynamicParameters();
        //    dbParams.Add("@Id", id);
        //    dbParams.Add("@TenantId", tenantId);
        //    dbParams.Add("@Operation", "GetProviderById");
        //    //  var benefitDictionary = new Dictionary<int, Providers>();

        //    var result = unitOfWork.Connection.Query<ProviderServiceMapping>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        //    return result;

        //}

        public IEnumerable<ModelProvider> GetAllProviderByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long ProviderTypeId)
        {

            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PageIndex", PageIndex);
            dbParams.Add("PageSize", PageSize);
            dbParams.Add("SearchText", SearchText);
            dbParams.Add("SearchOption", SearchOption);
            dbParams.Add("SortType", SortType);
            dbParams.Add("SortName", SortName);
            dbParams.Add("ProviderTypeId", ProviderTypeId);
            dbParams.Add("TotalRecord", dbType: DbType.Int64, direction: ParameterDirection.Output);

            var result = unitOfWork.Connection.Query<ModelProvider>("usp_GetAllProviderByPageIndex", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            long TotalRecords = dbParams.Get<long>("TotalRecord");
            TotalRecord = TotalRecords;

            return result;
        }

        IEnumerable<Provider> Providers = null;
        public IEnumerable<Provider> GetAllGarage(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId)
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
            //dbParams.Add("Operation", "GetGarage");
            var result = unitOfWork.Connection.Query<Provider>("usp_GetAllProviderByPageIndex", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            TotalRecord = dbParams.Get<long>("TotalRecord");
            Providers = result;

            return Providers;
        }

        public IEnumerable<ProviderLocation> GetAllLocation(long Id, long tenentId)
        {
            DynamicParameters dbParam = new DynamicParameters();
            dbParam.Add("@TenantId", tenentId);
            dbParam.Add("@Id", Id);
            dbParam.Add("@Operation", "GetLocationById");
            var location = unitOfWork.Connection.Query<ProviderLocation>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParam);

            //if ( SortName == "Garage address")
            //{
            //    Providers.OrderBy(p => p.ProviderLocations);
            //}

            return location;
        }

        public IEnumerable<ProviderContact> GetAllContact(long Id, long tenentId)
        {
            DynamicParameters dbParamc = new DynamicParameters();
            dbParamc.Add("TenantId", tenentId);
            dbParamc.Add("@Id", Id);
            dbParamc.Add("Operation", "GetContactById");
            var contact = unitOfWork.Connection.Query<ProviderContact>("usp_GetProvider", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParamc);
            return contact;
        }


    }
}
