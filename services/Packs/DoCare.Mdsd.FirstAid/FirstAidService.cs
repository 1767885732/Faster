using DoCare.Core;
using DoCare.Core.Common;
using DoCare.Core.Ecg;
using DoCare.Core.Ecg.Entities;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using DoCare.Core.Score;
using DoCare.Core.Score.Entities;
using DoCare.Core.TimeNode;
using DoCare.Core.TimeNode.Entities;
using DoCare.Mdsd.FirstAid.Driver;
using DoCare.Mdsd.FirstAid.Entities;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.Mapping;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DoCare.Mdsd.FirstAid
{
    public class FirstAidService : IFirstAid, IEcg, IMedPatTimeNode, IScores
    {
        private readonly ILogger<FirstAidService> logger;
        private readonly FirstAidApiClient firstAidApiClient;
        private readonly FirstAidEcgClient firstAidEcgClient;
        private readonly IRepository<MedPhepPatientInfo, string> medPhepPatientInfo;
        private readonly IRepository<MedPatientRegistInfo, string> medPatientRegistInfo;
        private readonly IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo;

        public FirstAidService(ILogger<FirstAidService> logger,
                    IRepository<MedPatientRegistInfo, string> medPatientRegistInfo,
                    IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo,
                                FirstAidApiClient firstAidApiClient,
                                FirstAidEcgClient firstAidEcgClient,
                                  IRepository<MedPhepPatientInfo, string> medPhepPatientInfo)
        {
            this.logger = logger;
            this.medPatientDetailsInfo = medPatientDetailsInfo;
            this.medPatientRegistInfo = medPatientRegistInfo;
            this.firstAidApiClient = firstAidApiClient;
            this.firstAidEcgClient = firstAidEcgClient;
            this.medPhepPatientInfo = medPhepPatientInfo;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="queryParms"></param>
        /// <returns></returns>
        public async Task<List<PatRegistOutDto>> GetList(PatRegistInputDto queryParms)
        {
            List<PatRegistOutDto> registDto = null;
            var lstPatient = await firstAidApiClient.GetRegistList(queryParms);

            if (lstPatient != null)
            {
                registDto = lstPatient.MapTo<List<PatRegistOutDto>>();
            }
            return registDto;
        }

        /// <summary>
        /// 获取院前评分
        /// </summary>
        /// <param name="relationEntity"></param>
        /// <returns></returns>
        public async Task<List<MedPatScore>> GetMedPatScore(MedPatRelationInfo relationEntity)
        {
            List<MedPatScore> medPatScores = new List<MedPatScore>();
            if (string.IsNullOrEmpty(relationEntity?.MissionId) || string.IsNullOrEmpty(relationEntity?.PhepId))
                return medPatScores;

            var firstAidData = firstAidApiClient.GetMissionInfo(relationEntity.PhepId);
            if (firstAidData == null) return medPatScores;


            logger.LogDebug($"firstAidData.Wound：{firstAidData.Wound.ToJson()}");
            if (firstAidData.Wound != null)
            {
                #region GCS评分
                if (firstAidData.Wound.EValue.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.GCS.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };
                    patScore.ScoreItem = "睁眼";
                    patScore.ScoreResult = firstAidData.Wound.EValue.Value.ToString();
                    medPatScores.Add(patScore);
                }

                if (firstAidData.Wound.MValue.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.GCS.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };
                    patScore.ScoreItem = "运动";
                    patScore.ScoreResult = firstAidData.Wound.MValue.Value.ToString();
                    medPatScores.Add(patScore);
                }

                if (firstAidData.Wound.VValue.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.GCS.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };
                    patScore.ScoreItem = "语言";
                    patScore.ScoreResult = firstAidData.Wound.VValue.Value.ToString();
                    medPatScores.Add(patScore);
                }
                #endregion
            }

            if (firstAidData.TraumaIndex != null)
            {
                var tiData = firstAidData.TraumaIndex;
                #region TI评分
                if (tiData.InjuredPosition.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.TI.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };

                    patScore.ScoreItem = "受伤部位";
                    patScore.ScoreResult = tiData.InjuredPosition.ToString();
                    medPatScores.Add(patScore);
                }

                if (tiData.InjuryType.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.TI.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };

                    patScore.ScoreItem = "损伤类型";
                    patScore.ScoreResult = tiData.InjuryType.ToString();
                    medPatScores.Add(patScore);
                }

                if (tiData.ExternalHemorrhage.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.TI.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };

                    patScore.ScoreItem = "外出血";
                    patScore.ScoreResult = tiData.ExternalHemorrhage.ToString();
                    medPatScores.Add(patScore);
                }

                if (tiData.BloodPressure.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.TI.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };

                    patScore.ScoreItem = "血压";
                    patScore.ScoreResult = tiData.BloodPressure.ToString();
                    medPatScores.Add(patScore);
                }

                if (tiData.Pulse.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.TI.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };

                    patScore.ScoreItem = "脉搏";
                    patScore.ScoreResult = tiData.Pulse.ToString();
                    medPatScores.Add(patScore);
                }

                if (tiData.BreathState.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.TI.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };

                    patScore.ScoreItem = "呼吸状态";
                    patScore.ScoreResult = tiData.BreathState.ToString();
                    medPatScores.Add(patScore);
                }

                if (tiData.ConsciousState.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.TI.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };

                    patScore.ScoreItem = "意识状态";
                    patScore.ScoreResult = tiData.ConsciousState.ToString();
                    medPatScores.Add(patScore);
                }
                #endregion
            }

            if (firstAidData.Stroke != null)
            {
                //FAST评分
                if (firstAidData.Stroke.Face.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.FAST.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };
                    patScore.ScoreItem = "FACE";
                    patScore.ScoreResult = firstAidData.Stroke.Face.Value.ToString();
                    medPatScores.Add(patScore);
                }

                //FAST评分
                if (firstAidData.Stroke.Arm.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.FAST.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };
                    patScore.ScoreItem = "ARM";
                    patScore.ScoreResult = firstAidData.Stroke.Arm.Value.ToString();
                    medPatScores.Add(patScore);
                }

                //FAST评分
                if (firstAidData.Stroke.Speach.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.FAST.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };
                    patScore.ScoreItem = "SPEACH";
                    patScore.ScoreResult = firstAidData.Stroke.Speach.Value.ToString();
                    medPatScores.Add(patScore);
                }

                //FAST评分
                if (firstAidData.Stroke.Times.HasValue)
                {
                    MedPatScore patScore = new MedPatScore()
                    {
                        PatientNo = relationEntity.Id,
                        ScoreType = ScoreType.FAST.ToString(),
                        ScoreSource = ScoreSource.PreHospital.ToString("d"),
                    };
                    patScore.ScoreItem = "TIMES";
                    patScore.ScoreResult = firstAidData.Stroke.Times.Value.ToString();
                    medPatScores.Add(patScore);
                }
            }

            return medPatScores;
        }

        /// <summary>
        /// 获取心电图
        /// </summary>
        /// <param name="relationEntity"></param>
        /// <returns></returns>
        public async Task<List<MedPatEcgInfo>> RunEcg(MedPatRelationInfo realtionEntity)
        {
            List<MedPatEcgInfo> lstEcg = new List<MedPatEcgInfo>();
            if (string.IsNullOrEmpty(realtionEntity?.MissionId) || string.IsNullOrEmpty(realtionEntity?.PhepId)
                || string.IsNullOrEmpty(realtionEntity.Id)) return lstEcg;

            #region 备份代码

            //logger.LogDebug($"获取院前心电图列表start，PhepId:{realtionEntity.PhepId},MissionId：{realtionEntity.MissionId}");
            //var ecgs = await firstAidEcgClient.GetEcgDataList(realtionEntity.PhepId, realtionEntity.MissionId);
            //logger.LogDebug($"获取院前心电图列表end，ecgs：{ecgs.ToJson()}");
            //if ((ecgs?.Count ?? 0) == 0) return lstEcg;

            //foreach (var ecgItem in ecgs)
            //{
            //    var item = await firstAidEcgClient.GetEcgData(ecgItem.EcgId);
            //    if (!item.EcgTime.HasValue) continue;

            //    var ecgModel = new MedPatEcgInfo()
            //    {
            //        PatientNo = realtionEntity.Id,
            //        ExamTime = item.EcgTime,
            //        DiagSource = EcgSource.PreHospital.ToString("d"),
            //        EcgPath = string.Empty,
            //        EcgData = $"data:image/jpeg;base64,{item.EcgPic}"
            //    };
            //    lstEcg.Add(ecgModel);
            //}

            #endregion

            #region 当前代码

            PatRegistInputDto queryParms = new PatRegistInputDto()
            { BeginTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1), EndTime = DateTime.Now };
            var lstPatient = await firstAidApiClient.GetRegistList(queryParms);
            if (lstPatient != null)
            {
                DateTime ecgtime;
                DateTime diagtime;
                lstPatient.ForEach(item=> {
                    if (item.ECGPATIENTID.Equals(realtionEntity.OtherNo))
                    {
                        var ecgModel = new MedPatEcgInfo();
                        ecgModel.PatientNo = realtionEntity.Id;
                        ecgModel.EcgPatientId = item.ECGPATIENTID;
                        ecgModel.DiagSource = EcgSource.PreHospital.ToString("d");
                        if (!string.IsNullOrEmpty(item.ECG_TIME))
                        {
                            if (DateTime.TryParse(item.ECG_TIME, out ecgtime))
                            {
                                ecgModel.ExamTime = ecgtime;//心电图时间
                                ecgModel.DiagTime = ecgtime;//心电图诊断时间
                            }
                        }
                        ecgModel.Id = System.Guid.NewGuid();
                        ecgModel.Name= item.NAME;
                        ecgModel.Sex = item.GENDER;
                        ecgModel.Age = item.AGE;
                        ecgModel.DiagDesc = item.SYMPTOM;
                        ecgModel.EcgPath = item.ECG_URL;
                        ecgModel.EcgUrl = item.ECG_URL;
                        ecgModel.EcgData = $"data:image/jpeg;base64,{item.ECG_URL}";
                        ecgModel.PdfData = $"data:image/jpeg;base64,{item.ECG_URL}";
                        lstEcg.Add(ecgModel);
                    }
                });
            }

            #endregion

            return lstEcg;
        }

        /// <summary>
        /// 获取腕带时间节点数据
        /// </summary>
        /// <param name="relationEntity"></param>
        /// <returns></returns>
        public async Task<List<MedPatTimeNodes>> SyncMedPatTimeNode(MedPatRelationInfo relationEntity)
        {
            logger.LogDebug($"院前腕带:{relationEntity.ToJson()}");
            var timeNodes = new List<MedPatTimeNodes>();
            if (string.IsNullOrEmpty(relationEntity?.Id)) return timeNodes;
            if ((string.IsNullOrEmpty(relationEntity.PhepId) || string.IsNullOrEmpty(relationEntity.MissionId)) &&
                string.IsNullOrEmpty(relationEntity.EmergencyId))
                return timeNodes;

            var requestRfidModel = new
            {
                PhepPatientId = relationEntity.PhepId,
                PhepVisitId = relationEntity.MissionId,
                PatientId = relationEntity.EmergencyId,
                VisitId = relationEntity.EmergencyId
            };

            logger.LogDebug($"requestRfidModel:{requestRfidModel.ToJson()}");
            var rfidData = await firstAidApiClient.GetRfidInfo(relationEntity.PhepId, relationEntity.MissionId,
                relationEntity.EmergencyId, relationEntity.EmergencyId);

            //logger.LogDebug($"rfidData:{rfidData.ToJsonString()}");
            //腕带数据
            timeNodes = ConvertToListMedPatTimeNodes(relationEntity.Id, rfidData);
            logger.LogDebug($"timeNodes:{timeNodes.ToJson()}");
            return timeNodes;
        }

        /// <summary>
        /// 同步患者详细数据
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public async Task<string> SyncPatientData(MedPatRelationInfo relation)
        {
            logger.LogDebug($"进入同步急救患者流程");
            MedPhepPatientInfo phepPatientInfo = null;
            MedPatientDetailsInfo detail = null;
            string ordCode = null;

            MedPatientRegistInfo regist = new MedPatientRegistInfo();

            if (string.IsNullOrEmpty(relation?.PhepId)) return ordCode;
            try
            {
                detail = new MedPatientDetailsInfo();
                var firstAidData = firstAidApiClient.GetMissionInfo(relation.PhepId);
                if (firstAidData != null)
                {
                    logger.LogDebug($"参数:{relation.ToJson()},MDSD院前信息:,Data:{firstAidData.ToJson()}");

                    phepPatientInfo = firstAidData.MapTo<MedPhepPatientInfo>();
                    phepPatientInfo.Id = relation.Id;


                    regist.Id = relation.Id;
                    regist.Name = phepPatientInfo.Name;
                    regist.Sex = phepPatientInfo.Sex;
                    regist.Age = phepPatientInfo.Res1;

                    detail.Id = relation.Id;
                    detail.AdmissionTime = firstAidData.Mission.ArrivedHospitalTime;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"院前异常:{ex.ToString()}");
            }

            try
            {
                Check.NotNull(phepPatientInfo);
                MedPhepPatientInfo phepDb = await medPhepPatientInfo.GetAsync(relation.Id);
                if (phepDb == null)
                {
                    await medPhepPatientInfo.InsertAsync(phepPatientInfo);
                }
                else
                {
                    phepDb = phepPatientInfo;
                    await medPhepPatientInfo.UpdateAsync(phepDb);
                }

                Check.NotNull(regist);
                MedPatientRegistInfo registDb = await medPatientRegistInfo.GetAsync(relation.Id);
                if (registDb == null)
                {
                    await medPatientRegistInfo.InsertAsync(regist);
                }
                else
                {
                    registDb = MergePatRegistInfo(registDb, regist);
                    await medPatientRegistInfo.UpdateAsync(registDb);
                }

                Check.NotNull(detail);
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
                logger.LogError($"急救患者基本信息同步失败,{ex.Message}");
            }
            return ordCode;
        }

        #region 辅助方法
        private MedPatientRegistInfo MergePatRegistInfo(MedPatientRegistInfo registDb, MedPatientRegistInfo regist)
        {
            if (string.IsNullOrEmpty(registDb.Name)) registDb.Name = regist.Name;
            if (!registDb.DateOfBirth.HasValue) registDb.DateOfBirth = regist.DateOfBirth;
            if (string.IsNullOrEmpty(registDb.Sex)) registDb.Sex = regist.Sex;
            if (string.IsNullOrEmpty(registDb.IdNo)) registDb.IdNo = regist.IdNo;
            if (string.IsNullOrEmpty(registDb.CitizenCard)) registDb.CitizenCard = regist.CitizenCard;
            if (string.IsNullOrEmpty(registDb.PatJob)) registDb.PatJob = regist.PatJob;
            if (string.IsNullOrEmpty(registDb.MailAddress)) registDb.MailAddress = regist.MailAddress;
            if (string.IsNullOrEmpty(registDb.PhoneNumber)) registDb.PhoneNumber = regist.PhoneNumber;
            if (string.IsNullOrEmpty(registDb.ComeType)) registDb.ComeType = regist.ComeType;
            return registDb;
        }

        private List<MedPatTimeNodes> ConvertToListMedPatTimeNodes(string patientNo, RfidData rfidData)
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

                    if (patTimeNodes.ItemValue.HasValue)
                        timeNodes.Add(patTimeNodes);
                }
            });
            return timeNodes;
        }
        #endregion
    }
}
