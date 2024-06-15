using Newtonsoft.Json;

namespace HospitalInsurance.Model.VO.Api
{
    /// <summary>
    /// 费用分解结果
    /// </summary>
    public class TradeDetailVO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public string Success { get; set; }

        /// <summary>
        /// 医保交易号
        /// </summary>
        [JsonProperty("tradeNumber")]
        public string TradeNumber {  get; set; }

        /// <summary>
        /// 总费用
        /// </summary>
        [JsonProperty("totalAmount")]
        public string TotalAmount { get; set; }

        /// <summary>
        /// 医保内金额
        /// </summary>
        [JsonProperty("inInsuranceAmount")]
        public string InInsuranceAmount { get; set; }

        /// <summary>
        /// 交易具体内容（Base64编码）
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }


    }
}
