using Newtonsoft.Json;

namespace HospitalInsurance.Model.VO
{
    /// <summary>
    /// 费用明细列表
    /// </summary>
    public class FeeItemsVO
    {
        /// <summary>
        /// 费用项目明细
        /// </summary>
        [JsonProperty("feeitem")]
        public FeeItemVO FeeItem { get; set; }
    }
}
