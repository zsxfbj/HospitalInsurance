using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HospitalInsurance.Model.DTO
{
    /// <summary>
    /// 费用分解请求
    /// </summary>
    public class DivideReqDTO
    {
        /// <summary>
        /// 社保卡卡号
        /// </summary>
        [Required(ErrorMessage = "社保卡卡号必须填写")]
        //[MaxLength(12, ErrorMessage = "社保卡卡号为12位数字")]
        [RegularExpression(pattern: "^[1-9]\\d{11}$", ErrorMessage = "社保卡卡号为12位数字")]
        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// 医疗类别，此处目前固定为17（互联网复诊费结算）
        /// </summary>
        [Required(ErrorMessage = "医疗类别必须填写")]
        //[MaxLength(2, ErrorMessage = "医疗类别最多2个字符")]
        [JsonProperty("cureType")]
        public string CureType { get; set; }

        /// <summary>
        /// 就诊方式，目前都是0普通，取值须在字典范围内，且严禁使用汉字
        /// </summary>
        [Required(ErrorMessage = "就诊方式必须填写")]
        //[MaxLength(2, ErrorMessage = "就诊方式2个字符")]
        [JsonProperty("illType")]
        public string IllType { get; set; }

        /// <summary>
        /// 收费单据号
        /// </summary>
        [JsonProperty("feeNumber")]
        //[MaxLength(20, ErrorMessage = "收费单据号最多20个字符")]
        public string FeeNumber { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        [JsonProperty("operator")]
        //[MaxLength(20, ErrorMessage = "收费员最多20个字")]
        public string Operator { get; set; }


        /// <summary>
        /// 处方时间，格式为yyyyMMddHHmmss
        /// </summary>      
        [Required(ErrorMessage = "处方时间必须填写")]
        [MaxLength(14, ErrorMessage = "处方时间为14个字符")]
        [JsonProperty("recipeDate")]
        public string RecipeDate { get; set; }

        /// <summary>
        /// 就诊科别代码
        /// </summary>       
        [JsonProperty("sectionCode")]
        public string SectionCode { get; set; }

        /// <summary>
        /// 就诊科别名称
        /// </summary>       
        [JsonProperty("sectionName")]
        public string SectionName { get; set; }

        /// <summary>
        /// 医师编码，北京市卫生局标准15位，部队16位，严禁使用汉字。
        /// </summary>      
        [JsonProperty("doctorId")]
        public string DoctorId { get; set; }

        /// <summary>
        /// 医师名称
        /// </summary>        
        [JsonProperty("doctorName")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 处方信息列表
        /// </summary>
        [JsonProperty("recipes")]
        public List<RecipeDTO> Recipes { get; set; }

        /// <summary>
        /// 明细信息列表
        /// </summary>
        [JsonProperty("feeItems")]
        public List<FeeItemDTO> FeeItems { get; set; }

    }
}