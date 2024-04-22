using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HospitalInsurance.WebApi.Utility.Converter;
using Newtonsoft.Json;

namespace HospitalInsurance.WebApi.Model.DTO
{
    /// <summary>
    /// 请求日志分页查询
    /// </summary>
    public class LogPageReqDTO
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        [JsonProperty("startTime")]
        [DefaultValue("2024-01-01 00:00:00")]
        [DataType(DataType.DateTime, ErrorMessage = "起始时间格式不正确")]
        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty("endTime")]
        [DefaultValue("2024-12-31 23:59:59")]
        [DataType(DataType.DateTime, ErrorMessage = "结束时间格式不正确")]
        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 访问的Ip
        /// </summary>
        [JsonProperty("clientIp")]
        [DefaultValue("127.0.0.1")]
        public string ClientIp { get; set; }

        /// <summary>
        /// 请求的路径
        /// </summary>
        [JsonProperty("requestUrl")]
        [DefaultValue("page")]
        public string RequestUrl {  get; set; }


        /// <summary>
        /// 关键词
        /// </summary>
        [JsonProperty("keyword")]
        [DefaultValue("")]
        public string Keyword { get; set; }

        /// <summary>
        /// 页码，从1开始
        /// </summary>
        [JsonProperty("page")]
        [DefaultValue("1")]
        [Range(1, int.MaxValue, ErrorMessage ="页码必须从1开始")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面数据大小，默认10，最大30
        /// </summary>
        [JsonProperty("size")]
        [DefaultValue("10")]
        [Range(1, 30, ErrorMessage = "每页数据量最少1个，最多不超过30")]
        public int PageSize { get; set; }

       
    }
}