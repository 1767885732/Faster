using System.ComponentModel;

namespace DoCare.Core.Common
{
    public enum EventSource
    {
        [Description("患者信息")]
        PatientBasic = 0,
        [Description("患者His信息")]
        His,
        [Description("患者Pacs信息")]
        Pacs,
        [Description("患者Lis信息")]
        Lis,
        [Description("患者Ecg信息")]
        Ecg
    }

    /// <summary>
    /// 患者来源
    /// </summary>
    public enum PatientSourceType
    {
        [Description("急救")]
        FirstAid = 0,
        [Description("急诊")]
        Emergency = 1,
        [Description("门诊")]
        OutPatient = 2,
        [Description("住院")]
        InPatient = 3
    }

    /// <summary>
    /// 来院方式
    /// </summary>
    public enum ComingType
    {
        [Description("120")]
        FirstAid120 = 1,
        [Description("转院")]
        Transfer = 2,
        [Description("自行来院")]
        Self = 3,
        [Description("院内发病")]
        Inhospital = 4
    }

    /// <summary>
    /// 专科类型
    /// </summary>
    public enum AppType
    {
        [Description("不推送")]
        No = -1,
        [Description("胸痛")]
        ChestPain = 1,
        [Description("卒中")]
        Stroke = 2,
        [Description("创伤")]
        Wound = 3,
        [Description("高危孕产妇")]
        Puerpera = 4,
        [Description("高危新生儿")]
        Newborn = 5,
    }

    public enum ApiConfig
    {
        [Description("麦迪院前服务配置")]
        MdsdFirstAid,
        [Description("院前心电图配置")]
        PreHospitalEcg,
        [Description("院内心电图配置")]
        InHospitalEcg,
        [Description("创伤中心患者推送配置")]
        Wound,
        [Description("高危孕产妇患者推送配置")]
        Puerpera,
        [Description("新生儿推送配置")]
        Newborn
    }

    /// <summary>
    /// 出车单位
    /// </summary>
    public enum DispatchType
    {
        Common120 = 1,
        SelfAmbulance,
        OutAmbulance
    }

    /// <summary>
    /// 心电图来源
    /// </summary>
    public enum EcgSource
    {
        [Description("院前")]
        PreHospital = 1,
        [Description("院内")]
        InHospital
    }

    /// <summary>
    /// 评分来源
    /// </summary>
    public enum ScoreSource
    {
        [Description("院前")]
        PreHospital = 1,
        [Description("院内")]
        InHospital
    }

    /// <summary>
    /// 评分类型
    /// </summary>
    public enum ScoreType
    {
        [Description("GCS评分")]
        GCS,
        [Description("TI评分")]
        TI,
        [Description("RTS评分")]
        RTS,
        [Description("疼痛评分")]
        疼痛,
        [Description("FAST评分")]
        FAST
    }

    public enum MedPatNoType
    {
        /// <summary>
        /// 急救号
        /// </summary>
        [Description("急救号")]
        FirstAidNo = 0,
        /// <summary>
        /// 急诊号
        /// </summary>
        [Description("急诊号")]
        EmergencyNo,
        /// <summary>
        /// 门诊号
        /// </summary>
        [Description("门诊号")]
        ClinicNo,
        /// <summary>
        /// 住院号
        /// </summary>
        [Description("住院号")]
        InPatientNo,
        /// <summary>
        /// 妇幼保健号
        /// </summary>
        [Description("妇幼保健号")]
        MacNo,
        [Description("母亲身份证号")]
        MotherIdCard
    }
}
