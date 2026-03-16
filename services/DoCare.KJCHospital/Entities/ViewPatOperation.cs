using FastFrame.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DoCare.KJCHospital.Entities
{
    /// <summary>
    /// 获取患者手术数据
    /// </summary>
    [Table("V_PAT_OPERATION")]
    public class ViewPatOperation : IEntity<string>
    {
        /// <summary>
        /// 手术编号
        /// </summary>
        [Column("APPLY_NO")]
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>
        [Column("PATIENT_ID")]
        public string PATIENT_ID { get; set; }

        ///// <summary>
        ///// 住院次数
        ///// </summary>
        //[Column("VISIT_ID")]
        //public decimal? VISIT_ID { get; set; }

        ///// <summary>
        ///// 住院号
        ///// </summary>
        //[Column("HIS_NO")]
        //public string HIS_NO { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        [Column("SEX")]
        public string Sex { get; set; }

        /// <summary>
        /// 患者出生日期
        /// </summary>
        [NotMapped]
        //[Column("DATE_OF_BIRTH")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        [Column("SCHEDULED_DATE_TIME")]
        public DateTime? ScheduledDateTime { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        [Column("ANESTHESIA_METHOD")]
        public string AnesthesiaMethod { get; set; }

        /// <summary>
        /// 主刀医生
        /// </summary>
        [Column("SURGEON")]
        public string Surgeon { get; set; }

        /// <summary>
        /// 麻醉医生
        /// </summary>
        [Column("ANESTHESIA_DOCTOR")]
        public string AnesthesiaDoctor { get; set; }

        /// <summary>
        /// 术前诊断
        /// </summary>
        [Column("DIAG_BEFORE_OPERATION")]
        public string DiagBeforeOperation { get; set; }

        /// <summary>
        /// 术后诊断
        /// </summary>
        [Column("DIAG_AFTER_OPERATION")]
        public string DiagAfterOperation { get; set; }

        /// <summary>
        /// 手术等级
        /// </summary>
        [Column("OPERATION_SCALE")]
        public string OperationScale { get; set; }

        /// <summary>
        /// 进入手术室时间
        /// </summary>
        [Column("IN_DATE_TIME")]
        public DateTime? InDateTime { get; set; }

        /// <summary>
        /// 出手术室时间
        /// </summary>
        [Column("OUT_DATE_TIME")]
        public DateTime? OutDateTime { get; set; }

        /// <summary>
        /// 麻醉开始时间
        /// </summary>
        [Column("ANES_START_TIME")]
        public DateTime? AnesStartTime { get; set; }

        /// <summary>
        /// 麻醉结束时间
        /// </summary>
        [Column("ANES_END_TIME")]
        public DateTime? AnesEndTime { get; set; }

        /// <summary>
        /// 手术开始时间
        /// </summary>
        [Column("START_DATE_TIME")]
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// 手术结束时间
        /// </summary>
        [Column("END_DATE_TIME")]
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// 复苏开始时间
        /// </summary>
        [Column("IN_PACU_DATE_TIME")]
        public DateTime? InPacuDateTime { get; set; }

        /// <summary>
        /// 复苏结束时间
        /// </summary>
        [Column("OUT_PACU_DATE_TIME")]
        public DateTime? OutPacuDateTime { get; set; }

        /// <summary>
        /// 失血量
        /// </summary>
        [Column("BLOOD_LOSSED")]
        public string BloodLossed { get; set; }

        /// <summary>
        /// 输血量
        /// </summary>
        [Column("BLOOD_TRANSFERED")]
        public string BloodTransfered { get; set; }

        /// <summary>
        /// 手术名称
        /// </summary>
        [Column("OPERATION_NAME")]
        public string OperationName { get; set; }

        ///// <summary>
        ///// 手术部位
        ///// </summary>
        //[Column("OPERATION_PART")]
        //public string OperationPart { get; set; }

        ///// <summary>
        ///// 器械护士
        ///// </summary>
        //[Column("OPERNURSE")]
        //public string OperNurse { get; set; }

        ///// <summary>
        ///// 巡回护士
        ///// </summary>
        //[Column("SUPPLYNURSE")]
        //public string SupplyNurse { get; set; }
    }
}
