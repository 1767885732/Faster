using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.KJCHospital.Entities
{
    [Table("VIEW_PATIENT_MAP_INFO")]
    public class ViewPatientMapInfo : IEntity<string>
    {
        /// <summary>
        /// 门诊就诊挂号编号
        /// </summary>
        [Key]
        [Column("CLINIC_NO")]
        public string Id { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        [Column("INP_NO")]
        public string INP_NO { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        [Column("REGIST_TIME")]
        public DateTime? REGIST_TIME { get; set; }
    }
}
