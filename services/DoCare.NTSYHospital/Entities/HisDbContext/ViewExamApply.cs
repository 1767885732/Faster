using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.NTSYHospital.Entities
{
    /// <summary>
    /// 检验申请单
    /// </summary>
    //[Table("VIEW_EXAM_APPLY")]
    [Table(ExamSQL)]

    public class ViewExamApply : IEntity<string>
    {
        private const string ExamSQL = @"(SELECT ITEM.REQUESTNO AS EXAM_NO,
                                               REP.PATIENTID AS PATIENT_NO,
                                               REP.STUDYCLASSNAME AS EXAM_CLASS,
                                               REP.STUDYSUBCLASSNAME AS EXAM_SUB_CLASS,
                                               REP.CLINICALDIAGINFO AS CLIN_DIAG,
                                               REP.STUDYMETHODNAME AS EXAM_MODE,
                                               REP.INSTRUMENT AS DEVICE,
                                               REP.EXECDEPTNAME AS PERFORMED_BY,
                                               REP.REQUESTDATETIME AS REQ_DATE_TIME,
                                               REP.DOCTORDEPTNAME AS REQ_DEPT,
                                               REP.DOCTORNAME AS REQ_PHYSICIAN,
                                               '' AS REQ_MEMO, --  申请备注
                                               '' AS NOTICE, --注意事项
                                               REP.STUDYOPERATEDATETIME AS EXAM_START_DATE,
                                               REP.STUDYOPERATEDATETIME AS EXAM_END_DATE,
                                               REP.STUDYREPORTDATETIME AS REPORT_DATE_TIME,
                                               ITEM.STUDYSITENAME AS EXAM_PARA,
                                               REP.IMAGINGFINDINGS AS DESCRIPTION,
                                               REP.STUDYCONCLUSION AS RECOMMENDATION,
                                               REP.STUDYOPERATERNAME AS TECHNICIAN,
                                               REP.STUDYREPORTERNAME AS REPORTER,
                                               REP.STATUSNAME AS RESULT_STATUS,
                                               REP.STUDYREVIEWERNAME AS VERIFIED_BY,
                                               REP.STUDYREVIEWDATETIME AS VERIFIED_DATE_TIME
                                          FROM CDR_RIS.DBO.RIS_REPORT REP
                                          LEFT JOIN CDR_RIS.DBO.RIS_REPORTITEM ITEM
                                            ON ITEM.REPORTNO = REP.REPORTNO
                                           AND ITEM.HOSPITALCODE = REP.HOSPITALCODE)";



        [Column("PATIENT_NO")]
        public string Id { get; set; }
        
        [Key]
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
