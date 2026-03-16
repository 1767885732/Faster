using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Patient.Entities
{
    /// <summary>
    /// 新生儿信息表
    /// </summary>
    [Description("新生儿信息表")]
    public class MedPatNewbornRecord : EntityBase<string>
    {
        /// <summary>
        /// 母亲姓名
        /// </summary>
        [MaxLength(60)]
        public string MotherName { get; set; }


        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(60)]
        public string PhoneNumber { get; set; }


        /// <summary>
        /// 主诉
        /// </summary>
        [MaxLength(60)]
        public string PersonSay { get; set; }


        /// <summary>
        /// 出生胎次
        /// </summary>
        [MaxLength(60)]
        public string BirthParity { get; set; }


        /// <summary>
        /// 产次
        /// </summary>
        [MaxLength(60)]
        public string DeliveryTimes { get; set; }


        /// <summary>
        /// 胎龄（周）
        /// </summary>
        [MaxLength(60)]
        public string GestationalAge { get; set; }


        /// <summary>
        /// 出生方式
        /// </summary>
        [MaxLength(60)]
        public string BirthWay { get; set; }

        /// <summary>
        /// 出生体重
        /// </summary>
        [MaxLength(60)]
        public string BirthWeight { get; set; }

        /// <summary>
        /// 出生缺陷
        /// </summary>
        [MaxLength(60)]
        public string BirthDefect { get; set; }


        /// <summary>
        /// 羊水
        /// </summary>
        [MaxLength(60)]
        public string AmnioticFluid { get; set; }

        /// <summary>
        /// 羊水量
        /// </summary>
        [MaxLength(60)]
        public string AmnioticFluidAmount { get; set; }

        /// <summary>
        /// 胎盘
        /// </summary>
        [MaxLength(60)]
        public string Placenta { get; set; }

        /// <summary>
        /// 脐带
        /// </summary>
        [MaxLength(60)]
        public string UmbilicalCord { get; set; }

        /// <summary>
        /// 产前类固醇激素
        /// </summary>
        [MaxLength(60)]
        public string PrenatalSteroidHormones { get; set; }

        /// <summary>
        /// Apgar评分1分钟
        /// </summary>
        [MaxLength(100)]
        public string ApgarOneMinute { get; set; }

        /// <summary>
        /// Apgar评分5分钟
        /// </summary>
        [MaxLength(100)]
        public string ApgarFiveMinute { get; set; }

        /// <summary>
        /// Apgar评分10分钟
        /// </summary>
        [MaxLength(100)]
        public string ApgarTenMinute { get; set; }

        /// <summary>
        /// 危重评分
        /// </summary>
        [MaxLength(60)]
        public string CriticalPoints { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime? InHosTime { get; set; }

        /// <summary>
        /// 入院诊断
        /// </summary>
        [MaxLength(1000)]
        public string InHosDesc { get; set; }

        /// <summary>
        /// 母亲血型
        /// </summary>
        [MaxLength(60)]
        public string MotherBloodType { get; set; }

        /// <summary>
        /// 母亲rh血型
        /// </summary>
        [MaxLength(60)]
        public string MotherRhBloodType { get; set; }

        /// <summary>
        /// 是否吸烟
        /// </summary>
        [MaxLength(60)]
        public string MotherWhetherSmoke { get; set; }

        /// <summary>
        /// 感染史
        /// </summary>
        [MaxLength(2000)]
        public string MotherInfectionHistory { get; set; }

        /// <summary>
        /// 用药史
        /// </summary>
        [MaxLength(2000)]
        public string MotherMedicationHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        [MaxLength(2000)]
        public string MotherAllergiesHistory { get; set; }

        /// <summary>
        /// 窒息复苏史
        /// </summary>
        [MaxLength(2000)]
        public string AsphyxiaResuscitationHistory { get; set; }

        /// <summary>
        /// 母亲年龄
        /// </summary>
        [MaxLength(50)]
        public string MotherAge { get; set; }

        /// <summary>
        /// 母亲身份证号
        /// </summary>
        [MaxLength(60)]
        public string MotherIdCard { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        [MaxLength(200)]
        public string FamilyAddress { get; set; }

        /// <summary>
        /// 出生医院
        /// </summary>
        [MaxLength(100)]
        public string BornHospital { get; set; }
    }
}
