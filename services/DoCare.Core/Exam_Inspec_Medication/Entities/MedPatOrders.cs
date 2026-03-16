using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Exam_Inspec_Medication.Entities
{
    /// <summary>
    /// 患者医嘱用药信息
    /// </summary>
    [Description("患者医嘱用药信息")]
    public class MedPatOrders : EntityBase<Guid>
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        [MaxLength(60)]
        public string PatientNo { get; set; }

        /// <summary>
        /// 父医嘱号、组号
        /// </summary>
        [MaxLength(60)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 子医嘱号
        /// </summary>
        [MaxLength(60)]
        public string OrderSubNo { get; set; }

        /// <summary>
        /// HIS系统门诊号
        /// </summary>
        [MaxLength(60)]
        public string ClinicNo { get; set; }

        /// <summary>
        /// HIS系统住院号
        /// </summary>
        [MaxLength(60)]
        public string InpNo { get; set; }

        /// <summary>
        /// 医嘱标志：1为长期医嘱，0为临时医嘱
        /// </summary>
        [MaxLength(60)]
        public string RepeatIndicator { get; set; }

        /// <summary>
        /// 医嘱类别
        /// </summary>
        [MaxLength(60)]
        public string OrderClass { get; set; }

        /// <summary>
        /// 医嘱正文
        /// </summary>
        [MaxLength(200)]
        public string OrderText { get; set; }

        /// <summary>
        /// 医嘱代码
        /// </summary>
        [MaxLength(60)]
        public string OrderCode { get; set; }

        /// <summary>
        /// 药品一次使用剂量
        /// </summary>
        [MaxLength(60)]
        public string Dosage { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        [MaxLength(60)]
        public string DosageUnits { get; set; }

        /// <summary>
        /// 给药途径
        /// </summary>
        [MaxLength(60)]
        public string Administration { get; set; }

        /// <summary>
        /// 医嘱起始日期及时间
        /// </summary>

        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// 医嘱停止日期及时间
        /// </summary>
        public DateTime? StopDateTime { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public DateTime? Duration { get; set; }

        /// <summary>
        /// 持续时间单位
        /// </summary>
        [MaxLength(60)]
        public string DurationUnits { get; set; }

        /// <summary>
        /// 执行频率描述
        /// </summary>
        [MaxLength(60)]
        public string Frequency { get; set; }

        /// <summary>
        /// 开医嘱科室
        /// </summary>
        [MaxLength(60)]
        public string OrderingDept { get; set; }

        /// <summary>
        /// 开医嘱医生
        /// </summary>
        [MaxLength(60)]
        public string Doctor { get; set; }

        /// <summary>
        /// 停医嘱医生
        /// </summary>
        [MaxLength(60)]
        public string StopDoctor { get; set; }

        /// <summary>
        /// 开医嘱校对护士
        /// </summary>
        [MaxLength(60)]
        public string Nurse { get; set; }

        /// <summary>
        /// 停医嘱校对护士
        /// </summary>
        [MaxLength(60)]
        public string StopNurse { get; set; }

        /// <summary>
        /// 医嘱录入的当前日期及时间
        /// </summary>
        public DateTime? EnterDateTime { get; set; }

        /// <summary>
        /// 停医嘱录入的当前日期及时间
        /// </summary>
        public DateTime? StopOrderDateTime { get; set; }

        /// <summary>
        /// 医嘱状态
        /// </summary>
        [MaxLength(60)]
        public string OrderStatus { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [MaxLength(60)]
        public string CurrentPrescNo { get; set; }
    }
}
