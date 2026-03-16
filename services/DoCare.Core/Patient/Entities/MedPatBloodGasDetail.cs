using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Patient.Entities
{
    [Description("患者血气分析信息")]
    public class MedPatBloodGasDetail : EntityBase<Guid>
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        [MaxLength(40)]
        public string PatientNo { get; set; }

        /// <summary>
        /// 抽血时间
        /// </summary>
        public DateTime? BloodTime { get; set; }

        /// <summary>
        /// 报告时间
        /// </summary>
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// 检验项目
        /// </summary>
        [MaxLength(100)]
        public string TestId { get; set; }

        /// <summary>
        /// 检验项目名
        /// </summary>
        [MaxLength(100)]
        public string TestName { get; set; }

        /// <summary>
        /// 检验值
        /// </summary>
        [MaxLength(100)]
        public string TestValue { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [MaxLength(100)]
        public string Unit { get; set; }

        /// <summary>
        /// 结论
        /// </summary>
        [MaxLength(100)]
        public string Conclusion { get; set; }

    }
}
