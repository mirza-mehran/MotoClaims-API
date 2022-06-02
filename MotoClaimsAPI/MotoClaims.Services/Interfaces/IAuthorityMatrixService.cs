using MotoClaims.Entities.AuthorityMatrix;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Product;
using MotoClaims.Entities.Provider;
using MotoClaims.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IAuthorityMatrixService
    {
        IEnumerable<PolicyMake> GetMake(long tenentId, long userId, string SearchText);
        IEnumerable<PolicyModel> GetModel(long Id, long tenentId, long userId, string SearchText);
        IEnumerable<ProductList> GetProducts(long tenentId, long userId, string SearchText);
        IEnumerable<UserProfiles> GetUserProfiles(long id, long roleId, long tenantId, string SearchText);
        IEnumerable<AuthorityMatrixAssess_Services> GetServices(long tenantId, string SearchText);


        string InsertAuthorityMatrix(AuthorityMatrixs obj, long userId, long tenantId, out bool bit);
        string UpdateAuthorityMatrix(AuthorityMatrixs obj, long userId, long tenantId, out bool bit);

        string InsertAuthorityMatrixAssessment(AuthorityMatrixAssessment obj, long userId, long tenantId, out bool bit);

        string UpdateAuthorityMatrixAssessment(AuthorityMatrixAssessment obj, long userId, long tenantId, out bool bit);

        string DeleteAuthorityMatrixAssess(long Id, long userId, long tenantId, out bool bit);
        string DeleteAuthorityMatrix(long Id, long userId, long tenantId, out bool bit);


        IEnumerable<AuthorityMatrixs> GetAuthoritysMatrixs(long Id, long tenentId, long userId);
        AuthorityMatrixs GetAuthorityMatrix(long Id, long tenentId, long userId);
        IEnumerable<AuthorityMatrixsAssessment> GetAuthoritysMatrixsAssess(long Id, long tenentId, long userId);
        AuthorityMatrixAssessment GetAuthorityMatrixAssess(long Id, long tenentId, long userId);
    }
}
