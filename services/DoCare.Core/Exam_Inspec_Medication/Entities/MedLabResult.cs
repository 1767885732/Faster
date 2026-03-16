using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Exam_Inspec_Medication.Entities
{
    /// <summary>
    /// 患者检验报告信息
    /// </summary>
    [Description("患者检验报告信息")]
    public class MedLabResult : EntityBase<string>
    {
        /// <summary>
        /// 申请序号（报告号）
        /// </summary>
        [MaxLength(40)]
        public string TestNo { get; set; }

        ///// <summary>
        ///// 序列号
        ///// </summary>
        //[MaxLength(40)]
        //public string ItemNo { get; set; }

        /// <summary>
        /// 检验报告项目名称
        /// </summary>
        [MaxLength(100)]
        public string ReportItemName { get; set; }

        /// <summary>
        /// 检验报告项目代码
        /// </summary>
        [MaxLength(60)]
        public string ReportItemCode { get; set; }

        /// <summary>
        /// 结果正常与否标志
        /// </summary>
        [MaxLength(60)]
        public string AbnormalIndicator { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        [MaxLength(200)]
        public string Result { get; set; }

        /// <summary>
        /// 结果单位
        /// </summary>
        [MaxLength(20)]
        public string Units { get; set; }

        /// <summary>
        /// 检验日期及时间
        /// </summary>
        public DateTime? ResultDateTime { get; set; }

        /// <summary>
        /// 检验结果参考值
        /// </summary>
        [MaxLength(500)]
        public string ReferenceResult { get; set; }
    }
}
