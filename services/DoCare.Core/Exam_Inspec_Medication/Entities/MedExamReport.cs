using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Exam_Inspec_Medication.Entities
{
    /// <summary>
    /// 患者检查报告信息
    /// </summary>
    [Description("患者检查报告信息")]
    public class MedExamReport : EntityBase<string>
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        [MaxLength(60)]
        public string PatientNo { get; set; }

        /// <summary>
        /// 检查项目类别
        /// </summary>
        [MaxLength(100)]
        public string ExamClass { get; set; }

        /// <summary>
        /// 检查项目子类
        /// </summary>
        [MaxLength(100)]
        public string ExamSubClass { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        [MaxLength(200)]
        public string ClinDiag { get; set; }

        /// <summary>
        /// 检查方式
        /// </summary>
        [MaxLength(100)]
        public string ExamMode { get; set; }

        /// <summary>
        /// 使用仪器
        /// </summary>
        [MaxLength(60)]
        public string Device { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        [MaxLength(60)]
        public string PerformedBy { get; set; }

        /// <summary>
        /// 申请日期及时间
        /// </summary>
        public DateTime? ReqDateTime { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary>
        [MaxLength(60)]
        public string ReqDept { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary>
        [MaxLength(60)]
        public string ReqPhysician { get; set; }

        /// <summary>
        /// 申请备注
        /// </summary>
        [MaxLength(100)]
        public string ReqMemo { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [MaxLength(60)]
        public string Notice { get; set; }

        /// <summary>
        /// 检查开始日期时间
        /// </summary>        
        public DateTime? ExamStartDate { get; set; }

        /// <summary>
        /// 检查结束日期时间
        /// </summary>
        public DateTime? ExamEndDate { get; set; }

        /// <summary>
        /// 报告日期及时间
        /// </summary>
        public DateTime? ReportDateTime { get; set; }

        /// <summary>
        /// 检查参数（部位）
        /// </summary>
        [MaxLength(60)]
        public string ExamPara { get; set; }

        /// <summary>
        /// 检查所见
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// 检查结论或建议
        /// </summary>
        [MaxLength(1000)]
        public string Recommendation { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        [MaxLength(60)]
        public string Technician { get; set; }

        /// <summary>
        /// 报告者
        /// </summary>
        [MaxLength(60)]
        public string Reporter { get; set; }

        /// <summary>
        /// 检查结果状态
        /// </summary>
        [MaxLength(60)]
        public string ResultStatus { get; set; }

        /// <summary>
        /// 审核者
        /// </summary>
        [MaxLength(60)]
        public string VerifiedBy { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? VerifiedDateTime { get; set; }
    }
}
