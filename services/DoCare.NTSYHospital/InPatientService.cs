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
using System.Threading.Tasks;

namespace DoCare.NTSYHospital
{
    public class InPatientService : IInPatient, IOutcomeInfo, IMedication, IInspection, IMedPatRelation, IExamination
    {
        private readonly ILogger<InPatientService> logger;
        private readonly HisDriver hisDriver;
        private readonly LisDriver lisDriver;
        private readonly IRepository<MedPatientRegistInfo, string> medPatientRegistInfo;
        private readonly IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo;
        public InPatientService(ILogger<InPatientService> logger
            , HisDriver hisDriver
            , LisDriver lisDriver
            , IRepository<MedPatientRegistInfo, string> medPatientRegistInfo
            , IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo)
        {
            this.logger = logger;
            this.hisDriver = hisDriver;
            this.lisDriver = lisDriver;
            this.medPatientRegistInfo = medPatientRegistInfo;
            this.medPatientDetailsInfo = medPatientDetailsInfo;
        }

        public async Task<List<PatRegistOutDto>> GetList(PatRegistInputDto queryParms)
        {
            List<PatRegistOutDto> regists = new List<PatRegistOutDto>();
            if (queryParms == null) return regists;

            var lstPatient = await hisDriver.MatchHisInHosPatientVM(queryParms);
         

            if (lstPatient != null && lstPatient.Count > 0)
            {
                regists = lstPatient.MapTo<List<PatRegistOutDto>>();
            }

            return regists;
        }


        public async Task<string> SyncPatientData(MedPatRelationInfo relation)
        {
            Check.NotNullOrEmpty(relation?.Id, nameof(relation.Id));
            Check.NotNullOrEmpty(relation?.InpRegistId, nameof(relation.InpRegistId));

            MedPatientRegistInfo regist = null;
            MedPatientDetailsInfo detail = null;

            try
            {
                ViewInHosPatientInfo viewPatientInfo = await hisDriver.GetViewInHosPatientInfo(relation.InpRegistId);

                if (viewPatientInfo != null)
                {
                    regist = viewPatientInfo.MapTo<MedPatientRegistInfo>();
                    regist.Id = relation.Id;
                    regist.ComeType = ComingType.Inhospital.ToString("d");

                    detail = viewPatientInfo.MapTo<MedPatientDetailsInfo>();
                    detail.Id = relation.Id;

                    ViewPatientInHospital viewPatientInHospital = await hisDriver.GetViewPatientInHospital(relation.InpRegistId);

                    if (viewPatientInHospital != null)
                    {
                        detail = MergePatDetailsInfo(viewPatientInHospital, detail);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"SyncPatientData-住院患者基本信息获取失败,{ex.Message}");
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
                logger.LogError($"SyncPatientData-住院患者基本信息同步失败,{ex.Message}");
            }
            return "";
        }

        public async Task<MedPatientDetailsInfo> GetOutcomeInfo(MedPatRelationInfo relation)
        {
            MedPatientDetailsInfo detail = null;
            if (string.IsNullOrEmpty(relation?.InpRegistId)) return detail;

            ViewPatientOutHospital viewPatientOutHospital = await hisDriver.GetViewPatientOutHospital(relation.InpRegistId);

            if (viewPatientOutHospital == null) return detail;

            detail = await medPatientDetailsInfo.GetAsync(relation.Id);
            if (detail == null) return detail;

            detail.OutHosDiagnosis = viewPatientOutHospital.OUT_DIAGNOSIS;
            detail.OutDeptTime = viewPatientOutHospital.OUT_DEPT_TIME;
            detail.OutHosTime = viewPatientOutHospital.OUT_HOS_TIME;
            detail.HosDay = viewPatientOutHospital.HOS_DAY.ToString();
            detail.TotalCost = viewPatientOutHospital.TOTAL_COST.ToString();
            detail.DrugCost = viewPatientOutHospital.DRUG_COST.ToString();
            detail.ExamCost = viewPatientOutHospital.EXAM_COST.ToString();
            detail.OutWay = viewPatientOutHospital.OUT_WAY;
            return detail;
        }

        public async Task<MedicationDto> GetMedications(MedPatRelationInfo relation)
        {
            MedicationDto result = null;

            if (string.IsNullOrEmpty(relation?.Id) || string.IsNullOrEmpty(relation.InpRegistId)) return result;
            try
            {
                var medications = await hisDriver.GetViewPatientOrdersByInp(relation.InpRegistId);
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
                logger.LogError($"获取住院用药信息异常:{ex.ToString()}");
            }
            return result;
        }

        public async Task<InspectionDto> GetInspections(MedPatRelationInfo relation)
        {
            InspectionDto inspectionDto = new InspectionDto();
            List<MedLabMaster> listMedLabMaster = new List<MedLabMaster>();
            List<MedLabResult> listMedLabResult = new List<MedLabResult>();

            if (string.IsNullOrEmpty(relation.InpRegistId)) return inspectionDto;

            List<ViewLabMaster> masterList = await lisDriver.GetViewLabMasterByInp(relation.InpRegistId);

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

        public async Task<MedPatRelationInfo> SyncPatientRelationNo(MedPatRelationInfo relationEntity)
        {
            return relationEntity;
        }

        #region private
        private MedPatientDetailsInfo MergePatDetailsInfo(ViewPatientInHospital viewPatientInHospital, MedPatientDetailsInfo detail)
        {
            if (viewPatientInHospital == null || detail == null) return detail;

            detail.InHosTime = viewPatientInHospital.IN_HOS_TIME;
            detail.InDeptCode = viewPatientInHospital.DEPT_CODE;
            detail.InWardCode = viewPatientInHospital.WARD_CODE;
            detail.InHosDiagnosis = viewPatientInHospital.IN_HOS_DIAGNOSIS;
            detail.InDeptTime = viewPatientInHospital.IN_DEPT_TIME.ToDateTime();
            detail.BedNo = viewPatientInHospital.BED_NO;
            detail.Doctor = viewPatientInHospital.DOCTOR;

            return detail;
        }

        public async Task<ExaminationDto> GetExaminations(MedPatRelationInfo relation)
        {
            ExaminationDto examinationDto = new ExaminationDto();
            List<MedExamReport> listMedExamReports = new List<MedExamReport>();

            if (string.IsNullOrEmpty(relation.InpRegistId)) return examinationDto;

            var listMedExamApply = await hisDriver.GetViewExamApply(relation.InpRegistId);
            foreach (var item in listMedExamApply)
            {
                MedExamReport examMaster = item.MapTo<MedExamReport>();
                listMedExamReports.Add(examMaster);
            }
            examinationDto.MedExamReports = listMedExamReports;
            return examinationDto;
            //throw new NotImplementedException();
        }



        #endregion
    }
}
