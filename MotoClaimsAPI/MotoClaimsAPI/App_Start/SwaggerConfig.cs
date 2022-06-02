using System.Web.Http;
using WebActivatorEx;
using MotoClaimsAPI;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using MotoClaimsAPI.App_Start;
using MotoClaimsAPI.SwaggerFile;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MotoClaimsAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {

            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c =>
              {
                  
                  c.SingleApiVersion( "v1", "Claimoto APIs");
                  c.IncludeXmlComments(string.Format(@"{0}\bin\MotoClaimsAPI.xml",
                                       System.AppDomain.CurrentDomain.BaseDirectory));
                  c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
                  c.OperationFilter<FileOperationFilter>();

              })
              .EnableSwaggerUi();
        }
    }
}
