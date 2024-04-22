using System.Web.Http;
using HospitalInsurance.WebApi.BLL;
using HospitalInsurance.WebApi.Model;
using HospitalInsurance.WebApi.Model.Common;
using HospitalInsurance.WebApi.Model.DTO;

namespace HospitalInsurance.WebApi.Controllers
{
    /// <summary>
    /// 请求日志查询接口
    /// </summary>
    [RoutePrefix("api/request-log")]
    public class RequestLogController : ApiController
    {
        /// <summary>
        /// 请求日志分页查询接口
        /// </summary>
        /// <param name="req">查询请求</param>
        /// <returns>Page of RequestLogVO List</returns>
        [HttpPost]
        [Route("page")]
        public ApiResult<PageResult<RequestLogVO>> GetPagedRequestLogs([FromBody] LogPageReqDTO req)
        {
            return new ApiResult<PageResult<RequestLogVO>> { Code = Enums.ResultCodeEnum.Success, Data = BRequestLog.GetInstance().GetPagedRequestLogs(req) };
        }
          
    }
}