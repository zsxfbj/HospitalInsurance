using System.ComponentModel.DataAnnotations;
using HospitalInsurance.Utility.Converter;
using Newtonsoft.Json;

namespace HospitalInsurance.Model.DTO
{
    /// <summary>
    /// 明细信息
    /// </summary>
    public class FeeItemDTO
    {
        /// <summary>
        /// 项目序号，为顺序号
        /// </summary>
        [Required(ErrorMessage = "项目序号必须填写")]
        [Range(1, 999999999, ErrorMessage = "项目序号为1~999999999之间的数字")]
        [JsonProperty("itemNumber")]
        public int ItemNumber { get; set; }

        /// <summary>
        /// 处方序号
        /// </summary>
        [Required(ErrorMessage = "处方序号必须填写")]
        //[MaxLength(20, ErrorMessage = "处方序号最多20个字符")]
        [JsonProperty("recipeNumber")]
        public string RecipeNumber { get; set; }

        /// <summary>
        /// HIS项目代码：药品、诊疗项目或服务设施编码
        /// </summary>
        [Required(ErrorMessage = "HIS项目代码必须填写")]
        //[MaxLength(40, ErrorMessage = "HIS项目代码最多40个字符")]
        [JsonProperty("hisCode")]
        public string HisCode { get; set; }

        /// <summary>
        /// HIS项目名称，本医院项目名称
        /// </summary>
        [Required(ErrorMessage = "HIS项目名称必须填写")]
        //[MaxLength(100, ErrorMessage = "HIS项目名称最多40个字符")]
        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        /// <summary>
        /// 项目类型：0-药品；1-诊疗项目和服务设施
        /// </summary>
        [Required(ErrorMessage = "项目类型必须填写")]
        [JsonProperty("itemType")]
        public int ItemType { get; set; }

        /// <summary>
        /// 单价，最多4位小数
        /// </summary>
        [Required(ErrorMessage = "单价必须填写")]
        [Range(-9999999999.9999, 9999999999.9999, ErrorMessage = "单价范围必须在-9999999999.9999至9999999999.9999之间")]
        [JsonProperty("unitPrice")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 数量，最多两位小数，适用于中药饮片需要精确到小数的情况
        /// </summary>
        [Required(ErrorMessage = "数量必须填写")]
        [JsonConverter(typeof(DecimalConverter))]
        [Range(-9999999999.99, 9999999999.99, ErrorMessage = "数量范围必须在-9999999999.99至9999999999.99之间")]
        [JsonProperty("count")]
        public decimal Count { get; set; }

        /// <summary>
        /// 项目总金额，该项目总金额，最多4位小数
        /// </summary>
        [Required(ErrorMessage = "项目总金额必须填写")]
        [JsonConverter(typeof(DecimalConverter))]
        [Range(-9999999999.9999, 9999999999.9999, ErrorMessage = "项目总金额范围必须在-9999999999.9999至9999999999.9999之间")]
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// 剂型，参见字典，取值须在字典范围内，且严禁使用汉字。
        /// </summary>
       // [MaxLength(6, ErrorMessage = "剂型最多6个字符")]
        [JsonProperty("dose")]
        public string Dose { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        //[MaxLength(40, ErrorMessage = "规格最多40个字符")]
        [JsonProperty("specification")]
        public string Specification { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        //[MaxLength(20, ErrorMessage = "单位最多6个字符")]
        [JsonProperty("unit")]
        public string Unit { get; set; }

        /// <summary>
        /// 用法（用药频次）,参见字典 BKC229。药品非空，诊疗服务设施可空。取值须在字典范围内，且严禁使用汉字
        /// </summary>
        //[MaxLength(3, ErrorMessage = "用法最多3个字符")]
        [JsonProperty("howToUse")]
        public string HowToUse { get; set; }

        /// <summary>
        /// 单次用量，药品非空，诊疗服务设施可空（数值型，含小数点最大20位）
        /// </summary>
        //[MaxLength(20, ErrorMessage = "单次用量最多20个字符")]
        [JsonProperty("dosage")]
        public string Dosage { get; set; }

        /// <summary>
        /// 包装单位，门诊零售包装单位，药品非空，诊疗服务设施可空
        /// </summary>
        //[MaxLength(10, ErrorMessage = "包装单位最多10个字符")]
        [JsonProperty("packaging")]
        public string Packaging { get; set; }

        /// <summary>
        /// 最小包装，最小（住院）零售包装单位，药品非空，诊疗服务设施可空
        /// </summary>
        //[MaxLength(10, ErrorMessage = "包装单位最多10个字符")]
        [JsonProperty("minPackage")]
        public string MinPackage { get; set; }

        /// <summary>
        /// 换算率，包装单位与最小包装单位之间的换算率，药品非空，诊疗服务设施可空； （数值型，含小数点最大10位）
        /// </summary>
        //[MaxLength(10, ErrorMessage = "换算率最多10个字符")]
        [JsonProperty("conversion")]
        public string Conversion { get; set; }

        /// <summary>
        /// 用药天数
        /// </summary>
        [JsonProperty("days")]
        [Range(1, 99999999, ErrorMessage = "用药天数范围1~99999999")]
        public int Days { get; set; }

        /// <summary>
        /// 生育费用标识：0-普通费用；1-生育类费用；对生育类门诊费用没有特殊处理的医院，可以不生成该节点；如使用并生成该节点，则不能为空。
        /// </summary>
        //[MaxLength(1, ErrorMessage = "生育费用标识仅为1位字符")]
        [JsonProperty("babyFlag")]
        public string BabyFlag { get; set; }

        /// <summary>
        /// 药品批准文号，当项目类别为0-药品时：传入本条药品费用对应的药品包装上的批准文号（其中，中药饮片及颗粒无需传入）；当项目类别为1诊疗项目和服务设施时：无需传入。
        /// </summary>
        [JsonProperty("drugApprovalNumber")]
        // [MaxLength(20, ErrorMessage = "药品批准文号最多20个字符")]
        public string DrugApprovalNumber { get; set; }


    }
}