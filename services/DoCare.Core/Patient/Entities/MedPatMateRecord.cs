using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Patient.Entities
{
    /// <summary>
    /// 孕产妇信息
    /// </summary>
    [Description("孕产妇信息")]
    public class MedPatMateRecord : EntityBase<string>
    {
        /// <summary>
        /// 入院科室
        /// </summary>
        [MaxLength(60)]
        public string InDept { get; set; }
        /// <summary>
        /// 床位
        /// </summary>
        [MaxLength(60)]
        public string BedNo { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime? InHosTime { get; set; }
        /// <summary>
        /// 入院主诉
        /// </summary>
        [MaxLength(200)]
        public string InComplaint { get; set; }
        /// <summary>
        /// 孕周
        /// </summary>
        [MaxLength(60)]
        public string PreganetWeek { get; set; }
        /// <summary>
        /// 孕次
        /// </summary>
        [MaxLength(60)]
        public string PreganetTimes { get; set; }
        /// <summary>
        /// 产次
        /// </summary>
        [MaxLength(60)]
        public string ProductTimes { get; set; }
        /// <summary>
        /// 意识
        /// </summary>
        [MaxLength(60)]
        public string InSense { get; set; }
        /// <summary>
        /// 体温
        /// </summary>
        [MaxLength(60)]
        public string InT { get; set; }
        /// <summary>
        /// 脉搏
        /// </summary>
        [MaxLength(60)]
        public string InP { get; set; }
        /// <summary>
        /// 呼吸
        /// </summary>
        [MaxLength(60)]
        public string InR { get; set; }
        /// <summary>
        /// 血压
        /// </summary>
        [MaxLength(60)]
        public string InBP { get; set; }
        /// <summary>
        /// 血氧
        /// </summary>
        [MaxLength(60)]
        public string InSpo2 { get; set; }
        /// <summary>
        /// 血糖
        /// </summary>
        [MaxLength(60)]
        public string InGlu { get; set; }
        /// <summary>
        /// 胎心
        /// </summary>
        [MaxLength(60)]
        public string FetelHeart { get; set; }
        /// <summary>
        /// 宫高
        /// </summary>
        [MaxLength(60)]
        public string UterHeight { get; set; }
        /// <summary>
        /// 腹围
        /// </summary>
        [MaxLength(60)]
        public string AbdIndex { get; set; }
        /// <summary>
        /// 既往史
        /// </summary>
        [MaxLength(1000)]
        public string PastHistory { get; set; }
        /// <summary>
        /// 家族史
        /// </summary>
        [MaxLength(1000)]
        public string FamilyHistory { get; set; }
        /// <summary>
        /// 初潮年龄
        /// </summary>
        [MaxLength(60)]
        public string MenarcheAge { get; set; }
        /// <summary>
        /// 经期
        /// </summary>
        [MaxLength(60)]
        public string MenstrualDay { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        [MaxLength(60)]
        public string MenstrualCycle { get; set; }
        /// <summary>
        /// 月经量
        /// </summary>
        [MaxLength(60)]
        public string MenstrualVolume { get; set; }
        /// <summary>
        /// 痛经
        /// </summary>
        [MaxLength(60)]
        public string Dysmenorrhea { get; set; }
        /// <summary>
        /// 初步诊断
        /// </summary>
        [MaxLength(1000)]
        public string InitialDiagnosis { get; set; }
        /// <summary>
        /// 初步诊断医生
        /// </summary>
        [MaxLength(60)]
        public string InitialDoctor { get; set; }
        /// <summary>
        /// 入院诊断
        /// </summary>
        [MaxLength(1000)]
        public string InhosDiagnosis { get; set; }
        /// <summary>
        /// 入院诊断医生
        /// </summary>
        [MaxLength(60)]
        public string InhosDoctor { get; set; }

        /// <summary>
        /// 高危因素
        /// </summary>
        [MaxLength(2000)]
        public string HighRisk { get; set; }

        /// <summary>
        /// 特殊治疗措施
        /// </summary>
        [MaxLength(200)]
        public string SpecialTreatment { get; set; }

        /// <summary>
        /// 产后出血
        /// </summary>
        [MaxLength(60)]
        public string ProductedBleed { get; set; }

        /// <summary>
        /// 指征
        /// </summary>
        [MaxLength(500)]
        public string Indication { get; set; }
    }
}
