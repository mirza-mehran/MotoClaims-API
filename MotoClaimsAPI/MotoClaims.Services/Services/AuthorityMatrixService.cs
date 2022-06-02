using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.AuthorityMatrix;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Product;
using MotoClaims.Entities.Provider;
using MotoClaims.Entities.User;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class AuthorityMatrixService : IAuthorityMatrixService
    {
        public IEnumerable<PolicyMake> GetMake(long tenentId, long userId, string SearchText)
        {
            IEnumerable<PolicyMake> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    obj = myRepository.GetMake(tenentId, userId, SearchText);
                   // unitOfWork.Commit();
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

        public IEnumerable<PolicyModel> GetModel(long Id, long tenentId, long userId, string SearchText)
        {
            IEnumerable<PolicyModel> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    obj = myRepository.GetModel(Id, tenentId, userId, SearchText);
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

        public IEnumerable<ProductList> GetProducts(long tenentId, long userId, string SearchText)
        {
            IEnumerable<ProductList> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    obj = myRepository.GetProducts(tenentId, userId, SearchText);
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

        public IEnumerable<UserProfiles> GetUserProfiles(long id, long roleId, long tenantId, string SearchText)
        {
            IEnumerable<UserProfiles> UserProfiles = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    UserProfiles = myRepository.GetUserProfiles(id, roleId, tenantId, SearchText);
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return UserProfiles;
        }

        public IEnumerable<AuthorityMatrixAssess_Services> GetServices(long tenantId, string SearchText)
        {
            IEnumerable<AuthorityMatrixAssess_Services> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    obj = myRepository.GetServices(tenantId, SearchText);
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

        public string InsertAuthorityMatrix(AuthorityMatrixs obj, long userId, long tenantId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Initial AuthorityMatrix", "AuthorityMatrix", "AM_Assign_ID", obj.AM_Assign_ID, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.InsertAuthorityMatrix( obj,  userId,  tenantId, out bit);
                 
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Initial AuthorityMatrix", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }

        public string UpdateAuthorityMatrix(AuthorityMatrixs obj, long userId, long tenantId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Edit AuthorityMatrix", "AuthorityMatrix", "AM_Assign_ID", obj.AM_Assign_ID, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.UpdateAuthorityMatrix(obj, userId, tenantId, out bit);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Edit AuthorityMatrix", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }

        public string InsertAuthorityMatrixAssessment(AuthorityMatrixAssessment obj, long userId, long tenantId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Add Assessment Authority Matrix", "AuthorityMatrixAssessment", "AM_Assess_ID", obj.AM_Assess_ID, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.InsertAuthorityMatrixAssessment(obj, userId, tenantId, out bit);
                    ErrorMessage = myRepository.InsertAuthorityMatrixAssess_Services(obj, userId, tenantId);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Assessment Authority Matrix", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.INSERT.ToString(), userId, tenantId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }

        public string UpdateAuthorityMatrixAssessment(AuthorityMatrixAssessment obj, long userId, long tenantId, out bool bit)
        {
            string ErrorMessage = null, ErrorMessage1 = null;
            
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    GeneralSettings.ActivityLog(obj, "Edit AuthorityMatrixAssessment", "AuthorityMatrixAssessment", "AM_Assess_ID", obj.AM_Assess_ID, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.UpdateAuthorityMatrixAssessment(obj, userId, tenantId, out bit);
                    ErrorMessage1 = myRepository.DeleteAuthorityMatrixAssess_Services(obj, userId, tenantId);
                    ErrorMessage = myRepository.InsertAuthorityMatrixAssess_Services(obj, userId, tenantId);
                    
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Edit AuthorityMatrixAssessment", GeneralSettings.ToXML(obj), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenantId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }

        public string DeleteAuthorityMatrixAssess(long Id, long userId, long tenantId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    GeneralSettings.ActivityLog(Id, "Delete AuthorityMatrixAssessment", "AuthorityMatrixAssessment", "AM_Assess_ID", Id, userId, GeneralSettings.CRUD.DELETE.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.DeleteAuthorityMatrixAssess(Id, userId, tenantId, out bit);
                   
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Delete AuthorityMatrixAssessment", GeneralSettings.ToXML(Id), GeneralSettings.CRUD.DELETE.ToString(), userId, tenantId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }

        public string DeleteAuthorityMatrix(long Id, long userId, long tenantId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    GeneralSettings.ActivityLog(Id, "Delete AuthorityMatrix", "AuthorityMatrix", "AM_Assign_ID", Id, userId, GeneralSettings.CRUD.DELETE.ToString(), unitOfWork, tenantId);
                    ErrorMessage = myRepository.DeleteAuthorityMatrix(Id, userId, tenantId, out bit);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Delete AuthorityMatrix", GeneralSettings.ToXML(Id), GeneralSettings.CRUD.DELETE.ToString(), userId, tenantId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }

        public IEnumerable<AuthorityMatrixs> GetAuthoritysMatrixs(long Id, long tenentId, long userId)
        {
            IEnumerable<AuthorityMatrixs> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    obj = myRepository.GetAuthoritysMatrixs(Id, tenentId, userId);
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

        public AuthorityMatrixs GetAuthorityMatrix(long Id, long tenentId, long userId)
        {
            AuthorityMatrixs obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    obj = myRepository.GetAuthorityMatrix( Id,  tenentId,  userId);
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

        public IEnumerable<AuthorityMatrixsAssessment> GetAuthoritysMatrixsAssess(long Id, long tenentId, long userId)
        {
            IEnumerable<AuthorityMatrixsAssessment> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    obj = myRepository.GetAuthoritysMatrixsAssess(Id, tenentId, userId);
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

        public AuthorityMatrixAssessment GetAuthorityMatrixAssess(long Id, long tenentId, long userId)
        {
            AuthorityMatrixAssessment obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    AuthorityMatrixRepository myRepository = new AuthorityMatrixRepository(unitOfWork);
                    obj = myRepository.GetAuthorityMatrixAssess( Id,  tenentId,  userId);
                   // unitOfWork.Commit();
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
    }
}
