using DoCare.Core;
using DoCare.Core.Common;
using DoCare.Core.Ecg;
using DoCare.Core.Ecg.Entities;
using DoCare.Core.Exam_Inspec_Medication;
using DoCare.Core.Exam_Inspec_Medication.Dtos;
using DoCare.Core.Exam_Inspec_Medication.Entities;
using DoCare.Core.Patient;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using DoCare.KJCHospital.Driver;
using DoCare.KJCHospital.Entities;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.Mapping;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.KJCHospital
{
    public class OutPatientService : IOutPatient, IMedication, IMedPatRelation, IInspection, IExamination, IEcg
    {
        private readonly ILogger<OutPatientService> logger;
        private readonly IRepository<MedPatientRegistInfo, string> medPatientRegistInfo;
        private readonly IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo;
        private readonly IRepository<ViewOutHosPatientInfo, string> viewOutHosPatientInfo;
        private readonly HisDriver hisDriver;
        private readonly EcgDriver ecgDriver;
        private ViewPatientInfo _viewPatientInfo;

        public OutPatientService(ILogger<OutPatientService> logger
            , IRepository<MedPatientRegistInfo, string> medPatientRegistInfo
            , IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo
            , IRepository<ViewOutHosPatientInfo, string> viewOutHosPatientInfo
            , EcgDriver ecgDriver
            , HisDriver hisDriver)
        {
            this.logger = logger;
            this.medPatientRegistInfo = medPatientRegistInfo;
            this.medPatientDetailsInfo = medPatientDetailsInfo;
            this.hisDriver = hisDriver;
            this.ecgDriver = ecgDriver;
            this.viewOutHosPatientInfo = viewOutHosPatientInfo;
        }

        public async Task<List<PatRegistOutDto>> GetList(PatRegistInputDto queryParms)
        {
            List<PatRegistOutDto> regists = new List<PatRegistOutDto>();
            if (queryParms == null) return regists;

            var lstPatient = await hisDriver.MatchHisPatientVM(queryParms, PatientSourceType.OutPatient);
            if (lstPatient != null && lstPatient.Count > 0)
            {
                regists = lstPatient.MapTo<List<PatRegistOutDto>>();
            }
            return regists;
        }

        public async Task<string> SyncPatientData(MedPatRelationInfo relation)
        {
            Check.NotNullOrEmpty(relation?.Id, nameof(relation.Id));
            Check.NotNullOrEmpty(relation?.RegistId, nameof(relation.RegistId));

            MedPatientRegistInfo regist = null;
            MedPatientDetailsInfo detail = null;
            string orgCode = null;

            try
            {
                if (_viewPatientInfo == null)
                {
                    _viewPatientInfo = await hisDriver.GetViewPatientInfo(relation.RegistId, PatientSourceType.OutPatient);
                }

                if (_viewPatientInfo != null)
                {
                    regist = _viewPatientInfo.MapTo<MedPatientRegistInfo>();
                    regist.Id = relation.Id;
                    //regist.ComeType = ComingType.Inhospital.ToString("d");
                    orgCode = regist.OrgCode;

                    detail = _viewPatientInfo.MapTo<MedPatientDetailsInfo>();
                    detail.Id = relation.Id;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"SyncPatientData-门诊患者基本信息获取失败,{ex.Message}");
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
                MedPatientDetailsInfo detailDb = await medPatientDetailsInfo.GetAsync(relation.Id);
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
                logger.LogError($"SyncPatientData-门诊患者基本信息同步失败,{ex.Message}");
            }
            return orgCode;
        }

        public ViewOutHosPatientInfo ViewOutHosPatientInfo
        {
            get; set;
        }

        public async Task<MedPatRelationInfo> SyncPatientRelationNo(MedPatRelationInfo relationEntity)
        {
            var relation = relationEntity;
            Check.NotNull(relation, nameof(relation));

            //如果患者来自急诊急救,从急诊服务里同步门诊号
            if (relation.PatientSource == PatientSourceType.OutPatient.ToString("d") || relation.PatientSource == PatientSourceType.InPatient.ToString("d"))
            {
                if (!string.IsNullOrEmpty(relation.RegistId))
                {
                    var viewPatientMapInfo = await hisDriver.GetViewPatientMapInfoByRegistId(relation.OtherNo, relation.RegistId, 2);
                    if (viewPatientMapInfo != null)
                    {
                        relation.ClinicNo = viewPatientMapInfo.Id;
                        //relation.InpRegistId = viewPatientMapInfo.Id;
                    }
                }
            }
            return relation;
        }

        public async Task<MedicationDto> GetMedications(MedPatRelationInfo relation)
        {
            MedicationDto result = null;

            if (string.IsNullOrEmpty(relation?.OtherNo) || string.IsNullOrEmpty(relation.RegistId)) return result;
            try
            {
                var medications = await hisDriver.GetViewPatientOrders(relation.OtherNo, relation.RegistId);
                if (medications != null)
                {
                    result = new MedicationDto();
                    result.MedPatOrderses = medications.MapTo<List<MedPatOrders>>();
                }

                if (result?.MedPatOrderses != null)
                {
                    result.MedPatOrderses.ForEach(a =>
                    {
                        a.PatientNo = relation.Id;
                        a.ClinicNo = relation.ClinicNo;
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"获取门诊用药信息异常:{ex.ToString()}");
            }
            return result;
        }

        public async Task<InspectionDto> GetInspections(MedPatRelationInfo relation)
        {
            InspectionDto inspectionDto = new InspectionDto();
            List<MedLabMaster> listMedLabMaster = new List<MedLabMaster>();
            List<MedLabResult> listMedLabResult = new List<MedLabResult>();

            if (string.IsNullOrEmpty(relation.OtherNo)) return inspectionDto;

            List<ViewLabMaster> masterList = await hisDriver.GetViewLabMaster(relation.OtherNo);

            foreach (ViewLabMaster master in masterList)
            {
                List<ViewLabResult> resultList = await hisDriver.GetViewLabResult(master.Id);

                if (resultList != null && resultList.Count > 0)
                {
                    MedLabMaster medLabMaster = master.MapTo<MedLabMaster>();
                    medLabMaster.PatientNo = relation.Id;

                    resultList = resultList.OrderBy(o => o.REPORT_ITEM_CODE).ToList();
                    int ItemNo = 1;
                    foreach (ViewLabResult result in resultList)
                    {
                        MedLabResult medLabResult = result.MapTo<MedLabResult>();
                        //视图文档缺少ItemNo字段,拼接生成
                        medLabResult.Id = result.TEST_NO + "_" + ItemNo.ToString();
                        ItemNo++;
                        medLabResult.ResultDateTime = medLabMaster.ResultRptDateTime;
                        listMedLabResult.Add(medLabResult);
                    }

                    listMedLabMaster.Add(medLabMaster);
                }
            }

            inspectionDto.MedLabMasters = listMedLabMaster;
            inspectionDto.MedLabResults = listMedLabResult;

            return inspectionDto;
        }

        public async Task<ExaminationDto> GetExaminations(MedPatRelationInfo relation)
        {
            ExaminationDto examinationDto = new ExaminationDto();
            if (string.IsNullOrEmpty(relation?.Id) || string.IsNullOrEmpty(relation?.OtherNo))
                return examinationDto;

            try
            {
                var examReportList = await hisDriver.GetViewExamReport(relation.OtherNo);
                if (examReportList != null)
                {
                    examinationDto.MedExamReports = new List<MedExamReport>();
                    examinationDto.MedExamReports = examReportList.MapTo<List<MedExamReport>>();
                }

                if (examinationDto?.MedExamReports != null)
                {
                    examinationDto.MedExamReports.ForEach(a =>
                    {
                        a.PatientNo = relation.Id;
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"获取门诊患者检查数据错误:{ex.ToString()}");
            }
            return examinationDto;
        }

        public async Task<List<MedPatEcgInfo>> RunEcg(MedPatRelationInfo relation)
        {
            List<MedPatEcgInfo> lstEcg = new List<MedPatEcgInfo>();
            if (string.IsNullOrEmpty(relation?.Id) || string.IsNullOrEmpty(relation?.RegistId))
                return lstEcg;

            List<ViewEcgInfo> viewEcgList = await ecgDriver.GetViewEcgInfo(relation.RegistId);
            if (viewEcgList != null && viewEcgList.Count > 0)
            {
                lstEcg = viewEcgList.MapTo<List<MedPatEcgInfo>>();
                lstEcg.ForEach(o =>
                {
                    o.PatientNo = relation.Id;
                });
            }
            return lstEcg;
        }
    }
}
