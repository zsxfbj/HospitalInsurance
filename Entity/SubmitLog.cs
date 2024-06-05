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
        /// 自增Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public long Id { get; set; }

        /// <summary>
        /// 访问的Ip
        /// </summary>
        public string ClientIp { get; set; }

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
            Id = 0;
            ClientIp = "127.0.0.1";
            SubmitType = 1;
            SubmitContent = "";
            ResultContent = "";
            Flag = 0;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

    }
}
