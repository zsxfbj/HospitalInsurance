using System.ComponentModel;
using System.Runtime.Serialization;

namespace HospitalInsurance.Enums
{
    /// <summary>
    /// 响应请求结果码
    /// </summary>
    public enum ResultCodeEnum
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        [EnumMember(Value = "200")]
        Success = 200,

        /// <summary>
        /// 请求参数错误
        /// </summary>
        [Description("请求参数错误")]
        [EnumMember(Value = "400")]
        RequestParamterError = 400,

        /// <summary>
        /// 禁止匿名用户访问
        /// </summary>
        [Description("禁止匿名用户访问")]
        [EnumMember(Value = "401")]
        NotAllowAnonymous = 401,

        /// <summary>
        /// 禁止访问
        /// </summary>
        [Description("禁止访问")]
        [EnumMember(Value = "403")]
        NotAllowAccess = 403,

        /// <summary>
        /// 记录不存在
        /// </summary>
        [Description("记录不存在")]
        [EnumMember(Value = "404")]
        NotFound = 404,

        /// <summary>
        /// 记录不存在
        /// </summary>
        [Description("记录已被逻辑删除")]
        [EnumMember(Value = "405")]
        IsDeleted = 405,

        /// <summary>
        /// 重复的请求
        /// </summary>
        [Description("重复的请求")]
        [EnumMember(Value = "406")]
        RepeatAction = 406,

        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("记录不存在")]
        [EnumMember(Value = "500")]
        SystemError = 500,

        /// <summary>
        /// 数据库访问异常
        /// </summary>
        [Description("数据库访问异常")]
        [EnumMember(Value = "501")]
        DatabaseAccessError = 501,

        /// <summary>
        /// 存在相同的键值
        /// </summary>
        [Description("存在相同的键值")]
        [EnumMember(Value = "502")]
        KeyIsExist = 502,

        /// <summary>
        /// 接口访问错误
        /// </summary>
        [Description("存在相同的键值")]
        [EnumMember(Value = "505")]
        InterfaceError = 505,

    }
}