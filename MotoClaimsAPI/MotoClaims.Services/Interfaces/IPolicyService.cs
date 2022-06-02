using MotoClaims.Entities;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IPolicyService
    {
        IEnumerable<ProductByID> GetProductName(long id, long tenentId, long userId);

        ProductByID GetProductDetails(long id, long tenentId, long userId);

        IEnumerable<VehicleColour> GetColour(long tenentId,long userId);

        IEnumerable<PolicyMake> GetPolicyMake(long tenentId, long userId);

        IEnumerable<PolicyModel> GetPolicyModel(long Id, long tenentId, long userId);

        long PostPolicy(Policy model, long tenentId, long userId);

        //string InsertPolicyImage(long Id,string ImageUrl, long tenentId , int ImageIndex);
        string InsertPolicyImage(ImagesUpload model,long tenentId,long userId);
        string UpdatePolicyImage(ImagesUpload model, long tenentId, long userId);
        IEnumerable<Policy> GetPolicies( long tenentId, long userId);

        Policy GetPolicyByID(long id, long tenentId, long userId);

        string DeletePolicy(long Id, long tenentId, long userId, out bool bit);

        long UpdatePolicy(Policy model, long tenentId, long userId);

        IEnumerable<ModelPolicy> GetAllPoliciesByPageIndex(out long TotalRecord, long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName, long tenentId, long userId);

    }
}
