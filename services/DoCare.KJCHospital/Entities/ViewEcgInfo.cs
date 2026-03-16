using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.KJCHospital.Entities
{
    /// <summary>
    /// 心电图
    /// </summary>
    [Table("VIEW_PATIENT_ECG_INFO")]
    public class ViewEcgInfo : IEntity<string>
    {
        [NotMapped]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        //[Column("PATIENT_ID")]
        public Guid PATIENT_ID { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        [Column("VISIT_ID")]
        public string VISIT_ID { get; set; }

        /// <summary>
        /// 门诊/住院 病案号
        /// </summary>
        [Column("HIS_NO")]
        public string HIS_NO { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column("NAME")]
        public string NAME { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Column("SEX")]
        public string SEX { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Column("AGE")]
        public double? AGE { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        [Column("EXAM_TIME")]
        public DateTime? EXAM_TIME { get; set; }

        /// <summary>
        /// 报告时间
        /// </summary>
        [Column("REPORT_TIME")]
        public DateTime? REPORT_TIME { get; set; }

        /// <summary>
        /// 诊断时间
        /// </summary>
        [Column("DIAG_TIME")]
        public DateTime? DIAG_TIME { get; set; }

        /// <summary>
        /// 诊断描述
        /// </summary>
        [Column("DIAG_DESC")]
        public string DIAG_DESC { get; set; }

        /// <summary>
        /// 心电图来源
        /// </summary>
        [Column("DIAG_SOURCE")]
        public string DIAG_SOURCE { get; set; }

        /// <summary>
        /// 心电文件路径
        /// </summary>
        [Column("ECG_PATH")]
        public string ECG_PATH { get; set; }

        /// <summary>
        /// 心电图内容 BASE64编码
        /// </summary>
        [Column("ECG_DATA")]
        public string ECG_DATA { get; set; }

    }
}
