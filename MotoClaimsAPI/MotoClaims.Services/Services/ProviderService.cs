using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Provider;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class ProviderService : IProviderService
    {
        
        public IEnumerable<ProviderCountry> GetProviderCountry(long tenantId)
        {
            IEnumerable<ProviderCountry> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                     obj = myRepository.GetProviderCountry(tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public bool DeleteProvider(long Id, long TenantId, long userId)
        {
            string ErrorMessage = null;
            bool bit;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    GeneralSettings.ActivityLog(Id, "Delete Provider", "Providers", "Id",Id, userId, GeneralSettings.CRUD.DELETE.ToString(), unitOfWork, TenantId);

                    bit = myRepository.DeleteProvider(Id, TenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Delete Provider", GeneralSettings.ToXML(Id), GeneralSettings.CRUD.DELETE.ToString(), userId, TenantId);
                    bit = false;
                }
            }
            return bit;
        }

        public IEnumerable<ProviderCity> GetProviderCity(long CountryId, long tenantId)
        {
            IEnumerable<ProviderCity> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    obj = myRepository.GetProviderCity(CountryId, tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<Areas> GetProviderArea(long CityId, long tenantId)
        {
            IEnumerable<Areas> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    obj = myRepository.GetProviderArea( CityId,  tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<ProviderServiceType> GetProviderServiceType(long tenantId)
        {
            IEnumerable<ProviderServiceType> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    obj = myRepository.GetProviderServiceType(tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<ProviderServices> GetProviderServices(long ServiceTypeId, long tenantId)
        {
            IEnumerable<ProviderServices> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    obj = myRepository.GetProviderServices(ServiceTypeId,tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        long ProviderId=0;
        public long PostProvider(Providers model, long tenantId, long userId)
        {
            string ErrorMessagePolicy = null,
                   ErrorMessagePolicyCoverage = null,
                   ErrorMessagePolicyBenefit = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Add Provider", "Providers", "Id", model.Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    ProviderId = myRepository.InsertProvider(model, tenantId, userId);
                    ErrorMessagePolicy = myRepository.InsertProviderContact(model, tenantId, userId);
                    ErrorMessagePolicyCoverage = myRepository.InsertProviderservicesMapping(model, tenantId, userId);
                    ErrorMessagePolicyBenefit = myRepository.InsertProviderLocation(model, tenantId, userId);                   
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Provider", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    return ProviderId = 0;
                }
            }
            return ProviderId;
        }
    
        public string InsertProviderImage(ImagesUpload model, long tenentId, long userId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Add Provider Image", "Providers", "Id", model.Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenentId);
                    ErrorMessage = myRepository.InsertProviderImage(model, tenentId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Provider Image", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), userId, tenentId);

                }
            }
            return ErrorMessage;
        }

        public long UpdateProvider(Providers model, long tenantId, long userId)
        {
            string ErrorMessagePolicy = null,
                  ErrorMessagePolicyCoverage = null,
                  ErrorMessagePolicyBenefit = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Edit Provider", "Providers", "Id", model.Id, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);
                    
                    ProviderId = myRepository.UpdateProvider(model, tenantId, userId);
                    ErrorMessagePolicy = myRepository.UpdateProviderContact(model, tenantId, userId);
                    ErrorMessagePolicyCoverage = myRepository.UpdateProviderservicesMapping(model, tenantId, userId);
                    ErrorMessagePolicyBenefit = myRepository.UpdateProviderLocation(model, tenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Edit Provider", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                    return ProviderId = 0;
                }
            }
            return ProviderId;
        }

        public string GetProviderName(long ProviderTypeId, string name,long tenantId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    ErrorMessage = myRepository.GetProviderName(ProviderTypeId, name, tenantId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return ErrorMessage;
        }


        public IEnumerable<Provider> GetGarage(long tenantId)
        {
            IEnumerable<Provider> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                     obj = myRepository.GetGarage(tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<Provider> GetAgency(long tenantId)
        {
            IEnumerable<Provider> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    obj = myRepository.GetAgency(tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<Provider> GetCarAgency(long tenantId)
        {
            IEnumerable<Provider> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    obj = myRepository.GetCarAgency(tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<Provider> GetSurveyor(long tenantId)
        {
            IEnumerable<Provider> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    obj = myRepository.GetSurveyor(tenantId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public Providers GetProviderByID(long id, long tenantId)
        {
            Providers Providers = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    Providers = myRepository.GetProviderByID(id, tenantId);
                    //  unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return Providers;
        }

        public IEnumerable<ModelProvider> GetAllProviderByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long ProviderTypeId)
        {
            IEnumerable<ModelProvider> AllProvider = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    AllProvider = myRepository.GetAllProviderByPageIndex(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, ProviderTypeId);
                    //unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    TotalRecord = 0;
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return AllProvider;
        }

        //public Providers GetProviderByID(long id, long tenantId)
        //{
        //    Providers Providers = new Providers();
        //    using (Repositories dalSession = new Repositories())
        //    {
        //        UnitOfWork unitOfWork = dalSession.UnitOfWork;
        //        //unitOfWork.Begin();
        //        try
        //        {
        //            ProviderRepository myRepository = new ProviderRepository(unitOfWork);
        //            Providers = myRepository.GetProviderByID(id, tenantId);
        //            //  unitOfWork.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            unitOfWork.Rollback();
        //            //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
        //            //throw;
        //        }
        //    }
        //    return Providers;
        //}

        public IEnumerable<Provider> GetAllGarage(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId)
        {
            IEnumerable<Provider> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderRepository myRepository = new ProviderRepository(unitOfWork);
                    obj = myRepository.GetAllGarage(out TotalRecord,  PageIndex,  PageSize,  SearchText,  SearchOption,  SortType,  SortName,  tenentId);

                    foreach (var item in obj)
                    {
                        item.ProviderLocations = myRepository.GetAllLocation(item.Id, tenentId);
                        item.ProviderContacts  = myRepository.GetAllContact(item.Id, tenentId);
                    }

                    if (SortName == "POCname")
                    {
                        obj.OrderBy(p => p.ProviderContacts).AsQueryable();
                    }

                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    TotalRecord = 0;
                    // unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }


    }
}
