using System.Web;

namespace HospitalInsurance.WebApi.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpContextUtil
    {
        private readonly static HttpContext _context = HttpContext.Current;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static HttpContext GetHttpContext()
        {             
            return _context;
        }
         
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            return _context.Request.UserHostAddress;
        }

        /// <summary>
        /// 获取客户端类型
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent()
        {
            return _context.Request.UserAgent;
        }

        /// <summary>
        /// 获取请求的相对url全部信息
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrl() {

            return _context.Request.Url.PathAndQuery;
        }

       
    }
}