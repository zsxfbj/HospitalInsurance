using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// 性别：1男；2女；9未说明性别
        /// </summary>
        [Required(ErrorMessage = "性别必须填写")]
        [JsonProperty("sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期，格式为yyyyMMdd
        /// </summary>
        [Required(ErrorMessage = "出生日期必须填写")]
        //[MaxLength(8, ErrorMessage = "出生日期格式为yyyyMMdd")]
        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        /// 险种类型：3城镇职工；31离休统筹；32 医疗照顾人员；33 超转人员；91 学生儿童；92 老年人；93 劳动年龄内居民
        /// </summary>
        [Required(ErrorMessage = "险种类型必须填写")]
        //[MaxLength(4, ErrorMessage = "险种类型最多4个字符")]
        [JsonProperty("fundType")]
        public string FundType { get; set; }

        /// <summary>
        /// 是否在院：0-未在；1-在
        /// </summary>
        [JsonProperty("inHospital")]
        public int? InHospital { get; set; }
    }
}