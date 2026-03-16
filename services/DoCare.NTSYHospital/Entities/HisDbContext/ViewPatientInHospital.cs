using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.NTSYHospital.Entities
{
    [Table("VIEW_PATIENT_IN_HOSPITAL")]
    public class ViewPatientInHospital : IEntity<string>
    {
        [NotMapped]
        public string Id { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        [Key]
        [Column("PATIENT_NO")]
        public string INP_NO { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary>
        [Column("IN_HOS_TIME")]
        public DateTime? IN_HOS_TIME { get; set; }

        /// <summary>
        /// 入院科室
        /// </summary>
        [Column("DEPT_CODE")]
        public string DEPT_CODE { get; set; }

        /// <summary>
        /// 入院病区
        /// </summary>
        [Column("WARD_CODE")]
        public string WARD_CODE { get; set; }

        /// <summary>
        /// 入院诊断
        /// </summary>
        [Column("IN_HOS_DIAGNOSIS")]
        public string IN_HOS_DIAGNOSIS { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        [Column("IN_DEPT_TIME")]
        public string IN_DEPT_TIME { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        [Column("BED_NO")]
        public string BED_NO { get; set; }

        /// <summary>
        /// 主治医生
        /// </summary>
        [Column("DOCTOR")]
        public string DOCTOR { get; set; }
    }
}
