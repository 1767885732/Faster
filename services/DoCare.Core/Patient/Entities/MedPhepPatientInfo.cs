using FastFrame.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoCare.Core.Patient.Entities
{
    /// <summary>
    /// 院前患者基本信息
    /// </summary>
    [Description("院前患者基本信息")]
    public class MedPhepPatientInfo : EntityBase<string>
    {


        /// <summary>
        /// 患者姓名
        /// </summary>
        [MaxLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(10)]
        public string Sex { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [MaxLength(20)]
        public string IdNo { get; set; }

        /// <summary>
        /// 市民卡号
        /// </summary>
        [MaxLength(40)]
        public string CitizenCard { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        [MaxLength(100)]
        public string MailAddress { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [MaxLength(40)]
        public string PhoneNumer { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? AttackTime { get; set; }

        /// <summary>
        /// 发病地址
        /// </summary>
        [MaxLength(100)]
        public string AttackAddress { get; set; }

        /// <summary>
        /// 呼救时间
        /// </summary>
        public DateTime? CallTime { get; set; }

        /// <summary>
        /// 接受任务时间
        /// </summary>
        public DateTime? AcceptTime { get; set; }

        /// <summary>
        /// 出车单位
        /// </summary>
        [MaxLength(40)]
        public string DispatchType { get; set; }

        /// <summary>
        /// 出车时间
        /// </summary>
        public DateTime? DispatchVehicletime { get; set; }

        /// <summary>
        /// 到达现场时间
        /// </summary>
        public DateTime? ArrivedTime { get; set; }

        /// <summary>
        /// 医疗接触时间
        /// </summary>
        public DateTime? TouchPatTime { get; set; }

        /// <summary>
        /// 离开现场时间
        /// </summary>
        public DateTime? LeftTime { get; set; }

        /// <summary>
        /// 到达医院时间
        /// </summary>
        public DateTime? ArrivedHosTime { get; set; }

        /// <summary>
        /// 出诊医生
        /// </summary>
        [MaxLength(20)]
        public string FmcDoctor { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary>
        [MaxLength(200)]
        public string FirstDiagnosis { get; set; }

        /// <summary>
        /// 送达医院编号
        /// </summary>
        [MaxLength(40)]
        public string ToHospital { get; set; }

        /// <summary>
        /// Killip分级
        /// </summary>
        [MaxLength(40)]
        public string KillipLevel { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        [MaxLength(40)]
        public string Complaint { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        [MaxLength(40)]
        public string Sense { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        [MaxLength(40)]
        public string Temp { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        [MaxLength(40)]
        public string Br { get; set; }

        /// <summary>
        /// 脉搏
        /// </summary>
        [MaxLength(40)]
        public string Plus { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        [MaxLength(40)]
        public string Hr { get; set; }

        /// <summary>
        /// 血压
        /// </summary>
        [MaxLength(40)]
        public string Bp { get; set; }

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        [MaxLength(40)]
        public string Spo2 { get; set; }

        /// <summary>
        /// 血糖
        /// </summary>
        [MaxLength(40)]
        public string BloodSugar { get; set; }

        /// <summary>
        /// 专科检查
        /// </summary>
        [MaxLength(100)]
        public string SpecialCheck { get; set; }

        /// <summary>
        /// 备用1
        /// </summary>
        [MaxLength(40)]
        public string Res1 { get; set; }

        /// <summary>
        /// 备用2
        /// </summary>
        [MaxLength(40)]
        public string Res2 { get; set; }

        /// <summary>
        /// 院前评估
        /// </summary>
        [MaxLength(40)]
        public string Symptom { get; set; }

        // <summary>
        // 通知急诊科时间
        // </summary>
        public DateTime? NotifyEmergency { get; set; }
        /// <summary>
        /// 现场诊断时间
        /// </summary>
        public DateTime? DiagnosisTime { get; set; }
        /// <summary>
        /// 现病史
        /// </summary>
        [MaxLength(200)]
        public string PresentIllness { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        [MaxLength(200)]
        public string Allergichistory { get; set; }
        /// <summary>
        /// 既往史
        /// </summary>
        [MaxLength(100)]
        public string Pasthistory { get; set; }
        /// <summary>
        /// 病情判断
        /// </summary>
        [MaxLength(40)]
        public string Conditiongrade { get; set; }

        /// <summary>
        /// 治疗措施
        /// </summary>
        [MaxLength(500)]
        public string TherapeuticMethod { get; set; }

        /// <summary>
        /// 感染史
        /// </summary>
        [MaxLength(200)]
        public string InfectionHistory { get; set; }
        /// <summary>
        /// 用药史
        /// </summary>
        [MaxLength(200)]
        public string MedicationHistory { get; set; }
        /// <summary>
        /// 窒息复苏史
        /// </summary>
        [MaxLength(200)]
        public string RecoveryHistory { get; set; }
        /// <summary>
        /// 出诊护士
        /// </summary>
        [MaxLength(20)]
        public string NurseName { get; set; }
    }
}
