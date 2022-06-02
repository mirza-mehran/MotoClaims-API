using AutoMapper;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.Product;
using MotoClaims.Entities.Provider;
using MotoClaims.Entities.User;
using MotoClaimsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoClaimsAPI.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProfile, ModelUserProfile>();
            CreateMap<ModelUserProfile, UserProfile>();
            CreateMap<Product, ProductAdd>();

            CreateMap<PolicyImport, Policy>();
            CreateMap<ProviderImport, Providers>();

            CreateMap<Provider_Services_ContractImport, Provider_Services_Contract>();
            //Get Api first parameter get list and map into second param 
            CreateMap<PolicyModel, ModelPolicyModel>();

        }
    }
}