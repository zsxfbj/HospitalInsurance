using System;
using System.Runtime.Caching;
using HospitalInsurance.Utility;

namespace HospitalInsurance.BLL
{
    /// <summary>
    /// 操作判断
    /// </summary>
    public class BActionCheck : Singleton<BActionCheck>
    {
        /// <summary>
        /// 操作是否重复提交
        /// </summary>
        /// <param name="actionKey">行为key</param>
        /// <returns>bool</returns>
        public bool IsRepeat(string actionKey)
        {
            if (MemoryCache.Default.Contains(actionKey))
            {
                return true;
            }
            MemoryCache.Default.Set(actionKey, "1", new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(2) });
            return false;
        }

    }
}