using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.Core.Patient.Entities
{
    [Description("患者详细信息")]
    public class MedPatientDetailsInfo : EntityBase<string>
    {
        /// <summary>
        /// 门急诊挂号时间
        /// </summary>
        public DateTime? RegistTime { get; set; }

        /// <summary>
        /// 挂号科室
        /// </summary>
        [MaxLength(60)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 到达科室时间
        /// </summary>
        public DateTime? ArrivedDeptTime { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public DateTime? TriageTime { get; set; }

        /// <summary>
        /// 接诊医生
        /// </summary>
        [MaxLength(60)]
        public string Doctor { get; set; }

        /// <summary>
        /// 接诊时间
        /// </summary>
        public DateTime? AdmissionTime { get; set; }

        /// <summary>
        /// 初步诊断医生
        /// </summary>
        [MaxLength(60)]
        public string DiagDoctor { get; set; }

        /// <summary>
        /// 初步诊断时间
        /// </summary>
        public DateTime? DiagTime { get; set; }

        /// <summary>
        /// 初步诊断描述
        /// </summary>
        [MaxLength(100)]
        public string DiagDesc { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        [MaxLength(100)]
        public string Complaint { get; set; }

        /// <summary>
        /// 是否进入抢救室
        /// </summary>
        [MaxLength(10)]
        public string IsEmg { get; set; }

        /// <summary>
        /// 抢救室床位
        /// </summary>
        [MaxLength(60)]
        public string EmgBedNo { get; set; }

        /// <summary>
        /// 离开科室时间
        /// </summary>
        public DateTime? TransferTime { get; set; }

        /// <summary>
        /// 急诊绿通标志
        /// </summary>
        public bool IsGreenChannel { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        public string IcuSense { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        [MaxLength(20)]
        public string Temp { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        [MaxLength(20)]
        public string Br { get; set; }

        /// <summary>
        /// 脉搏
        /// </summary>
        [MaxLength(20)]
        public string Plus { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        [MaxLength(20)]
        public string Hr { get; set; }

        /// <summary>
        /// 血压
        /// </summary>
        [MaxLength(20)]
        public string Bp { get; set; }

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        [MaxLength(20)]
        public string Spo2 { get; set; }

        /// <summary>
        /// 住院时间
        /// </summary>
        public DateTime? InHosTime { get; set; }

        /// <summary>
        /// 住院科室
        /// </summary>
        [MaxLength(100)]
        public string InDeptCode { get; set; }

        /// <summary>
        /// 住院病区
        /// </summary>
        [MaxLength(20)]
        public string InWardCode { get; set; }

        /// <summary>
        /// 住院诊断
        /// </summary>
        [MaxLength(1000)]
        public string InHosDiagnosis { get; set; }

        /// <summary>
        /// 住院入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 住院床位
        /// </summary>
        [MaxLength(20)]
        public string BedNo { get; set; }

        /// <summary>
        /// 入院TOAST分型
        /// </summary>
        [MaxLength(20)]
        public string ToastFlag { get; set; }

        /// <summary>
        /// 出院诊断
        /// </summary>
        [MaxLength(1000)]
        public string OutHosDiagnosis { get; set; }

        /// <summary>
        /// 住院出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 出院时间
        /// </summary>
        public DateTime? OutHosTime { get; set; }

        /// <summary>
        /// 住院天数
        /// </summary>
        [MaxLength(20)]
        public string HosDay { get; set; }

        /// <summary>
        /// 总住院费用
        /// </summary>
        [MaxLength(40)]
        public string TotalCost { get; set; }

        /// <summary>
        /// 药品费用
        /// </summary>
        [MaxLength(40)]
        public string DrugCost { get; set; }

        /// <summary>
        /// 检查费用
        /// </summary>
        [MaxLength(40)]
        public string ExamCost { get; set; }

        /// <summary>
        /// 患者去向
        /// </summary>
        [MaxLength(200)]
        public string OutWay { get; set; }
    }
}
