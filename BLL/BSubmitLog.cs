using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Hospitalinsurance.Entity;
using HospitalInsurance.Utility;

namespace HospitalInsurance.BLL
{
    /// <summary>
    /// 提交日志管理
    /// </summary>
    public class BSubmitLog : Singleton<BSubmitLog>
    {
        /// <summary>
        /// 保存分解请求
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public int Save(SubmitLog entity)
        {
            using (var context = new HCContext())
            {
                context.SubmitLogs.Add(entity);
                return context.SaveChanges();
            }
        }        

        /// <summary>
        /// 获取请求RequestId获取记录
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public SubmitLog GetSubmitLog(string requestId)
        {
            using (var context = new HCContext())
            {
                return context.SubmitLogs.FirstOrDefault(x => x.RequestId == requestId);
            }           
        }
    }
}
