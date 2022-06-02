using MotoClaims.DataAccess.Repositories;
using MotoClaims.Services.Interfaces;
using MotoClaims.Services.Services;
using MotoClaimsAPI.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;
using Microsoft.Owin.Security.OAuth;
using MotoClaims.Entities;

namespace MotoClaimsAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            var container = new UnityContainer();
            


            ////// Service

            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IUserManagementService, UserManagementService>();
            container.RegisterType<IPolicyService, PolicyService>();
            container.RegisterType<IProviderService, ProviderService>();
            container.RegisterType<IGlobalEmailConfigService, GlobalEmailConfigService>();
            container.RegisterType<ITenantService, TenantService>();
            container.RegisterType<IProductTypeService, ProductTypeService>();
            container.RegisterType<ICommonService, CommonService>();
            container.RegisterType<IAuthorityMatrixService, AuthorityMatrixService>();
            container.RegisterType<IProviderServicesContractService, ProviderServicesContractService>();
            container.RegisterType<IClaimsService, ClaimsService>();
            container.RegisterType<IForgotPasswordService, ForgotPasswordService>();
            container.RegisterType<IMyTaskService, MyTaskService>();
            container.RegisterType<IScheduledCallsAndChatService, ScheduledCallsAndChatService>();
            container.RegisterType<IAgenciesService, AgenciesService>();
            container.RegisterType<ISurveyorService, SurveyorService>();
            container.RegisterType<ICarReplacementService, CarReplacementService>();

            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

          
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
