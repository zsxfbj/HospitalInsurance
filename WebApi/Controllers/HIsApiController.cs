using System.Threading.Tasks;
using System.Web.Http;
using HospitalInsurance.BLL;
using HospitalInsurance.Model.Common;
using HospitalInsurance.Model.DTO;
using HospitalInsurance.Model.VO.Api;

namespace HospitalInsurance.WebApi.Controllers
{
    /// <summary>
    /// 移动互联网医院接口
    /// </summary>
    [Route("api/webyb")]
    public class HISApiController : ApiController
    {
        /// <summary>
        /// 费用分解请求
        /// </summary>
        /// <param name="req">费用分解请求</param>
        /// <returns>string 请求Id</returns>
        [HttpPost]
        [Route("divide")]
        public ApiResult<string> Divide([FromBody] DivideReqDTO req)
        {
            return new ApiResult<string>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().DivideFee(req)
            };
        }

        /// <summary>
        /// 退费请求
        /// </summary>
        /// <param name="req">退费请求数据</param>
        /// <returns>string 请求Id</returns>
        [HttpPost]
        [Route("refundment")]
        public ApiResult<string> Refundment([FromBody] RefundmentReqDTO req)
        {
            return new ApiResult<string>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().GetRefundTrade(req)              
            };
        }

        /// <summary>
        /// 交易状态查询
        /// </summary>
        /// <param name="tradeNumber">交易流水号</param>
        /// <returns>string 请求Id</returns>
        [HttpGet]
        [Route("trade-state/{tradeNumber}")]
        public ApiResult<string> QueryTradeState(string tradeNumber)
        {
            return new ApiResult<string>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().GetTradeState(tradeNumber)              
            };
        }

        /// <summary>
        /// 根据请求Id查询最终结果
        /// </summary>
        /// <param name="requestId">请求Id</param>
        /// <returns>string 请求Id</returns>
        [HttpGet]
        [Route("trade-detail/{requestId}")]
        public ApiResult<TradeDetailVO> GetTradeDetail(string requestId)
        {
            TradeDetailVO tradeDetail = BHISInterface.GetInstance().GetTradeDetail(requestId);
            if(tradeDetail == null)
            {
                return new ApiResult<TradeDetailVO>
                {
                    Code = Enums.ResultCodeEnum.NotFound,
                    ErrorMessage = "记录不存在"
                };
            }
            return new ApiResult<TradeDetailVO>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = tradeDetail
            };
        }
    }
}