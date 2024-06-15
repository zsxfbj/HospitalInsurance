using Newtonsoft.Json;

namespace HospitalInsurance.Model.VO.Web
{
    /// <summary>
    /// 接口处理状态及错误消息对象
    /// </summary>
    public class StateVO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public string Success { get; set; }

        /// <summary>
        /// 警告信息
        /// </summary>
        [JsonProperty("warning", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessageVO Warning { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessageVO Error { get; set; }

    }
}
