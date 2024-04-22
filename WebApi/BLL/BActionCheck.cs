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
        /// <param name="requestData">请求的内容</param>
        /// <returns>bool</returns>
        public bool IsRepeat(string requestData)
        {             
            string signValue = StringHelper.MD5Encrypt32(requestData);

            if(SystemCache.Contains(signValue))
            {
                return false;
            }
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(3),
            };
            SystemCache.Add(signValue, "缓存3秒过期", cacheItemPolicy);

            return true;
        }

    }
}