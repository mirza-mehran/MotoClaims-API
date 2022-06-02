using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.AgencyGarage;
using MotoClaims.Entities.Provider;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class AgenciesService : IAgenciesService
    {
        public AgencyGarageWithCount GetAgencyGarage(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long ProviderTypeId)
        {
            AgencyGarageWithCount obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AgencyGarageRepository myRepository = new AgencyGarageRepository(unitOfWork);
                    obj = myRepository.GetAgencyGarage( PageIndex,  PageSize,  SearchText,  SearchOption,  SortType,  SortName,  tenentId,  ProviderTypeId);
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

        public IEnumerable<ProviderLocation> GetAllLocationByProviderId(long Id, long tenentId)
        {
            IEnumerable<ProviderLocation> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AgencyGarageRepository myRepository = new AgencyGarageRepository(unitOfWork);
                    obj = myRepository.GetAllLocationByProviderId(Id,tenentId);
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

        public string InsertAgencyGarageClaimsAssigned(AgencyGarageClaimsAssigned obj, long tenantId, long userId)
        {
            string message = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AgencyGarageRepository myRepository = new AgencyGarageRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add AgencyGarageClaimsAssigned", "AgencyGarageClaimsAssigned", "AGCA_Id", obj.AGCA_Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    message = myRepository.InsertAgencyGarageClaimsAssigned(obj, tenantId, userId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add AgencyGarageClaimsAssigned", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                }
            }

            return message;
        }
    }
}
