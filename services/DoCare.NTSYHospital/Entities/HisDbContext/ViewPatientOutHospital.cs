using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.NTSYHospital.Entities
{
    [Table("VIEW_PATIENT_OUT_HOSPITAL")]
    public class ViewPatientOutHospital : IEntity<string>
    {
        /// <summary>
        /// 住院号
        /// </summary>
        [NotMapped]
        public string Id { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        [Key]
        [Column("PATIENT_NO")]
        public string INP_NO { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        [Column("OUT_DEPT_TIME")]
        public DateTime? OUT_DEPT_TIME { get; set; }

        /// <summary>
        /// 出院时间
        /// </summary>
        [Column("OUT_HOS_TIME")]
        public DateTime? OUT_HOS_TIME { get; set; }

        /// <summary>
        /// 出院诊断
        /// </summary>
        [Column("OUT_DIAGNOSIS")]
        public string OUT_DIAGNOSIS { get; set; }

        /// <summary>
        /// 住院天数
        /// </summary>
        [Column("HOS_DAY")]
        public decimal? HOS_DAY { get; set; }

        /// <summary>
        /// 总住院费用
        /// </summary>
        [Column("TOTAL_COST")]
        public decimal? TOTAL_COST { get; set; }

        /// <summary>
        /// 药品费用
        /// </summary>
        [Column("DRUG_COST")]
        public decimal? DRUG_COST { get; set; }

        /// <summary>
        /// 检查费用
        /// </summary>
        [Column("EXAM_COST")]
        public decimal? EXAM_COST { get; set; }

        /// <summary>
        /// 患者去向(好转，死亡等)
        /// </summary>
        [Column("OUT_WAY")]
        public string OUT_WAY { get; set; }
    }
}
