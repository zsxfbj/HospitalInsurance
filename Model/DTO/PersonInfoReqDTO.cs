﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HospitalInsurance.Model.DTO
{
    /// <summary>
    /// 请求获取参保人员信息
    /// </summary>
    public class PersonInfoReqDTO
    {
        /// <summary>
        /// 社保卡卡号
        /// </summary>
        [Required(ErrorMessage = "社保卡卡号必须填写")]
        //[MaxLength(12, ErrorMessage = "社保卡卡号为12位数字")]
        //[RegularExpression(pattern: "^[1-9]\\d{11}$", ErrorMessage = "社保卡卡号为12位数字")]
        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// 公民身份证号
        /// </summary>
        [Required(ErrorMessage = "公民身份证号必须填写")]
        //[MaxLength(18, ErrorMessage = "公民身份证号为18位")]
        //[RegularExpression(pattern: "^\\d{18}$|^\\d{17}(\\d|X|x)$", ErrorMessage = "公民身份证号格式错误")]
        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        /// <summary>
        /// 参保人姓名
        /// </summary>
        [Required(ErrorMessage = "参保人姓名必须填写")]
        //[MaxLength(50, ErrorMessage = "参保人姓名最多50个字")]
        [JsonProperty("personName")]
        public string PersonName { get; set; }
    }
}