using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HospitalInsurance.Model.DTO
{
    /// <summary>
    /// 退费请求内容
    /// </summary>
    public class RefundmentReqDTO
    {
        /// <summary>
        /// 人员信息
        /// </summary>
        [Required(ErrorMessage = "人员信息必须填写")]
        [JsonProperty("person")]
        public PersonInfoReqDTO Person { get; set; }

        /// <summary>
        /// 原交易流水号
        /// </summary>
        [Required(ErrorMessage = "原交易流水号必须填写")]
        //[MaxLength(20, ErrorMessage = "原交易流水号最多20个字符")]
        [JsonProperty("tradeNumber")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        [JsonProperty("operator")]
        //[MaxLength(20, ErrorMessage = "收费员最多20个字")]
        public string Operator { get; set; }

    }
}