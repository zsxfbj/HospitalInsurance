using System;
using System.Web.Http;
using HospitalInsurance.BLL;
using HospitalInsurance.Model.Common;
using HospitalInsurance.Model.DTO;
using HospitalInsurance.Model.VO;

namespace HospitalInsurance.WebApi.Controllers
{
    /// <summary>
    /// 移动互联网医院接口
    /// </summary>
    [RoutePrefix("api/net-yb")]
    public class HISApiController : ApiController
    {

        /// <summary>
        /// 获取医保人员信息
        /// </summary>
        /// <param name="req">获取医保人员信息请求</param>
        /// <returns>PersonVO就医人员医保信息</returns>
        [HttpPost]
        [Route("get-person")]
        public ApiResult<PersonVO> GetPersonInfo([FromBody] GetPersonInfoReqDTO req)
        {
            return new ApiResult<PersonVO>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().GetPersonInfo(req),
                RequestId = Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// 获取医保人员信息
        /// </summary>
        /// <param name="req">获取医保人员信息请求</param>
        /// <returns>PersonVO就医人员医保信息</returns>
        [HttpPost]
        [Route("divide")]
        public ApiResult<TradeVO> Divide([FromBody] DivideReqDTO req)
        {
            return new ApiResult<TradeVO>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().DivideFee(req),
                RequestId = Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// 门诊交易确认
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns>交易信息</returns>
        [HttpGet]
        [Route("trade/{cardNumber}")]
        public ApiResult<TradeResultVO> TradeConfirm(string cardNumber)
        {
            return new ApiResult<TradeResultVO>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().TradeConfirm(cardNumber),
                RequestId = Guid.NewGuid().ToString()
            };
        }


        /// <summary>
        /// 退费请求
        /// </summary>
        /// <param name="req">退费请求数据</param>
        /// <returns>退费交易结果</returns>
        [HttpPost]
        [Route("refundment")]
        public ApiResult<RefundTradeVO> Refundment([FromBody] RefundFeeReqDTO req)
        {
            return new ApiResult<RefundTradeVO>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().GetRefundTrade(req),
                RequestId = Guid.NewGuid().ToString()
            };
        }


        /// <summary>
        /// 交易查询及回退
        /// </summary>
        /// <param name="tradeNumber">交易流水号</param>
        /// <returns>退费交易结果</returns>
        [HttpPost]
        [Route("trade-state/{tradeNumber}")]
        public ApiResult<TradeStateVO> GetTradeState(string tradeNumber)
        {
            return new ApiResult<TradeStateVO>
            {
                Code = Enums.ResultCodeEnum.Success,
                Data = BHISInterface.GetInstance().GetTradeState(tradeNumber),
                RequestId = Guid.NewGuid().ToString()
            };
        }

    }
}