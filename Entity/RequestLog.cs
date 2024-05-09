using System;
using LiteDB;

namespace Hospitalinsurance.Entity
{
    /// <summary>
    /// 请求日志
    /// </summary>
    public class RequestLog
    {
        /// <summary>
        /// 内部Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 客户端Ip
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求内容
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public string ResponseData { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RequestLog() {
            Id = ObjectId.Empty;
            RequestTime = DateTime.Now;
            ClientIp = string.Empty;
            UserAgent = string.Empty;
            RequestUrl = string.Empty;
            RequestData = string.Empty;
            ResponseData = string.Empty;
        }
            
    }
}
