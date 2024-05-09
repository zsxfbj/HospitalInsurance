using System.Web.Http;

namespace HospitalInsurance.WebApi
{
    /// <summary>
    /// 应用入口参数
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);
            
        }
    }
}
