using System;
using System.IO;
using HospitalInsurance.Utility;

namespace HospitalInsurance.WebApi.BLL
{
    /// <summary>
    /// LiteDb封装业务类
    /// </summary>
    public class BLiteDb : Singleton<BLiteDb>
    {
        /// <summary>
        /// 获取数据库文件路径
        /// </summary>
        /// <returns></returns>
        public string GetLogDbPath()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "/db/" + DateTime.Now.Year + "/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return filePath + DateTime.Now.ToString("yyyyMM") + "log.db";
        } 
    }
}