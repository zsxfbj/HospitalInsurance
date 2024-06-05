﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitalinsurance.Entity
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    [Table("ActionLog")]
    public class ActionLog
    {
        /// <summary>
        /// 操作流水号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ActionId { get; set; }

        /// <summary>
        /// 提交的事务Id
        /// </summary>
        [Index]
        public long SubmitId { get; set; }

        /// <summary>
        /// 操作步骤。默认 1
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 请求的内容
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// 响应的内容
        /// </summary>
        public string ResponseData { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
