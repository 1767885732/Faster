using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.KJCHospital.Entities
{
    /// <summary>
    /// 住院登记信息
    /// </summary>
    //[Table("VIEW_INP_PATIENT_MASTER")]
    //[Table(ViewSQL)]

    public class ViewInHosPatientInfo : IEntity<string>
    {     
        //private const string ViewSQL = @"(
        //            SELECT ENC.PATIENTID AS PATIENT_NO, --患者ID
        //                   ENC.PATIENTNAME AS NAME, --患者姓名
        //                   PAT.PATIENTDOB AS DATE_OF_BIRTH, --出生日期
        //                   PAT.PATIENTSEXNAME AS SEX, --性别
        //                   PAT.IDNUMBER AS ID_NO,
        //                   MR.OCCUPATIONNAME AS PAT_JOB,
        //                   MR.ID AS MR_NO, ------------------------
        //                   ENC.MEDICARECARDNO AS CITIZEN_CARD,
        //                   PAT.HOMEADDRESS AS MAIL_ADDRESS,
        //                   PAT.HOMEPHONE AS PHONE_NUMBER,
        //                   PAT.CONTACTPERSONNAME AS NEXT_OF_KIN,
        //                   MR.CONTACTRELATIONSHIPNAME AS RELATIONSHIP,
        //                   PAT.CONTACTPHONE AS NEXT_OF_KIN_PHONE,
        //                   '' AS REGIST_TIME, --挂号时间
        //                   '' AS DEPT_CODE, --挂号科室
        //                   '' AS ARRVIED_DEPT_TIME, --到达门诊科室时间
        //                   '' AS TRIAGE_TIME, --分诊时间
        //                   '' AS DOCTOR, --门诊接诊医生
        //                   '' AS ADMISSTION_TIME, --接诊时间
        //                   DIAG.DOCTORNAME AS DIAG_DOCTOR,
        //                   DIAG.DIAGNOSEDATETIME AS DIAG_TIME,
        //                   '' AS DIAG_DESC, --初步诊断描述
        //                   '' AS COMPLAINT, --患者主诉
        //                   '' AS TRANSFER_TIME --离开门诊科室时间    
        //              FROM (
        //                    SELECT * FROM (
        //                    SELECT ROW_NUMBER() OVER(PARTITION BY HOSPITALCODE,PATIENTID ORDER BY ADMISSIONDATETIME DESC)R, * FROM CDR_ADT.DBO.ADT_IP_ENCOUNTER
        //                    WHERE  ADMISSIONDATETIME>=DATEADD(MONTH,-2,GETDATE())
        //                    )X WHERE X.R=1
        //                    ) ENC
        //             INNER JOIN CDR_ADT.DBO.ADT_IP_PATIENTACOUNT PAT
        //                ON PAT.PATIENTID = ENC.PATIENTID
        //               AND PAT.HOSPITALCODE = ENC.HOSPITALCODE
        //              LEFT JOIN (SELECT ROW_NUMBER() OVER(PARTITION BY PATIENTID, VISITNUMBER ORDER BY DIAGNOSEDATETIME) R,
        //                                *
        //                           FROM CDR_ADT.DBO.ADT_IP_DIAGNOSIS
        //                          WHERE DIAGCATEGORYNAME = '初步诊断') DIAG
        //                ON ENC.VISITNUMBER = DIAG.VISITNUMBER
        //               AND DIAG.HOSPITALCODE = ENC.HOSPITALCODE
        //               AND ENC.PATIENTID = DIAG.PATIENTID
        //               AND DIAG.R = 1
        //              LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
        //                ON ENC.PATIENTID = MR.PATIENTID
        //               AND ENC.VISITNUMBER = MR.VISITNUMBER
        //               AND ENC.HOSPITALCODE = MR.HOSPITALCODE
        //    )";

        /// <summary>
        /// 住院号
        /// </summary>

        [Key]
        [Column("PATIENT_NO")]
        public string Id { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Column("NAME")]
        public string NAME { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Column("DATE_OF_BIRTH")]
        public DateTime? DATE_OF_BIRTH { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Column("SEX")]
        public string SEX { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [Column("ID_NO")]
        public string ID_NO { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [Column("PAT_JOB")]
        public string PAT_JOB { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        [Column("MR_NO")]
        public string MR_NO { get; set; }

        /// <summary>
        /// 市民卡号
        /// </summary>
        [Column("CITIZEN_CARD")]
        public string CITIZEN_CARD { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        [Column("MAIL_ADDRESS")]
        public string MAIL_ADDRESS { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Column("PHONE_NUMBER")]
        public string PHONE_NUMER { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [Column("NEXT_OF_KIN")]
        public string NEXT_OF_KIN { get; set; }

        /// <summary>
        /// 联系人关系
        /// </summary>
        [Column("RELATIONSHIP")]
        public string RELATIONSHIP { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [Column("NEXT_OF_KIN_PHONE")]
        public string NEXT_OF_KIN_PHONE { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        [Column("REGIST_TIME")]
        public DateTime? REGIST_TIME { get; set; }

        /// <summary>
        /// 挂号科室
        /// </summary>
        [Column("DEPT_CODE")]
        public string DEPT_CODE { get; set; }

        /// <summary>
        /// 到达门诊科室时间
        /// </summary>
        [Column("ARRIVED_DEPT_TIME")]
        public DateTime? ARRIVED_DEPT_TIME { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        [Column("TRIAGE_TIME")]
        public DateTime? TRIAGE_TIME { get; set; }

        /// <summary>
        /// 门诊接诊医生
        /// </summary>
        [Column("DIAG_DOCTOR")]
        public string DOCTOR { get; set; }

        /// <summary>
        /// 接诊时间
        /// </summary>
        [Column("ADMISSTION_TIME")]
        public DateTime? ADMISSTION_TIME { get; set; }

        /// <summary>
        /// 初步诊断医生
        /// </summary>
        [Column("DIAG_DOCTOR")]
        public string DIAG_DOCTOR { get; set; }

        /// <summary>
        /// 初步诊断时间
        /// </summary>
        [Column("DIAG_TIME")]
        public DateTime? DIAG_TIME { get; set; }

        /// <summary>
        /// 初步诊断描述
        /// </summary>
        [Column("DIAG_DESC")]
        public string DIAG_DESC { get; set; }

        /// <summary>
        /// 患者主诉
        /// </summary>
        [Column("COMPLAINT")]
        public string COMPLAINT { get; set; }

        /// <summary>
        /// 离开门诊科室时间
        /// </summary>
        [Column("TRANSFER_DEPT_TIME")]
        public DateTime? TRANSFER_TIME { get; set; }
    }
}
