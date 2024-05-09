using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HospitalInsurance.WebApi.Filters;

namespace HospitalInsurance.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            ////支持cookie跨域问题
            //EnableCrossSiteRequests(config);
        

            // Web API 配置和服务
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new ExceptionHandlingAttribute());

           
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
