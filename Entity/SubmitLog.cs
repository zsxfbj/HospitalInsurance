﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitalinsurance.Entity
{
    /// <summary>
    /// 提交医保的日志
    /// </summary>
    [Table("SubmitLog")]
    public class SubmitLog
    {
        /// <summary>
        /// 请求业务Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RequestId { get; set; }


        /// <summary>
        /// 提交类型：1-费用分解；2-退费申请；3-查询交易状态
        /// </summary>
        public int SubmitType { get; set; }

        /// <summary>
        /// 提交的内容
        /// </summary>
        public string SubmitContent { get; set; }

        /// <summary>
        /// 返回的内容
        /// </summary>
        public string ResultContent { get; set; }

        /// <summary>
        /// 操作状态标记
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SubmitLog()
        {             
            RequestId = Guid.NewGuid().ToString();
            SubmitType = 1;
            SubmitContent = "";
            ResultContent = "";
            Flag = 0;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

    }
}
