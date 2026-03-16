using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Exam_Inspec_Medication.Entities
{
    /// <summary>
    /// 患者检验申请单信息
    /// </summary>
    [Description("患者检验申请单信息")]
    public class MedLabMaster : EntityBase<string>
    {
        /// <summary>
        /// 优先标志
        /// </summary>
        [MaxLength(40)]
        public string PriorityIndicator { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>
        [MaxLength(40)]
        public string PatientNo { get; set; }

        /// <summary>
        /// 病人检验姓名
        /// </summary>
        [MaxLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// 检验目的
        /// </summary>
        [MaxLength(200)]
        public string TestCause { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        [MaxLength(200)]
        public string RelevantClinicDiag { get; set; }

        /// <summary>
        /// 标本，如血、尿、痰等化验样本
        /// </summary>
        [MaxLength(200)]
        public string Specimen { get; set; }

        /// <summary>
        /// 上述样本的采样时间
        /// </summary>
        public DateTime? SpcmReceivedDateTime { get; set; }

        /// <summary>
        /// 检验申请时间
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 开出该申请单的科室：科室代码
        /// </summary>
        [MaxLength(40)]
        public string OrderingDept { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        [MaxLength(40)]
        public string OrderingProvider { get; set; }

        /// <summary>
        /// 执行该申请单的科室：科室代码
        /// </summary>
        [MaxLength(40)]
        public string PerformedBy { get; set; }

        /// <summary>
        /// 反映申请的执行情况
        /// </summary>
        [MaxLength(40)]
        public string ResultStatus { get; set; }

        /// <summary>
        /// 检验报告完成时间
        /// </summary>
        public DateTime? ResultRptDateTime { get; set; }

        /// <summary>
        /// 报告者
        /// </summary>
        [MaxLength(40)]
        public string Transcriptionist { get; set; }

        /// <summary>
        /// 审核者
        /// </summary>
        [MaxLength(40)]
        public string VerifiedBy { get; set; }
    }
}
