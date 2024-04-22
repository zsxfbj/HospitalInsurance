using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalInsurance.WebApi.BLL;

namespace HospitalInsurance.WebApi.Controllers
{
    /// <summary>
    /// 医保Api模拟接口
    /// </summary>
    [RoutePrefix("api/ybapi")]
    public class YbApiController : ApiController
    {
        /// <summary>
        /// 获取参保人员信息
        /// </summary>
        /// <param name="reqXml"></param>
        /// <returns></returns>
        [HttpPost, Route("GetPersonInfo_Web")]
        public HttpResponseMessage GetPersonInfo([FromBody] string reqXml)
        {
            return new HttpResponseMessage
              {
                  Content = new StringContent(BTestAction.GetInstance().GetPersonInfoWeb(reqXml)),
                  StatusCode = HttpStatusCode.OK
           };
           
        }

    }
}