﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace HospitalInsurance.Model.VO.Web
{
    /// <summary>
    /// 交易信息
    /// </summary>
    public class TradeVO
    {
        /// <summary>
        /// 交易流水
        /// </summary>
        [JsonProperty("tradeno")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 收费单据号
        /// </summary>
        [JsonProperty("feeno")]
        public string FeeNumber { get; set; }

        /// <summary>
        /// 交易日期时间，格式为：yyyyMMddHHmmss
        /// </summary>
        [JsonProperty("tradedate")]
        public string TradeDate { get; set; }

    }
}