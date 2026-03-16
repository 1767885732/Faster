using Dapper;
using DoCare.Core;
using DoCare.Core.Common;
using DoCare.Core.Patient;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using DoCare.Core.TimeNode;
using DoCare.Core.TimeNode.Entities;
using DoCare.Core.Score;
using DoCare.Mdsd.Emergency.Common;
using DoCare.Mdsd.Emergency.Entities;
using FastFrame.Data;
using FastFrame.EFCore;
using FastFrame.Entity;
using FastFrame.Mapping;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoCare.Core.Score.Entities;
using DoCare.Core.Ecg;
using DoCare.Core.Ecg.Entities;
using FastFrame.Core;
using DoCare.Core.Exam_Inspec_Medication.Entities;

namespace DoCare.Mdsd.Emergency
{
    /// <summary>
    /// 急诊接口实现
    /// </summary>
    public class EmergencyService : IEmergency, IMedPatTimeNode, IMedPatRelation, IScores
    {
        private readonly ILogger<EmergencyService> logger;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelation;
        private readonly IRepository<MedPatientRegistInfo, string> medPatientRegistInfo;
        private readonly IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo;
        private readonly IRepository<ViewCpcPatientInfo, string> viewCpcPatientInfo;
        private readonly IRepository<ViewCpcEmergencyInfo, string> viewCpcEmergencyInfo;
        private readonly IRepository<ViewCpcDocureInfo, string> viewCpcDocureInfo;
        private readonly IRepository<ViewCpcScoreInfo, string> viewCpcScoreInfo;
        private readonly IRepository<AutoTime, string> autoTime;
        private readonly IRepository<MedExamReport, string> medExamReport;

        public EmergencyService(ILogger<EmergencyService> logger, IRepository<MedPatRelationInfo, string> medPatRelation,
            IRepository<ViewCpcPatientInfo, string> viewCpcPatientInfo,
            IRepository<ViewCpcEmergencyInfo, string> viewCpcEmergencyInfo,
            IRepository<MedPatientRegistInfo, string> medPatientRegistInfo,
            IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo,
            IRepository<ViewCpcScoreInfo, string> viewCpcScoreInfo,
            IRepository<AutoTime, string> autoTime,
            IRepository<ViewCpcDocureInfo, string> viewCpcDocureInfo,
            IRepository<MedExamReport, string> medExamReport)
        {
            this.logger = logger;
            this.medPatRelation = medPatRelation;
            this.viewCpcPatientInfo = viewCpcPatientInfo;
            this.viewCpcEmergencyInfo = viewCpcEmergencyInfo;
            this.medPatientRegistInfo = medPatientRegistInfo;
            this.medPatientDetailsInfo = medPatientDetailsInfo;
            this.viewCpcDocureInfo = viewCpcDocureInfo;
            this.viewCpcScoreInfo = viewCpcScoreInfo;
            this.autoTime = autoTime;
            this.medExamReport = medExamReport;
        }

        private WebApiConfigOption EcgOption
        {
            get
            {
                WebApiConfigOption option = null;
                try
                {
                    option = Singleton<FastFrameOptions>.Instance?.GetWebApiOption(ApiConfig.InHospitalEcg.ToString());
                }
                catch { }

                if (option == null) throw new Exception("缺少院内心电图配置信息");
                return option;
            }
        }

        protected ViewCpcPatientInfo ViewCpcPatientInfo { get; set; }

        protected AutoTime PhepAutoTime { get; set; }

        protected ViewCpcEmergencyInfo ViewCpcEmergencyInfo { get; set; }

        public async Task<List<PatRegistOutDto>> GetList(PatRegistInputDto inDto)
        {
            List<PatRegistOutDto> regists = new List<PatRegistOutDto>();
            if (inDto == null) return regists;

            var iquery = viewCpcPatientInfo.Query();

            if (!string.IsNullOrEmpty(inDto.PatientId))
            {
                iquery = iquery.Where(o => o.Id == inDto.PatientId);
            }

            if (!string.IsNullOrEmpty(inDto.Name))
            {
                iquery = iquery.Where(o => o.NAME != null && o.NAME.Contains(inDto.Name));
            }

            if (!string.IsNullOrEmpty(inDto.IdCard))
            {
                iquery = iquery.Where(o => o.IDCARDNO != null && o.IDCARDNO.Contains(inDto.IdCard));
            }

            if (!string.IsNullOrEmpty(inDto.DiagnosisDesc))
            {
                iquery = iquery.Where(o => o.PATIENTSAY != null && o.PATIENTSAY.Contains(inDto.DiagnosisDesc));
            }

            if (inDto.BeginTime.HasValue && inDto.BeginTime.Value > DateTime.MinValue)
            {
                iquery = iquery.Where(o => o.REGISTTIME.CompareTo(inDto.BeginTime.Value.ToStringYMDHMS()) >= 0);
            }

            if (inDto.EndTime.HasValue && inDto.EndTime.Value > inDto.BeginTime.Value)
            {
                iquery = iquery.Where(o => o.REGISTTIME.CompareTo(inDto.EndTime.Value.ToStringYMDHMS()) < 0);
            }

            if (inDto.BindFlag == 1)
            {
                iquery = iquery.Where(o => o.WRISTBARCODE != null);
            }

            if (!string.IsNullOrEmpty(inDto.TagId))
            {
                iquery = iquery.Where(o => o.WRISTBARCODE == inDto.TagId);
            }

            List<ViewCpcPatientInfo> lstPatient = iquery.ToList();
            if (lstPatient != null && lstPatient.Count() > 0)
            {
                regists = lstPatient.MapTo<List<PatRegistOutDto>>();
            }
            return regists;
        }

        public async Task<List<MedPatTimeNodes>> SyncMedPatTimeNode(MedPatRelationInfo relationEntity)
        {
            try
            {
                var lstMedPatTimeNodes = new List<MedPatTimeNodes>();
                if (relationEntity == null) return lstMedPatTimeNodes;

                //var viewDocureInfo = await viewCpcDocureInfo.GetFirstAsync(a => a.Id == relationEntity.EmergencyId);
                //if (!string.IsNullOrEmpty(viewDocureInfo?.DocureInRoomTime))
                //{
                //    lstMedPatTimeNodes.Add(new MedPatTimeNodes
                //    {
                //        ItemName = "RESCUETIME",
                //        ItemValue = viewDocureInfo.DocureInRoomTime.ToDateTime(),
                //    });
                //}

                //if (!string.IsNullOrEmpty(viewDocureInfo?.DocureOutRoomTime))
                //{
                //    lstMedPatTimeNodes.Add(new MedPatTimeNodes
                //    {
                //        ItemName = "LEFTRESCUETIME",
                //        ItemValue = viewDocureInfo.DocureOutRoomTime.ToDateTime(),
                //    });
                //}
                logger.LogDebug($"同步患者{relationEntity.EmergencyId}的手环数据");
                PhepAutoTime = await autoTime.GetFirstAsync(a => a.PatientId == relationEntity.EmergencyId);
                if (PhepAutoTime != null)
                {
                    var timeNodes = new List<MedPatTimeNodes>();
                    timeNodes = ConvertToListMedPatTimeNodes(relationEntity.Id, PhepAutoTime);
                    if (timeNodes != null)
                        lstMedPatTimeNodes.AddRange(timeNodes);

                    if (PhepAutoTime.RescueTime > DateTime.MinValue)
                    { }
                    else
                    {
                        lstMedPatTimeNodes.Add(new MedPatTimeNodes
                        {
                            PatientNo = relationEntity.Id,
                            TagId = PhepAutoTime.TagId,
                            ItemName = "RESCUETIME",
                            ItemValue = PhepAutoTime.BindTagTime,
                            BindTime = PhepAutoTime?.BindTagTime
                        });
                    }
                }

                return lstMedPatTimeNodes;
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return null;
            }
        }

        public async Task<MedPatRelationInfo> SyncPatientRelationNo(MedPatRelationInfo relationEntity)
        {
            var relation = relationEntity;
            Check.NotNull(relation, nameof(relation));

            if (relation.PatientSource == PatientSourceType.FirstAid.ToString("d"))
            {
                //Check.NotNullOrEmpty(relation.MissionId, nameof(relation.MissionId));
                Check.NotNullOrEmpty(relation.PhepId, nameof(relation.PhepId));

                ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.PHEPMISSIONID == relation.MissionId && a.PHEPPATIENTID == relation.PhepId, false);
            }
            else if (relation.PatientSource == PatientSourceType.Emergency.ToString("d"))
            {
                Check.NotNullOrEmpty(relation.EmergencyId, nameof(relation.EmergencyId));
                ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.Id.Equals(relation.EmergencyId), false);
                string mdtType = ViewCpcPatientInfo.MDT_TYPE;
                if (mdtType == null)
                {

                }
                else if (mdtType.Equals("1"))
                {
                    //胸痛患者
                    ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.Id.Equals(relation.EmergencyId), false);
                }
                else if (mdtType.Equals("2"))
                {
                    //卒中患者
                    ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.Id.Equals(relation.EmergencyId), false);
                    if (string.IsNullOrEmpty(ViewCpcPatientInfo.TAG_ID))
                    {
                        ViewCpcPatientInfo = null;
                    }
                }
            }
            else if (relation.PatientSource == PatientSourceType.OutPatient.ToString("d") || relation.PatientSource == PatientSourceType.InPatient.ToString("d"))
            {
                if (!string.IsNullOrEmpty(relation.RegistId))
                {
                    ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.VISITID == relation.RegistId, false);
                }
            }
            else
            {
                logger.LogDebug($"不支持得患者来源:PatientSource:{relation.PatientSource}");
            }

            if (ViewCpcPatientInfo == null) return relation;
            relation.PhepId = ViewCpcPatientInfo.PHEPPATIENTID;
            relation.MissionId = ViewCpcPatientInfo.PHEPMISSIONID;
            relation.EmergencyId = ViewCpcPatientInfo.Id;
            relation.RegistId = ViewCpcPatientInfo.VISITID;
            relation.ClinicNo = ViewCpcPatientInfo.CLINICNO;
            relation.OtherNo = ViewCpcPatientInfo.CLINICNO;
            return relation;
        }

        public async Task<string> SyncPatientData(MedPatRelationInfo relation)
        {
            Check.NotNullOrEmpty(relation?.Id, nameof(relation.Id));

            MedPatientRegistInfo regist = null;
            MedPatientDetailsInfo detail = null;
            string orgCode = null;
            bool isGreen = false;

            try
            {
                if (ViewCpcPatientInfo == null)
                    ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.Id == relation.EmergencyId);
                string mdtType = ViewCpcPatientInfo.MDT_TYPE;
                if (mdtType == null)
                {
                    
                }
                else if (mdtType.Equals("1"))
                {
                    //胸痛患者
                    ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.Id.Equals(relation.EmergencyId), false);
                }
                else if (mdtType.Equals("2"))
                {
                    //卒中患者
                    ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.Id.Equals(relation.EmergencyId), false);
                    if(string.IsNullOrEmpty(ViewCpcPatientInfo.TAG_ID))
                    {
                        ViewCpcPatientInfo = null;
                    }
                }

                if (ViewCpcPatientInfo != null)
                {
                    regist = ViewCpcPatientInfo.MapTo<MedPatientRegistInfo>();
                    regist.Id = relation.Id;
                    regist.PhoneNumber = ViewCpcPatientInfo.PHONE;
                    orgCode = regist.OrgCode;
                    isGreen = ViewCpcPatientInfo.GREENCHANNEL.Equals("1") ? true : false;
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"急诊患者基本信息获取失败,{ex.Message}");
            }

            try
            {
                detail = new MedPatientDetailsInfo();
                if (ViewCpcEmergencyInfo == null)
                    ViewCpcEmergencyInfo = await viewCpcEmergencyInfo.GetFirstAsync(a => a.Id.Equals(relation.EmergencyId));
                string mdtType = ViewCpcEmergencyInfo.MDT_TYPE;
                if (mdtType == null)
                {
                    
                }
                else if (mdtType.Equals("1"))
                {
                    //胸痛患者
                    ViewCpcEmergencyInfo = await viewCpcEmergencyInfo.GetFirstAsync(a => a.Id.Equals(relation.EmergencyId));
                }
                else if (mdtType.Equals("2"))
                {
                    //卒中患者
                    ViewCpcEmergencyInfo = await viewCpcEmergencyInfo.GetFirstAsync(a => a.Id.Equals(relation.EmergencyId));
                    if (string.IsNullOrEmpty(ViewCpcEmergencyInfo.TAG_ID))
                    {
                        ViewCpcEmergencyInfo = null;
                    }
                }

                if (ViewCpcEmergencyInfo != null)
                {
                    detail = ViewCpcEmergencyInfo.MapTo<MedPatientDetailsInfo>();
                    regist.DiagLev = ViewCpcEmergencyInfo.RESETLEVEL;
                    //regist.ComeType = ViewCpcEmergencyInfo.ToComingType(ViewCpcPatientInfo);
                    regist.ComeType = ViewCpcEmergencyInfo.COMINGTYPE;
                    regist.ArriveTypeExtension = ViewCpcEmergencyInfo?.ARRIVETYPE.ToInt32().ToString();
                }
                detail.RegistTime = ViewCpcPatientInfo?.REGISTTIME?.ToString()?.ToDateTime();
                detail.TriageTime = ViewCpcPatientInfo?.REGISTTIME?.ToString()?.ToDateTime();
                detail.AdmissionTime = ViewCpcPatientInfo?.REGISTTIME?.ToString()?.ToDateTime();
                detail.Bp = ViewCpcEmergencyInfo?.BLOODPRESSURE;
                detail.Id = relation.Id;
                detail.IsGreenChannel = isGreen;
            }
            catch (Exception ex)
            {
                logger.LogError($"急诊患者详细信息同步失败,{ex.Message}");
            }

            try
            {
                Check.NotNull(regist);
                MedPatientRegistInfo registDb = await medPatientRegistInfo.GetAsync(relation.Id);
                if (registDb == null)
                {
                    await medPatientRegistInfo.InsertAsync(regist);
                }
                else
                {
                    registDb = regist;
                    await medPatientRegistInfo.UpdateAsync(registDb);
                }

                Check.NotNull(detail);
                //MedPatientDetailsInfo detailDb =  await medPatientDetailsInfo.GetAsync(relation.Id);
                MedPatientDetailsInfo detailDb = medPatientDetailsInfo.Get(relation.Id);
                if (detailDb == null)
                {
                    await medPatientDetailsInfo.InsertAsync(detail);
                }
                else
                {
                    detailDb = detail;
                    await medPatientDetailsInfo.UpdateAsync(detailDb);
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"急诊患者基本信息同步失败,{ex.Message}");
            }
            return orgCode;
        }

        /// <summary>
        /// 获取急诊评分
        /// </summary>
        /// <param name="relationEntity"></param>
        /// <returns></returns>
        public async Task<List<MedPatScore>> GetMedPatScore(MedPatRelationInfo relationEntity)
        {
            List<MedPatScore> medPatScores = new List<MedPatScore>();
            if (string.IsNullOrEmpty(relationEntity?.EmergencyId))
                return medPatScores;

            List<ViewCpcScoreInfo> cpcScores = null;
            string OpenEye = "不睁眼,刺痛睁眼,呼唤睁眼,自发睁眼,";
            string Language = "无反应,唯有声叹,含糊不清,回答错误,回答正确,";
            string Sport = "迟缓无反应,刺激后肢体过伸,刺激后肢体屈曲,刺激后回缩,定位动作,遵命动作,";
            try
            {
                cpcScores = viewCpcScoreInfo.Query().Where(o => o.Id == relationEntity.EmergencyId).ToList();
                if ((cpcScores?.Count ?? 0) == 0) return medPatScores;

                cpcScores.ForEach(a =>
                {
                    MedPatScore patScores = new MedPatScore();
                    switch (a.VULNUS_TYPE)
                    {
                        case "2"://GCS评分

                            if (OpenEye.Contains(a.PLACE + ","))
                            {
                                patScores.ScoreItem = "睁眼";
                            }
                            else if (Language.Contains(a.PLACE + ","))
                            {
                                patScores.ScoreItem = "语言";
                            }
                            else if (Sport.Contains(a.PLACE + ","))
                            {
                                patScores.ScoreItem = "运动";
                            }
                            patScores.PatientNo = relationEntity.Id;
                            patScores.ScoreSource = ScoreSource.InHospital.ToString("d");
                            patScores.ScoreType = ScoreType.GCS.ToString();
                            patScores.ScoreResult = a.DEGREE.ToString();
                            medPatScores.Add(patScores);
                            break;
                        case "7"://RTS评分
                            patScores.PatientNo = relationEntity.Id;
                            patScores.ScoreSource = ScoreSource.InHospital.ToString("d");
                            patScores.ScoreType = ScoreType.RTS.ToString();
                            patScores.ScoreItem = a.PLACE;
                            patScores.ScoreResult = a.DEGREE.ToString();
                            medPatScores.Add(patScores);
                            break;
                        case "3"://疼痛评分
                            patScores.PatientNo = relationEntity.Id;
                            patScores.ScoreSource = ScoreSource.InHospital.ToString("d");
                            patScores.ScoreType = ScoreType.疼痛.ToString();
                            patScores.ScoreItem = "疼痛";
                            patScores.ScoreResult = a.DEGREE.ToString();
                            medPatScores.Add(patScores);
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
            }
            return medPatScores;
        }

        public async Task<List<MedPatEcgInfo>> RunEcg(MedPatRelationInfo relationEntity)
        {
            List<MedPatEcgInfo> lstEcg = new List<MedPatEcgInfo>();
            if (string.IsNullOrEmpty(relationEntity?.Id) || string.IsNullOrEmpty(relationEntity?.EmergencyId))
                return lstEcg;

            var emergencyInfo = await viewCpcEmergencyInfo.GetFirstAsync(a => a.Id == relationEntity.EmergencyId);
            if (emergencyInfo == null) return lstEcg;

            var inHosTime = emergencyInfo.INHOSPITALECGTIME.ToDateTime();
            var cpcPatient = await viewCpcPatientInfo.GetFirstAsync(a => a.Id == relationEntity.EmergencyId);

            var medPatEcg = new MedPatEcgInfo()
            {
                PatientNo = relationEntity.Id,
                ExamTime = inHosTime,
                ReportTime = inHosTime,
                DiagTime = emergencyInfo.ECGDIAGNOSISTIME.ToDateTime(),
                DiagSource = EcgSource.InHospital.ToString("d"),
                EcgUrl = GetEcgView(cpcPatient.PATIENTID, cpcPatient.VISITID)
            };
            lstEcg.Add(medPatEcg);
            return lstEcg;
        }

        /// <summary>
        /// 根据挂号编号获取急诊号
        /// </summary>
        /// <param name="relationEntity"></param>
        /// <returns></returns>
        public async Task<MedPatRelationInfo> GetEmergencyNo(MedPatRelationInfo relationEntity)
        {
            var relation = relationEntity;
            Check.NotNull(relation, nameof(relation));

            if (!string.IsNullOrEmpty(relation.RegistId))
            {
                ViewCpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(a => a.VISITID == relation.RegistId, false);
            }

            if (ViewCpcPatientInfo == null) return relation;
            relation.EmergencyId = ViewCpcPatientInfo.Id;
            relation.RegistId = ViewCpcPatientInfo.VISITID;
            relation.ClinicNo = ViewCpcPatientInfo.PATIENTID;
            return relation;
        }

        private List<MedPatTimeNodes> ConvertToListMedPatTimeNodes(string patientNo, AutoTime rfidData)
        {
            List<MedPatTimeNodes> timeNodes = null;
            if (string.IsNullOrEmpty(patientNo)) return timeNodes;
            if (string.IsNullOrEmpty(rfidData?.TagId)) return timeNodes;

            timeNodes = new List<MedPatTimeNodes>();
            var tagId = rfidData.TagId;
            var properties = rfidData.GetType().GetProperties();
            properties.ToList().ForEach(a =>
            {
                if (a.PropertyType == typeof(DateTime?))
                {
                    var patTimeNodes = new MedPatTimeNodes()
                    {
                        PatientNo = patientNo,
                        TagId = tagId,
                        ItemName = a.Name.ToUpper(),
                        ItemValue = (DateTime?)a.GetValue(rfidData, null),
                        BindTime = rfidData?.BindTagTime
                    };
                    //if (patTimeNodes.ItemName.ToUpper() == "RESCUETIME")
                    //{
                    //    patTimeNodes.ItemValue = rfidData?.BindTagTime;
                    //}
                    if (patTimeNodes.ItemValue.HasValue)
                        timeNodes.Add(patTimeNodes);
                }
            });
            return timeNodes;
        }

        /// <summary>
        /// 同步患者急诊急救时间信息
        /// </summary>
        /// <param name="relationEntity"></param>
        /// <returns></returns>
        public async Task<PhepAutoTimes> SyncPhepAutoTime(MedPatRelationInfo relationEntity)
        {
            var phepAutoTimes = new PhepAutoTimes();
            if (relationEntity == null) return phepAutoTimes;

            logger.LogDebug($"同步患者{relationEntity.EmergencyId}的急诊急救时间信息");
            PhepAutoTime = await autoTime.GetFirstAsync(a => a.PatientId == relationEntity.EmergencyId);
            var examReportList = medExamReport.Query().Where((MedExamReport a) => a.PatientNo.Equals(relationEntity.Id) && a.ExamClass.Equals("ＣＴ") && a.ExamSubClass.Equals("头部")).OrderByDescending((MedExamReport a) => a.ReqDateTime);
            var examReport = examReportList != null ? examReportList.FirstOrDefault() : null;
            if (PhepAutoTime != null)
            {
                phepAutoTimes = new PhepAutoTimes()
                {
                    Id = PhepAutoTime.Id,
                    PatientId = PhepAutoTime.PatientId,
                    MISSIONID = PhepAutoTime.MISSIONID,
                    TimeFlag = PhepAutoTime.TimeFlag,
                    TagId = PhepAutoTime.TagId,
                    BindTagTime = PhepAutoTime.BindTagTime,
                    TouchPatientTime = PhepAutoTime.TouchPatientTime,
                    ConduitTime = PhepAutoTime.ConduitTime,
                    LeftConduitTime = PhepAutoTime.LeftConduitTime,
                    RescueTime = PhepAutoTime.RescueTime,
                    LeftRescueTime = PhepAutoTime.LeftRescueTime,
                    ArrivedCCUTime = PhepAutoTime.ArrivedCCUTime,
                    LeftCCUTime = PhepAutoTime.LeftCCUTime,
                    ArrivedCTTime = PhepAutoTime.ArrivedCTTime,
                    LeftCTTime = PhepAutoTime.LeftCTTime,
                    ArrivedOperTime = PhepAutoTime.ArrivedOperTime,
                    LeftOperTime = PhepAutoTime.LeftOperTime,
                    ArrivedStrokeTime = PhepAutoTime.ArrivedStrokeTime,
                    LeftStrokeTime = PhepAutoTime.LeftStrokeTime,
                    ArrivedICUTime = PhepAutoTime.ArrivedICUTime,
                    LeftICUTime = PhepAutoTime.LeftICUTime,
                    CreateTime = PhepAutoTime.CreateTime,
                    UpdateTime = PhepAutoTime.UpdateTime
                };
            }
            phepAutoTimes.CtOpenTime = examReport != null ? examReport.ReqDateTime : null;
            phepAutoTimes.CtResultTime = examReport != null ? examReport.ReportDateTime : null;
            return phepAutoTimes;
        }

        private string GetEcgView(string patientId, string visitId)
        {
            var viewUrl = string.Empty;
            if (string.IsNullOrEmpty(patientId)) return viewUrl;
            viewUrl = $"{EcgOption.Host}/ECGBrowser/PatientEcgReport?patientId={patientId}&visitId={visitId}";
            return viewUrl;
        }

        /// <summary>
        /// 根据患者ID、分诊挂号日期获取患者关系
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public async Task<MedPatRelationInfo> GetPatientRelationInfo(string patientId, string regTime)
        {
            MedPatRelationInfo relationInfo = null;
            DateTime regDate;
            if (!DateTime.TryParse(regTime, out regDate))
                return relationInfo;
            var cpcPatientInfo = await viewCpcPatientInfo.GetFirstAsync(p => p.PATIENTID.Equals(patientId) && (DateTime.Parse(p.REGISTTIME).Date.CompareTo(regDate.Date) == 0));
            if (cpcPatientInfo == null)
            {
                cpcPatientInfo = viewCpcPatientInfo.Query().Where(p => p.PATIENTID.Equals(patientId) && (DateTime.Parse(p.REGISTTIME).Date.CompareTo(regDate.Date) < 0)).OrderByDescending(p => p.REGISTTIME).FirstOrDefault();
            }  
            if (cpcPatientInfo == null)
                return relationInfo;
            relationInfo = await medPatRelation.GetFirstAsync(r => r.EmergencyId.Equals(cpcPatientInfo.Id));
            return relationInfo;
        }
    }
}
