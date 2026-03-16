using FastFrame.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoCare.KJCHospital.Entities
{
    /// <summary>
    /// 门诊就诊信息
    /// </summary>
    //[Table("VIEW_PATIENT_MASTER")]

    //[Table(ViewSQL)]
    public class ViewOutHosPatientInfo : EntityBase<string>
    {
        //private const string ViewSQL = @"( SELECT ENC.VISITNumber AS CLINIC_REGISTER_NO, --挂号编号
        //                                           ENC.outpatientNo AS CLINIC_NO, --门诊号
        //                                           ENC.PATIENTID AS PATIENT_ID, --患者ID
        //                                           ENC.PATIENTNAME AS NAME, --患者姓名
        //                                           '' AS NATION,
        //                                           PAT.PATIENTDOB AS DATE_OF_BIRTH, --出生日期
        //                                           PAT.PATIENTSEXNAME AS SEX, --性别
        //                                           PAT.IDNUMBER AS ID_NO,
        //                                           MR.OCCUPATIONNAME AS PAT_JOB,
        //                                           ENC.MEDICARECARDNO AS CARD_ID,
        //                                           PAT.HOMEADDRESS AS MAIL_ADDRESS,
        //                                           PAT.HOMEPHONE AS PHONE_NUMBER,
        //                                           '' AS PATIENT_TYPE,
        //                                           ENC.REGISTERDATETIME AS REGIST_TIME, --挂号时间
        //                                           ENC.DEPTNAME AS DEPT_CODE, --挂号科室
        //                                           ENC.FIRSTINSERTDATETIME AS ARRVIED_DEPT_TIME, --到达门诊科室时间
        //                                           ENC.DATAENTERDATETIME AS TRIAGE_TIME, --分诊时间
        //                                           ENC.DOCTORNAME AS DOCTOR, --门诊接诊医生
        //                                           ENC.FIRSTINSERTDATETIME AS ADMISSTION_TIME, --接诊时间
        //                                           DIAG.DOCTORNAME AS DIAG_DOCTOR,
        //                                           DIAG.DIAGNOSEDATETIME AS DIAG_TIME,
        //                                           '' AS DIAG_DESC, --初步诊断描述
        //                                           '' AS COMPLAINT, --患者主诉
        //                                           ENC.LASTUPDATEDATETIME AS TRANSFER_TIME, --离开门诊科室时间                
        //                                           '1' AS IS_USED, --退费标志。1 有效数据 0 退费数据
        //                                           ENC.MEDICARECATEGORYNAME AS REGISTER_TYPE, --病人挂号类别(自费，医保等)
        //                                           PAT.HOSPITALCODE AS HOSPITAL_AREA --医院院区的属性（描述患者在某一院区挂号标志，可为空）
        //                                      FROM (SELECT *
        //                                              FROM (SELECT ROW_NUMBER() OVER(PARTITION BY HOSPITALCODE, PATIENTID ORDER BY REGISTERDATETIME DESC) R,
        //                                                           *
        //                                                      FROM CDR_ADT.DBO.ADT_OP_ENCOUNTER
        //                                                     WHERE REGISTERDATETIME >= DATEADD(MONTH, -2, GETDATE())) X
        //                                             WHERE X.R = 1) ENC
        //                                     INNER JOIN CDR_ADT.DBO.ADT_OP_PATIENTACOUNT PAT
        //                                        ON PAT.PATIENTID = ENC.PATIENTID
        //                                       AND PAT.HOSPITALCODE = ENC.HOSPITALCODE
        //                                      LEFT JOIN (SELECT ROW_NUMBER() OVER(PARTITION BY PATIENTID, VISITNUMBER ORDER BY DIAGNOSEDATETIME) R,
        //                                                        *
        //                                                   FROM CDR_ADT.DBO.ADT_IP_DIAGNOSIS
        //                                                  WHERE DIAGNO = '0') DIAG
        //                                        ON ENC.VISITNUMBER = DIAG.VISITNUMBER
        //                                       AND DIAG.HOSPITALCODE = ENC.HOSPITALCODE
        //                                       AND ENC.PATIENTID = DIAG.PATIENTID
        //                                       AND DIAG.R = 1
        //                                      LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
        //                                        ON ENC.PATIENTID = MR.PATIENTID
        //                                       AND ENC.VISITNUMBER = MR.VISITNUMBER
        //                                       AND ENC.HOSPITALCODE = MR.HOSPITALCODE
        //                                      LEFT JOIN CDR_ADT.DBO.ADT_OP_ADMISSION AD
        //                                        ON ENC.PATIENTID = AD.PATIENTID
        //                                       AND ENC.VISITNUMBER = AD.VISITNUMBER
        //                                       AND ENC.HOSPITALCODE = AD.HOSPITALCODE
        //                                    WHERE ENC.PATIENTNAME='管达坤')";

       
        /// <summary>
        /// 挂号编号
        /// </summary>
        [Key]
        [Column("CLINIC_REGISTER_NO")]
        public string Id { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        [Column("CLINIC_NO")]
        public string CLINIC_NO { get; set; }

        /// <summary>
        /// 病历档案表主键编号
        /// </summary>
        [Column("PATIENT_ID")]
        public string PATIENT_ID { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Column("NAME")]
        public string NAME { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [Column("NATION")]
        public string NATION { get; set; }

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
        /// 市民卡号
        /// </summary>
        [Column("CARD_ID")]
        public string CARD_ID { get; set; }

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
        /// 患者类别(急诊,门诊)
        /// </summary>
        [Column("PATIENT_TYPE")]
        public string PATIENT_TYPE { get; set; }

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
        [Column("DOCTOR")]
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
        [Column("TRANSFER_TIME")]
        public DateTime? TRANSFER_TIME { get; set; }

        /// <summary>
        /// 退费标志。1 有效数据 0 退费数据
        /// </summary>
        [Column("IS_USED")]
        public string IS_USED { get; set; }

        /// <summary>
        /// 病人挂号类别(自费，医保等)
        /// </summary>
        [Column("REGISTER_TYPE")]
        public string REGISTER_TYPE { get; set; }

        /// <summary>
        /// 医院院区的属性（描述患者在某一院区挂号标志，可为空）
        /// </summary>
        [Column("HOSPITAL_AREA")]
        public string HOSPITAL_AREA { get; set; }

    }
}
