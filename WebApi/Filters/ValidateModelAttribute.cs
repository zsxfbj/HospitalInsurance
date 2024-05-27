using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using HospitalInsurance.Enums;
using HospitalInsurance.Model.Common;
using Newtonsoft.Json;

namespace HospitalInsurance.WebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var errorMsg = actionContext.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.OK, actionContext.ModelState);
                ApiResult<string> result = new ApiResult<string>
                {
                    Code = ResultCodeEnum.RequestParamterError,
                    ErrorMessage = errorMsg                    
                };
                actionContext.Response.Content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");
                return;
            }
        }
    }
}