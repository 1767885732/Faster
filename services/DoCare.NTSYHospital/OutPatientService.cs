using DoCare.Core;
using DoCare.Core.Common;
using DoCare.Core.Exam_Inspec_Medication;
using DoCare.Core.Exam_Inspec_Medication.Dtos;
using DoCare.Core.Exam_Inspec_Medication.Entities;
using DoCare.Core.Patient;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using DoCare.NTSYHospital.Entities;
using FastFrame.Data;
using FastFrame.Entity;
using FastFrame.Mapping;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.NTSYHospital
{
    public class OutPatientService : IOutPatient, IMedPatRelation, IMedication, IInspection, IExamination
    {
        private readonly ILogger<OutPatientService> logger;
        private readonly IRepository<MedPatientRegistInfo, string> medPatientRegistInfo;
        private readonly IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo;
        private readonly IRepository<ViewOutHosPatientInfo, string> viewOutHosPatientInfo;
        private readonly HisDriver hisDriver;
        private readonly LisDriver lisDriver;
        private readonly IUnitOfWorkManager unitOfWorkManager;
        public OutPatientService(ILogger<OutPatientService> logger
            , IRepository<MedPatientRegistInfo, string> medPatientRegistInfo
            , IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo
            , IRepository<ViewOutHosPatientInfo, string> viewOutHosPatientInfo
            , HisDriver hisDriver
            , LisDriver lisDriver
            , IUnitOfWorkManager unitOfWorkManager)
        {
            this.logger = logger;
            this.medPatientRegistInfo = medPatientRegistInfo;
            this.medPatientDetailsInfo = medPatientDetailsInfo;
            this.hisDriver = hisDriver;
            this.lisDriver = lisDriver;
            this.viewOutHosPatientInfo = viewOutHosPatientInfo;
            this.unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<List<PatRegistOutDto>> GetList(PatRegistInputDto queryParms)
        {
            List<PatRegistOutDto> regists = new List<PatRegistOutDto>();
            if (queryParms == null) return regists;

            var lstPatient = await hisDriver.MatchHisOutHosPatientVM(queryParms);


            if (lstPatient != null && lstPatient.Count > 0)
            {
                regists = lstPatient.MapTo<List<PatRegistOutDto>>();
            }
            return regists;
        }

        public ViewOutHosPatientInfo ViewOutHosPatientInfo
        {
            get; set;
        }

        public async Task<string> SyncPatientData(MedPatRelationInfo relation)
        {
            Check.NotNullOrEmpty(relation?.Id, nameof(relation.Id));
            Check.NotNullOrEmpty(relation?.RegistId, nameof(relation.RegistId));

            MedPatientRegistInfo regist = null;
            MedPatientDetailsInfo detail = null;

            try
            {
                if (ViewOutHosPatientInfo == null)
                    ViewOutHosPatientInfo = await hisDriver.GetViewOutHosPatientInfo(relation.RegistId);

                if (ViewOutHosPatientInfo != null)
                {
                    regist = ViewOutHosPatientInfo.MapTo<MedPatientRegistInfo>();
                    regist.Id = relation.Id;
                    regist.ComeType = ComingType.Inhospital.ToString("d");

                    detail = ViewOutHosPatientInfo.MapTo<MedPatientDetailsInfo>();
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

                var diags = await hisDriver.GetViewPatientDiagnosis(relation.ClinicNo);
                if (diags != null)
                {
                    var firstDiag = diags.FirstOrDefault(a => a.DIAGNOSIS_TYPE == "主诊断") ??
                        diags.FirstOrDefault();
                    detail.DiagDesc = firstDiag?.DIAGNOSIS_DESC;
                    detail.DiagDoctor = firstDiag?.DOCTOR_ID;
                    detail.DiagTime = firstDiag?.DIAGNOSIS_DATE;
                }

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
            return "";
        }

        public async Task<MedPatRelationInfo> SyncPatientRelationNo(MedPatRelationInfo relationEntity)
        {
            var relation = relationEntity;
            Check.NotNull(relation, nameof(relation));

            ViewPatientMapInfo viewPatientMapInfo = null;
            if (relation.PatientSource == PatientSourceType.FirstAid.ToString("d")
                || relation.PatientSource == PatientSourceType.Emergency.ToString("d"))
            {
                Check.NotNullOrEmpty(relation.RegistId, nameof(relation.RegistId));
                ViewOutHosPatientInfo = await hisDriver.GetViewOutHosPatientInfo(relation.RegistId);
                if (viewOutHosPatientInfo != null)
                    relation.ClinicNo = ViewOutHosPatientInfo.CLINIC_NO;

                viewPatientMapInfo = await hisDriver.GetViewPatientMapInfoByRegistId(relation.RegistId);
            }
            else if (relation.PatientSource == PatientSourceType.OutPatient.ToString("d"))
            {
                Check.NotNullOrEmpty(relation.RegistId, nameof(relation.RegistId));
                ViewOutHosPatientInfo = await hisDriver.GetViewOutHosPatientInfo(relation.RegistId);
                if (viewOutHosPatientInfo != null)
                    relation.ClinicNo = ViewOutHosPatientInfo.CLINIC_NO;

                viewPatientMapInfo = await hisDriver.GetViewPatientMapInfoByRegistId(relation.RegistId);
            }
            else if (relation.PatientSource == PatientSourceType.InPatient.ToString("d"))
            {
                Check.NotNullOrEmpty(relation.InpRegistId, nameof(relation.InpRegistId));
            }
            else
            {
                logger.LogDebug($"不支持得患者来源:PatientSource:{relation.PatientSource}");
            }

            if (viewPatientMapInfo == null) return relation;
            relation.InpRegistId = viewPatientMapInfo.INP_NO;
            relation.InpNo = relation.InpRegistId;
            return relation;
        }

        public async Task<MedicationDto> GetMedications(MedPatRelationInfo relation)
        {
            MedicationDto result = null;

            if (string.IsNullOrEmpty(relation?.Id) || string.IsNullOrEmpty(relation.RegistId)) return result;
            try
            {
                var medications = await hisDriver.GetViewPatientOrdersByRegistId(relation.RegistId);

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

            if (string.IsNullOrEmpty(relation.RegistId)) return inspectionDto;

            List<ViewLabMaster> masterList = await lisDriver.GetViewLabMasterByRegistId(relation.RegistId);

            foreach (ViewLabMaster master in masterList)
            {
                List<ViewLabResult> resultList = await lisDriver.GetViewLabResult(master.Id);

                if (resultList != null && resultList.Count > 0)
                {
                    int ItemNo = 1;
                    foreach (ViewLabResult result in resultList)
                    {
                        MedLabResult medLabResult = result.MapTo<MedLabResult>();
                        //视图文档缺少ItemNo字段,拼接生成
                        medLabResult.ItemNo = result.TEST_NO + "_" + ItemNo.ToString();
                        medLabResult.TestNo = result.TEST_NO;
                        ItemNo++;
                        listMedLabResult.Add(medLabResult);
                    }

                    MedLabMaster medLabMaster = master.MapTo<MedLabMaster>();
                    medLabMaster.PatientNo = relation.Id;

                    listMedLabMaster.Add(medLabMaster);
                }
            }

            inspectionDto.MedLabMasters = listMedLabMaster;
            inspectionDto.MedLabResults = listMedLabResult;

            return inspectionDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public async Task<ExaminationDto> GetExaminations(MedPatRelationInfo relation)
        {
            ExaminationDto examinationDto = new ExaminationDto();
            List<MedExamReport> listMedExamReports = new List<MedExamReport>();

            if (string.IsNullOrEmpty(relation.RegistId)) return examinationDto;

            var listMedExamApply = await lisDriver.GetViewExamApply(relation.RegistId);
            foreach (var item in listMedExamApply)
            {
                MedExamReport examMaster = item.MapTo<MedExamReport>();
                listMedExamReports.Add(examMaster);
            }
            examinationDto.MedExamReports = listMedExamReports;
            return examinationDto;
        }
    }
}
