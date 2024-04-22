using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HospitalInsurance.WebApi.Model.DTO
{
    /// <summary>
    /// 处方信息
    /// </summary>
    public class RecipeDTO
    {

        /// <summary>
        /// 诊断序号。严禁使用汉字。
        /// </summary>
        [Required(ErrorMessage = "诊断序号必须填写")]
        [MaxLength(9, ErrorMessage = "诊断序号最多9个字符")]
        [JsonProperty("diagnoseNumber")]
        public string DiagnoseNumber { get; set; }

        /// <summary>
        /// 处方序号
        /// </summary>
        [Required(ErrorMessage = "处方序号必须填写")]
        [MaxLength(20, ErrorMessage = "处方序号最多20个字符")]
        [JsonProperty("recipeNumber")]
        public string RecipeNumber { get; set; }


        /// <summary>
        /// 处方时间，格式为yyyyMMddHHmmss
        /// </summary>
        [Required(ErrorMessage = "处方时间必须填写")]
        [MaxLength(14, ErrorMessage = "处方时间为14个字符")]
        [JsonProperty("recipeDate")]
        public string RecipeDate { get; set; }

        /// <summary>
        /// 诊断编码，采用ICD国际诊断标准，或采用ICPC诊断标准(使用门诊医生工作站者为“必填项”) ，严禁使用汉字，字母严禁使用全角字符。
        /// </summary>
        [MaxLength(20, ErrorMessage = "诊断编码最多20个字符")]
        [JsonProperty("diagnoseCode")]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称，与编码对应的诊断名称(使用门诊医生工作站者为“必填项”)，长度最多41个汉字。
        /// </summary>
        [MaxLength(82, ErrorMessage = "诊断名称最多82个字符")]
        [JsonProperty("diagnoseName")]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 病历信息，HIS能采集的所有本次就诊的门诊病历信息。最多400个汉字
        /// </summary>
        [MaxLength(800, ErrorMessage = "病历信息最多400个汉字")]
        [JsonProperty("medicalRecord")]
        public string MedicalRecord { get; set; }

        /// <summary>
        /// 就诊科别代码
        /// </summary>
        [MaxLength(4, ErrorMessage = "就诊科别代码最多4个字符")]
        [JsonProperty("sectionCode")]
        public string SectionCode { get; set; }

        /// <summary>
        /// 就诊科别名称
        /// </summary>
        [MaxLength(20, ErrorMessage = "就诊科别名称最多20个字")]
        [JsonProperty("sectionName")]
        public string SectionName { get; set; }

        /// <summary>
        /// 本院科别名称
        /// </summary>
        [MaxLength(20, ErrorMessage = "本院科别名称最多20个字")]
        [JsonProperty("hisSectionName")]
        public string HisSectionName { get; set; }

        /// <summary>
        /// 医师编码，北京市卫生局标准15位，部队16位，严禁使用汉字。
        /// </summary>
        [MaxLength(20, ErrorMessage = "本院科别名称最多20个字")]
        [JsonProperty("doctorId")]
        public string DoctorId { get; set; }

        /// <summary>
        /// 医师名称
        /// </summary>
        [MaxLength(20, ErrorMessage = "医师名称最多20个字")]
        [JsonProperty("doctorName")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 处方类型：1-医保内处方；2-医保外处方
        /// </summary>
        [Required(ErrorMessage = "处方类型必须填写")]
        [Range(minimum:1, maximum:2, ErrorMessage = "处方类型：1-医保内处方；2-医保外处方")]
        [JsonProperty("recipeType")]
        public int RecipeType { get; set; }

        /// <summary>
        /// 代开药标识：0：普通处方；1:代开药处方。不需要区分代开药的医院。
        /// </summary>
        [Required(ErrorMessage = "代开药标识必须填写")]
        [JsonProperty("helpMedicineFlag")]
        [MaxLength(1, ErrorMessage = "代开药标识只有1个字符")]
        public string HelpMedicineFlag { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(100, ErrorMessage = "备注最多100个字")]
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 挂号交易流水号，本次门诊对应的挂号交易流水号
        /// </summary>
        [MaxLength(22, ErrorMessage = "挂号交易流水号最多22个字符")]
        [JsonProperty("registerTradeNumber")]
        public string RegisterTradeNumber { get; set; }

        /// <summary>
        /// 单据类型，本次互联网复诊费结算的单据类型；1-互联网复诊费
        /// </summary>
        [JsonProperty("billsType")]
        [MaxLength(1, ErrorMessage = "单据类型只有1个字符")]
        public string BillsType { get; set; }


    }
}