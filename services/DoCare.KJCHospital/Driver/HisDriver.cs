using Dapper;
using DoCare.Core.Common;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using DoCare.KJCHospital.Entities;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.Entity;
using HisWebService;
using JJRM;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DoCare.KJCHospital.Driver
{
    [Dependency(ServiceLifetime.Scoped)]
    public class HisDriver
    {
        private readonly ILogger<HisDriver> logger;
        //private readonly PUB0020SoapClient hisClient;
        private readonly JJRMQueryPortTypeClient hisClient;
        private readonly IRepository<ViewPatientDiagnosis, string> viewPatientDiagnosis;
        //private readonly JJRM2.JJRMQueryPortTypeClient hisClient2;

        public HisDriver(ILogger<HisDriver> logger
            , IRepository<ViewPatientDiagnosis, string> viewPatientDiagnosis
             )
        {
            this.logger = logger;

            this.hisClient = new JJRMQueryPortTypeClient(JJRMQueryPortTypeClient.EndpointConfiguration.JJRMQueryHttpSoap11Endpoint);
            //this.hisClient2 = new JJRM2.JJRMQueryPortTypeClient(JJRM2.JJRMQueryPortTypeClient.EndpointConfiguration.JJRMQueryHttpSoap11Endpoint);
            this.viewPatientDiagnosis = viewPatientDiagnosis;
        }
        public async Task<List<ViewPatientDiagnosis>> GetViewPatientDiagnosis(string clinicNo)
        {
            return viewPatientDiagnosis.Query().Where(o => o.CLINIC_REGIST_NO == clinicNo).ToList();
        }

        public async Task<List<ViewPatientInfo>> MatchHisPatientVM(PatRegistInputDto dto, PatientSourceType patientSourceType)
        {
            List<ViewPatientInfo> list = new List<ViewPatientInfo>();
            IConfiguration configuration = Singleton<IConfiguration>.Instance;
            string pgconnectionString = configuration["ConnectionStrings:PgSQLDbConn"];

            using (IDbConnection connection = new NpgsqlConnection(pgconnectionString))
            {
                var visitType = patientSourceType == PatientSourceType.InPatient ? "I" : "O";
                var startTime = dto.BeginTime.Value.ToStringYMD();
                var endTime = dto.EndTime.Value.ToStringYMD();
                var nameCondition = string.IsNullOrEmpty(dto.Name) ? string.Empty : "and name=@patname";

                string sql = "select * from(" +
                    $"select ROW_NUMBER() OVER(PARTITION BY patient_id ORDER BY regist_time DESC) rn,* from specialized_center.specialized_center_patvisit" +
                    $" where visit_type = '{visitType}' and regist_time>='{startTime}' and regist_time<'{endTime}' {nameCondition} " +
                    $") a where a.rn = 1";
                DynamicParameters Parameters = new DynamicParameters();
                Parameters.Add("@patname", string.IsNullOrEmpty(dto.Name) ? string.Empty : dto.Name);
                var query = connection.Query<MedHisUserModel>(sql, Parameters, commandTimeout: 1000);
                logger.LogDebug($"[HisDriver.MatchHisPatientVM]关联,sql :'{sql}',参数：'{Parameters}',结果：'{query.ToList()}' ");
                var userList = query.ToList();
                if (userList != null && userList.Count > 0)
                {
                    for (int i = 0; i < userList.Count; i++)
                    {
                        var res = new ViewPatientInfo();
                        res.Id = userList[i].patient_id;
                        if (patientSourceType == PatientSourceType.InPatient)
                        {
                            res.MR_NO = userList[i].visit_id;
                        }
                        else
                        {
                            res.VISIT_NO = userList[i].visit_id;
                        }
                        res.NAME = userList[i].name;
                        res.DATE_OF_BIRTH = userList[i].date_of_birth;
                        res.SEX = userList[i].sex;
                        res.ID_NO = userList[i].id_no;
                        res.PAT_JOB = userList[i].pat_job;
                        res.MAIL_ADDRESS = userList[i].mail_address;
                        res.PHONE_NUMER = userList[i].phone_numer;
                        res.PATIENT_TYPE = userList[i].visit_type;
                        res.NEXT_OF_KIN = userList[i].next_of_kin;
                        res.RELATIONSHIP = userList[i].relationship;
                        res.NEXT_OF_KIN_PHONE = userList[i].next_of_kin_phone;
                        res.REGIST_TIME = userList[i].regist_time;
                        res.DEPT_CODE = userList[i].dept_code;
                        res.DIAG_DESC = userList[i].diag_desc;
                        if(res.DEPT_CODE.Equals("0207")|| res.DEPT_CODE.Equals("0248"))
                        {
                            //门诊、住院只关注科室神经内一、神经内二的患者
                            list.Add(res);
                        }
                    }
                }
                return list;
            }
        }

        public async Task<ViewOutHosPatientInfo> GetViewOutHosPatientInfo(string registId)
        {
            List<ViewOutHosPatientInfo> list = new List<ViewOutHosPatientInfo>();
            //try
            //{

            //    #region Sql映射              
            //    var sqlMapper = @"SELECT ENC.VISITNumber AS CLINIC_REGISTER_NO, --挂号编号
            //                                                           ENC.outpatientNo AS CLINIC_NO, --门诊号
            //                                                           ENC.PATIENTID AS PATIENT_ID, --患者ID
            //                                                           ENC.PATIENTNAME AS NAME, --患者姓名
            //                                                           '' AS NATION,
            //                                                           PAT.PATIENTDOB AS DATE_OF_BIRTH, --出生日期
            //                                                           PAT.PATIENTSEXNAME AS SEX, --性别
            //                                                           PAT.IDNUMBER AS ID_NO,
            //                                                           MR.OCCUPATIONNAME AS PAT_JOB,
            //                                                           ENC.MEDICARECARDNO AS CARD_ID,
            //                                                           PAT.HOMEADDRESS AS MAIL_ADDRESS,
            //                                                           PAT.HOMEPHONE AS PHONE_NUMBER,
            //                                                           '' AS PATIENT_TYPE,
            //                                                           ENC.REGISTERDATETIME AS REGIST_TIME, --挂号时间
            //                                                           ENC.DEPTNAME AS DEPT_CODE, --挂号科室
            //                                                           ENC.FIRSTINSERTDATETIME AS ARRVIED_DEPT_TIME, --到达门诊科室时间
            //                                                           ENC.DATAENTERDATETIME AS TRIAGE_TIME, --分诊时间
            //                                                           ENC.DOCTORNAME AS DOCTOR, --门诊接诊医生
            //                                                           ENC.FIRSTINSERTDATETIME AS ADMISSTION_TIME, --接诊时间
            //                                                           DIAG.DOCTORNAME AS DIAG_DOCTOR,
            //                                                           DIAG.DIAGNOSEDATETIME AS DIAG_TIME,
            //                                                           '' AS DIAG_DESC, --初步诊断描述
            //                                                           '' AS COMPLAINT, --患者主诉
            //                                                           ENC.LASTUPDATEDATETIME AS TRANSFER_TIME, --离开门诊科室时间                
            //                                                           '1' AS IS_USED, --退费标志。1 有效数据 0 退费数据
            //                                                           ENC.MEDICARECATEGORYNAME AS REGISTER_TYPE, --病人挂号类别(自费，医保等)
            //                                                           PAT.HOSPITALCODE AS HOSPITAL_AREA --医院院区的属性（描述患者在某一院区挂号标志，可为空）
            //                                                      FROM (SELECT *
            //                                                              FROM (SELECT ROW_NUMBER() OVER(PARTITION BY HOSPITALCODE, PATIENTID ORDER BY REGISTERDATETIME DESC) R,
            //                                                                           *
            //                                                                      FROM CDR_ADT.DBO.ADT_OP_ENCOUNTER
            //                                                                     WHERE REGISTERDATETIME >= DATEADD(MONTH, -2, GETDATE())) X
            //                                                             WHERE X.R = 1) ENC
            //                                                     INNER JOIN CDR_ADT.DBO.ADT_OP_PATIENTACOUNT PAT
            //                                                        ON PAT.PATIENTID = ENC.PATIENTID
            //                                                       AND PAT.HOSPITALCODE = ENC.HOSPITALCODE
            //                                                      LEFT JOIN (SELECT ROW_NUMBER() OVER(PARTITION BY PATIENTID, VISITNUMBER ORDER BY DIAGNOSEDATETIME) R,
            //                                                                        *
            //                                                                   FROM CDR_ADT.DBO.ADT_IP_DIAGNOSIS
            //                                                                  WHERE DIAGNO = '0') DIAG
            //                                                        ON ENC.VISITNUMBER = DIAG.VISITNUMBER
            //                                                       AND DIAG.HOSPITALCODE = ENC.HOSPITALCODE
            //                                                       AND ENC.PATIENTID = DIAG.PATIENTID
            //                                                       AND DIAG.R = 1
            //                                                      LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
            //                                                        ON ENC.PATIENTID = MR.PATIENTID
            //                                                       AND ENC.VISITNUMBER = MR.VISITNUMBER
            //                                                       AND ENC.HOSPITALCODE = MR.HOSPITALCODE
            //                                                      LEFT JOIN CDR_ADT.DBO.ADT_OP_ADMISSION AD
            //                                                        ON ENC.PATIENTID = AD.PATIENTID
            //                                                       AND ENC.VISITNUMBER = AD.VISITNUMBER
            //                                                       AND ENC.HOSPITALCODE = AD.HOSPITALCODE ";

            //    var whereStr = " WHERE 1=1 ";
            //    if (!string.IsNullOrEmpty(registId))
            //    {
            //        whereStr += string.Format(" AND ENC.PATIENTID='{0}'", registId);
            //    }

            //    var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

            //    #endregion
            //    logger.LogInformation("记录门诊SqllastSql\r\n" + lastSql);

            //    list = DBContextFactory.GetContext().Ado.SqlQuery<ViewOutHosPatientInfo>(lastSql);
            //    logger.LogInformation("获取到数据");

            //}
            //catch (Exception ex)
            //{
            //    logger.LogError($"sqlsugar获取数据异常，{ex.Message}");

            //}
            return list.FirstOrDefault();
        }

        public async Task<ViewInHosPatientInfo> GetViewInHosPatientInfo(string inpRegistId)
        {
            List<ViewInHosPatientInfo> list = new List<ViewInHosPatientInfo>();
            //try
            //{
            //    #region Sql映射              
            //    var sqlMapper = @"SELECT ENC.PATIENTID AS PATIENT_NO, --患者ID
            //                            ENC.PATIENTNAME AS NAME, --患者姓名
            //                            PAT.PATIENTDOB AS DATE_OF_BIRTH, --出生日期
            //                            PAT.PATIENTSEXNAME AS SEX, --性别
            //                            PAT.IDNUMBER AS ID_NO,
            //                            MR.OCCUPATIONNAME AS PAT_JOB,
            //                            MR.ID AS MR_NO, 
            //                            ENC.MEDICARECARDNO AS CITIZEN_CARD,
            //                            PAT.HOMEADDRESS AS MAIL_ADDRESS,
            //                            PAT.HOMEPHONE AS PHONE_NUMBER,
            //                            PAT.CONTACTPERSONNAME AS NEXT_OF_KIN,
            //                            MR.CONTACTRELATIONSHIPNAME AS RELATIONSHIP,
            //                            PAT.CONTACTPHONE AS NEXT_OF_KIN_PHONE,
            //                            ENC.DATAENTERDATETIME AS REGIST_TIME, --挂号时间
            //                            ENC.WARDNAME AS DEPT_CODE, --挂号科室
            //                            ENC.FIRSTINSERTDATETIME AS ARRVIED_DEPT_TIME, --到达门诊科室时间
            //                            ENC.DATAENTERDATETIME AS TRIAGE_TIME, --分诊时间
            //                            '' AS DOCTOR, --门诊接诊医生
            //                            ENC.FIRSTINSERTDATETIME AS ADMISSTION_TIME, --接诊时间
            //                            DIAG.DOCTORNAME AS DIAG_DOCTOR,
            //                            DIAG.DIAGNOSEDATETIME AS DIAG_TIME,
            //                            '' AS DIAG_DESC, --初步诊断描述
            //                            '' AS COMPLAINT, --患者主诉
            //                            ENC.LASTUPDATEDATETIME AS TRANSFER_TIME --离开门诊科室时间    
            //                        FROM (SELECT *
            //                                FROM (SELECT ROW_NUMBER() OVER(PARTITION BY HOSPITALCODE, PATIENTID ORDER BY ADMISSIONDATETIME DESC) R,
            //                                            *
            //                                        FROM CDR_ADT.DBO.ADT_IP_ENCOUNTER
            //                                        WHERE ADMISSIONDATETIME >= DATEADD(MONTH, -2, GETDATE())) X
            //                                WHERE X.R = 1) ENC
            //                        INNER JOIN CDR_ADT.DBO.ADT_IP_PATIENTACOUNT PAT
            //                        ON PAT.PATIENTID = ENC.PATIENTID
            //                        AND PAT.HOSPITALCODE = ENC.HOSPITALCODE
            //                        LEFT JOIN (SELECT ROW_NUMBER() OVER(PARTITION BY PATIENTID, VISITNUMBER ORDER BY DIAGNOSEDATETIME) R,
            //                                        *
            //                                    FROM CDR_ADT.DBO.ADT_IP_DIAGNOSIS
            //                                    WHERE DIAGCATEGORYNAME = '初步诊断') DIAG
            //                        ON ENC.VISITNUMBER = DIAG.VISITNUMBER
            //                        AND DIAG.HOSPITALCODE = ENC.HOSPITALCODE
            //                        AND ENC.PATIENTID = DIAG.PATIENTID
            //                        AND DIAG.R = 1
            //                        LEFT JOIN CDR_MR.DBO.MR_MAINRECORD MR
            //                        ON ENC.PATIENTID = MR.PATIENTID
            //                        AND ENC.VISITNUMBER = MR.VISITNUMBER
            //                        AND ENC.HOSPITALCODE = MR.HOSPITALCODE";

            //    var whereStr = " WHERE 1=1 ";
            //    if (!string.IsNullOrEmpty(inpRegistId))
            //    {
            //        whereStr += string.Format(" AND ENC.PATIENTID='{0}'", inpRegistId);
            //    }

            //    var lastSql = string.Format("{0} {1}", sqlMapper, whereStr);

            //    #endregion

            //    logger.LogInformation("记录住院SqllastSql\r\n" + lastSql);

            //    list = DBContextFactory.GetContext().Ado.SqlQuery<ViewInHosPatientInfo>(lastSql);
            //    logger.LogInformation("获取到数据");
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError($"sqlsugar获取数据异常，{ex.Message}");

            //}
            return list.FirstOrDefault();
        }

        public async Task<ViewPatientMapInfo> GetViewPatientMapInfoByRegistId(string patientid, string visitid, int type)
        {
            #region 注释
            //ViewPatientMapInfo mapInfo = viewPatientMapInfo.Query().Where(o => o.Id == registId).FirstOrDefault();
            //return mapInfo;
            #endregion

            //初始化
            XmlDocument doc = new XmlDocument();
            DateTime datetime;
            decimal dc;
            ViewPatientMapInfo model = new ViewPatientMapInfo();

            //调用接口
            string XmlString = "<Request><Header>" +
                               "<SourceSystem>ZKZX</SourceSystem>" +
                               "<MessageID>" + Guid.NewGuid().ToString() + "</MessageID>" +
                               "<CreateTime>" + DateTime.Now.ToString() + "</CreateTime>" +
                               "</Header><Body>" +
                               "<VisitId>" + visitid + "</VisitId>" +
                               "<PatientId>" + patientid + "</PatientId>" +
                               "<Type>" + type + "</Type>" +
                               "</Body></Request>";

            logger.LogError(XmlString);
            var res = hisClient.GetVPatientMapInfoAsync(XmlString);
            doc.LoadXml(res.Result.Body.payload);
            logger.LogError(res.Result.Body.payload);

            XmlNodeList paraList = doc["Response"].ChildNodes;

            //转换实体
            foreach (XmlNode node in paraList)
            {
                if (type == 1)
                {
                    model.INP_NO = node["INP_NO"].InnerText.Trim();
                }
                else
                {
                    model.Id = node["CLINIC_NO"].InnerText.Trim();
                }
                if (DateTime.TryParse(node["REGIST_TIME"].InnerText.Trim(), out datetime))
                {
                    model.REGIST_TIME = datetime;
                }
            }
            return model;
        }

        /// <summary>
        /// 获取患者基本信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<ViewPatientInfo> GetViewPatientInfo(string patientId)
        {
            ViewPatientInfo info = new ViewPatientInfo();
            XmlDocument doc = new XmlDocument();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<Request>");
            stringBuilder.Append("<Header>");

            stringBuilder.Append("<SourceSystem>");
            stringBuilder.Append("HIS");
            stringBuilder.Append("</SourceSystem>");

            stringBuilder.Append("<MessageID>");
            stringBuilder.Append(System.Guid.NewGuid().ToString());
            stringBuilder.Append("</MessageID>");

            stringBuilder.Append("<CreateTime>");
            stringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            stringBuilder.Append("</CreateTime>");

            stringBuilder.Append("</Header>");

            stringBuilder.Append("<Body>");

            stringBuilder.Append("<PatientID>");
            stringBuilder.Append(patientId);
            stringBuilder.Append("</PatientID>");

            stringBuilder.Append("<semanticsText>");
            stringBuilder.Append("PATIENTID");
            stringBuilder.Append("</semanticsText>");

            stringBuilder.Append("</Body>");
            stringBuilder.Append("</Request>");

            string strXml = stringBuilder.ToString();
            logger.LogInformation("开始调用服务getpatientinfo...");
            logger.LogInformation($"入参：{strXml}");
            var res = hisClient.getpatientinfoAsync(strXml);
            if (res != null)
            {
                if (res.Result != null)
                {
                    if (res.Result.Body != null)
                    {
                        doc.LoadXml(res.Result.Body.payload);
                        XmlNode node = doc.SelectSingleNode("Response/Body/ResultContent");
                        if (node != null)
                        {
                            XmlNodeList nodeList = node.ChildNodes;
                            foreach (XmlNode n in nodeList)
                            {
                                if (n.Name.Equals("INMEDNO"))
                                {
                                    info.MR_NO = n.InnerText.Trim();
                                }
                                if (n.Name.Equals("INPNO"))
                                {
                                    info.VISIT_NO = n.InnerText.Trim();
                                }
                                if (n.Name.Equals("PHONE"))
                                {
                                    info.PHONE_NUMER = n.InnerText.Trim();
                                }
                                if (n.Name.Equals("CONPERNAME"))
                                {
                                    info.NEXT_OF_KIN = n.InnerText.Trim();
                                }
                                if (n.Name.Equals("HCISSUERSCODE"))
                                {
                                    info.PAY_TYPE = n.InnerText.Trim();
                                }
                                if (n.Name.Equals("CONPERPHONE"))
                                {
                                    info.NEXT_OF_KIN_PHONE = n.InnerText.Trim();
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.LogInformation("Result.Body为空");
                    }
                }
                else
                {
                    logger.LogInformation("Result为空");
                }
            }
            else
            {
                logger.LogInformation("返回为空");
            }
            return info;
        }

        /// <summary>
        /// 获取就诊详情
        /// </summary>
        /// <param name="registId"></param>
        /// <returns></returns>
        public async Task<ViewPatientInfo> GetViewPatientInfo(string serialNo, PatientSourceType patientSourceType)
        {
            IConfiguration configuration = Singleton<IConfiguration>.Instance;
            string pgconnectionString = configuration["ConnectionStrings:PgSQLDbConn"];

            var res = new ViewPatientInfo();
            using (IDbConnection connection = new NpgsqlConnection(pgconnectionString))
            {
                string sql = "";
                if (patientSourceType == PatientSourceType.InPatient)
                {
                    sql = $"SELECT * FROM specialized_center.specialized_center_patvisit where visit_type='I' and visit_id=@inpat_no order by regist_time desc limit 1";
                }
                else
                {
                    sql = $"SELECT * FROM specialized_center.specialized_center_patvisit where visit_type='O' and visit_id=@inpat_no order by regist_time desc limit 1";
                }
                DynamicParameters Parameters = new DynamicParameters();
                Parameters.Add("@inpat_no", serialNo);
                var query = connection.Query<MedHisUserModel>(sql, Parameters, commandTimeout: 1000);
                logger.LogDebug($"[HisDriver.GetViewPatientInfo]关联,sql :'{sql}',参数：'{Parameters}',结果：'{query.ToList()}' ");
                var userList = query.ToList();
                if (userList != null && userList.Count > 0)
                {
                    var usermodel = userList[0];
                    res.Id = usermodel.patient_id;
                    res.VISIT_NO = patientSourceType == PatientSourceType.OutPatient ? usermodel.clinic_no : "";
                    res.MR_NO = patientSourceType == PatientSourceType.InPatient ? usermodel.inpat_no : "";
                    res.NAME = usermodel.name;
                    res.DATE_OF_BIRTH = usermodel.date_of_birth;
                    res.SEX = usermodel.sex == "女性" ? "女" : "男";
                    res.ID_NO = usermodel.id_no;
                    res.PAT_JOB = usermodel.pat_job;
                    res.MAIL_ADDRESS = usermodel.mail_address;
                    res.PHONE_NUMER = usermodel.phone_numer;
                    res.PATIENT_TYPE = usermodel.visit_type;
                    res.NEXT_OF_KIN = usermodel.next_of_kin;
                    res.RELATIONSHIP = usermodel.relationship;
                    res.NEXT_OF_KIN_PHONE = usermodel.next_of_kin_phone;
                    res.REGIST_TIME = usermodel.regist_time;
                    res.DEPT_CODE = usermodel.dept_code;
                    res.CARD_ID = usermodel.citizen_card;
                }
                return res;
            }
        }

        /// <summary>
        /// 获取住院详情
        /// </summary>
        /// <param name="inpNo"></param>
        /// <returns></returns>
        public async Task<ViewPatientInHospital> GetViewPatientInHospital(string serialNo)
        {
            #region 注释
            //ViewPatientInHospital entity = null;
            //string requestNo = "MES0082";
            //string requestXml = $"<Request><PATIENT_NO>{serialNo}</PATIENT_NO></Request>";
            //string responseXml = await RequestWebservice(requestNo, requestXml);
            //if (!string.IsNullOrEmpty(responseXml))
            //{
            //    var hisResponse = SerializeHelper.FromXml<ViewPatientInHospital_Response>(responseXml);
            //    if (hisResponse?.PatientInHospital != null)
            //    {
            //        entity = hisResponse.PatientInHospital;
            //    }
            //}
            //return entity;
            #endregion

            //初始化
            XmlDocument doc = new XmlDocument();
            DateTime datetime;
            ViewPatientInHospital model = new ViewPatientInHospital();

            //调用接口
            string XmlString = "<Request><Header>" +
                               "<SourceSystem>ZKZX</SourceSystem>" +
                               "<MessageID>" + Guid.NewGuid().ToString() + "</MessageID>" +
                               "<CreateTime>" + DateTime.Now.ToString() + "</CreateTime>" +
                               "</Header><Body>" +
                               "<VisitId>" + serialNo + "</VisitId>" +
                               "</Body></Request>";

            logger.LogInformation("开始调用服务GetVPatientInHospital...");
            var res = hisClient.GetVPatientInHospitalAsync(XmlString);
            doc.LoadXml(res.Result.Body.payload);
            logger.LogInformation("返回："+ res.Result.Body.payload);
            XmlNodeList paraList = doc["Response"].ChildNodes;

            //转换实体
            foreach (XmlNode node in paraList)
            {
                if (node.Name.Equals("data"))
                {
                    model.Id = node["VISIT_ID"].InnerText.Trim();
                    if (DateTime.TryParse(node["IN_TIME"].InnerText.Trim(), out datetime))
                    {
                        model.IN_HOS_TIME = datetime.ToString();
                    }
                    model.DEPT_CODE = node["IN_DEPT_CODE"].InnerText.Trim();
                    model.WARD_CODE = node["IN_WARD_CODE"].InnerText.Trim();
                    model.IN_HOS_DIAGNOSIS = node["IN_DIAG"].InnerText.Trim();
                    model.IN_DEPT_TIME = node["IN_DEPT_TIME"].InnerText.Trim();
                    model.BED_NO = node["BED_NO"].InnerText.Trim();
                    model.DOCTOR = node["DOC_NAME"].InnerText.Trim();
                }
            }
            return model;
        }

        /// <summary>
        /// 获取出院详情
        /// </summary>
        /// <param name="inpNo"></param>
        /// <returns></returns>
        public async Task<ViewPatientOutHospital> GetViewPatientOutHospital(string patid, string visitid)
        {
            #region 注释
            //ViewPatientOutHospital entity = null;
            //string requestNo = "MES0088";
            //string requestXml = $"<Request><PATIENT_NO>{serialNo}</PATIENT_NO></Request>";
            //string responseXml = await RequestWebservice(requestNo, requestXml);
            //if (!string.IsNullOrEmpty(responseXml))
            //{
            //    var hisResponse = SerializeHelper.FromXml<ViewPatientOutHospital_Response>(responseXml);
            //    if (hisResponse?.PatientInOutHospital?.PatientInOutHospitalList != null)
            //    {
            //        entity = hisResponse.PatientInOutHospital.PatientInOutHospitalList.FirstOrDefault();
            //    }
            //}
            //return entity;
            #endregion

            //初始化
            XmlDocument doc = new XmlDocument();
            DateTime datetime;
            decimal dc;
            ViewPatientOutHospital model = new ViewPatientOutHospital();

            //调用接口
            string XmlString = "<Request><Header>" +
                               "<SourceSystem>ZKZX</SourceSystem>" +
                               "<MessageID>" + Guid.NewGuid().ToString() + "</MessageID>" +
                               "<CreateTime>" + DateTime.Now.ToString() + "</CreateTime>" +
                               "</Header><Body>" +
                               "<VisitId>" + visitid + "</VisitId>" +
                                "<PatientId>" + patid + "</PatientId>" +
                               "</Body></Request>";

            logger.LogInformation("开始调用服务GetVPatientOutHospital...");
            logger.LogInformation($"入参：{XmlString}");
            var res = hisClient.GetVPatientOutHospitalAsync(XmlString);
            if (res != null)
            {
                if (res.Result != null)
                {
                    if (res.Result.Body != null)
                    {
                        logger.LogInformation($"返回：{res.Result.Body.payload}");
                        doc.LoadXml(res.Result.Body.payload);
                        XmlNodeList paraList = doc["Response"].ChildNodes;

                        //转换实体（取最大的visitid）
                        var visitidTemp = 0;
                        foreach (XmlNode node in paraList)
                        {
                            if (node.Name.Equals("date"))
                            {
                                var patidTemp = node["PATIENT_ID"].InnerText.Trim();
                                var visitnum = Convert.ToInt32(node["VISIT_ID"].InnerText.Trim().Substring(patidTemp.Length));
                                if (visitnum > visitidTemp)
                                {
                                    visitidTemp = visitnum;
                                    model.Id = node["VISIT_ID"].InnerText.Trim();
                                    if (DateTime.TryParse(node["OUT_DEPT_TIME"].InnerText.Trim(), out datetime)) model.OUT_DEPT_TIME = datetime.ToString();
                                    if (DateTime.TryParse(node["OUT_HOS_TIME"].InnerText.Trim(), out datetime)) model.OUT_HOS_TIME = datetime.ToString();
                                    model.OUT_DIAGNOSIS = node["OUT_DIAGNOSIS"].InnerText.Trim();
                                    if (decimal.TryParse(node["IN_DAYS"].InnerText.Trim(), out dc)) model.HOS_DAY = dc.ToString();
                                    if (decimal.TryParse(node["TOTAL_COST"].InnerText.Trim(), out dc)) model.TOTAL_COST = dc.ToString();
                                    if (decimal.TryParse(node["DRUG_COST"].InnerText.Trim(), out dc)) model.DRUG_COST = dc.ToString();
                                    if (decimal.TryParse(node["CHECK_COST"].InnerText.Trim(), out dc)) model.EXAM_COST = dc.ToString();
                                    model.OUT_WAY = node["OUT_WAY"].InnerText.Trim();
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.LogInformation("Result.Body为空");
                    }
                }
                else
                {
                    logger.LogInformation("Result为空");
                }
            }
            else
            {
                logger.LogInformation("返回为空");
            }
            return model;
        }

        /// <summary>
        /// 获取用药信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ViewPatientOrders>> GetViewPatientOrders(string patientid, string visitid)
        {
            #region 注释
            //List<ViewPatientOrders> orderList = new List<ViewPatientOrders>();
            //string requestNo = "MES0083";
            //string requestXml = $"<Request><PATIENT_NO>{serialNo}</PATIENT_NO></Request>";
            //string responseXml = await RequestWebservice(requestNo, requestXml);
            //if (!string.IsNullOrEmpty(responseXml))
            //{
            //    var hisResponse = SerializeHelper.FromXml<ViewPatientOrders_Response>(responseXml);
            //    if (hisResponse?.PatientOrdersList?.PatientOrders != null)
            //    {
            //        orderList = hisResponse.PatientOrdersList?.PatientOrders;
            //        if (orderList != null && orderList.Count > 0 && patientSourceType == PatientSourceType.OutPatient)
            //        {
            //            orderList.ForEach(o => o.ORDER_NO = o.PRESCRIPTION_NO);
            //        }
            //    }
            //}
            //return orderList;
            #endregion

            //初始化
            XmlDocument doc = new XmlDocument();
            DateTime datetime;
            decimal dc;
            List<ViewPatientOrders> list = new List<ViewPatientOrders>();

            //调用接口
            string XmlString = "<Request><Header>" +
                               "<SourceSystem>ZKZX</SourceSystem>" +
                               "<MessageID>" + Guid.NewGuid().ToString() + "</MessageID>" +
                               "<CreateTime>" + DateTime.Now.ToString() + "</CreateTime>" +
                               "</Header><Body>" +
                               "<VisitId>" + visitid + "</VisitId>" +
                               "<PatientId>" + patientid + "</PatientId>" +
                               "<StartTime>" + DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss") + "</StartTime>" +
                               "<EndTime>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</EndTime>" +
                               "</Body></Request>";
            var res = hisClient.GetVPatientOrdersAsync(XmlString);
            doc.LoadXml(res.Result.Body.payload);
            XmlNodeList paraList = doc["Response"].ChildNodes;

            //转换实体
            foreach (XmlNode node in paraList)
            {
                if (node.Name.Equals("Orders"))
                {
                    var model = new ViewPatientOrders();
                    model.PATIENT_NO = node["PATIENT_ID"].InnerText.Trim();
                    model.ORDER_NO = node["ORDER_GROUP_NO"].InnerText.Trim();
                    model.ORDER_SUB_NO = node["ORDER_NO"].InnerText.Trim();
                    model.ORDER_CLASS = node["ORDER_CALSS"].InnerText.Trim();
                    model.ORDER_TEXT = node["ORDER_NAME"].InnerText.Trim();
                    model.ORDER_CODE = node["ORDER_CODE"].InnerText.Trim();
                    model.DOSAGE_UNITS = node["DOSE_UNIT"].InnerText.Trim();
                    model.ADMINISTRATION = node["USAGE_NAME"].InnerText.Trim();
                    model.DURATION = node["DURATION"].InnerText.Trim();
                    model.DURATION_UNITS = node["DURATION_UNITS"].InnerText.Trim();
                    model.FREQUENCY = node["FREQUENCY_NAME"].InnerText.Trim();
                    model.ORDERING_DEPT = node["INPUT_DEPT_CODE"].InnerText.Trim();
                    model.DOCTOR = node["INPUT_DOC_CODE"].InnerText.Trim();
                    model.STOP_DOCTOR = node["INPUT_DOC_CODE"].InnerText.Trim();
                    model.NURSE = node["INPUT_CONFIRM_NURSE"].InnerText.Trim();
                    model.STOP_NURSE = node["STOP_CONFIRM_NURSE"].InnerText.Trim();
                    if (DateTime.TryParse(node["STOP_ORDER_DATE_TIME"].InnerText.Trim(), out datetime)) model.STOP_ORDER_DATE_TIME = datetime.ToString();
                    model.ORDER_STATUS = node["STATUS"].InnerText.Trim();
                    if (DateTime.TryParse(node["ENTER_DATE_TIME"].InnerText.Trim(), out datetime)) model.ENTER_DATE_TIME = datetime.ToString();
                    if (DateTime.TryParse(node["START_DATE_TIME"].InnerText.Trim(), out datetime)) model.START_DATE_TIME = datetime.ToString();
                    if (DateTime.TryParse(node["STOP_DATE_TIME"].InnerText.Trim(), out datetime)) model.STOP_DATE_TIME = datetime.ToString();
                    if (decimal.TryParse(node["ONCE_DOSE"].InnerText.Trim(), out dc)) model.DOSAGE = dc.ToString();
                    if (decimal.TryParse(node["ONCE_DOSE"].InnerText.Trim(), out dc)) model.TOTAL_AMOUNT = dc.ToString();
                    if (node["ORDER_FLAG"].InnerText.Trim() == "A")
                    {
                        model.REPEAT_INDICATOR = "1";
                    }
                    else
                    {
                        model.REPEAT_INDICATOR = "0";
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取检验申请单
        /// </summary>
        /// <returns></returns>
        public async Task<List<ViewLabMaster>> GetViewLabMaster(string serialNo)
        {
            #region 注释
            //List<ViewLabMaster> labMasterList = new List<ViewLabMaster>();
            //string requestNo = "MES0086";
            //string requestXml = $"<Request><PATIENT_NO>{serialNo}</PATIENT_NO></Request>";
            //string responseXml = await RequestWebservice(requestNo, requestXml);
            //if (!string.IsNullOrEmpty(responseXml))
            //{
            //    var hisResponse = SerializeHelper.FromXml<ViewLabMaster_Response>(responseXml);
            //    if (hisResponse?.LabMasterList?.LabMaster != null)
            //    {
            //        labMasterList = hisResponse.LabMasterList?.LabMaster;
            //    }
            //}
            //return labMasterList;
            #endregion

            //初始化
            XmlDocument doc = new XmlDocument();
            DateTime datetime;
            decimal dc;
            List<ViewLabMaster> list = new List<ViewLabMaster>();

            //调用接口
            string XmlString = "<Request><Header>" +
                               "<SourceSystem>ZKZX</SourceSystem>" +
                               "<MessageID>" + Guid.NewGuid().ToString() + "</MessageID>" +
                               "<CreateTime>" + DateTime.Now.ToString() + "</CreateTime>" +
                               "</Header><Body>" +
                               "<PatientId>" + serialNo + "</PatientId>" +
                               "<StartTime>" + DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd HH:mm:ss") + "</StartTime>" +
                               "<EndTime>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</EndTime>" +
                               "</Body></Request>";
            logger.LogDebug($"获取检验申请单GetViewLabMaster接口参数:{XmlString}");
            var res = hisClient.GetVLabMasterAsync(XmlString);
            doc.LoadXml(res.Result.Body.payload);
            XmlNodeList paraList = doc["Response"].ChildNodes;

            //转换实体
            foreach (XmlNode node in paraList)
            {
                if (node.Name.Equals("date"))
                {
                    var model = new ViewLabMaster();
                    model.Id = node["TEST_ITEM_NO"].InnerText.Trim();
                    model.PRIORITY_INDICATOR = node["PRIORITY_INDICATOR"].InnerText.Trim();
                    model.PATIENT_NO = node["PATIENT_ID"].InnerText.Trim();
                    model.NAME = node["PAT_NAME"].InnerText.Trim();
                    model.TEST_CAUSE = node["INSPECTION_CATEGORY"].InnerText.Trim();
                    model.RELEVANT_CLINIC_DIAG = node["CLINIC_DIAG"].InnerText.Trim();
                    model.SPECIMEN = node["INSPECTION_SPECIMEN"].InnerText.Trim();
                    if (DateTime.TryParse(node["INSPECTION_SPECIMENDATE"].InnerText.Trim(), out datetime)) model.SPCM_RECEIVED_DATE_TIME = datetime.ToString();
                    if (DateTime.TryParse(node["APPLY_TIME"].InnerText.Trim(), out datetime)) model.APPLY_TIME = datetime.ToString();
                    model.ORDERING_DEPT = node["INSPECTION_APPLYDEPT"].InnerText.Trim();
                    model.ORDERING_PROVIDER = node["INSPECTION_APPLYDOCTOR"].InnerText.Trim();
                    model.PERFORMED_BY = node["INSPECTION_DEPT"].InnerText.Trim();
                    model.RESULT_STATUS = node["INSPECTION_STATUS"].InnerText.Trim();
                    if (DateTime.TryParse(node["INSPECTION_RPTDATE"].InnerText.Trim(), out datetime)) model.RESULTS_RPT_DATE_TIME = datetime.ToString();
                    model.TRANSCRIPTIONIST = node["INSPECTION_RPTBY"].InnerText.Trim();
                    model.VERIFIED_BY = node["INSPECTION_AUDITORBY"].InnerText.Trim();
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取检验申请单
        /// </summary>
        /// <returns></returns>
        public async Task<List<ViewLabResult>> GetViewLabResult(string testNo)
        {
            #region 注释
            //List<ViewLabResult> labResultList = new List<ViewLabResult>();
            //string requestNo = "MES0085";
            //string requestXml = $"<Request><TEST_NO>{testNo}</TEST_NO></Request>";
            //string responseXml = await RequestWebservice(requestNo, requestXml);
            //if (!string.IsNullOrEmpty(responseXml))
            //{
            //    var hisResponse = SerializeHelper.FromXml<ViewLabResult_Response>(responseXml);
            //    if (hisResponse?.LabResultList?.LabResult != null)
            //    {
            //        labResultList = hisResponse.LabResultList?.LabResult;
            //    }
            //}
            //return labResultList;
            #endregion

            //初始化
            XmlDocument doc = new XmlDocument();
            DateTime datetime;
            decimal dc;
            var list = new List<ViewLabResult>();

            //调用接口
            string XmlString = "<Request><Header>" +
                               "<SourceSystem>ZKZX</SourceSystem>" +
                               "<MessageID>" + Guid.NewGuid().ToString() + "</MessageID>" +
                               "<CreateTime>" + DateTime.Now.ToString() + "</CreateTime>" +
                               "</Header><Body>" +
                               "<TestNo>" + testNo + "</TestNo>" +
                               "</Body></Request>";
            var res = hisClient.GetVLabResultAsync(XmlString);
            logger.LogDebug($"获取检验申请单GetViewLabResult接口参数:{XmlString}");
            doc.LoadXml(res.Result.Body.payload);
            XmlNodeList paraList = doc["Response"].ChildNodes;

            //转换实体
            foreach (XmlNode node in paraList)
            {
                if (node.Name.Equals("date"))
                {
                    var model = new ViewLabResult();
                    model.TEST_NO = node["TEST_ITEM_NO"].InnerText.Trim();
                    model.REPORT_ITEM_NAME = node["REPORT_ITEM_NAME"].InnerText.Trim();
                    model.REPORT_ITEM_CODE = node["REPORT_ITEM_CODE"].InnerText.Trim();
                    model.ABNORMAL_INDICATOR = node["REPORT_ABNORMAL"].InnerText.Trim();
                    model.RESULT = node["REPORT_RESULT"].InnerText.Trim();
                    model.UNITS = node["REPORT_UNITS"].InnerText.Trim();
                    if (DateTime.TryParse(node["TEST_TIME"].InnerText.Trim(), out datetime)) model.RESULT_DATE_TIME = datetime.ToString();
                    model.REFERENCE_RESULT = node["INSPECTION_REFRANGE"].InnerText.Trim();
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取检查报告
        /// </summary>
        /// <returns></returns>
        public async Task<List<ViewExamReport>> GetViewExamReport(string serialNo)
        {
            #region 注释
            //List<ViewExamReport> examReportList = new List<ViewExamReport>();
            //string requestNo = "MES0084";
            //string requestXml = $"<Request><PATIENT_NO>{serialNo}</PATIENT_NO></Request>";
            //string responseXml = await RequestWebservice(requestNo, requestXml);
            //if (!string.IsNullOrEmpty(responseXml))
            //{
            //    var hisResponse = SerializeHelper.FromXml<ViewExamReport_Response>(responseXml);
            //    if (hisResponse?.ExamReportList?.ExamReport != null)
            //    {
            //        examReportList = hisResponse.ExamReportList?.ExamReport;
            //    }
            //}
            //return examReportList;
            #endregion

            //初始化
            XmlDocument doc = new XmlDocument();
            DateTime datetime;
            decimal dc;
            List<ViewExamReport> list = new List<ViewExamReport>();

            //调用接口
            string XmlString = "<Request><Header>" +
                               "<SourceSystem>ZKZX</SourceSystem>" +
                               "<MessageID>" + Guid.NewGuid().ToString() + "</MessageID>" +
                               "<CreateTime>" + DateTime.Now.ToString() + "</CreateTime>" +
                               "</Header><Body>" +
                               "<PatientId>" + serialNo + "</PatientId>" +
                               "<StartTime>" + DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd") + "</StartTime>" +
                               "<EndTime>" + DateTime.Now.ToString("yyyy-MM-dd") + "</EndTime>" +
                               "</Body></Request>";

            logger.LogError(XmlString);
            var res = hisClient.GetVExamReportAsync(XmlString);
            doc.LoadXml(res.Result.Body.payload);
            logger.LogError(res.Result.Body.payload);
            XmlNodeList paraList = doc["Response"].ChildNodes;

            //转换实体
            foreach (XmlNode node in paraList)
            {
                if (node.Name.Equals("date"))
                {
                    var model = new ViewExamReport();
                    model.PATIENT_NO = node["PATIENT_ID"].InnerText.Trim();
                    model.EXAM_NO = node["EXAM_NO"].InnerText.Trim();
                    model.EXAM_CLASS = node["EXAM_CLASS"].InnerText.Trim();
                    model.EXAM_SUB_CLASS = node["EXAM_SUB_CLASS"].InnerText.Trim();
                    model.CLIN_DIAG = node["CLIN_DIAG"].InnerText.Trim();
                    model.EXAM_MODE = node["EXAM_MODE"].InnerText.Trim();
                    model.DEVICE = node["DEVICE"].InnerText.Trim();
                    model.PERFORMED_BY = node["PERFORMED_BY"].InnerText.Trim();
                    if (DateTime.TryParse(node["REQ_DATE_TIME"].InnerText.Trim(), out datetime)) model.REQ_DATE_TIME = datetime.ToString();
                    model.REQ_DEPT = node["REQ_DEPT"].InnerText.Trim();
                    model.REQ_PHYSICIAN = node["REQ_PHYSICIAN"].InnerText.Trim();
                    model.REQ_MEMO = node["REQ_MEMO"].InnerText.Trim();
                    model.NOTICE = node["NOTICE"].InnerText.Trim();
                    if (DateTime.TryParse(node["EXAM_START_TIME"].InnerText.Trim(), out datetime)) model.EXAM_START_DATE = datetime.ToString();
                    if (DateTime.TryParse(node["EXAM_END_TIME"].InnerText.Trim(), out datetime)) model.EXAM_END_DATE = datetime.ToString();
                    model.EXAM_PARA = node["EXAM_PARA"].InnerText.Trim();
                    model.DESCRIPTION = node["DESCRIPTION"].InnerText.Trim();
                    model.RECOMMENDATION = node["RECOMMENDATION"].InnerText.Trim();
                    model.TECHNICIAN = node["TECHNICIAN"].InnerText.Trim();
                    model.REPORTER = node["REPORTER"].InnerText.Trim();
                    model.RESULT_STATUS = node["RESULT_STATUS"].InnerText.Trim();
                    model.VERIFIED_BY = node["VERIFIED_BY"].InnerText.Trim();
                    if (DateTime.TryParse(node["REPORT_DATE_TIME"].InnerText.Trim(), out datetime)) model.REPORT_DATE_TIME = datetime.ToString();
                    if (DateTime.TryParse(node["VERIFIED_DATE_TIME"].InnerText.Trim(), out datetime)) model.VERIFIED_DATE_TIME = datetime.ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取患者门诊病历
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<ViewOutHosPatientInfo> GetPatientOutpatientRecord(string patientId)
        {
            DateTime dateTime;
            XmlDocument doc = new XmlDocument();
            ViewOutHosPatientInfo model = new ViewOutHosPatientInfo();

            StringBuilder builder = new StringBuilder();
            builder.Append("<Request>");
            builder.Append("<Header>");
            builder.Append("<SourceSystem>");
            builder.Append("ZK");
            builder.Append("</SourceSystem>");
            builder.Append("<MessageID>");
            builder.Append(Guid.NewGuid().ToString());
            builder.Append("</MessageID>");
            builder.Append("<CreateTime>");
            builder.Append(DateTime.Now.ToString());
            builder.Append("</CreateTime>");
            builder.Append("</Header>");
            builder.Append("<Body>");
            builder.Append("<PATIENT_ID>");
            builder.Append(patientId);
            builder.Append("</PATIENT_ID>");
            builder.Append("</Body>");
            builder.Append("</Request>");

            string strXml = builder.ToString();
            logger.LogInformation("开始调用服务GetZkBlDt...");
            logger.LogInformation($"入参：{strXml}");
            var res = hisClient.GetZkBlDtAsync(strXml);
            //var res = hisClient2.GetZkBlDtAsync(strXml);
            if (res != null)
            {
                if (res.Result != null)
                {
                    if (res.Result.Body != null)
                    {
                        logger.LogInformation($"返回：{res.Result.Body.payload}");
                        doc.LoadXml(res.Result.Body.payload);
                        XmlNode node = doc.SelectSingleNode("Response/date");
                        if (node!=null)
                        {
                            XmlNodeList nodeList = node.ChildNodes;
                            foreach (XmlNode n in nodeList)
                            {
                                if (n.Name.Equals("CREATE_DATE"))
                                {
                                    if (DateTime.TryParse(n.InnerText.Trim(), out dateTime))
                                    {
                                        model.ADMISSTION_TIME = dateTime;
                                        model.REGIST_TIME = dateTime;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.LogInformation("Result.Body为空");
                    }
                }
                else
                {
                    logger.LogInformation("Result为空");
                }
            }
            else
            {
                logger.LogInformation("返回为空");
            }
            return model;
        }

        /// <summary>
        /// 同步科室字典
        /// </summary>
        /// <returns></returns>
        public async Task<List<MedDeptDict>> GetDeptDict()
        {
            List<MedDeptDict> deptDict = new List<MedDeptDict>();
            string file = AppDomain.CurrentDomain.BaseDirectory + "XMLFile.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode node = doc.SelectSingleNode("Response/getlocationinfoRt");
            XmlNodeList nodeList = node.SelectNodes("dept");
            foreach (XmlNode n in nodeList)
            {
                MedDeptDict dict = new MedDeptDict();
                dict.Dept_Code = n["dept_code"].InnerText.Trim();
                dict.Dept_Name= n["dept_name"].InnerText.Trim();
                dict.Input_Code = n["spell_code"].InnerText.Trim();
                deptDict.Add(dict);
            }
            return deptDict;
        }

        //public async Task<string> GetPatCreateTime(string patientId)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    DateTime datetime;
        //    decimal dc;
        //    List<ViewExamReport> list = new List<ViewExamReport>();

        //    //调用接口
        //    string XmlString = "<Request><Header>" +
        //                       "<SourceSystem>ZKZX</SourceSystem>" +
        //                       "<MessageID>" + Guid.NewGuid().ToString() + "</MessageID>" +
        //                       "<CreateTime>" + DateTime.Now.ToString() + "</CreateTime>" +
        //                       "</Header><Body>" +
        //                       "<PatientId>" + serialNo + "</PatientId>" +
        //                       "<StartTime>" + DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd") + "</StartTime>" +
        //                       "<EndTime>" + DateTime.Now.ToString("yyyy-MM-dd") + "</EndTime>" +
        //                       "</Body></Request>";

        //    logger.LogError(XmlString);
        //    var res = hisClient.GetVExamReportAsync(XmlString);
        //    doc.LoadXml(res.Result.Body.payload);
        //    logger.LogError(res.Result.Body.payload);
        //    XmlNodeList paraList = doc["Response"].ChildNodes;

        //    //转换实体
        //    foreach (XmlNode node in paraList)
        //    {
        //        if (node.Name.Equals("date"))
        //        {
        //            var model = new ViewExamReport();
        //            model.PATIENT_NO = node["PATIENT_ID"].InnerText.Trim();
        //            model.EXAM_NO = node["EXAM_NO"].InnerText.Trim();
        //            model.EXAM_CLASS = node["EXAM_CLASS"].InnerText.Trim();
        //            model.EXAM_SUB_CLASS = node["EXAM_SUB_CLASS"].InnerText.Trim();
        //            model.CLIN_DIAG = node["CLIN_DIAG"].InnerText.Trim();
        //            model.EXAM_MODE = node["EXAM_MODE"].InnerText.Trim();
        //            model.DEVICE = node["DEVICE"].InnerText.Trim();
        //            model.PERFORMED_BY = node["PERFORMED_BY"].InnerText.Trim();
        //            if (DateTime.TryParse(node["REQ_DATE_TIME"].InnerText.Trim(), out datetime)) model.REQ_DATE_TIME = datetime.ToString();
        //            model.REQ_DEPT = node["REQ_DEPT"].InnerText.Trim();
        //            model.REQ_PHYSICIAN = node["REQ_PHYSICIAN"].InnerText.Trim();
        //            model.REQ_MEMO = node["REQ_MEMO"].InnerText.Trim();
        //            model.NOTICE = node["NOTICE"].InnerText.Trim();
        //            if (DateTime.TryParse(node["EXAM_START_TIME"].InnerText.Trim(), out datetime)) model.EXAM_START_DATE = datetime.ToString();
        //            if (DateTime.TryParse(node["EXAM_END_TIME"].InnerText.Trim(), out datetime)) model.EXAM_END_DATE = datetime.ToString();
        //            model.EXAM_PARA = node["EXAM_PARA"].InnerText.Trim();
        //            model.DESCRIPTION = node["DESCRIPTION"].InnerText.Trim();
        //            model.RECOMMENDATION = node["RECOMMENDATION"].InnerText.Trim();
        //            model.TECHNICIAN = node["TECHNICIAN"].InnerText.Trim();
        //            model.REPORTER = node["REPORTER"].InnerText.Trim();
        //            model.RESULT_STATUS = node["RESULT_STATUS"].InnerText.Trim();
        //            model.VERIFIED_BY = node["VERIFIED_BY"].InnerText.Trim();
        //            if (DateTime.TryParse(node["REPORT_DATE_TIME"].InnerText.Trim(), out datetime)) model.REPORT_DATE_TIME = datetime.ToString();
        //            if (DateTime.TryParse(node["VERIFIED_DATE_TIME"].InnerText.Trim(), out datetime)) model.VERIFIED_DATE_TIME = datetime.ToString();
        //            list.Add(model);
        //        }
        //    }

        //    return list;
        //}

        //public async Task<string> RequestWebservice(string requestNo, string requestXml)
        //{
        //    logger.LogDebug($"请求HIS接口,参数requestNo:{requestNo},requestXml:{requestXml}");
        //    var response = await hisClient.HIPMessageServerAsync(requestNo, requestXml);
        //    logger.LogDebug($"HIS接口返回值:{response?.Body?.HIPMessageServerResult}");
        //    if (!string.IsNullOrEmpty(response?.Body?.HIPMessageServerResult) && !response.Body.HIPMessageServerResult.StartsWith("-1"))
        //    {
        //        return response.Body.HIPMessageServerResult;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
