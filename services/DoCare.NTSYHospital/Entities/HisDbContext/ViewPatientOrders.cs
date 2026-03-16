using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.NTSYHospital.Entities
{
    [Table("VIEW_PATIENT_ORDERS")]
    public class ViewPatientOrders : IEntity<string>
    {
        /// <summary>
        /// 忽略，默认实现接口
        /// </summary>
        [NotMapped]
        public string Id { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        [Column("ORDER_NO")]
        public string CLINIC_NO { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        [Column("PATIENT_ID")]
        public string INP_NO { get; set; }

        /// <summary>
        /// 病历档案表主键编号
        /// </summary>
        [Column("PATIENT_ID")]
        public string PATIENT_ID { get; set; }

        /// <summary>
        /// 门诊就诊挂号编号
        /// </summary>
        [Column("ORDER_NO")]
        public string CLINIC_REGIST_NO { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [Column("PRESCRIPTION_NO")]
        public string PRESCRIPTION_NO { get; set; }

        /// <summary>
        /// 父医嘱号、组号
        /// </summary>
        [Column("ORDER_NO")]
        public string ORDER_NO { get; set; }

        /// <summary>
        /// 子医嘱号
        /// </summary>
        [Column("ORDER_SUB_NO")]
        public string ORDER_SUB_NO { get; set; }

        /// <summary>
        /// 医嘱类别
        /// </summary>
        [Column("ORDER_CLASS")]
        public string ORDER_CLASS { get; set; }

        /// <summary>
        /// 医嘱正文
        /// </summary>
        [Column("ORDER_TEXT")]
        public string ORDER_TEXT { get; set; }

        /// <summary>
        /// 医嘱代码
        /// </summary>
        [Column("ORDER_CODE")]
        public string ORDER_CODE { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        [Column("DOSAGE_UNITS")]
        public string DOSAGE_UNITS { get; set; }

        /// <summary>
        /// 给药途径
        /// </summary>
        [Column("ADMINISTRATION")]
        public string ADMINISTRATION { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        [Column("DURATION")]
        public string DURATION { get; set; }

        /// <summary>
        /// 持续时间单位
        /// </summary>
        [Column("DURATION_UNITS")]
        public string DURATION_UNITS { get; set; }

        /// <summary>
        /// 执行频率描述
        /// </summary>
        [Column("FREQUENCY")]
        public string FREQUENCY { get; set; }

        /// <summary>
        /// 开医嘱科室
        /// </summary>
        [Column("ORDERING_DEPT")]
        public string ORDERING_DEPT { get; set; }

        /// <summary>
        /// 开医嘱医生
        /// </summary>
        [Column("DOCTOR")]
        public string DOCTOR { get; set; }

        /// <summary>
        /// 停医嘱医生
        /// </summary>
        [Column("STOP_DOCTOR")]
        public string STOP_DOCTOR { get; set; }

        /// <summary>
        /// 开医嘱校对护士
        /// </summary>
        [Column("NURSE")]
        public string NURSE { get; set; }

        /// <summary>
        /// 停医嘱校对护士
        /// </summary>
        [Column("STOP_NURSE")]
        public string STOP_NURSE { get; set; }

        /// <summary>
        /// 停医嘱录入的当前日期及时间
        /// </summary>
        [Column("STOP_ORDER_DATE_TIME")]
        public DateTime? STOP_ORDER_DATE_TIME { get; set; }

        /// <summary>
        /// 医嘱状态
        /// </summary>
        [Column("ORDER_STATUS")]
        public string ORDER_STATUS { get; set; }

        /// <summary>
        /// 医嘱录入的当前日期及时间
        /// </summary>
        [Column("ENTER_DATE_TIME")]
        public DateTime? ENTER_DATE_TIME { get; set; }

        /// <summary>
        /// 医嘱起始日期及时间
        /// </summary>
        [Column("START_DATE_TIME")]
        public DateTime? START_DATE_TIME { get; set; }

        /// <summary>
        /// 医嘱停止日期及时间
        /// </summary>
        [Column("STOP_DATE_TIME")]
        public DateTime? STOP_DATE_TIME { get; set; }

        /// <summary>
        /// 药品一次使用剂量
        /// </summary>
        [Column("DOSAGE")]
        public decimal? DOSAGE { get; set; }

        /// <summary>
        /// 药品总量
        /// </summary>
        [Column("TOTAL_AMOUNT")]
        public decimal? TOTAL_AMOUNT { get; set; }

        /// <summary>
        /// 医嘱标志：1为长期医嘱，0为临时医嘱
        /// </summary>
        [Column("REPEAT_INDICATOR")]
        public int? REPEAT_INDICATOR { get; set; }
    }
}
