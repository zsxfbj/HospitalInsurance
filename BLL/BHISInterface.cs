﻿using HospitalInsurance.Utility;
using HospitalInsurance.Model.Common;
using HospitalInsurance.Model.DTO;
using Newtonsoft.Json;
using HospitalInsurance.Enums;
using Hospitalinsurance.Entity;
using System.Threading.Tasks;
using HospitalInsurance.Model.VO.Api;
using HospitalInsurance.Model.VO.Web;
using System;
using System.Text;

namespace HospitalInsurance.BLL
{
    /// <summary>
    /// 医保接口
    /// </summary>
    public class BHISInterface : Singleton<BHISInterface>
    {    
        #region public async Task<string> DivideFee(DivideReqDTO req)
        /// <summary>
        /// 费用分解
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<string> DivideFeeAsync(DivideReqDTO req)
        {
            if (BActionCheck.GetInstance().IsRepeat("divide-fee-" + req.Person.CardNumber))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交费用分解数据" };
            }
            SubmitLog submitLog = new SubmitLog
            {              
                RequestId = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Flag = 0,
                ResultContent =  "",
                SubmitType = 1,
                SubmitContent = JsonConvert.SerializeObject(req)

            };
            await BSubmitLog.GetInstance().SaveAsync(submitLog);
            return submitLog.RequestId;
        }
        #endregion public TradeVO DivideFee(DivideReqDTO req)

        #region public Task<string> GetRefundTrade(RefundFeeReqDTO req)
        /// <summary>
        /// 退费交易请求
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<string> GetRefundTradeAsync(RefundmentReqDTO req)
        {
            if (BActionCheck.GetInstance().IsRepeat("refundment-" + req.Person.CardNumber))
            {
                throw new ServiceException { ResultCode = ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交退费分解请求" };
            }

            SubmitLog submitLog = new SubmitLog
            {             
                RequestId = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Flag = 0,
                ResultContent = "",
                SubmitType = 2,
                SubmitContent = JsonConvert.SerializeObject(req)
            };
            await BSubmitLog.GetInstance().SaveAsync(submitLog);
            return submitLog.RequestId;
        }
        #endregion public Task<string> GetRefundTrade(RefundFeeReqDTO req)

        #region public TradeStateVO GetTradeState(string tradeNumber)
        /// <summary>
        /// 交易状态查询
        /// </summary>
        /// <param name="tradeNumber"></param>
        /// <returns></returns>     
        public async Task<string> GetTradeStateAsync(string tradeNumber)
        {
            if (string.IsNullOrEmpty(tradeNumber) || tradeNumber.Trim().Length > 22)
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RequestParamterError, ErrorMessage = "交易流水号格式错误" };
            }
            if (BActionCheck.GetInstance().IsRepeat("comfirm-trade-state-" + tradeNumber.Trim()))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交交易状态确认查询请求" };
            }
            SubmitLog submitLog = new SubmitLog
            {                
                RequestId = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Flag = 0,
                ResultContent = "",
                SubmitType = 3,
                SubmitContent = tradeNumber
            };
            await BSubmitLog.GetInstance().SaveAsync(submitLog);
            return submitLog.RequestId;
        }
        #endregion public TradeStateVO GetTradeState(string tradeNumber)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<TradeDetailVO> GetTradeDetailAsync(string requestId)
        {  
            SubmitLog submitLog = await BSubmitLog.GetInstance().GetSubmitLogAsync(requestId);
            if(submitLog != null && !string.IsNullOrEmpty(submitLog.ResultContent))
            {
                return JsonConvert.DeserializeObject<TradeDetailVO>(submitLog.ResultContent);                               
            }
            return null;
        }

    }
}