using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HospitalInsurance.WebApi.Model.DTO
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
        [MaxLength(20, ErrorMessage = "处方序号最多20个字符")]
        [JsonProperty("recipeNumber")]
        public string RecipeNumber { get; set; }


    }
}