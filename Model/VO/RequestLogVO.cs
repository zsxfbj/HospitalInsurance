using System;
using Newtonsoft.Json;

namespace HospitalInsurance.Model
{
    /// <summary>
    /// 请求日志对象
    /// </summary>
    public class RequestLogVO
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [JsonProperty("id")]
        public String Id { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        [JsonProperty("requestTime")]
        [JsonConverter(typeof(Utility.Converter.DefaultDateTimeConverter))]
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 请求的Ip地址
        /// </summary>
        [JsonProperty("clientIp")]
        public string ClientIp { get; set; }

        /// <summary>
        /// 客户端信息
        /// </summary>
        [JsonProperty("userAgent")]
        public string UserAgent { get; set; }

        /// <summary>
        /// 请求URL地址
        /// </summary>
        [JsonProperty("requestUrl")]
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求的内容
        /// </summary>
        [JsonProperty("requestData")]
        public string RequestData { get; set; }

        /// <summary>
        /// 响应的内容
        /// </summary>
        [JsonProperty("responseData")]
        public string ResponseData { get; set; }

    }
}