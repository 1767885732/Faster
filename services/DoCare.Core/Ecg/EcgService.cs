using DoCare.Core.Common;
using DoCare.Core.Ecg.Entities;
using DoCare.Core.Ecg.Entities.XmlSerialization;
using DoCare.Core.Exam_Inspec_Medication.Entities;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DoCare.Core.Ecg
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class EcgService
    {
        private readonly ILogger<EcgService> logger;
        private readonly IFirstAid firstAid;
        private readonly IEmergency emergency;
        private readonly IOutPatient outPatient;
        private readonly IInPatient inPatient;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelationInfo;
        private readonly IRepository<MedPatEcgInfo, Guid> medPatEcgInfo;
        private readonly IRepository<MedPatientRegistInfo, string> medPatientRegistInfo;
        private readonly OracleCRUD oracleCRUD;

        public EcgService(ILogger<EcgService> logger, IFirstAid firstAid, IEmergency emergency,
            IOutPatient outPatient, IInPatient inPatient,
            IRepository<MedPatEcgInfo, Guid> medPatEcgInfo,
            IRepository<MedPatRelationInfo, string> medPatRelationInfo,
            IRepository<MedPatientRegistInfo, string> medPatientRegistInfo,
            OracleCRUD oracleCRUD)
        {
            this.logger = logger;
            this.firstAid = firstAid;
            this.emergency = emergency;
            this.outPatient = outPatient;
            this.inPatient = inPatient;
            this.medPatRelationInfo = medPatRelationInfo;
            this.medPatEcgInfo = medPatEcgInfo;
            this.medPatientRegistInfo = medPatientRegistInfo;
            this.oracleCRUD = oracleCRUD;
        }

        /// <summary>
        /// 同步心电数据
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public async Task<bool> SyncEcgs(SyncPatientDto dto)
        {
            var res = false;
            if (string.IsNullOrEmpty(dto.PatientNo)) return res;
            var relationEntity = await medPatRelationInfo.GetAsync(dto.PatientNo);
            if (relationEntity == null) return res;

            List<MedPatEcgInfo> lstPatEcg = new List<MedPatEcgInfo>();

            #region 备份代码

            //if (firstAid is IEcg firstAidEcgImpl)
            //{
            //    try
            //    {
            //        var data = await firstAidEcgImpl.RunEcg(relationEntity);
            //        if ((data?.Count ?? 0) > 0)
            //            lstPatEcg.AddRange(data);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError($"SyncEcgs-急救同步错误:{ex.Message}");
            //    }
            //}
            //if (emergency is IEcg emergencyEcgImpl)
            //{
            //    try
            //    {
            //        var data = await emergencyEcgImpl.RunEcg(relationEntity);
            //        if ((data?.Count ?? 0) > 0)
            //            lstPatEcg.AddRange(data);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError($"SyncEcgs-急诊同步错误:{ex.Message}");
            //    }
            //}
            //if (outPatient is IEcg outPatientEcgImpl)
            //{
            //    try
            //    {
            //        var data = await outPatientEcgImpl.RunEcg(relationEntity);
            //        if ((data?.Count ?? 0) > 0)
            //            lstPatEcg.AddRange(data);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError($"SyncEcgs-门诊同步错误:{ex.Message}");
            //    }
            //}
            //if (inPatient is IEcg inPatientEcgImpl)
            //{
            //    try
            //    {
            //        var data = await inPatientEcgImpl.RunEcg(relationEntity);
            //        if ((data?.Count ?? 0) > 0)
            //            lstPatEcg.AddRange(data);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError($"SyncEcgs-住院同步错误:{ex.Message}");
            //    }
            //}

            #endregion

            #region 当前代码

            if (dto.PatientType.Equals("1")) //胸痛患者
            {
                logger.LogInformation("SyncEcgs-胸痛患者");
                string cometype = "1";
                var registInfo = await medPatientRegistInfo.GetFirstAsync(i => i.Id.Equals(dto.PatientNo));
                cometype = registInfo != null ? registInfo.ComeType : cometype;
                if (cometype.Equals("1")) //120来院
                {
                    logger.LogInformation("SyncEcgs-胸痛患者-120来院");
                    if (firstAid is IEcg firstAidEcgImp)
                    {
                        try
                        {
                            var data = await firstAidEcgImp.RunEcg(relationEntity);
                            if ((data?.Count ?? 0) > 0)
                                lstPatEcg.AddRange(data);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError($"SyncEcgs-心电数据同步错误：{ex.Message}");
                        }
                    }
                }
            }
            else if (dto.PatientType.Equals("2")) //卒中患者
            {
                logger.LogInformation("SyncEcgs-卒中患者");
                if (firstAid is IEcg firstAidEcgImp)
                {
                    try
                    {
                        var data = await firstAidEcgImp.RunEcg(relationEntity);
                        if ((data?.Count ?? 0) > 0)
                            lstPatEcg.AddRange(data);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"SyncEcgs-心电数据同步错误：{ex.Message}");
                    }
                }
            }

            #endregion

            if (lstPatEcg.Count == 0) return res;
            logger.LogDebug($"PatientNo：{dto.PatientNo},心电图数据量：{lstPatEcg.Count}");

            lstPatEcg.ForEach(item =>
           {
               oracleCRUD.InsertOrUpdate(item);
           });
            return true;
        }

        /// <summary>
        /// 推送心电图信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> PushEcgs(Request request)
        {
            logger.LogInformation("PushEcgs-推送心电图信息");
            string xmlResult = string.Empty;
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Request));
                xmlSerializer.Serialize(stringWriter, request);
                xmlResult = stringWriter.ToString();
            }
            logger.LogInformation($"接收内容：{xmlResult}");
            string messageId = string.Empty;
            if (!string.IsNullOrEmpty(xmlResult))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xmlResult);
                XmlNode msgNode = document.SelectSingleNode("Request/Header/MessageID");
                if (msgNode != null)
                {
                    messageId = msgNode.InnerText.Trim();
                }

                XmlNode bodyNode = document.SelectSingleNode("Request/Body");
                if (bodyNode != null && bodyNode.ChildNodes.Count > 0)
                {
                    string patientId = bodyNode["PatientID"].InnerText.Trim();
                    string repTime = bodyNode["RepTime"].InnerText.Trim();
                    //1、胸痛患者进入急诊确认分诊后，先去抢救室做心电图，心电图做完才去挂号，进而产生PatientId，患者信息维护完成，再出具心电图报告，并推送专科平台。
                    //2、先去查询分诊视图，获取患者关系。
                    MedPatRelationInfo relationInfo = await emergency.GetPatientRelationInfo(patientId, repTime);
                    if (relationInfo == null)
                        throw new Exception($"MessageID:{messageId}-心电图信息未关联到分诊患者");

                    var patient = await medPatientRegistInfo.GetFirstAsync(p => p.Id.Equals(relationInfo.Id));
                    //心电图信息
                    DateTime exam_Time, report_Time;
                    string examTime = bodyNode["ExamTime"].InnerText.Trim();
                    string reportTime = bodyNode["RepTime"].InnerText.Trim();
                    bool examFlag = DateTime.TryParse(examTime, out exam_Time);
                    bool reportFlag = DateTime.TryParse(reportTime, out report_Time);
                    StringBuilder result_builder = new StringBuilder();
                    XmlNodeList resultNodeList = bodyNode.SelectNodes("RepResultList/RepResult");
                    if (resultNodeList != null && resultNodeList.Count > 0)
                    {
                        foreach (XmlNode node in resultNodeList)
                        {
                            result_builder.Append(node["ExamResult"].InnerText.Trim().Replace("\r\n", ""));
                            result_builder.Append("\r\n");
                        }
                    }
                    string diagDesc = result_builder.Length > 0 ? result_builder.ToString().Substring(0, result_builder.ToString().LastIndexOf("\r\n")) : string.Empty;
                    MedPatEcgInfo ecgInfo = new MedPatEcgInfo();
                    ecgInfo.Id = System.Guid.NewGuid();
                    ecgInfo.PatientNo = relationInfo.Id;
                    ecgInfo.EcgPatientId = patientId;
                    ecgInfo.ExamTime = exam_Time;
                    ecgInfo.ReportTime = report_Time;
                    ecgInfo.DiagTime = exam_Time;
                    ecgInfo.DiagDesc = diagDesc;
                    ecgInfo.DiagSource = EcgSource.InHospital.ToString("d");
                    ecgInfo.EcgPath = bodyNode["DocPath"].InnerText.Trim();
                    ecgInfo.EcgUrl = bodyNode["PictureUrl"].InnerText.Trim();
                    ecgInfo.EcgData = bodyNode["DocContent"].InnerText.Trim();
                    ecgInfo.PdfData = bodyNode["DocContent"].InnerText.Trim();
                    ecgInfo.Name = patient != null ? patient.Name : string.Empty;
                    ecgInfo.Sex = patient != null ? patient.Sex : string.Empty;
                    ecgInfo.Age = patient != null ? patient.Age : string.Empty;
                    oracleCRUD.InsertOrUpdate(ecgInfo);
                }
            }
            return messageId;
        }

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        private string GetSerialNo()
        {
            PatientNoData patientNoData = Singleton<PatientNoData>.Instance;

            if (patientNoData != null && patientNoData.CreateDate.Date == DateTime.Now.Date
                && patientNoData.OrderNo != 0)
            {
                patientNoData.OrderNo += 1;
                patientNoData.CreateDate = DateTime.Now;
            }
            else
            {
                patientNoData = new PatientNoData();
                var newestData = GetNewestRelationEntity();
                if (newestData != null && newestData.CreatedTime.Date == DateTime.Now.Date)
                {
                    patientNoData.OrderNo = newestData.Id.Substring(newestData.Id.Length - 3).ToInt16() + 1;
                }
                else
                {
                    patientNoData.OrderNo += 1;
                }
            }

            Singleton<PatientNoData>.Instance = patientNoData;
            return patientNoData.OrderNoStr;
        }

        /// <summary>
        /// 获取PatientNo最新记录
        /// </summary>
        /// <returns></returns>
        private MedPatRelationInfo GetNewestRelationEntity()
        {
            var prePNo = DateTime.Now.ToString($"{HospitalStatic.HospitalNo}yyMMdd");
            var entity = medPatRelationInfo.Query().OrderByDescending(a => a.Id).FirstOrDefault(a => a.Id.Contains(prePNo));
            return entity;
        }
    }
}
