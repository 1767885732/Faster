using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Ecg.Entities
{
    /// <summary>
    /// 心电数据表
    /// </summary>
    [Description("心电数据表")]
    public class MedPatEcgInfo : EntityBase<Guid>
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        [MaxLength(60)]
        public string PatientNo { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime? ExamTime { get; set; }

        /// <summary>
        /// 报告时间
        /// </summary>
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// 诊断时间
        /// </summary>
        public DateTime? DiagTime { get; set; }

        /// <summary>
        /// 诊断描述
        /// </summary>
        [MaxLength(200)]
        public string DiagDesc { get; set; }

        /// <summary>
        /// 心电图来源
        /// </summary>
        [MaxLength(40)]
        public string DiagSource { get; set; }

        /// <summary>
        /// 心电文件路径
        /// </summary>
        [MaxLength(200)]
        public string EcgPath { get; set; }

        /// <summary>
        /// 心电URL
        /// </summary>
        [MaxLength(200)]
        public string EcgUrl { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 心电图患者ID
        /// </summary>
        public string EcgPatientId { get; set; }

        /// <summary>
        /// 心电图数据
        /// </summary>
        public string EcgData { get; set; }

        /// <summary>
        /// 原始pdf心电图base64字符串
        /// </summary>
        public string PdfData { get; set; }
    }
}
