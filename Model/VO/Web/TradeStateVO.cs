﻿using Newtonsoft.Json;

namespace HospitalInsurance.Model.VO.Web
{
    /// <summary>
    /// 交易状态
    /// </summary>
    public class TradeStateVO
    {   
        /// <summary>
        /// 交易状态: ok-交易成功，cancel-交易撤销
        /// </summary>
        [JsonProperty("state")]    
        public string State {  get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        [JsonProperty("stateName")]
        public string StateName { get; set; }
    }
}