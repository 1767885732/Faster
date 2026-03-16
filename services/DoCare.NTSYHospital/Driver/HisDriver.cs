using DoCare.Core.Patient.Dtos;
using DoCare.NTSYHospital.Entities;
using DoCare.NTSYHospital.service;
using FastFrame.Core;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.NTSYHospital
{
    [Dependency(ServiceLifetime.Scoped)]
    public class HisDriver
    {
        private readonly ILogger<HisDriver> logger;
        private readonly IRepository<ViewOutHosPatientInfo, string> viewOutHosPatientInfo;
        private readonly IRepository<ViewInHosPatientInfo, string> viewInHosPatientInfo;
        private readonly IRepository<ViewPatientInHospital, string> viewPatientInHospital;
        private readonly IRepository<ViewPatientOutHospital, string> viewPatientOutHospital;
        private readonly IRepository<ViewPatientMapInfo, string> viewPatientMapInfo;
        private readonly IRepository<ViewPatientOrders, string> viewPatientOrders;
        private readonly IRepository<ViewExamApply, string> viewExamApply;
        private readonly IRepository<ViewPatientDiagnosis, string> viewPatientDiagnosis;



        public HisDriver(ILogger<HisDriver> logger
            , IRepository<ViewOutHosPatientInfo, string> viewOutHosPatientInfo
            , IRepository<ViewInHosPatientInfo, string> viewInHosPatientInfo
            , IRepository<ViewPatientInHospital, string> viewPatientInHospital
            , IRepository<ViewPatientOutHospital, string> viewPatientOutHospital
            , IRepository<ViewPatientMapInfo, string> viewPatientMapInfo
            , IRepository<ViewPatientOrders, string> viewPatientOrders
            , IRepository<ViewExamApply, string> viewExamApply
            , IRepository<ViewPatientDiagnosis, string> viewPatientDiagnosis
            )
        {
            this.logger = logger;
            this.viewOutHosPatientInfo = viewOutHosPatientInfo;
            this.viewInHosPatientInfo = viewInHosPatientInfo;
            this.viewPatientInHospital = viewPatientInHospital;
            this.viewPatientOutHospital = viewPatientOutHospital;
            this.viewPatientMapInfo = viewPatientMapInfo;
            this.viewPatientOrders = viewPatientOrders;
            this.viewExamApply = viewExamApply;
            this.viewPatientDiagnosis = viewPatientDiagnosis;
        }

        public async Task<List<ViewOutHosPatientInfo>> MatchHisOutHosPatientVM(PatRegistInputDto dto)
        {
            List<ViewOutHosPatientInfo> list = new List<ViewOutHosPatientInfo>();
            try
            {

                #region Sql映射              
                var sqlMapper = @"SELECT ENC.VISITNumber AS  ID , -- CLINIC_REGISTER_NO  挂号编号
                                                                       ENC.outpatientNo AS CLINIC_NO, --门诊号
                                                                       ENC.PATIENTID AS PATIENT_ID, --患者ID
                                                                       ENC.PATIENTNAME AS NAME, --患者姓名
                                                                       '' AS NATION,
                                                                       PAT.PATIENTDOB AS DATE_OF_BIRTH, --出生日期
                                                                       PAT.PATIENTSEXNAME AS SEX, --性别
                                                                       PAT.IDNUMBER AS ID_NO,
                                                                       MR.OCCUPATIONNAME AS PAT_JOB,
                                                                       ENC.MEDICARECARDNO AS CARD_ID,
                                                                       PAT.HOMEADDRESS AS MAIL_ADDRESS,
                                                                       PAT.HOMEPHONE AS PHONE_NUMBER,
                                                                       '' AS PATIENT_TYPE,
                                                                       ENC.REGISTERDATETIME AS REGIST_TIME, --挂号时间
                                                                       ENC.DEPTNAME AS DEPT_CODE, --挂号科室
                                                                       ENC.FIRSTINSERTDATETIME AS ARRVIED_DEPT_TIME, --到达门诊科室时间
                                                                       ENC.DATAENTERDATETIME AS TRIAGE_TIME, --分诊时间
                                                                       ENC.DOCTORNAME AS DOCTOR, --门诊接诊医生
                                                                       ENC.FIRSTINSERTDATETIME AS ADMISSTION_TIME, --接诊时间
                                                                       DIAG.DOCTORNAME AS DIAG_DOCTOR,
                                                                       DIAG.DIAGNOSEDATETIME AS DIAG_TIME,
                                                                       '' AS DIAG_DESC, --初步诊断描述
                                                                       '' AS COMPLAINT, --患者主诉
                                                                       ENC.LASTUPDATEDATETIME AS TRANSFER_TIME, --离开门诊科室时间                
                                                                       '1' AS IS_USED, --退费标志。1 有效数据 0 退费数据
                                                                       ENC.MEDICARECATEGORYNAME AS REGISTER_TYPE, --病人挂号类别(自费，医保等)
                                                                       PAT.HOSPITALCODE AS HOSPITAL_AREA --医院院区的属性（描述患者在某一院区挂号标志，可为空）
                                                                  FROM (SELECT *
                                                                          FROM (SELECT ROW_NUMBER() OVER(PARTITION BY HOSPITALCODE, PATIENTID ORDER BY REGISTERDATETIME DESC) R,
                                                                                       *
                                                                                  FROM CDR_ADT.DBO.ADT_OP_ENCOUNTER
                                                                                 WHERE REGISTERDATETIME >= DATEADD(MONTH, -2, GETDATE())) X
                                                                         WHERE X.R = 1) ENC
                                                                 INNER JOIN CDR_ADT.DBO.ADT_OP_PATIENTACOUNT PAT
                                                                    ON PAT.PATIENTID = ENC.PATIENTID
                                                                   AND PAT.HOSPITALCODE = ENC.HOSPITALCODE
                                                                  LEFT JOIN (SELECT ROW_NUMBER() OVER(PARTITION BY PATIENTID, VISITNUMBER ORDER BY DIAGNOSEDATETIME) R,
                                                                                    *
                                                                               FROM CDR_ADT.DBO.ADT_IP_DIAGNOSIS
                                                                              WHERE DIAGNO = '0') DIAG
                                                                    ON ENC.VISITNUMBER = DIAG.VISITNUMBER
                                                                   AND DIAG.HOSPITALCODE = ENC.HOSPITALCODE
                                                                   AND ENC.PATIENTID = DIAG.PATIENTID
                                                                   AND DIAG.R = 1
                                                                  LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
                                                                    ON ENC.PATIENTID = MR.PATIENTID
                                                                   AND ENC.VISITNUMBER = MR.VISITNUMBER
                                                                   AND ENC.HOSPITALCODE = MR.HOSPITALCODE
                                                                  LEFT JOIN CDR_ADT.DBO.ADT_OP_ADMISSION AD
                                                                    ON ENC.PATIENTID = AD.PATIENTID
                                                                   AND ENC.VISITNUMBER = AD.VISITNUMBER
                                                                   AND ENC.HOSPITALCODE = AD.HOSPITALCODE
                                                                   ";

                var whereStr = " WHERE 1=1 "; //AND ENC.PATIENTNAME = '郑秋金'
                if (!string.IsNullOrEmpty(dto.PatientId))
                {
                    whereStr += string.Format(" AND ENC.PATIENTID='{0}'", dto.PatientId);
                }

                if (!string.IsNullOrEmpty(dto.Name))
                {
                    whereStr += string.Format(" AND ENC.PATIENTNAME='{0}'", dto.Name);
                }

                if (!string.IsNullOrEmpty(dto.IdCard))
                {
                    whereStr += string.Format("AND ENC.MEDICARECARDNO='{0}'", dto.IdCard);
                }

                if (dto.BeginTime.HasValue && dto.BeginTime.Value > DateTime.MinValue && dto.EndTime.HasValue && dto.EndTime.Value > dto.BeginTime.Value)
                {
                    whereStr += string.Format("AND ENC.REGISTERDATETIME BETWEEN  CONVERT(DATETIME,'{0}', 20) AND CONVERT(DATETIME,'{1}', 20)", dto.BeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), dto.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion
                logger.LogInformation("记录门诊SqllastSql\r\n" + lastSql);

                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewOutHosPatientInfo>(lastSql);
                logger.LogInformation("获取到数据");

            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");

            }
            return list.AsQueryable().ToList();

            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取


            //var iquery = viewOutHosPatientInfo.Query().AsQueryable();          

            //if (!string.IsNullOrEmpty(dto.Name))
            //{
            //    iquery = iquery.Where(o => o.NAME.Contains(dto.Name));
            //}

            //if (!string.IsNullOrEmpty(dto.IdCard))
            //{
            //    iquery = iquery.Where(o => o.ID_NO.Contains(dto.IdCard));
            //}

            //if (!string.IsNullOrEmpty(dto.DiagnosisDesc))
            //{
            //    iquery = iquery.Where(o => o.DIAG_DESC.Contains(dto.DiagnosisDesc));
            //}

            //if (dto.BeginTime.HasValue && dto.BeginTime.Value > DateTime.MinValue)
            //{
            //    iquery = iquery.Where(o => o.REGIST_TIME >= dto.BeginTime.Value);
            //}

            //if (dto.EndTime.HasValue && dto.EndTime.Value > dto.BeginTime.Value)
            //{
            //    iquery = iquery.Where(o => o.REGIST_TIME < dto.EndTime.Value);
            //}

            //return iquery.ToList();

            #endregion
        }

        public async Task<List<ViewInHosPatientInfo>> MatchHisInHosPatientVM(PatRegistInputDto dto)
        {
            List<ViewInHosPatientInfo> list = new List<ViewInHosPatientInfo>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT ENC.PATIENTID AS ID, --  PATIENT_NO  患者ID
                                                                               ENC.PATIENTNAME AS NAME, --患者姓名
                                                                               PAT.PATIENTDOB AS DATE_OF_BIRTH, --出生日期
                                                                               PAT.PATIENTSEXNAME AS SEX, --性别
                                                                               PAT.IDNUMBER AS ID_NO,
                                                                               MR.OCCUPATIONNAME AS PAT_JOB,
                                                                               MR.ID AS MR_NO, 
                                                                               ENC.MEDICARECARDNO AS CITIZEN_CARD,
                                                                               PAT.HOMEADDRESS AS MAIL_ADDRESS,
                                                                               PAT.HOMEPHONE AS PHONE_NUMBER,
                                                                               PAT.CONTACTPERSONNAME AS NEXT_OF_KIN,
                                                                               MR.CONTACTRELATIONSHIPNAME AS RELATIONSHIP,
                                                                               PAT.CONTACTPHONE AS NEXT_OF_KIN_PHONE,
                                                                               ENC.DATAENTERDATETIME AS REGIST_TIME, --挂号时间
                                                                               ENC.WARDNAME AS DEPT_CODE, --挂号科室
                                                                               ENC.FIRSTINSERTDATETIME AS ARRVIED_DEPT_TIME, --到达门诊科室时间
                                                                               ENC.DATAENTERDATETIME AS TRIAGE_TIME, --分诊时间
                                                                               '' AS DOCTOR, --门诊接诊医生
                                                                               ENC.FIRSTINSERTDATETIME AS ADMISSTION_TIME, --接诊时间
                                                                               DIAG.DOCTORNAME AS DIAG_DOCTOR,
                                                                               DIAG.DIAGNOSEDATETIME AS DIAG_TIME,
                                                                               '' AS DIAG_DESC, --初步诊断描述
                                                                               '' AS COMPLAINT, --患者主诉
                                                                               ENC.LASTUPDATEDATETIME AS TRANSFER_TIME --离开门诊科室时间    
                                                                          FROM (SELECT *
                                                                                  FROM (SELECT ROW_NUMBER() OVER(PARTITION BY HOSPITALCODE, PATIENTID ORDER BY ADMISSIONDATETIME DESC) R,
                                                                                               *
                                                                                          FROM CDR_ADT.DBO.ADT_IP_ENCOUNTER
                                                                                         WHERE ADMISSIONDATETIME >= DATEADD(MONTH, -2, GETDATE())) X
                                                                                 WHERE X.R = 1) ENC
                                                                         INNER JOIN CDR_ADT.DBO.ADT_IP_PATIENTACOUNT PAT
                                                                            ON PAT.PATIENTID = ENC.PATIENTID
                                                                           AND PAT.HOSPITALCODE = ENC.HOSPITALCODE
                                                                          LEFT JOIN (SELECT ROW_NUMBER() OVER(PARTITION BY PATIENTID, VISITNUMBER ORDER BY DIAGNOSEDATETIME) R,
                                                                                            *
                                                                                       FROM CDR_ADT.DBO.ADT_IP_DIAGNOSIS
                                                                                      WHERE DIAGCATEGORYNAME = '初步诊断') DIAG
                                                                            ON ENC.VISITNUMBER = DIAG.VISITNUMBER
                                                                           AND DIAG.HOSPITALCODE = ENC.HOSPITALCODE
                                                                           AND ENC.PATIENTID = DIAG.PATIENTID
                                                                           AND DIAG.R = 1
                                                                          LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
                                                                            ON ENC.PATIENTID = MR.PATIENTID
                                                                           AND ENC.VISITNUMBER = MR.VISITNUMBER
                                                                           AND ENC.HOSPITALCODE = MR.HOSPITALCODE
                                                                        ";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(dto.PatientId))
                {
                    whereStr += string.Format(" AND ENC.PATIENTID='{0}'", dto.PatientId);
                }

                if (!string.IsNullOrEmpty(dto.Name))
                {
                    whereStr += string.Format(" AND ENC.PATIENTNAME='{0}'", dto.Name);
                }

                if (!string.IsNullOrEmpty(dto.IdCard))
                {
                    whereStr += string.Format("AND ENC.MEDICARECARDNO='{0}'", dto.IdCard);
                }

                if (dto.BeginTime.HasValue && dto.BeginTime.Value > DateTime.MinValue && dto.EndTime.HasValue && dto.EndTime.Value > dto.BeginTime.Value)
                {
                    whereStr += string.Format("AND ENC.ADMISSIONDATETIME BETWEEN  CONVERT(DATETIME,'{0}', 20) AND CONVERT(DATETIME,'{1}', 20)", dto.BeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), dto.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("记录住院SqllastSql\r\n" + lastSql);

                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewInHosPatientInfo>(lastSql);
                logger.LogInformation("获取到数据");

            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");

            }
            return list.AsQueryable().ToList();

            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //var iquery = viewInHosPatientInfo.Query().AsQueryable();
            //if (!string.IsNullOrEmpty(dto.PatientId))
            //{
            //    iquery = iquery.Where(o => o.Id.Contains(dto.PatientId));
            //}

            //if (!string.IsNullOrEmpty(dto.Name))
            //{
            //    iquery = iquery.Where(o => o.NAME.Contains(dto.Name));
            //}

            //if (!string.IsNullOrEmpty(dto.IdCard))
            //{
            //    iquery = iquery.Where(o => o.ID_NO.Contains(dto.IdCard));
            //}

            //if (!string.IsNullOrEmpty(dto.DiagnosisDesc))
            //{
            //    iquery = iquery.Where(o => o.DIAG_DESC.Contains(dto.DiagnosisDesc));
            //}

            //if (dto.BeginTime.HasValue && dto.BeginTime.Value > DateTime.MinValue)
            //{
            //    iquery = iquery.Where(o => o.REGIST_TIME >= dto.BeginTime.Value);
            //}

            //if (dto.EndTime.HasValue && dto.EndTime.Value > dto.BeginTime.Value)
            //{
            //    iquery = iquery.Where(o => o.REGIST_TIME < dto.EndTime.Value);
            //}

            //return iquery.ToList();
            #endregion
        }

        public async Task<ViewOutHosPatientInfo> GetViewOutHosPatientInfo(string registId)
        {
            List<ViewOutHosPatientInfo> list = new List<ViewOutHosPatientInfo>();
            try
            {

                #region Sql映射              
                var sqlMapper = @"SELECT ENC.VISITNumber AS CLINIC_REGISTER_NO, --挂号编号
                                                                       ENC.outpatientNo AS CLINIC_NO, --门诊号
                                                                       ENC.PATIENTID AS PATIENT_ID, --患者ID
                                                                       ENC.PATIENTNAME AS NAME, --患者姓名
                                                                       '' AS NATION,
                                                                       PAT.PATIENTDOB AS DATE_OF_BIRTH, --出生日期
                                                                       PAT.PATIENTSEXNAME AS SEX, --性别
                                                                       PAT.IDNUMBER AS ID_NO,
                                                                       MR.OCCUPATIONNAME AS PAT_JOB,
                                                                       ENC.MEDICARECARDNO AS CARD_ID,
                                                                       PAT.HOMEADDRESS AS MAIL_ADDRESS,
                                                                       PAT.HOMEPHONE AS PHONE_NUMBER,
                                                                       '' AS PATIENT_TYPE,
                                                                       ENC.REGISTERDATETIME AS REGIST_TIME, --挂号时间
                                                                       ENC.DEPTNAME AS DEPT_CODE, --挂号科室
                                                                       ENC.FIRSTINSERTDATETIME AS ARRVIED_DEPT_TIME, --到达门诊科室时间
                                                                       ENC.DATAENTERDATETIME AS TRIAGE_TIME, --分诊时间
                                                                       ENC.DOCTORNAME AS DOCTOR, --门诊接诊医生
                                                                       ENC.FIRSTINSERTDATETIME AS ADMISSTION_TIME, --接诊时间
                                                                       DIAG.DOCTORNAME AS DIAG_DOCTOR,
                                                                       DIAG.DIAGNOSEDATETIME AS DIAG_TIME,
                                                                       '' AS DIAG_DESC, --初步诊断描述
                                                                       '' AS COMPLAINT, --患者主诉
                                                                       ENC.LASTUPDATEDATETIME AS TRANSFER_TIME, --离开门诊科室时间                
                                                                       '1' AS IS_USED, --退费标志。1 有效数据 0 退费数据
                                                                       ENC.MEDICARECATEGORYNAME AS REGISTER_TYPE, --病人挂号类别(自费，医保等)
                                                                       PAT.HOSPITALCODE AS HOSPITAL_AREA --医院院区的属性（描述患者在某一院区挂号标志，可为空）
                                                                  FROM (SELECT *
                                                                          FROM (SELECT ROW_NUMBER() OVER(PARTITION BY HOSPITALCODE, PATIENTID ORDER BY REGISTERDATETIME DESC) R,
                                                                                       *
                                                                                  FROM CDR_ADT.DBO.ADT_OP_ENCOUNTER
                                                                                 WHERE REGISTERDATETIME >= DATEADD(MONTH, -2, GETDATE())) X
                                                                         WHERE X.R = 1) ENC
                                                                 INNER JOIN CDR_ADT.DBO.ADT_OP_PATIENTACOUNT PAT
                                                                    ON PAT.PATIENTID = ENC.PATIENTID
                                                                   AND PAT.HOSPITALCODE = ENC.HOSPITALCODE
                                                                  LEFT JOIN (SELECT ROW_NUMBER() OVER(PARTITION BY PATIENTID, VISITNUMBER ORDER BY DIAGNOSEDATETIME) R,
                                                                                    *
                                                                               FROM CDR_ADT.DBO.ADT_IP_DIAGNOSIS
                                                                              WHERE DIAGNO = '0') DIAG
                                                                    ON ENC.VISITNUMBER = DIAG.VISITNUMBER
                                                                   AND DIAG.HOSPITALCODE = ENC.HOSPITALCODE
                                                                   AND ENC.PATIENTID = DIAG.PATIENTID
                                                                   AND DIAG.R = 1
                                                                  LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
                                                                    ON ENC.PATIENTID = MR.PATIENTID
                                                                   AND ENC.VISITNUMBER = MR.VISITNUMBER
                                                                   AND ENC.HOSPITALCODE = MR.HOSPITALCODE
                                                                  LEFT JOIN CDR_ADT.DBO.ADT_OP_ADMISSION AD
                                                                    ON ENC.PATIENTID = AD.PATIENTID
                                                                   AND ENC.VISITNUMBER = AD.VISITNUMBER
                                                                   AND ENC.HOSPITALCODE = AD.HOSPITALCODE ";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(registId))
                {
                    whereStr += string.Format(" AND ENC.PATIENTID='{0}'", registId);
                }

                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion
                logger.LogInformation("记录门诊SqllastSql\r\n" + lastSql);

                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewOutHosPatientInfo>(lastSql);
                logger.LogInformation("获取到数据");

            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");

            }
            return list.FirstOrDefault();

            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //ViewOutHosPatientInfo viewPatientInfo = viewOutHosPatientInfo.Query().Where(o => o.Id == registId).FirstOrDefault();
            //return viewPatientInfo;
            #endregion
        }

        public async Task<ViewInHosPatientInfo> GetViewInHosPatientInfo(string inpRegistId)
        {
            List<ViewInHosPatientInfo> list = new List<ViewInHosPatientInfo>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT ENC.PATIENTID AS PATIENT_NO, --患者ID
                                        ENC.PATIENTNAME AS NAME, --患者姓名
                                        PAT.PATIENTDOB AS DATE_OF_BIRTH, --出生日期
                                        PAT.PATIENTSEXNAME AS SEX, --性别
                                        PAT.IDNUMBER AS ID_NO,
                                        MR.OCCUPATIONNAME AS PAT_JOB,
                                        MR.ID AS MR_NO, 
                                        ENC.MEDICARECARDNO AS CITIZEN_CARD,
                                        PAT.HOMEADDRESS AS MAIL_ADDRESS,
                                        PAT.HOMEPHONE AS PHONE_NUMBER,
                                        PAT.CONTACTPERSONNAME AS NEXT_OF_KIN,
                                        MR.CONTACTRELATIONSHIPNAME AS RELATIONSHIP,
                                        PAT.CONTACTPHONE AS NEXT_OF_KIN_PHONE,
                                        ENC.DATAENTERDATETIME AS REGIST_TIME, --挂号时间
                                        ENC.WARDNAME AS DEPT_CODE, --挂号科室
                                        ENC.FIRSTINSERTDATETIME AS ARRVIED_DEPT_TIME, --到达门诊科室时间
                                        ENC.DATAENTERDATETIME AS TRIAGE_TIME, --分诊时间
                                        '' AS DOCTOR, --门诊接诊医生
                                        ENC.FIRSTINSERTDATETIME AS ADMISSTION_TIME, --接诊时间
                                        DIAG.DOCTORNAME AS DIAG_DOCTOR,
                                        DIAG.DIAGNOSEDATETIME AS DIAG_TIME,
                                        '' AS DIAG_DESC, --初步诊断描述
                                        '' AS COMPLAINT, --患者主诉
                                        ENC.LASTUPDATEDATETIME AS TRANSFER_TIME --离开门诊科室时间    
                                    FROM (SELECT *
                                            FROM (SELECT ROW_NUMBER() OVER(PARTITION BY HOSPITALCODE, PATIENTID ORDER BY ADMISSIONDATETIME DESC) R,
                                                        *
                                                    FROM CDR_ADT.DBO.ADT_IP_ENCOUNTER
                                                    WHERE ADMISSIONDATETIME >= DATEADD(MONTH, -2, GETDATE())) X
                                            WHERE X.R = 1) ENC
                                    INNER JOIN CDR_ADT.DBO.ADT_IP_PATIENTACOUNT PAT
                                    ON PAT.PATIENTID = ENC.PATIENTID
                                    AND PAT.HOSPITALCODE = ENC.HOSPITALCODE
                                    LEFT JOIN (SELECT ROW_NUMBER() OVER(PARTITION BY PATIENTID, VISITNUMBER ORDER BY DIAGNOSEDATETIME) R,
                                                    *
                                                FROM CDR_ADT.DBO.ADT_IP_DIAGNOSIS
                                                WHERE DIAGCATEGORYNAME = '初步诊断') DIAG
                                    ON ENC.VISITNUMBER = DIAG.VISITNUMBER
                                    AND DIAG.HOSPITALCODE = ENC.HOSPITALCODE
                                    AND ENC.PATIENTID = DIAG.PATIENTID
                                    AND DIAG.R = 1
                                    LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
                                    ON ENC.PATIENTID = MR.PATIENTID
                                    AND ENC.VISITNUMBER = MR.VISITNUMBER
                                    AND ENC.HOSPITALCODE = MR.HOSPITALCODE";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(inpRegistId))
                {
                    whereStr += string.Format(" AND ENC.PATIENTID='{0}'", inpRegistId);
                }

                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("记录住院SqllastSql\r\n" + lastSql);

                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewInHosPatientInfo>(lastSql);
                logger.LogInformation("获取到数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");

            }
            return list.FirstOrDefault();

            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //ViewInHosPatientInfo viewPatientInfo = viewInHosPatientInfo.Query().Where(o => o.Id == inpRegistId).FirstOrDefault();
            //return viewPatientInfo;
            #endregion

        }

        public async Task<ViewPatientInHospital> GetViewPatientInHospital(string inpRegistId)
        {
            //获取患者住院记录
            List<ViewPatientInHospital> list = new List<ViewPatientInHospital>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT PATIENTID                AS PATIENT_NO,
                                           ADMISSIONDATETIME        AS IN_HOS_TIME,
                                           ADMISSIONSERVICEUNITCODE AS DEPT_CODE,
                                           ADMISSIONWARDCODE        AS WARD_CODE,
                                           ADMISSIONDIAGNAME        AS IN_HOS_DIAGNOSIS,
                                           ADMISSIONWARDDATETIME    AS IN_DEPT_TIME,
                                           BEDNO                    AS BED_NO,
                                           ATTENDINGDOCTORNAME      AS DOCTOR
                                      FROM CDR_ADT.DBO.ADT_IP_ENCOUNTER WITH(NOLOCK)
                                     WHERE STATUSNAME IN ('入院', '入区')";

                var whereStr = "  ";
                if (!string.IsNullOrEmpty(inpRegistId))
                {
                    whereStr += string.Format(" AND PATIENTID='{0}'", inpRegistId);
                }

                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("记录获取患者住院记录SqllastSql\r\n" + lastSql);

                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewPatientInHospital>(lastSql);
                logger.LogInformation("获取到数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");

            }
            return list.FirstOrDefault();


            #region  Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //ViewPatientInHospital entity = viewPatientInHospital.Query().Where(o => o.INP_NO == inpRegistId).FirstOrDefault();
            //return entity;
            #endregion

        }

        public async Task<ViewPatientOutHospital> GetViewPatientOutHospital(string inpRegistId)
        {
            //获取患者出院记录
            List<ViewPatientOutHospital> list = new List<ViewPatientOutHospital>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT ENC.PATIENTID AS PATIENT_NO,
                                           ENC.DISCHARGEWARDDATETIME AS OUT_DEPT_TIME,
                                           ENC.DISCHARGEDATETIME AS OUT_HOS_TIME,
                                           ENC.DISCHARGEDIAGNAME AS OUT_DIAGNOSIS,
                                           MR.LOS AS HOS_DAY,
                                           DFT.TOTALAMOUNT AS TOTAL_COST,
                                           (DFT.WESTERNMEDICINEAMOUNT + DFT.TCMPREPARATIONMEDICINEAMOUNT +
                                           TCMHERBALMEDICINEAMOUNT) AS DRUG_COST,
                                           (EXAMINATIONMAERTIALAMOUNT + IMAGEDIAGAMOUNT) AS EXAM_COST,
                                           DISCHARGEDISPOSTIONNAME AS OUT_WAY
                                      FROM CDR_ADT.DBO.ADT_IP_ENCOUNTER ENC
                                      LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
                                        ON ENC.HOSPITALCODE = MR.HOSPITALCODE
                                       AND ENC.VISITNUMBER = MR.VISITNUMBER
                                       AND ENC.PATIENTID = MR.PATIENTID
                                      LEFT JOIN CDR_MR.DBO.MR_DFT DFT
                                        ON MR.PATIENTID = DFT.PATIENTID
                                       AND MR.VISITNUMBER = DFT.VISITNUMBER
                                       AND ENC.HOSPITALCODE = MR.HOSPITALCODE";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(inpRegistId))
                {
                    whereStr += string.Format(" AND ENC.PATIENTID='{0}'", inpRegistId);
                }

                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("记录获取患者出院记录SqllastSql\r\n" + lastSql);

                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewPatientOutHospital>(lastSql);
                logger.LogInformation("获取到数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");

            }
            return list.FirstOrDefault();



            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //ViewPatientOutHospital entity = viewPatientOutHospital.Query().Where(o => o.INP_NO == inpRegistId).FirstOrDefault();
            //return entity;
            #endregion
        }

        public async Task<ViewPatientMapInfo> GetViewPatientMapInfoByRegistId(string registId)
        {
            ViewPatientMapInfo mapInfo = viewPatientMapInfo.Query().Where(o => o.Id == registId).FirstOrDefault();
            return mapInfo;
        }     

        public async Task<List<ViewPatientOrders>> GetViewPatientOrdersByRegistId(string registId)
        {
            List<ViewPatientOrders> list = new List<ViewPatientOrders>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT A.PATIENTID AS PATIENT_ID,
                                           A.PRESCRIPTIONNO AS ORDER_NO, --父医嘱号、组号
                                           B.ID AS ORDER_SUB_NO, --子医嘱号
                                           0 AS REPEAT_INDICATOR, --医嘱标志：1为长期医嘱，0为临时医嘱
                                           B.DRUGCATEGORYNAME AS ORDER_CLASS, --医嘱类别
                                           B.DRUGNAME AS ORDER_TEXT, --医嘱正文
                                           B.DRUGCODE AS ORDER_CODE, --医嘱代码
                                           DOSAGE AS DOSAGE, --药品一次使用剂量
                                           DOSAGEUNIT AS DOSAGE_UNITS, --剂量单位
                                           B.ROUTENAME AS ADMINISTRATION, --给药途径
                                           A.PRESCRIBEDATE AS START_DATE_TIME, --医嘱起始日期及时间
                                           A.PRESCRIBEDATE AS STOP_DATE_TIME, --医嘱停止日期及时间
                                           B.DURATION AS DURATION, --持续时间
                                           'day' DURATION_UNITS, --持续时间单位
                                           B.FREQUENCYNAME AS FREQUENCY,
                                           A.DEPTNAME AS ORDERING_DEPT,
                                           A.DOCTORNAME AS DOCTOR,
                                           '' AS STOP_DOCTOR,
                                           '' AS NURSE, --开医嘱校对护士
                                           '' AS STOP_NURSE, --停医嘱校对护士
                                            A.PRESCRIBEDATE AS ENTER_DATE_TIME,
                                            A.PRESCRIBEDATE AS STOP_ORDER_DATE_TIME,
                                           A.STATUSNAME AS ORDER_STATUS,
                                           A.PRESCRIPTIONNO AS PRESCRIPTION_NO
                                      FROM CDR_ORDER.DBO.ORDER_OP_DRUG A
                                     INNER JOIN CDR_ORDER.DBO.ORDER_OP_DRUGITEM B
                                        ON A.PRESCRIPTIONNO = B.PRESCRIPTIONNO
                                       AND B.HOSPITALCODE = A.HOSPITALCODE";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(registId))
                {
                    whereStr += string.Format(" AND A.PATIENTID='{0}'", registId);
                }


                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("获取检查信息GetViewExamApplySql\r\n" + lastSql);

                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewPatientOrders>(lastSql);
                logger.LogInformation("获取到检查数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");
            }
            return list.AsQueryable().ToList();

            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //return viewPatientOrders.Query().Where(o => o.CLINIC_REGIST_NO == registId).ToList();
            #endregion

        }

        public async Task<List<ViewPatientOrders>> GetViewPatientOrdersByInp(string inpRegistId)
        {
            List<ViewPatientOrders> list = new List<ViewPatientOrders>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT A.PATIENTID AS PATIENT_ID,
                                           B.ORDERGROUPNO AS ORDER_NO,
                                           A.ORDERNO AS ORDER_SUB_NO,
                                           CASE ORDERTYPENAME
                                             WHEN '长期' THEN
                                              1
                                             WHEN '临时' THEN
                                              0
                                             ELSE
                                              9
                                           END AS REPEAT_INDICATOR,
                                           ORDERCATEGORYNAME AS ORDER_CLASS,
                                           '' AS ORDER_TEXT, --医嘱正文
                                           '' AS ORDER_CODE, --医嘱代码
                                           DOSAGE AS DOSAGE,
                                           DOSAGEUNIT AS DOSAGE_UNITS,
                                           ROUTENAME AS ADMINISTRATION,
                                           A.STARTDATETIME AS START_DATE_TIME,
                                           A.STOPDATETIME AS STOP_DATE_TIME,
                                           DATEDIFF(DAY, A.STOPDATETIME, A.STARTDATETIME) AS DURATION,
                                           'day' AS DURATION_UNITS,
                                           FREQUENCYNAME AS FREQUENCY,
                                           A.DEPTNAME AS ORDERING_DEPT,
                                           A.DOCTORNAME AS DOCTOR,
                                           A.STOPDOCTORNAME AS STOP_DOCTOR,
                                           A.AUDITNURSENAME AS NURSE,
                                           A.AUDITNURSENAME AS STOP_NURSE, --停医嘱校对护士
                                           A.ORDERDATETIME AS ENTER_DATE_TIME, --医嘱录入的当前日期及时间
                                           A.ORDERDATETIME AS STOP_ORDER_DATE_TIME, --停医嘱录入的当前日期及时间
                                           A.STATUSNAME AS ORDER_STATUS,
                                           '' AS PRESCRIPTION_NO
                                      FROM CDR_ORDER.DBO.ORDER_IP_ORDER A
                                     INNER JOIN CDR_ORDER.DBO.ORDER_IP_ORDERITEM B
                                        ON A.ORDERNO = B.ORDERNO
                                       AND B.HOSPITALCODE = A.HOSPITALCODE";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(inpRegistId))
                {
                    whereStr += string.Format(" AND A.PATIENTID='{0}'", inpRegistId);
                }
                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogInformation("获取检查信息GetViewPatientOrdersByInp\r\n" + lastSql);

                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewPatientOrders>(lastSql);
                logger.LogInformation("获取到检查数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");
            }
            return list.AsQueryable().ToList();

            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //return viewPatientOrders.Query().Where(o => o.INP_NO == inpRegistId).ToList();
            #endregion
        }


        public async Task<List<ViewExamApply>> GetViewExamApply(string registId)
        {
            List<ViewExamApply> list = new List<ViewExamApply>();
            try
            {
                #region Sql映射              
                var sqlMapper = @"SELECT ITEM.REQUESTNO AS EXAM_NO,
                                               REP.PATIENTID AS Id,
                                               REP.STUDYCLASSNAME AS EXAM_CLASS,
                                               REP.STUDYSUBCLASSNAME AS EXAM_SUB_CLASS,
                                               REP.CLINICALDIAGINFO AS CLIN_DIAG,
                                               REP.STUDYMETHODNAME AS EXAM_MODE,
                                               REP.INSTRUMENT AS DEVICE,
                                               REP.EXECDEPTNAME AS PERFORMED_BY,
                                               REP.REQUESTDATETIME AS REQ_DATE_TIME,
                                               REP.DOCTORDEPTNAME AS REQ_DEPT,
                                               REP.DOCTORNAME AS REQ_PHYSICIAN,
                                               '' AS REQ_MEMO, --  申请备注
                                               '' AS NOTICE, --注意事项
                                               REP.STUDYOPERATEDATETIME AS EXAM_START_DATE,
                                               REP.STUDYOPERATEDATETIME AS EXAM_END_DATE,
                                               REP.STUDYREPORTDATETIME AS REPORT_DATE_TIME,
                                               ITEM.STUDYSITENAME AS EXAM_PARA,
                                               REP.IMAGINGFINDINGS AS DESCRIPTION,
                                               REP.STUDYCONCLUSION AS RECOMMENDATION,
                                               REP.STUDYOPERATERNAME AS TECHNICIAN,
                                               REP.STUDYREPORTERNAME AS REPORTER,
                                               REP.STATUSNAME AS RESULT_STATUS,
                                               REP.STUDYREVIEWERNAME AS VERIFIED_BY,
                                               REP.STUDYREVIEWDATETIME AS VERIFIED_DATE_TIME
                                          FROM CDR_RIS.DBO.RIS_REPORT REP
                                          LEFT JOIN CDR_RIS.DBO.RIS_REPORTITEM ITEM
                                            ON ITEM.REPORTNO = REP.REPORTNO
                                           AND ITEM.HOSPITALCODE = REP.HOSPITALCODE";

                var whereStr = " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(registId))
                {
                    whereStr += string.Format(" AND REP.PATIENTID='{0}'", registId);
                }


                var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

                #endregion

                logger.LogError("获取检查信息GetViewExamApplySql\r\n" + lastSql);
                list = DBContextFactory.GetContext().Ado.SqlQuery<ViewExamApply>(lastSql);
                logger.LogError("获取到检查数据");
            }
            catch (Exception ex)
            {
                logger.LogError($"sqlsugar获取数据异常，{ex.Message}");
            }
            return list.AsQueryable().ToList();

            #region Old使用Ef进行数据映射获取数据，改成上面的使用sqlSurge获取
            //return viewExamApply.Query().Where(o => o.Id == registId).ToList();
            #endregion

        }

        public async Task<List<ViewPatientDiagnosis>> GetViewPatientDiagnosis(string clinicNo)
        {
            return viewPatientDiagnosis.Query().Where(o => o.CLINIC_REGIST_NO == clinicNo).ToList();
        }
    }
}
