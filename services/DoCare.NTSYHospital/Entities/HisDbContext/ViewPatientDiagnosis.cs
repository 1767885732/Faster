using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.NTSYHospital.Entities
{
    /// <summary>
    /// 检验申请单
    /// </summary>
    [Table("VIEW_PATIENT_DIAGNOSIS")]
    public class ViewPatientDiagnosis : IEntity<string>
    {
        [NotMapped]
        public string Id { get; set; }

        [Key]
        [Column("CLINIC_REGIST_NO")]
        public string CLINIC_REGIST_NO { get; set; }

        [Column("CLINIC_NO")]
        public string CLINIC_NO { get; set; }

        [Column("INP_NO")]
        public string INP_NO { get; set; }

        [Column("DIAGNOSIS_TYPE")]
        public string DIAGNOSIS_TYPE { get; set; }

        [Column("DIAGNOSIS_DESC")]
        public string DIAGNOSIS_DESC { get; set; }

        [Column("DIAGNOSIS_DATE")]
        public DateTime? DIAGNOSIS_DATE { get; set; }

        [Column("DOCTOR_ID")]
        public string DOCTOR_ID { get; set; }
    }
}
