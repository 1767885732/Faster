using DoCare.Core.Common;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.EFCore;
using FastFrame.Entity;
using FastFrame.EventBuses;
using FastFrame.Filter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DoCare.Core.Patient
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class PatientService
    {
        private readonly IFirstAid firstAid;
        private readonly IEmergency emergency;
        private readonly IOutPatient outPatient;
        private readonly IInPatient inPatient;
        private readonly IRepository<MedPhepPatientInfo, string> medPhepPatientInfo;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelation;
        private readonly IRepository<MedPatientRegistInfo, string> medPatRegist;
        private readonly IRepository<MedPatientDetailsInfo, string> medPatDetail;
        private readonly IFilterService filterService;
        private readonly ILogger<PatientService> logger;
        public PatientService(IFirstAid firstAid, IEmergency emergency, IOutPatient outPatient,
           IInPatient inPatient, IFilterService filterService, IRepository<MedPatRelationInfo, string> medPatRelation,
           IRepository<MedPhepPatientInfo, string> medPhepPatientInfo,
           IRepository<MedPatientRegistInfo, string> medPatRegist,
           IRepository<MedPatientDetailsInfo, string> medPatDetail,
            ILogger<PatientService> logger)
        {
            this.firstAid = firstAid;
            this.emergency = emergency;
            this.outPatient = outPatient;
            this.inPatient = inPatient;
            this.filterService = filterService;
            this.medPatRelation = medPatRelation;
            this.medPatRegist = medPatRegist;
            this.medPatDetail = medPatDetail;
            this.medPhepPatientInfo = medPhepPatientInfo;
            this.logger = logger;
        }

        public async Task<List<PatRegistOutDto>> Query(PatRegistInputDto dto)
        {
            List<PatRegistOutDto> regists = null;
            if ((!dto.BeginTime.HasValue || !dto.EndTime.HasValue) && string.IsNullOrEmpty(dto.PatientId)) return regists;

            var pSource = (PatientSourceType)dto.DataSource;
            //var pSource = PatientSourceType.InPatient;
            if (pSource == PatientSourceType.FirstAid)
            {
                if (firstAid is IPatient firstAidPatient)
                {
                    regists = await firstAidPatient.GetList(dto);
                    if (dto.IsFliter == 1)
                    {
                        var list = new List<PatRegistOutDto>();
                        var nowPatList = medPatRelation.Query().Where(x => x.PatientSource == "0").Select(x => x.PhepId).ToList();
                        foreach (var i in regists)
                        {
                            if (!nowPatList.Contains(i.RelationId))
                            {
                                list.Add(i);
                            }
                        }
                        regists = list;
                    }
                }
            }
            else if (pSource == PatientSourceType.Emergency)
            {
                if (emergency is IPatient emergencyPatient)
                {
                    regists = await emergencyPatient.GetList(dto);
                }
            }
            else if (pSource == PatientSourceType.OutPatient)
            {
                if (outPatient is IPatient outPatientImpl)
                {
                    regists = await outPatientImpl.GetList(dto);
                }
            }
            else if (pSource == PatientSourceType.InPatient)
            {
                if (inPatient is IPatient inpatientImpl)
                {
                    regists = await inpatientImpl.GetList(dto);
                }
            }
            return regists;
        }

        /// <summary>
        /// 患者推送使用
        /// </summary>
        public async Task<MedPatRelationInfo> Save(string pId, PatientSourceType sourceType, string patientId)
        {
            Check.NotNullOrEmpty(pId, nameof(pId));
            pId = pId.Trim();
            Expression<Func<MedPatRelationInfo, bool>> expression = null;

            MedPatRelationInfo entity = new MedPatRelationInfo();
            FilterGroup group = new FilterGroup();
            entity.OtherNo = patientId;

            switch (sourceType)
            {
                case PatientSourceType.FirstAid:
                    #region 院前解析
                    var array = pId.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (array.Length == 1)
                    {
                        group.AddRule(nameof(MedPatRelationInfo.PhepId), pId);
                        entity.PhepId = pId;
                    }
                    else if (array.Length == 2)
                    {
                        group.AddRule(nameof(MedPatRelationInfo.PhepId), array[0]);
                        group.AddRule(nameof(MedPatRelationInfo.MissionId), array[1]);

                        entity.PhepId = array[0];
                        entity.MissionId = array[1];
                    }
                    else
                    {
                        throw new Exception("不支持得院前ID格式");
                    }
                    #endregion
                    entity.PatientSource = PatientSourceType.FirstAid.ToString("D");
                    break;
                case PatientSourceType.Emergency:
                    group.AddRule(nameof(MedPatRelationInfo.EmergencyId), pId);

                    entity.EmergencyId = pId;
                    entity.PatientSource = PatientSourceType.Emergency.ToString("D");
                    break;
                case PatientSourceType.OutPatient:
                    group.AddRule(nameof(MedPatRelationInfo.RegistId), pId);

                    entity.RegistId = pId;
                    entity.PatientSource = PatientSourceType.OutPatient.ToString("D");
                    break;
                case PatientSourceType.InPatient:
                    group.AddRule(nameof(MedPatRelationInfo.InpRegistId), pId);

                    entity.InpRegistId = pId;
                    entity.PatientSource = PatientSourceType.InPatient.ToString("D");
                    break;
                default:
                    break;
            }

            expression = filterService.GetExpression<MedPatRelationInfo>(group);
            var dbEntity = await medPatRelation.GetFirstAsync(expression);
            if (!string.IsNullOrEmpty(dbEntity?.Id)) return dbEntity;

            try
            {
                entity.Id = GetSerialNo();
                var affect = await medPatRelation.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                if (Singleton<PatientNoData>.Instance != null)
                    Singleton<PatientNoData>.Instance.OrderNo = 0;
                throw ex;
            }
            return entity;
        }

        /// <summary>
        /// 更新关系数据
        /// </summary>
        public async Task<bool> Save(SyncRelationInputDto dto)
        {
            var result = false;
            var relation = await medPatRelation.GetAsync(dto.PatientNo);
            if (relation == null) return false;

            MedPatNoType patNoType = (MedPatNoType)dto.RelationNoType;

            if (patNoType == MedPatNoType.FirstAidNo)
            {
                var ids = dto.RelationNo.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length == 2)
                {
                    relation.PhepId = ids[0];
                    relation.MissionId = ids[1];
                }
            }
            else if (patNoType == MedPatNoType.EmergencyNo)
            {
                relation.EmergencyId = dto.RelationNo;
            }
            else if (patNoType == MedPatNoType.ClinicNo)
            {
                relation.RegistId = dto.RelationNo;
            }
            else if (patNoType == MedPatNoType.InPatientNo)
            {
                relation.InpRegistId = dto.RelationNo;
            }
            else if (patNoType == MedPatNoType.MacNo)
            {
                relation.MacNo = dto.RelationNo;
            }
            else if (patNoType == MedPatNoType.MotherIdCard)
            {
                relation.MotherIdCard = dto.RelationNo;
            }
            else
            {
                return false;
            }

            relation.CreatedTime = DateTime.Now;
            var affect = await medPatRelation.UpdateAsync(relation);
            result = affect > 0;
            return result;
        }

        /// <summary>
        /// 同步患者基本信息
        /// </summary>
        /// <param name="patientNo">患者编号</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SyncPatientBasicInfo(string patientNo)
        {
            var relation = await SyncMedPatRelation(patientNo);
            //Check.NotNull(relation, nameof(relation));
            //await medPatRelation.UpdateAsync(relation);
            //logger.LogDebug($"SyncPatientBasicInfo-relation:{relation.ToJson()}");

            string ordCode = null;
            if (relation.PatientSource != PatientSourceType.OutPatient.ToString("d"))
            {
                logger.LogError("A2");

                #region 急救信息
                try
                {
                    if (firstAid is IPatient firstAidPatient)
                    {
                        if (string.IsNullOrEmpty(relation.PhepId))
                        {
                            // throw new Exception("缺少院前相关ID");
                        }
                        else
                        {
                            string syncOrdCode = await firstAidPatient.SyncPatientData(relation);
                            if (!string.IsNullOrEmpty(syncOrdCode))
                            {
                                ordCode = syncOrdCode;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncPatientBasicInfo-急救同步异常，{ex.Message}");
                }
                #endregion

                #region 急诊
                try
                {
                    if (emergency is IPatient emergencyPatient && !string.IsNullOrEmpty(relation.EmergencyId))
                    {
                        string syncOrdCode = await emergencyPatient.SyncPatientData(relation);
                        if (!string.IsNullOrEmpty(syncOrdCode))
                        {
                            ordCode = syncOrdCode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncPatientBasicInfo-急诊同步异常，{ex.Message}");
                }
                #endregion
            }
            else if (relation.PatientSource != PatientSourceType.Emergency.ToString("d"))
            {
                logger.LogError("A3");
                #region 门诊
                try
                {
                    if (outPatient is IPatient outPatientImpl && !string.IsNullOrEmpty(relation.RegistId))
                    {
                        string syncOrdCode = await outPatientImpl.SyncPatientData(relation);
                        if (!string.IsNullOrEmpty(syncOrdCode))
                        {
                            ordCode = syncOrdCode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncPatientBasicInfo-门诊同步异常，{ex.Message}");
                }
                #endregion
            }
            #region 住院
            try
            {
                if (inPatient is IPatient inpatientImpl && !string.IsNullOrEmpty(relation.InpRegistId))
                {
                    logger.LogError("A4");
                    string syncOrdCode = await inpatientImpl.SyncPatientData(relation);
                    if (!string.IsNullOrEmpty(syncOrdCode))
                    {
                        ordCode = syncOrdCode;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"SyncPatientBasicInfo-住院同步异常，{ex.Message}");
            }
            #endregion
            logger.LogDebug($"同步患者基本信息成功");

            if (!string.IsNullOrEmpty(ordCode))
            {
                logger.LogError("A5");
                relation.OrgCode = ordCode;
                await medPatRelation.UpdateAsync(relation);
                logger.LogDebug($"SyncPatientBasicInfo-同步院区代码ordCode:{ordCode}");
            }

            return true;
        }

        public async Task<bool> SyncPatientId(string patientNo, string clinicno) 
        {
            logger.LogError("A1");
            var relation = medPatRelation.Query().Where(x => x.EmergencyId == clinicno).FirstOrDefault();
            logger.LogError("A2"+ relation.Id);
            if (relation != null)
            {
                logger.LogError("已查询到关系表中该患者数据" + clinicno);
                relation.OtherNo = patientNo;
                await medPatRelation.UpdateAsync(relation);
            }
            else 
            {
                logger.LogError("未查询到关系表中该患者数据"+ clinicno);
            }

            return true;
        }

        /// <summary>
        /// 同步患者转归信息
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task<bool> SyncPatOutProcess(SyncPatientDto dto)
        {
            Check.NotNull(dto);
            var result = false;
            //测试代码
            //var relation = new MedPatRelationInfo();
            //relation.Id = dto.PatientNo;
            //relation.InpRegistId = relation.Id + "36";
            //relation.RegistId = relation.InpRegistId;
            var relation = await medPatRelation.GetAsync(dto.PatientNo);
            if (relation == null) return false;

            MedPatientDetailsInfo detail = null;
            if (firstAid is IOutcomeInfo firstAidOutcomeInfoImpl)
            {
                try
                {
                    detail = await firstAidOutcomeInfoImpl.GetOutcomeInfo(relation);
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncPatOutProcess-急救同步错误:{ex.Message}");
                }
                goto save;
            }

            if (emergency is IOutcomeInfo emergencyOutcomeInfoImpl)
            {
                try
                {
                    detail = await emergencyOutcomeInfoImpl.GetOutcomeInfo(relation);
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncPatOutProcess-急诊同步错误:{ex.Message}");
                }
                goto save;
            }

            if (outPatient is IOutcomeInfo outPatientOutcomeInfoImpl)
            {
                try
                {
                    detail = await outPatientOutcomeInfoImpl.GetOutcomeInfo(relation);
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncPatOutProcess-门诊同步错误:{ex.Message}");
                }
                goto save;
            }

            if (inPatient is IOutcomeInfo inpatientOutcomeInfoImpl)
            {
                try
                {
                    detail = await inpatientOutcomeInfoImpl.GetOutcomeInfo(relation);
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncPatOutProcess-住院同步错误:{ex.Message}");
                }
                goto save;
            }

        save:
            logger.LogDebug($"同步完转归数据:{detail?.ToJson()}");
            if (string.IsNullOrEmpty(detail?.Id)) return result;

            result = await medPatDetail.UpdateAsync(detail) > 0;
            return result;
        }

        /// <summary>
        /// 同步患者转归信息
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task<object> GetEmergencyNo(string registId)
        {
            if (string.IsNullOrWhiteSpace(registId)) return null;
            MedPatRelationInfo relationInfo = new MedPatRelationInfo() { RegistId = registId };
            if (emergency is IEmergency emergencyImpl)
            {
                try
                {
                    relationInfo = await emergencyImpl.GetEmergencyNo(relationInfo);
                }
                catch (Exception ex)
                {
                    logger.LogError($"GetEmergencyNo-获取急诊号错误:{ex.Message}");
                }
            }
            logger.LogDebug($"获取急诊号:{relationInfo?.ToJson()}");
            if (string.IsNullOrEmpty(relationInfo?.EmergencyId)) return null;

            return new
            {
                PatientId = relationInfo.ClinicNo,
                VisitId = relationInfo.RegistId,
                EmergencyId = relationInfo.EmergencyId
            };
        }

        /// <summary>
        /// 同步患者急诊急救时间信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PhepAutoTimes> SyncPhepAutoTime(SyncPatientDto dto)
        {
            var phepAutoTimes = new PhepAutoTimes();
            var relation = await medPatRelation.GetAsync(dto.PatientNo);
            if (relation == null) return phepAutoTimes;

            if (emergency is IEmergency emergencyImpl)
            {
                try
                {
                    phepAutoTimes = await emergencyImpl.SyncPhepAutoTime(relation);
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncPhepAutoTime-同步患者急诊急救时间信息错误：{ex.Message}");
                }
            }
            return phepAutoTimes;
        }

        /// <summary>
        /// 同步科室字典
        /// </summary>
        /// <returns></returns>
        public async Task<List<MedDeptDict>> SyncDeptDict()
        {
            List<MedDeptDict> deptDict = null;
            if (inPatient is IInPatient inPatientService)
            {
                try
                {
                    deptDict = await inPatientService.SyncDeptDict();
                }
                catch (Exception ex)
                {
                    logger.LogError($"SyncDeptDict-同步科室字典错误：{ex.Message}");
                }
            }
            return deptDict;
        }

        #region private
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
            var entity = medPatRelation.Query().OrderByDescending(a => a.Id).FirstOrDefault(a => a.Id.Contains(prePNo));
            return entity;
        }

        /// <summary>
        /// 同步关系数据
        /// </summary>
        private async Task<MedPatRelationInfo> SyncMedPatRelation(string patientNo)
        {
            logger.LogDebug($"同步关系数据,患者编号:{patientNo}");
            Check.NotNullOrEmpty(patientNo, nameof(patientNo));
            MedPatRelationInfo result = null;
            MedPatRelationInfo relation = await medPatRelation.GetAsync(patientNo);
            if (string.IsNullOrEmpty(relation?.Id) || string.IsNullOrEmpty(relation?.PatientSource))
                return result;

            result = relation;

            if (emergency is IMedPatRelation emergencySyncRelation)
            {
                try
                {
                    result = await emergencySyncRelation.SyncPatientRelationNo(result);
                }
                catch (Exception ex)
                {
                    logger.LogError($"同步急诊关系信息错误:{ex.Message}");
                }
            }

            if (inPatient is IMedPatRelation inPatientSyncRelation)
            {
                try
                {
                    result = await inPatientSyncRelation.SyncPatientRelationNo(result);
                }
                catch (Exception ex)
                {
                    logger.LogError($"同步住院关系信息错误:{ex.Message}");
                }
            }

            return result;
        }
        #endregion
    }
}
