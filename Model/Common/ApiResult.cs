using System;
using System.ComponentModel;
using HospitalInsurance.Enums;
using Newtonsoft.Json;

namespace HospitalInsurance.Model.Common
{
    /// <summary>
    /// 通用输出类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 返回的数据内容
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]

        public T Data { get; set; }

        /// <summary>
        /// 结果码：200正常，其余异常错误
        /// </summary>
        [JsonProperty("code")]
        [DefaultValue(ResultCodeEnum.Success)]
        public ResultCodeEnum Code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("errorMessage")]
        [DefaultValue("请求成功")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 返回的请求Id
        /// </summary>
        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiResult()
        {
            RequestId = Guid.NewGuid().ToString();
            Code = ResultCodeEnum.Success;
            ErrorMessage = "请求成功";
            Data = default;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public ApiResult(T value)
        {
            RequestId = Guid.NewGuid().ToString();
            Code = ResultCodeEnum.Success;
            ErrorMessage = "请求成功";
            Data = value;
        }

        /// <summary>
        /// 获取错误结果
        /// </summary>
        /// <param name="resultCode">错误码</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>ApiResult</returns>
        public static ApiResult<T> Error(ResultCodeEnum resultCode, string errorMessage)
        {
            ApiResult<T> result = new ApiResult<T>
            {
                RequestId = Guid.NewGuid().ToString(),
                Code = resultCode,
                ErrorMessage = errorMessage,
                Data = default
            };
            return result;
        }
    }
}