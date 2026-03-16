using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DoCare.KJCHospital.Entities
{
    [XmlRoot("Response")]
    public class ViewPatientInfo_Response
    {
        public string ResultCode { get; set; }
        public string ResultContent { get; set; }
        public ViewPatientInfo_List PatientInfo { get; set; }
    }

    public class ViewPatientInfo_List
    {
        [XmlElement(ElementName = "PatientInfo")]
        public List<ViewPatientInfo> PatientInfoList { get; set; }
    }

    /// <summary>
    /// 就诊信息
    /// </summary>
    public class ViewPatientInfo
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        [XmlElement(ElementName = "PATIENT_NO")]
        public string Id { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string VISIT_NO { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string NATION { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string DATE_OF_BIRTH { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string SEX { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string ID_NO { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string PAT_JOB { get; set; }

        /// <summary>
        /// 市民卡号
        /// </summary>
        public string CARD_ID { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        public string MAIL_ADDRESS { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PHONE_NUMER { get; set; }

        /// <summary>
        /// 患者类别(急诊,门诊)
        /// </summary>
        public string PATIENT_TYPE { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string NEXT_OF_KIN { get; set; }

        /// <summary>
        /// 联系人关系
        /// </summary>
        public string RELATIONSHIP { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string NEXT_OF_KIN_PHONE { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public string REGIST_TIME { get; set; }

        /// <summary>
        /// 挂号科室
        /// </summary>
        public string DEPT_CODE { get; set; }

        /// <summary>
        /// 到达门诊科室时间
        /// </summary>
        public string ARRVIED_DEPT_TIME { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public string TRIAGE_TIME { get; set; }

        /// <summary>
        /// 门诊接诊医生
        /// </summary>
        public string DOCTOR { get; set; }

        /// <summary>
        /// 接诊时间
        /// </summary>
        public string ADMISSTION_TIME { get; set; }

        /// <summary>
        /// 初步诊断医生
        /// </summary>
        public string DIAG_DOCTOR { get; set; }

        /// <summary>
        /// 初步诊断时间
        /// </summary>
        public string DIAG_TIME { get; set; }

        /// <summary>
        /// 初步诊断描述
        /// </summary>
        public string DIAG_DESC { get; set; }

        /// <summary>
        /// 患者主诉
        /// </summary>
        public string COMPLAINT { get; set; }

        /// <summary>
        /// 离开门诊科室时间
        /// </summary>
        public string TRANSFER_TIME { get; set; }

        /// <summary>
        /// 病案号(住院号)
        /// </summary>
        public string MR_NO { get; set; }

        /// <summary>
        /// 医疗付费方式
        /// </summary>
        public string PAY_TYPE { get; set; }
    }
}
