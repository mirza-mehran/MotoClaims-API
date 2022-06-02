using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
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
   public class ProviderServicesContractService : IProviderServicesContractService
    {

        public IEnumerable<ProviderServiceType> GetProviderServiceType(long ProviderTypeId, long tenantId)
        {
            IEnumerable<ProviderServiceType> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj = myRepository.GetProviderServiceType( ProviderTypeId,  tenantId);
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
                    obj = myRepository.GetProviderServices(ServiceTypeId, tenantId);
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

        public IEnumerable<PolicyMake> GetMake(long tenentId, long userId)
        {
            IEnumerable<PolicyMake> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj = myRepository.GetMake(tenentId, userId);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<PolicyModel> GetModel(long Id, long tenentId, long userId)
        {
            IEnumerable<PolicyModel> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj = myRepository.GetModel(Id, tenentId, userId);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<ProviderServices> GetProviderServices(long ProviderId, long tenantId, long userId)
        {
            IEnumerable<ProviderServices> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj = myRepository.GetProviderServices(ProviderId,tenantId,  userId);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //throw;
                }
            }
            return obj;
        }

        public string InsertProviderContract(Provider_Services_Contract obj, long tenantId, long userId, out bool bit)
        {
            PS_Prices obj1 = null;
            
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
             
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add Provider_Services_Contract", "Provider_Services_Contract", "PSC_Id", obj.PSC_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.InsertProviderContract(obj, tenantId, userId, out bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Provider_Services_Contract", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }

            return ErrorMessage;
        }

        public string InsertProviderContractPrice(PS_Prices obj, long tenantId, long userId, out bool bit)
        {
            PS_Prices obj1 = null;
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add Provider_Services_Contract_Prices", "PS_Prices", "PS_Price_Id", obj.PSC_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);

                    obj1 = myRepository.GetDate(obj.PSC_Id);
                    if (obj1.End_Date == DateTime.MinValue)
                    {
                        ErrorMessage = myRepository.InsertPS_Prices(obj, tenantId, userId, out bit);
                        unitOfWork.Commit();
                    }
                    else if (obj1.End_Date < obj.End_Date)
                    {
                        ErrorMessage = myRepository.InsertPS_Prices(obj, tenantId, userId, out bit);
                        unitOfWork.Commit();
                    }
                    else
                    {
                        ErrorMessage = "End Date should be greater then Start date";
                        bit = false;
                    }
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Provider_Services_Contract_Prices", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }

            return ErrorMessage;
        }

        public string UpdateProviderContract(Provider_Services_Contract obj, long tenantId, long userId, out bool bit)
        {
            PS_Prices obj1 = null;
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Edit Provider_Services_Contract", "Provider_Services_Contract", "PSC_Id", obj.PSC_Id, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.UpdateProviderContract(obj, tenantId, userId, out bit);
                    unitOfWork.Commit();
                    //if (bit == true)
                    //{
                    //   // obj1 = myRepository.GetDate(obj.Provider_Id, obj.ProviderService_Id);
                    //    if (obj1.End_Date == DateTime.MinValue)
                    //    {
                    //        ErrorMessage = myRepository.UpdatePS_Prices(obj.PS_Prices, tenantId, userId, out bit);
                    //        unitOfWork.Commit();
                    //    }
                    //    else if (obj1.End_Date < obj.PS_Prices.End_Date)
                    //    {
                    //        ErrorMessage = myRepository.UpdatePS_Prices(obj.PS_Prices, tenantId, userId, out bit);
                    //        unitOfWork.Commit();
                    //    }
                    //    else
                    //    {
                    //        ErrorMessage = "End Date should be greater then Start date";
                    //        bit = false;
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Edit Provider_Services_Contract", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                    bit = false;
                }
            }

            return ErrorMessage;
        }

        public string UpdateProviderContractPrice(PS_Prices obj, long tenantId, long userId, out bool bit)
        {
            PS_Prices obj1 = null;
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Update Provider_Services_Contract_Prices", "PS_Prices", "PS_Price_Id", obj.PSC_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);

                    obj1 = myRepository.GetDate(obj.PSC_Id);
                    if (obj1.End_Date == DateTime.MinValue)
                    {
                        ErrorMessage = myRepository.UpdatePS_Prices(obj, tenantId, userId, out bit);
                        unitOfWork.Commit();
                    }
                    else if (obj1.End_Date < obj.End_Date)
                    {
                        ErrorMessage = myRepository.UpdatePS_Prices(obj, tenantId, userId, out bit);
                        unitOfWork.Commit();
                    }
                    else
                    {
                        ErrorMessage = "End Date should be greater then Start date";
                        bit = false;
                    }
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Update Provider_Services_Contract_Prices", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }

            return ErrorMessage;
        }

        public ModelProvider_Services_Contract GetProvider_Services_Contract(long Id, long tenentId, long userId)
        {
            ModelProvider_Services_Contract obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj = myRepository.GetProvider_Services_Contract( Id,  tenentId,  userId);
                    //obj.PS_Prices = myRepository.GetProvider_Services_Price( Id,  tenentId,  userId);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                }
            }
            return obj;
        }

        public bool DeleteProvider_Services_Contract(long Id, long TenantId, long userId)
        {
            string ErrorMessage = null;
            bool bit;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    GeneralSettings.ActivityLog(Id, "Delete Provider_Services_Contract", "Provider_Services_Contract", "PSC_Id", Id, userId, GeneralSettings.CRUD.DELETE.ToString(), unitOfWork, TenantId);

                    bit = myRepository.DeleteProvider_Services_Contract(Id, TenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Delete Provider_Services_Contract", GeneralSettings.ToXML(Id), GeneralSettings.CRUD.DELETE.ToString(), userId, TenantId);
                    bit = false;
                }
            }
            return bit;
        }

        public IEnumerable<ModelProviderContractServices> GetProvider_Services_Contrac_By_ProviderId(long ProviderId, long tenentId, long userId)
        {
            IEnumerable<ModelProviderContractServices> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj = myRepository.GetProvider_Services_Contrac_By_ProviderId( ProviderId,  tenentId,  userId);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //throw;
                }
            }
            return obj;
        }

        public IEnumerable<PS_Prices> GetProvider_Services_Contrac_Prices(long PSC_Id, long tenentId, long userId)
        {
            IEnumerable<PS_Prices> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj = myRepository.GetProvider_Services_Contrac_Prices(PSC_Id, tenentId, userId);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //throw;
                }
            }
            return obj;
        }

        public bool DeleteProvider_Services_Contract_Price(long Id, long TenantId, long userId)
        {
            string ErrorMessage = null;
            bool bit;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    GeneralSettings.ActivityLog(Id, "Delete Provider_Services_Contract", "Provider_Services_Contract", "PSC_Id", Id, userId, GeneralSettings.CRUD.DELETE.ToString(), unitOfWork, TenantId);

                    bit = myRepository.DeleteProvider_Services_Contract_Price(Id, TenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Delete Provider_Services_Contract", GeneralSettings.ToXML(Id), GeneralSettings.CRUD.DELETE.ToString(), userId, TenantId);
                    bit = false;
                }
            }
            return bit;
        }

        public PS_Prices GetProvider_Services_Price(long Id, long tenentId, long userId)
        {
            PS_Prices obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj = myRepository.GetProvider_Services_Price( Id,  tenentId,  userId);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                }
            }
            return obj;
        }

        public PS_Prices GetDate(long Id)
        {
            PS_Prices obj1 = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProviderServicesContractRepository myRepository = new ProviderServicesContractRepository(unitOfWork);
                    obj1 = myRepository.GetDate(Id);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //throw;
                }
            }
            return obj1;
        }
    }
}
