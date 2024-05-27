using System.Data.Entity;

namespace Hospitalinsurance.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class HCContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HCContext() : base("HCContext")
        { }

        /// <summary>
        /// 提交日志表
        /// </summary>
        public DbSet<SubmitLog> SubmitLogs
        {
            get { return Set<SubmitLog>(); }

        }


        /// <summary>
        /// 医保网关操作日志表
        /// </summary>
        public DbSet<ActionLog> ActionLogs { get { return Set<ActionLog>(); } }
    }
}
