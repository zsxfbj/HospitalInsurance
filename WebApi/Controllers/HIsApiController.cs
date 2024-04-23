using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using HospitalInsurance.WebApi.BLL;
using HospitalInsurance.WebApi.Model.Common;
using HospitalInsurance.WebApi.Model.DTO;
using HospitalInsurance.WebApi.Model.VO;

namespace HospitalInsurance.WebApi.Controllers
{
    /// <summary>
    /// 移动互联网医院接口
    /// </summary>
    [RoutePrefix("api/net-yb")]
    public class HISApiController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-person-info")]
        public ApiResult<PersonVO> GetPersonInfo([FromBody] GetPersonInfoReqDTO req)
        {
            return new ApiResult<PersonVO>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().GetPersonInfo(req),
                RequestId = Guid.NewGuid().ToString()
            };
        }
    }
}