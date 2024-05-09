using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Routing;
using Swagger.Net;

//[assembly: WebActivator.PreApplicationStartMethod(typeof(HospitalInsurance.WebApi.App_Start.SwaggerNet), "PreStart")]
//[assembly: WebActivator.PostApplicationStartMethod(typeof(HospitalInsurance.WebApi.App_Start.SwaggerNet), "PostStart")]
namespace HospitalInsurance.WebApi.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerNet 
    {
        /// <summary>
        /// 
        /// </summary>
        public static void PreStart() 
        {
            RouteTable.Routes.MapHttpRoute(
                name: "SwaggerApi",
                routeTemplate: "api/docs/{controller}",
                defaults: new { swagger = true }
            );            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void PostStart() 
        {
            var config = GlobalConfiguration.Configuration;

            config.Filters.Add(new SwaggerActionFilter());
            
            try
            {
                config.Services.Replace(typeof(IDocumentationProvider),
                    new XmlCommentDocumentationProvider(String.Format(@"{0}\\bin\\HospitalInsurance.WebApi.XML", AppDomain.CurrentDomain.BaseDirectory)));
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Please enable \"XML documentation file\" in project properties with default (bin\\HospitalInsurance.WebApi.XML) value or edit value in App_Start\\SwaggerNet.cs");
            }
        }
    }
}