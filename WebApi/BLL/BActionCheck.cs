using System;
using System.Runtime.Caching;
using HospitalInsurance.Utility;
using HospitalInsurance.WebApi.Utility;

namespace HospitalInsurance.WebApi.BLL
{
    /// <summary>
    /// 操作判断
    /// </summary>
    public class BActionCheck : Singleton<BActionCheck>
    {

        private readonly static ObjectCache SystemCache = MemoryCache.Default;

        /// <summary>
        /// 操作是否重复提交
        /// </summary>
        /// <param name="actionKey">行为key</param>
        /// <returns>bool</returns>
        public bool IsRepeat(string actionKey)
        {     
            if(SystemCache.Contains(actionKey))
            {
                return true;
            }
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(2),
            };
            SystemCache.Add(actionKey, "缓存2秒过期", cacheItemPolicy);

            return false;
        }

    }
}