using System.Threading.Tasks;
using Hospitalinsurance.Entity;
using HospitalInsurance.Model.DTO;
using HospitalInsurance.Utility;
using Newtonsoft.Json;

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
        public async void Save(SubmitLog entity)
        {  
            await Task.Run(() => {

                using (var context = new HCContext())
                {
                    context.SubmitLogs.Add(entity);
                }
            });
        }        
    }
}
