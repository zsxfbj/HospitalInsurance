using System.Net.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using System.Net;
using HospitalInsurance.Model.Common;
using System.Text;

namespace HospitalInsurance.WebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 统一对调用异常信息进行处理，返回自定义的异常信息
        /// </summary>
        /// <param name="context">HTTP上下文对象</param>
        public override void OnException(HttpActionExecutedContext context)
        { 
            ApiResult<string> apiResult = new ApiResult<string>();

          
            if (context.Exception.GetType() == typeof(ServiceException))
            {
                ServiceException se = (ServiceException)context.Exception;
                apiResult.Code = se.ResultCode;
                apiResult.ErrorMessage = se.ErrorMessage;
            }
            else if (context.Exception.GetType() == typeof(System.IO.FileNotFoundException))
            {
                apiResult.Code = Enums.ResultCodeEnum.NotFound;
                apiResult.ErrorMessage = "文件未找到";
            }
            else
            {
                apiResult.Code = Enums.ResultCodeEnum.SystemError;
                apiResult.ErrorMessage = context.Exception.Message;
            }
            
            context.Response = new HttpResponseMessage(HttpStatusCode.OK);
            context.Response.Content = new StringContent(JsonConvert.SerializeObject(apiResult), Encoding.UTF8, "application/json"); 
            context.Exception = null; //Handled! 
        }
    }
}