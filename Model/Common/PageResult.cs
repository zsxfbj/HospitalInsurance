using System.Collections.Generic;
using HospitalInsurance.Utility.Converter;
using Newtonsoft.Json;

namespace HospitalInsurance.Model.Common
{
    /// <summary>
    /// 分页结果
    /// </summary>
    public class PageResult<T>
    {
        /// <summary>
        /// 具体列表数据
        /// </summary>
        [JsonProperty("list")]
        public List<T> List { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        [JsonProperty("page")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        [JsonProperty("size")]
        public int PageSize { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        [JsonProperty("recordCount")]
        public long RecordCount { get; set; }

        /// <summary>
        /// 当前页面记录数
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        [JsonProperty("currentCount")]
        public long CurrentCount
        {
            get
            {
                if (List != null)
                {
                    return List.Count;
                }
                return 0;
            }
        }

    }
}