using System;
using System.Globalization;

namespace HospitalInsurance.Utility
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 默认时间格式
        /// </summary>
        public const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 年月格式
        /// </summary>
        public const string YYYYMM = "yyyyMM";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public static DateTime Parse(string srcString, string formatString = DefaultDateTimeFormat)
        {
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo
            {
                FullDateTimePattern = formatString
            };
            return Convert.ToDateTime(srcString, dtFormat);
        }
    }
}