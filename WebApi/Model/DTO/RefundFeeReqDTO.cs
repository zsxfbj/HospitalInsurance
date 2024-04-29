using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HospitalInsurance.WebApi.Model.DTO
{
    /// <summary>
    /// 退费请求内容
    /// </summary>
    public class RefundFeeReqDTO
    {
        /// <summary>
        /// 社保卡卡号
        /// </summary>
        [Required(ErrorMessage = "社保卡卡号必须填写")]
        [MaxLength(12, ErrorMessage = "社保卡卡号为12位数字")]
        [RegularExpression(pattern: "^[1-9]\\d{11}$", ErrorMessage = "社保卡卡号为12位数字")]
        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }


        /// <summary>
        /// 原交易流水号
        /// </summary>
        [Required(ErrorMessage = "原交易流水号必须填写")]
        [MaxLength(20, ErrorMessage = "原交易流水号最多20个字符")]
        [JsonProperty("tradeNumber")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        [JsonProperty("operator")]
        [MaxLength(20, ErrorMessage = "收费员最多20个字")]
        public string Operator { get; set; }

    }
}