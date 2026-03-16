using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Mdsd.Emergency.Entities
{
    /// <summary>
    /// 检验申请单
    /// </summary>
    [Table("VIEW_EXAM_APPLY")]
    public class ViewExamApply : IEntity<string>
    {
        [Column("PATIENT_NO")]
        public string Id { get; set; }
        
        [Column("EXAM_NO")]
        public string EXAM_NO { get; set; }

        [Column("EXAM_CLASS")]
        public string EXAM_CLASS { get; set; }

        [Column("EXAM_SUB_CLASS")]
        public string EXAM_SUB_CLASS { get; set; }

        [Column("CLIN_DIAG")]
        public string CLIN_DIAG { get; set; }

        [Column("EXAM_MODE")]
        public string EXAM_MODE { get; set; }

        [Column("DEVICE")]
        public string DEVICE { get; set; }

        [Column("PERFORMED_BY")]
        public string PERFORMED_BY { get; set; }

        [Column("REQ_DATE_TIME")]
        public DateTime? REQ_DATE_TIME { get; set; }

        [Column("REQ_DEPT")]
        public string REQ_DEPT { get; set; }

        [Column("REQ_PHYSICIAN")]
        public string REQ_PHYSICIAN { get; set; }

        [Column("REQ_MEMO")]
        public string REQ_MEMO { get; set; }

        [Column("NOTICE")]
        public string NOTICE { get; set; }

        [Column("EXAM_START_DATE")]
        public DateTime? EXAM_START_DATE { get; set; }

        [Column("EXAM_END_DATE")]
        public DateTime? EXAM_END_DATE { get; set; }

        [Column("REPORT_DATE_TIME")]
        public DateTime? REPORT_DATE_TIME { get; set; }

        [Column("EXAM_PARA")]
        public string EXAM_PARA { get; set; }

        [Column("DESCRIPTION")]
        public string DESCRIPTION { get; set; }

        [Column("RECOMMENDATION")]
        public string RECOMMENDATION { get; set; }

        [Column("TECHNICIAN")]
        public string TECHNICIAN { get; set; }

        [Column("REPORTER")]
        public string REPORTER { get; set; }

        [Column("RESULT_STATUS")]
        public string RESULT_STATUS { get; set; }

        [Column("VERIFIED_BY")]
        public string VERIFIED_BY { get; set; }

        [Column("VERIFIED_DATE_TIME")]
        public DateTime? VERIFIED_DATE_TIME { get; set; }

    }
}
