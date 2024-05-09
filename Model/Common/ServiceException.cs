using System;
using HospitalInsurance.Enums;

namespace HospitalInsurance.Model.Common
{
    /// <summary>
    /// 服务异常类
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public ResultCodeEnum ResultCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}