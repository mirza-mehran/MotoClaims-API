using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Product;
using MotoClaims.Entities.User;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class PolicyService : IPolicyService
    {
        public IEnumerable<VehicleColour> GetColour(long tenentId, long userId)
        {
            IEnumerable<VehicleColour> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    obj = myRepository.GetColour( tenentId,  userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }
        public IEnumerable<PolicyMake> GetPolicyMake(long tenentId, long userId)
        {
            IEnumerable<PolicyMake> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    obj = myRepository.GetPolicyMake(tenentId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }
        long PolicyVehicleId;
        public long PostPolicy(Policy model, long tenentId, long userId)
        {
            string ErrorMessagePolicyModel= null,
                   ErrorMessagePolicy = null,
                   ErrorMessagePolicyCoverage = null,
                   ErrorMessagePolicyBenefit = null,
                   ErrorMessagePolicyVehicle = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Add Policy", "Policies", "Id", model.Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenentId);
                    // ErrorMessagePolicyModel = myRepository.InsertPolicyModel(model);
                    ErrorMessagePolicy = myRepository.InsertPolicy(model, tenentId, userId);
                    ErrorMessagePolicyCoverage = myRepository.InsertPolicyCoverage(model, tenentId, userId);
                    ErrorMessagePolicyBenefit = myRepository.InsertPolicyBenefit(model, tenentId, userId);
                    //ErrorMessagePolicyVehicle = myRepository.InsertPolicyVehicle(model);
                    PolicyVehicleId = myRepository.InsertPolicyVehicle(model, tenentId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Policy", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), userId, tenentId);
                    return PolicyVehicleId = 0;
                }
            }
            return PolicyVehicleId;
        }

        public  ProductByID GetProductDetails(long id, long tenentId, long userId)
        {
            ProductByID UserProfiles = new ProductByID();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    UserProfiles =  myRepository.GetProductByID(id, tenentId, userId);
                  //  unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return UserProfiles;
        }
        
        public IEnumerable<ProductByID> GetProductName(long id, long tenentId, long userId)
        {
            IEnumerable<ProductByID> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    obj = myRepository.GetProductName(id, tenentId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<PolicyModel> GetPolicyModel(long Id, long tenentId, long userId)
        {
            IEnumerable<PolicyModel> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    obj = myRepository.GetPolicyModel(Id, tenentId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public string InsertPolicyImage(ImagesUpload model, long tenentId, long userId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Add PolicyImage", "Policies", "Id", model.Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenentId);

                    ErrorMessage = myRepository.InsertPolicyImage( model,  tenentId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add PolicyImage", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), userId, tenentId);

                }
            }
            return ErrorMessage;
        }

        public string UpdatePolicyImage(ImagesUpload model, long tenentId, long userId)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Edit PolicyImage", "Policies", "Id", model.Id, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenentId);

                    ErrorMessage = myRepository.UpdatePolicyImage(model, tenentId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Edit PolicyImage", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenentId);
                }
            }
            return ErrorMessage;
        }

        public IEnumerable<Policy> GetPolicies(long tenentId, long userId)
        {
            IEnumerable<Policy> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    obj = myRepository.GetPolicies(tenentId, userId);
                   // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }

        public Policy GetPolicyByID(long id, long tenentId, long userId)
        {
            Policy UserProfiles = new Policy();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    UserProfiles = myRepository.GetPolicyByID(id, tenentId, userId);
                    //  unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return UserProfiles;
        }

        public string DeletePolicy(long Id, long tenentId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    GeneralSettings.ActivityLog(Id, "Delete Policy", "Providers", "Id", Id, userId, GeneralSettings.CRUD.DELETE.ToString(), unitOfWork, tenentId);

                    ErrorMessage = myRepository.DeletePolicy( Id, tenentId, userId, out bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Delete Policy", GeneralSettings.ToXML(Id), GeneralSettings.CRUD.DELETE.ToString(), userId, tenentId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }

        public long UpdatePolicy(Policy model, long tenentId, long userId)
        {
            string
                   ErrorMessagePolicy = null,
                   ErrorMessagePolicyCoverage = null,
                   ErrorMessagePolicyBenefit = null;
                   
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Edit Policy", "Policies", "Id", model.Id, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenentId);
                    ErrorMessagePolicy = myRepository.UpdatePolicy(model, tenentId, userId);
                    ErrorMessagePolicyCoverage = myRepository.UpdatePolicyCoverage(model, tenentId, userId);
                    ErrorMessagePolicyBenefit = myRepository.UpdatePolicyBenefit(model, tenentId, userId);
                    PolicyVehicleId = myRepository.UpdatePolicyVehicle(model, tenentId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Edit Policy", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenentId);
                    PolicyVehicleId = 0;
                }
            }
            //return ErrorMessagePolicyModel +" "+ ErrorMessagePolicy+" "+ ErrorMessagePolicyCoverage+" "+ErrorMessagePolicyBenefit+" "+ ErrorMessagePolicyVehicle; 
            return PolicyVehicleId;
        }

        public IEnumerable<ModelPolicy> GetAllPoliciesByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long userId)
        {
            IEnumerable<ModelPolicy> AllProvider = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    PolicyRepository myRepository = new PolicyRepository(unitOfWork);
                    AllProvider = myRepository.GetAllPoliciesByPageIndex(out TotalRecord, PageIndex, PageSize, SearchText, SearchOption, SortType, SortName, tenentId, userId);
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
    }
}
