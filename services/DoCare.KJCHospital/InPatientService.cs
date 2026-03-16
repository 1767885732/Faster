using DoCare.Core;
using DoCare.Core.Common;
using DoCare.Core.Ecg;
using DoCare.Core.Ecg.Entities;
using DoCare.Core.Exam_Inspec_Medication;
using DoCare.Core.Exam_Inspec_Medication.Dtos;
using DoCare.Core.Exam_Inspec_Medication.Entities;
using DoCare.Core.Operation;
using DoCare.Core.Operation.Dtos;
using DoCare.Core.Operation.Entities;
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
    public class InPatientService : IInPatient, IMedication, IMedPatRelation, IInspection, IExamination, IOutcomeInfo, IOperation, IEcg
    {
        private readonly ILogger<InPatientService> logger;
        private readonly IRepository<MedPatientRegistInfo, string> medPatientRegistInfo;
        private readonly IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo;
        private readonly HisDriver hisDriver;
        private readonly OperationDriver operationDriver;
        private readonly EcgDriver ecgDriver;
        private ViewPatientInfo _viewPatientInfo;

        public InPatientService(ILogger<InPatientService> logger
            , IRepository<MedPatientRegistInfo, string> medPatientRegistInfo
            , IRepository<MedPatientDetailsInfo, string> medPatientDetailsInfo
            , HisDriver hisDriver
            , EcgDriver ecgDriver
            , OperationDriver operationDriver)
        {
            this.logger = logger;
            this.medPatientRegistInfo = medPatientRegistInfo;
            this.medPatientDetailsInfo = medPatientDetailsInfo;
            this.hisDriver = hisDriver;
            this.operationDriver = operationDriver;
            this.ecgDriver = ecgDriver;
        }

        public async Task<List<PatRegistOutDto>> GetList(PatRegistInputDto queryParms)
        {
            List<PatRegistOutDto> regists = new List<PatRegistOutDto>();
            if (queryParms == null) return regists;

            var lstPatient = await hisDriver.MatchHisPatientVM(queryParms, PatientSourceType.InPatient);
            if (lstPatient != null && lstPatient.Count > 0)
            {
                List<PatRegistOutDto> temp = lstPatient.MapTo<List<PatRegistOutDto>>();
                foreach (var item in temp)
                {
                    item.DisplayId = item.MrNo;
                    regists.Add(item);
                }
            }
            return regists;
        }

        public async Task<string> SyncPatientData(MedPatRelationInfo relation)
        {
            Check.NotNullOrEmpty(relation?.Id, nameof(relation.Id));
            Check.NotNullOrEmpty(relation?.InpRegistId, nameof(relation.InpRegistId));

            MedPatientRegistInfo regist = null;
            MedPatientDetailsInfo detail = null;
            string orgCode = null;

            try
            {
                if (_viewPatientInfo == null)
                {
                    _viewPatientInfo = await hisDriver.GetViewPatientInfo(relation.InpRegistId, PatientSourceType.InPatient);
                }

                if (_viewPatientInfo != null)
                {
                    regist = _viewPatientInfo.MapTo<MedPatientRegistInfo>();
                    regist.Id = relation.Id;
                    //regist.ComeType = ComingType.Inhospital.ToString("d");
                    orgCode = regist.OrgCode;

                    detail = _viewPatientInfo.MapTo<MedPatientDetailsInfo>();
                    detail.Id = relation.Id;

                    ViewPatientInHospital viewPatientInHospital = await hisDriver.GetViewPatientInHospital(relation.InpRegistId);

                    if (viewPatientInHospital != null)
                    {
                        detail = MergePatDetailsInfo(viewPatientInHospital, detail);
                    }
                }

                _viewPatientInfo = await hisDriver.GetViewPatientInfo(relation.OtherNo);
                if (_viewPatientInfo != null)
                {
                    regist.PhoneNumber = _viewPatientInfo.PHONE_NUMER;
                    regist.ContactName = _viewPatientInfo.NEXT_OF_KIN;
                    regist.PayType = _viewPatientInfo.PAY_TYPE;
                    regist.ContactPhoneNumber = _viewPatientInfo.NEXT_OF_KIN_PHONE;
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
                    registDb = MergePatRegistInfo(registDb, regist);
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

                    detailDb = MergePatDetailsInfo(detailDb, detail);
                    await medPatientDetailsInfo.UpdateAsync(detailDb);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"SyncPatientData-住院患者基本信息同步失败,{ex.Message}");
            }
            return orgCode;
        }

        public async Task<MedPatRelationInfo> SyncPatientRelationNo(MedPatRelationInfo relationEntity)
        {
            var relation = relationEntity;
            Check.NotNull(relation, nameof(relation));

            if (!string.IsNullOrEmpty(relation.RegistId))
            {
                //var viewPatientMapInfo = await hisDriver.GetViewPatientMapInfoByRegistId(relation.OtherNo, relation.RegistId, 1);
                //if (viewPatientMapInfo != null)
                //{
                //    //relation.RegistId = viewPatientMapInfo.INP_NO;
                //    relation.InpNo = viewPatientMapInfo.INP_NO;
                //    relation.InpRegistId = viewPatientMapInfo.INP_NO;
                //}
                var viewPatientInfo = await hisDriver.GetViewPatientInfo(relation.OtherNo);
                if (viewPatientInfo != null)
                {
                    relation.InpNo = viewPatientInfo.MR_NO;
                    relation.InpRegistId = viewPatientInfo.VISIT_NO;
                }
            }
            return relation;
        }


        public async Task<MedPatientDetailsInfo> GetOutcomeInfo(MedPatRelationInfo relation)
        {
            MedPatientDetailsInfo detail = null;
            if (string.IsNullOrEmpty(relation?.RegistId)) return detail;
            try
            {
                ViewPatientOutHospital viewPatientOutHospital = await hisDriver.GetViewPatientOutHospital(relation.OtherNo, relation.RegistId);
                if (viewPatientOutHospital == null) return detail;

                ViewOutHosPatientInfo viewOutHosPatientInfo = await hisDriver.GetPatientOutpatientRecord(relation.OtherNo);

                detail = await medPatientDetailsInfo.GetAsync(relation.Id);
                if (detail == null) return detail;

                detail.OutHosDiagnosis = viewPatientOutHospital.OUT_DIAGNOSIS;
                detail.OutDeptTime = viewPatientOutHospital.OUT_DEPT_TIME.ToDateTime();
                detail.OutHosTime = viewPatientOutHospital.OUT_HOS_TIME.ToDateTime();
                detail.HosDay = viewPatientOutHospital.HOS_DAY;
                detail.TotalCost = viewPatientOutHospital.TOTAL_COST;
                detail.DrugCost = viewPatientOutHospital.DRUG_COST;
                detail.ExamCost = viewPatientOutHospital.EXAM_COST;
                detail.OutWay = viewPatientOutHospital.OUT_WAY;
                #region SyncPatientBasicInfo同步过程已赋值以下字段，此处注释--Modify By Peter.Lv 2022.07.26
                //detail.AdmissionTime = viewOutHosPatientInfo.ADMISSTION_TIME;
                //detail.RegistTime= viewOutHosPatientInfo.REGIST_TIME;
                //detail.InHosTime= viewOutHosPatientInfo.REGIST_TIME;
                #endregion
            }
            catch (Exception ex)
            {
                logger.LogError($"GetOutcomeInfo-住院获取转归数据失败,{ex.Message}");
            }
            return detail;
        }

        public async Task<MedicationDto> GetMedications(MedPatRelationInfo relation)
        {
            MedicationDto result = null;

            if (string.IsNullOrEmpty(relation?.OtherNo) || (string.IsNullOrEmpty(relation.InpRegistId) && string.IsNullOrEmpty(relation.RegistId))) return result;
            try
            {
                var visitId = "";
                if (relation.PatientSource == PatientSourceType.InPatient.ToString("d"))
                    visitId = relation.InpRegistId;
                else 
                    visitId = relation.RegistId;

                var medications = await hisDriver.GetViewPatientOrders(relation.OtherNo, visitId);
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
                        a.InpNo = !string.IsNullOrEmpty(relation.InpRegistId) ? relation.InpRegistId : relation.InpNo;
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

            if (string.IsNullOrEmpty(relation?.OtherNo)) return inspectionDto;

            List<ViewLabMaster> masterList = await hisDriver.GetViewLabMaster(relation.OtherNo);
            logger.LogError($"A4" + masterList.Count);

            foreach (ViewLabMaster master in masterList)
            {
                List<ViewLabResult> resultList = await hisDriver.GetViewLabResult(master.Id.Split('|')[0]);

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
                logger.LogError($"获取住院患者检查数据错误:{ex.ToString()}");
            }
            return examinationDto;
        }

        /// <summary>
        /// 住院手术信息
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public async Task<OperationDto> GetOperations(MedPatRelationInfo relation)
        {
            OperationDto operationDto = new OperationDto();
            if (string.IsNullOrEmpty(relation.OtherNo)) return operationDto;

            MedPatientDetailsInfo detailDb = await medPatientDetailsInfo.GetAsync(relation.Id);
            if (detailDb == null || !detailDb.InHosTime.HasValue) return operationDto;

            List<ViewPatOperation> operationList = await operationDriver.GetOperationList(relation.OtherNo, detailDb.InHosTime.Value, DateTime.Now);
            if (operationList != null && operationList.Count > 0)
            {
                List<MedPatOperation> medPatOperations = operationList.MapTo<List<MedPatOperation>>();
                medPatOperations.ForEach(o =>
                {
                    o.PatientNo = relation.Id;
                });
                operationDto.MedPatOperations = medPatOperations;
            }
            return operationDto;
        }

        public async Task<List<MedPatEcgInfo>> RunEcg(MedPatRelationInfo relation)
        {
            List<MedPatEcgInfo> lstEcg = new List<MedPatEcgInfo>();
            if (string.IsNullOrEmpty(relation?.Id) || string.IsNullOrEmpty(relation?.InpRegistId))
                return lstEcg;

            List<ViewEcgInfo> viewEcgList = await ecgDriver.GetViewEcgInfo(relation.InpRegistId);
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

        /// <summary>
        /// 同步科室字典
        /// </summary>
        /// <returns></returns>
        public async Task<List<MedDeptDict>> SyncDeptDict()
        {
            List<MedDeptDict> deptDict = new List<MedDeptDict>();
            deptDict = await hisDriver.GetDeptDict();
            return deptDict;
        }

        #region private
        private MedPatientDetailsInfo MergePatDetailsInfo(ViewPatientInHospital viewPatientInHospital, MedPatientDetailsInfo detail)
        {
            if (viewPatientInHospital == null || detail == null) return detail;

            detail.InHosTime = viewPatientInHospital.IN_HOS_TIME.ToDateTime();
            detail.InDeptCode = viewPatientInHospital.DEPT_CODE;
            detail.InWardCode = viewPatientInHospital.WARD_CODE;
            detail.InHosDiagnosis = viewPatientInHospital.IN_HOS_DIAGNOSIS;
            detail.InDeptTime = viewPatientInHospital.IN_DEPT_TIME.ToDateTime();
            detail.BedNo = viewPatientInHospital.BED_NO;
            detail.Doctor = viewPatientInHospital.DOCTOR;

            return detail;
        }

        private MedPatientDetailsInfo MergePatDetailsInfo(MedPatientDetailsInfo detailDb, MedPatientDetailsInfo detail)
        {
            detailDb.InHosTime = detailDb.RegistTime;
            detailDb.InDeptCode = detail.InDeptCode;
            detailDb.InWardCode = detail.InWardCode;
            detailDb.InHosDiagnosis = detail.InHosDiagnosis;
            detailDb.InDeptTime = detail.InDeptTime;
            detailDb.BedNo = detail.BedNo;

            return detailDb;
        }

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
            if (string.IsNullOrEmpty(registDb.ContactName)) registDb.ContactName = regist.ContactName;
            if (string.IsNullOrEmpty(registDb.PayType)) registDb.PayType = regist.PayType;
            if (string.IsNullOrEmpty(registDb.ContactPhoneNumber)) registDb.ContactPhoneNumber = regist.ContactPhoneNumber;

            return registDb;
        }
        #endregion
    }
}
