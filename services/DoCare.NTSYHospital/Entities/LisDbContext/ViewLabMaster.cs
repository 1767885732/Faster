using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.NTSYHospital.Entities
{
    /// <summary>
    /// 检验申请单
    /// </summary>
    [Table("VIEW_LAB_MASTER")]
    public class ViewLabMaster : IEntity<string>
    {
        /// <summary>
        /// 申请序号（报告号）
        /// </summary>
        [Column("TEST_NO")]
        public string Id { get; set; }

        /// <summary>
        /// 优先标志
        /// </summary>
        [Column("PRIORITY_INDICATOR")]
        public string PRIORITY_INDICATOR { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        [Column("VISIT_ID")]
        public string CLINIC_NO { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        [Column("PATIENT_ID")]
        public string INP_NO { get; set; }

        /// <summary>
        /// 病历档案表主键编号
        /// </summary>
        [Column("PATIENT_ID")]
        public string PATIENT_ID { get; set; }

        /// <summary>
        /// 门诊就诊时挂号编号
        /// </summary>
        [Column("VISIT_ID")]
        public string CLINIC_REGIST_NO { get; set; }

        /// <summary>
        /// 病人检验姓名
        /// </summary>
        [Column("NAME")]
        public string NAME { get; set; }

        /// <summary>
        /// 检验目的
        /// </summary>
        [Column("TEST_CAUSE")]
        public string TEST_CAUSE { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        [Column("RELEVANT_CLINIC_DIAG")]
        public string RELEVANT_CLINIC_DIAG { get; set; }

        /// <summary>
        /// 标本，如血、尿、痰等化验样本
        /// </summary>
        [Column("SPECIMEN")]
        public string SPECIMEN { get; set; }

        /// <summary>
        /// 上述样本的采样时间
        /// </summary>
        [Column("SPCM_RECEIVED_DATE_TIME")]
        public DateTime? SPCM_RECEIVED_DATE_TIME { get; set; }

        /// <summary>
        /// 检验申请时间
        /// </summary>
        [Column("APPLY_TIME")]
        public DateTime? APPLY_TIME { get; set; }

        /// <summary>
        /// 开出该申请单的科室：科室代码
        /// </summary>
        [Column("ORDERING_DEPT")]
        public string ORDERING_DEPT { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        [Column("ORDERING_PROVIDER")]
        public string ORDERING_PROVIDER { get; set; }

        /// <summary>
        /// 执行该申请单的科室：科室代码
        /// </summary>
        [Column("PERFORMED_BY")]
        public string PERFORMED_BY { get; set; }

        // <summary>
        // 反映申请的执行情况改
        // </summary>
        [Column("RESULT_STATUS")]
        public string RESULT_STATUS { get; set; }

        /// <summary>
        /// 检验报告完成时间
        /// </summary>
        [Column("RESULTS_RPT_DATE_TIME")]
        public DateTime? RESULTS_RPT_DATE_TIME { get; set; }

        /// <summary>
        /// 报告者
        /// </summary>
        [Column("TRANSCRIPTIONIST")]
        public string TRANSCRIPTIONIST { get; set; }

        /// <summary>
        /// 审核者
        /// </summary>
        [Column("VERIFIED_BY")]
        public string VERIFIED_BY { get; set; }
    }
}
