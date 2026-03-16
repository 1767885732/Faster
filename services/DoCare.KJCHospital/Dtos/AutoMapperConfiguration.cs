using AutoMapper.Configuration;
using DoCare.Core.Common;
using DoCare.Core.Ecg.Entities;
using DoCare.Core.Exam_Inspec_Medication.Entities;
using DoCare.Core.Operation.Entities;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using DoCare.KJCHospital.Entities;
using FastFrame.AutoMapper;
using FastFrame.Core;
using FastFrame.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.KJCHospital.Dtos
{
    [Dependency(ServiceLifetime.Singleton)]
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public void CreateMaps(MapperConfigurationExpression mapper)
        {
            #region 门诊患者基本信息
            mapper.CreateMap<ViewOutHosPatientInfo, MedPatientRegistInfo>()
           .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
           .ForMember(rn => rn.PatJob, opt => opt.MapFrom(r => r.PAT_JOB))
           .ForMember(rn => rn.OrgCode, opt => opt.MapFrom(r => HospitalStatic.OrgCode))
           .ForMember(rn => rn.DiagLev, opt => { opt.Ignore(); })
           .ForMember(rn => rn.ComeType, opt => { opt.Ignore(); })
           .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
           .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.DATE_OF_BIRTH))
           .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => r.SEX))
           .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.ID_NO))
           .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.CARD_ID))
           .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.MAIL_ADDRESS))
           .ForMember(rn => rn.PhoneNumber, opt => opt.MapFrom(r => r.PHONE_NUMER));

            mapper.CreateMap<ViewOutHosPatientInfo, PatRegistOutDto>()
            .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            .ForMember(rn => rn.PatJob, opt => opt.MapFrom(r => r.PAT_JOB))
            .ForMember(rn => rn.OrgCode, opt => opt.MapFrom(r => HospitalStatic.OrgCode))
            .ForMember(rn => rn.DiagLev, opt => { opt.Ignore(); })
            .ForMember(rn => rn.ComeType, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
            .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.DATE_OF_BIRTH))
            .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => r.SEX))
            .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.ID_NO))
            .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.CARD_ID))
            .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.MAIL_ADDRESS))
            .ForMember(rn => rn.RelationId, opt => opt.MapFrom(r => r.Id))
            .ForMember(rn => rn.DisplayId, opt => opt.MapFrom(r => r.CLINIC_NO))
            .ForMember(rn => rn.RegistTime, opt => opt.MapFrom(r => r.REGIST_TIME))
            .ForMember(rn => rn.PhoneNumber, opt => opt.MapFrom(r => r.PHONE_NUMER))
            .ForMember(rn => rn.PatientSay, opt => opt.MapFrom(r => r.COMPLAINT))
            .ForMember(rn => rn.Diagnosis, opt => opt.MapFrom(r => r.DIAG_DESC));
            #endregion

            #region 门诊患者详情
            mapper.CreateMap<ViewOutHosPatientInfo, MedPatientDetailsInfo>()
            .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            .ForMember(rn => rn.DeptCode, opt => opt.MapFrom(r => r.DEPT_CODE))
            .ForMember(rn => rn.ArrivedDeptTime, opt => opt.MapFrom(r => r.ARRIVED_DEPT_TIME))
            .ForMember(rn => rn.IsGreenChannel, opt => { opt.Ignore(); })
            .ForMember(rn => rn.RegistTime, opt => opt.MapFrom(r => r.REGIST_TIME))
            .ForMember(rn => rn.InHosTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InDeptCode, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InWardCode, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InHosDiagnosis, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InDeptTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.ToastFlag, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutDeptTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutHosTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.HosDay, opt => { opt.Ignore(); })
            .ForMember(rn => rn.TotalCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.DrugCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.ExamCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutWay, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Doctor, opt => opt.MapFrom(r => r.DOCTOR))
            .ForMember(rn => rn.AdmissionTime, opt => opt.MapFrom(r => r.ADMISSTION_TIME))
            .ForMember(rn => rn.DiagDoctor, opt => opt.MapFrom(r => r.DIAG_DOCTOR))
            .ForMember(rn => rn.TriageTime, opt => opt.MapFrom(r => r.TRIAGE_TIME))
            .ForMember(rn => rn.DiagTime, opt => opt.MapFrom(r => r.DIAG_TIME))
            .ForMember(rn => rn.DiagDesc, opt => opt.MapFrom(r => r.DIAG_DESC))
            .ForMember(rn => rn.Complaint, opt => opt.MapFrom(r => r.COMPLAINT))
            .ForMember(rn => rn.IsEmg, opt => { opt.Ignore(); })
            .ForMember(rn => rn.EmgBedNo, opt => { opt.Ignore(); })
            .ForMember(rn => rn.TransferTime, opt => opt.MapFrom(r => r.TRANSFER_TIME))
            .ForMember(rn => rn.Temp, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Br, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Plus, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Hr, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Bp, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Spo2, opt => { opt.Ignore(); });
            #endregion

            #region 门诊/住院患者基本信息
            mapper.CreateMap<ViewPatientInfo, MedPatientRegistInfo>()
           .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
           .ForMember(rn => rn.PatJob, opt => opt.MapFrom(r => r.PAT_JOB))
           .ForMember(rn => rn.OrgCode, opt => opt.MapFrom(r => HospitalStatic.OrgCode))
           .ForMember(rn => rn.DiagLev, opt => { opt.Ignore(); })
           .ForMember(rn => rn.ComeType, opt => { opt.Ignore(); })
           .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
           .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.DATE_OF_BIRTH.ToDateTime()))
           .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => r.SEX))
           .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.ID_NO))
           .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.CARD_ID))
           .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.MAIL_ADDRESS))
           .ForMember(rn => rn.PhoneNumber, opt => opt.MapFrom(r => r.PHONE_NUMER));

            mapper.CreateMap<ViewPatientInfo, PatRegistOutDto>()
            .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            .ForMember(rn => rn.PatJob, opt => opt.MapFrom(r => r.PAT_JOB))
            .ForMember(rn => rn.OrgCode, opt => opt.MapFrom(r => HospitalStatic.OrgCode))
            .ForMember(rn => rn.DiagLev, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Diagnosis, opt => opt.MapFrom(r => r.DIAG_DESC))
            .ForMember(rn => rn.ComeType, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
            .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.DATE_OF_BIRTH.ToDateTime()))
            .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => r.SEX))
            .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.ID_NO))
            .ForMember(rn => rn.CitizenCard, opt => opt.MapFrom(r => r.CARD_ID))
            .ForMember(rn => rn.MailAddress, opt => opt.MapFrom(r => r.MAIL_ADDRESS))
            .ForMember(rn => rn.RelationId, opt => opt.MapFrom(r => r.Id))
            .ForMember(rn => rn.DisplayId, opt => opt.MapFrom(r => r.VISIT_NO))
            .ForMember(rn => rn.RegistTime, opt => opt.MapFrom(r => r.REGIST_TIME))
            .ForMember(rn => rn.PhoneNumber, opt => opt.MapFrom(r => r.PHONE_NUMER))
            .ForMember(rn => rn.PatientSay, opt => opt.MapFrom(r => r.COMPLAINT))
            .ForMember(rn => rn.MrNo, opt => opt.MapFrom(r => r.MR_NO));
            #endregion

            #region 门诊/住院患者详情
            mapper.CreateMap<ViewPatientInfo, MedPatientDetailsInfo>()
            .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            .ForMember(rn => rn.DeptCode, opt => opt.MapFrom(r => r.DEPT_CODE))
            .ForMember(rn => rn.ArrivedDeptTime, opt => opt.MapFrom(r => r.ARRVIED_DEPT_TIME.ToDateTime()))
            .ForMember(rn => rn.IsGreenChannel, opt => { opt.Ignore(); })
            .ForMember(rn => rn.RegistTime, opt => opt.MapFrom(r => r.REGIST_TIME.ToDateTime()))
            .ForMember(rn => rn.InHosTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InDeptCode, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InWardCode, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InHosDiagnosis, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InDeptTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.ToastFlag, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutDeptTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutHosTime, opt => { opt.Ignore(); })
            .ForMember(rn => rn.HosDay, opt => { opt.Ignore(); })
            .ForMember(rn => rn.TotalCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.DrugCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.ExamCost, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OutWay, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Doctor, opt => opt.MapFrom(r => r.DOCTOR))
            .ForMember(rn => rn.AdmissionTime, opt => opt.MapFrom(r => r.ADMISSTION_TIME.ToDateTime()))
            .ForMember(rn => rn.DiagDoctor, opt => opt.MapFrom(r => r.DIAG_DOCTOR))
            .ForMember(rn => rn.TriageTime, opt => opt.MapFrom(r => r.TRIAGE_TIME.ToDateTime()))
            .ForMember(rn => rn.DiagTime, opt => opt.MapFrom(r => r.DIAG_TIME.ToDateTime()))
            .ForMember(rn => rn.DiagDesc, opt => opt.MapFrom(r => r.DIAG_DESC))
            .ForMember(rn => rn.Complaint, opt => opt.MapFrom(r => r.COMPLAINT))
            .ForMember(rn => rn.IsEmg, opt => { opt.Ignore(); })
            .ForMember(rn => rn.EmgBedNo, opt => { opt.Ignore(); })
            .ForMember(rn => rn.TransferTime, opt => opt.MapFrom(r => r.TRANSFER_TIME.ToDateTime()))
            .ForMember(rn => rn.Temp, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Br, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Plus, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Hr, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Bp, opt => { opt.Ignore(); })
            .ForMember(rn => rn.Spo2, opt => { opt.Ignore(); });
            #endregion

            #region 门诊/住院用药
            mapper.CreateMap<ViewPatientOrders, MedPatOrders>()
            .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            .ForMember(rn => rn.PatientNo, opt => { opt.Ignore(); })
            .ForMember(rn => rn.OrderNo, opt => opt.MapFrom(r => r.ORDER_NO))
            .ForMember(rn => rn.OrderSubNo, opt => opt.MapFrom(r => r.ORDER_SUB_NO))
            .ForMember(rn => rn.ClinicNo, opt => { opt.Ignore(); })
            .ForMember(rn => rn.InpNo, opt => { opt.Ignore(); })
            .ForMember(rn => rn.RepeatIndicator, opt => opt.MapFrom(r => r.REPEAT_INDICATOR))
            .ForMember(rn => rn.OrderClass, opt => opt.MapFrom(r => r.ORDER_CLASS))
            .ForMember(rn => rn.OrderText, opt => opt.MapFrom(r => r.ORDER_TEXT))
            .ForMember(rn => rn.OrderCode, opt => opt.MapFrom(r => r.ORDER_CODE))
            .ForMember(rn => rn.Dosage, opt => opt.MapFrom(r => r.DOSAGE))
            .ForMember(rn => rn.DosageUnits, opt => opt.MapFrom(r => r.DOSAGE_UNITS))
            .ForMember(rn => rn.Administration, opt => opt.MapFrom(r => r.ADMINISTRATION))
            .ForMember(rn => rn.StartDateTime, opt => opt.MapFrom(r => r.START_DATE_TIME.ToDateTime()))
            .ForMember(rn => rn.StopDateTime, opt => opt.MapFrom(r => r.STOP_DATE_TIME.ToDateTime()))
            .ForMember(rn => rn.Duration, opt => opt.MapFrom(r => r.DURATION.ToDateTime()))
            .ForMember(rn => rn.DurationUnits, opt => opt.MapFrom(r => r.DURATION_UNITS))
            .ForMember(rn => rn.Frequency, opt => opt.MapFrom(r => r.FREQUENCY))
            .ForMember(rn => rn.OrderingDept, opt => opt.MapFrom(r => r.ORDERING_DEPT))
            .ForMember(rn => rn.Doctor, opt => opt.MapFrom(r => r.DOCTOR))
            .ForMember(rn => rn.StopDoctor, opt => opt.MapFrom(r => r.STOP_DOCTOR))
            .ForMember(rn => rn.Nurse, opt => opt.MapFrom(r => r.NURSE))
            .ForMember(rn => rn.StopNurse, opt => opt.MapFrom(r => r.STOP_NURSE))
            .ForMember(rn => rn.EnterDateTime, opt => opt.MapFrom(r => r.ENTER_DATE_TIME.ToDateTime()))
            .ForMember(rn => rn.StopOrderDateTime, opt => opt.MapFrom(r => r.STOP_ORDER_DATE_TIME.ToDateTime()))
            .ForMember(rn => rn.OrderStatus, opt => opt.MapFrom(r => r.ORDER_STATUS))
            .ForMember(rn => rn.CurrentPrescNo, opt => opt.MapFrom(r => r.PRESCRIPTION_NO));
            #endregion

            #region 门诊/住院检验信息
            mapper.CreateMap<ViewLabMaster, MedLabMaster>()
               .ForMember(rn => rn.Id, opt => opt.MapFrom(r => r.Id))
               .ForMember(rn => rn.PriorityIndicator, opt => opt.MapFrom(r => r.PRIORITY_INDICATOR))
               .ForMember(rn => rn.PatientNo, opt => { opt.Ignore(); })
               .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
               .ForMember(rn => rn.TestCause, opt => opt.MapFrom(r => r.TEST_CAUSE))
               .ForMember(rn => rn.RelevantClinicDiag, opt => opt.MapFrom(r => r.RELEVANT_CLINIC_DIAG))
               .ForMember(rn => rn.Specimen, opt => opt.MapFrom(r => r.SPECIMEN))
               .ForMember(rn => rn.SpcmReceivedDateTime, opt => opt.MapFrom(r => r.SPCM_RECEIVED_DATE_TIME.ToDateTime()))
               .ForMember(rn => rn.ApplyTime, opt => opt.MapFrom(r => r.APPLY_TIME.ToDateTime()))
               .ForMember(rn => rn.OrderingDept, opt => opt.MapFrom(r => r.ORDERING_DEPT))
               .ForMember(rn => rn.OrderingProvider, opt => opt.MapFrom(r => r.ORDERING_PROVIDER))
               .ForMember(rn => rn.PerformedBy, opt => opt.MapFrom(r => r.PERFORMED_BY))
               .ForMember(rn => rn.ResultStatus, opt => opt.MapFrom(r => r.RESULT_STATUS))
               .ForMember(rn => rn.ResultRptDateTime, opt => opt.MapFrom(r => r.RESULTS_RPT_DATE_TIME.ToDateTime()))
               .ForMember(rn => rn.Transcriptionist, opt => opt.MapFrom(r => r.TRANSCRIPTIONIST))
               .ForMember(rn => rn.VerifiedBy, opt => opt.MapFrom(r => r.VERIFIED_BY));

            mapper.CreateMap<ViewLabResult, MedLabResult>()
                .ForMember(rn => rn.Id, opt => opt.MapFrom(r => r.ITEM_NO))
                .ForMember(rn => rn.TestNo, opt => opt.MapFrom(r => r.TEST_NO))
                //.ForMember(rn => rn.ItemNo, opt => opt.MapFrom(r => r.ITEM_NO))
                .ForMember(rn => rn.ReportItemName, opt => opt.MapFrom(r => r.REPORT_ITEM_NAME))
                .ForMember(rn => rn.ReportItemCode, opt => opt.MapFrom(r => r.REPORT_ITEM_CODE))
                .ForMember(rn => rn.AbnormalIndicator, opt => opt.MapFrom(r => ConvertABNORMAL_INDICATOR(r.ABNORMAL_INDICATOR)))
                .ForMember(rn => rn.Result, opt => opt.MapFrom(r => r.RESULT))
                .ForMember(rn => rn.Units, opt => opt.MapFrom(r => r.UNITS))
                .ForMember(rn => rn.ResultDateTime, opt => opt.MapFrom(r => r.RESULT_DATE_TIME.ToDateTime()))
                .ForMember(rn => rn.ReferenceResult, opt => opt.MapFrom(r => r.REFERENCE_RESULT));
            #endregion

            //#region 门诊/住院心电图信息
            //mapper.CreateMap<ViewPatientEcgInfo, MedPatEcgInfo>()
            //  .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
            //  .ForMember(rn => rn.PatientNo, opt => { opt.Ignore(); })
            //  .ForMember(rn => rn.ExamTime, opt => opt.MapFrom(r => r.EXAM_TIME))
            //  .ForMember(rn => rn.ReportTime, opt => opt.MapFrom(r => r.REPORT_TIME))
            //  .ForMember(rn => rn.DiagTime, opt => opt.MapFrom(r => r.DIAG_TIME))
            //  .ForMember(rn => rn.DiagDesc, opt => opt.MapFrom(r => r.DIAG_DESC))
            //  .ForMember(rn => rn.DiagSource, opt => opt.MapFrom(r => ConvertEcgSource(r.DIAG_SOURCE)))
            //  .ForMember(rn => rn.EcgPath, opt => opt.MapFrom(r => r.ECG_PATH))
            //  .ForMember(rn => rn.EcgUrl, opt => { opt.Ignore(); })
            //  .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
            //  .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => r.SEX))
            //  .ForMember(rn => rn.Age, opt => opt.MapFrom(r => r.AGE))
            //  .ForMember(rn => rn.EcgPatientId, opt => { opt.Ignore(); })
            //  .ForMember(rn => rn.EcgData, opt => { opt.Ignore(); });
            //#endregion

            #region 门诊/住院检查信息
            mapper.CreateMap<ViewExamReport, MedExamReport>()
              .ForMember(rn => rn.Id, opt => opt.MapFrom(r => r.EXAM_NO))
              .ForMember(rn => rn.PatientNo, opt => { opt.Ignore(); })
              .ForMember(rn => rn.ExamClass, opt => opt.MapFrom(r => r.EXAM_CLASS))
              .ForMember(rn => rn.ExamSubClass, opt => opt.MapFrom(r => r.EXAM_SUB_CLASS))
              .ForMember(rn => rn.ClinDiag, opt => opt.MapFrom(r => r.CLIN_DIAG))
              .ForMember(rn => rn.ExamMode, opt => opt.MapFrom(r => r.EXAM_MODE))
              .ForMember(rn => rn.Device, opt => opt.MapFrom(r => r.DEVICE))
              .ForMember(rn => rn.PerformedBy, opt => opt.MapFrom(r => r.PERFORMED_BY))
              .ForMember(rn => rn.ReqDateTime, opt => opt.MapFrom(r => r.REQ_DATE_TIME.ToDateTime()))
              .ForMember(rn => rn.ReqDept, opt => opt.MapFrom(r => r.REQ_DEPT))
              .ForMember(rn => rn.ReqPhysician, opt => opt.MapFrom(r => r.REQ_PHYSICIAN))
              .ForMember(rn => rn.ReqMemo, opt => opt.MapFrom(r => r.REQ_MEMO))
              .ForMember(rn => rn.Notice, opt => opt.MapFrom(r => r.NOTICE))
              .ForMember(rn => rn.ExamStartDate, opt => opt.MapFrom(r => r.EXAM_START_DATE.ToDateTime()))
              .ForMember(rn => rn.ExamEndDate, opt => opt.MapFrom(r => r.EXAM_END_DATE.ToDateTime()))
              .ForMember(rn => rn.ReportDateTime, opt => opt.MapFrom(r => r.REPORT_DATE_TIME.ToDateTime()))
              .ForMember(rn => rn.ExamPara, opt => opt.MapFrom(r => r.EXAM_PARA))
              .ForMember(rn => rn.Description, opt => opt.MapFrom(r => r.DESCRIPTION))
              .ForMember(rn => rn.Recommendation, opt => opt.MapFrom(r => r.RECOMMENDATION))
              .ForMember(rn => rn.Technician, opt => opt.MapFrom(r => r.TECHNICIAN))
              .ForMember(rn => rn.Reporter, opt => opt.MapFrom(r => r.REPORTER))
              .ForMember(rn => rn.ResultStatus, opt => opt.MapFrom(r => r.RESULT_STATUS))
              .ForMember(rn => rn.VerifiedBy, opt => opt.MapFrom(r => r.VERIFIED_BY))
              .ForMember(rn => rn.VerifiedDateTime, opt => opt.MapFrom(r => r.VERIFIED_DATE_TIME.ToDateTime()));
            #endregion

            #region 手术信息
            mapper.CreateMap<ViewPatOperation, MedPatOperation>()
                .ForMember(rn => rn.Id, opt => opt.MapFrom(r => r.Id))
                .ForMember(rn => rn.PatientNo, opt => { opt.Ignore(); })
                .ForMember(rn => rn.ScheduledDateTime, opt => opt.MapFrom(r => r.ScheduledDateTime))
                .ForMember(rn => rn.AnesthesiaMethod, opt => opt.MapFrom(r => r.AnesthesiaMethod))
                .ForMember(rn => rn.Surgeon, opt => opt.MapFrom(r => r.Surgeon))
                .ForMember(rn => rn.AnesthesiaDoctor, opt => opt.MapFrom(r => r.AnesthesiaDoctor))
                .ForMember(rn => rn.DiagBeforeOperation, opt => opt.MapFrom(r => r.DiagBeforeOperation))
                .ForMember(rn => rn.DiagAfterOperation, opt => opt.MapFrom(r => r.DiagAfterOperation))
                .ForMember(rn => rn.OperationScale, opt => opt.MapFrom(r => r.OperationScale))
                .ForMember(rn => rn.InDateTime, opt => opt.MapFrom(r => r.InDateTime))
                .ForMember(rn => rn.OutDateTime, opt => opt.MapFrom(r => r.OutDateTime))
                .ForMember(rn => rn.AnesStartTime, opt => opt.MapFrom(r => r.AnesStartTime))
                .ForMember(rn => rn.AnesEndTime, opt => opt.MapFrom(r => r.AnesEndTime))
                .ForMember(rn => rn.StartDateTime, opt => opt.MapFrom(r => r.StartDateTime))
                .ForMember(rn => rn.EndDateTime, opt => opt.MapFrom(r => r.EndDateTime))
                .ForMember(rn => rn.InPacuDateTime, opt => opt.MapFrom(r => r.InPacuDateTime))
                .ForMember(rn => rn.OutPacuDateTime, opt => opt.MapFrom(r => r.OutPacuDateTime))
                .ForMember(rn => rn.BloodLossed, opt => opt.MapFrom(r => r.BloodLossed.ToDecimal()))
                .ForMember(rn => rn.BloodTransfered, opt => opt.MapFrom(r => r.BloodTransfered.ToDecimal()))
                .ForMember(rn => rn.OperationName, opt => opt.MapFrom(r => r.OperationName))
                //.ForMember(rn => rn.OperationPart, opt => opt.MapFrom(r => r.OperationPart))
                //.ForMember(rn => rn.OperNurse, opt => opt.MapFrom(r => r.OperNurse))
                //.ForMember(rn => rn.SupplyNurse, opt => opt.MapFrom(r => r.SupplyNurse))
                ;
            #endregion

            #region 门诊/住院心电图信息
            mapper.CreateMap<ViewEcgInfo, MedPatEcgInfo>()
              .ForMember(rn => rn.Id, opt => { opt.Ignore(); })
              .ForMember(rn => rn.PatientNo, opt => { opt.Ignore(); })
              .ForMember(rn => rn.ExamTime, opt => opt.MapFrom(r => r.EXAM_TIME))
              .ForMember(rn => rn.ReportTime, opt => opt.MapFrom(r => r.REPORT_TIME))
              .ForMember(rn => rn.DiagTime, opt => opt.MapFrom(r => r.DIAG_TIME))
              .ForMember(rn => rn.DiagDesc, opt => opt.MapFrom(r => r.DIAG_DESC))
              .ForMember(rn => rn.DiagSource, opt => opt.MapFrom(r => ConvertEcgSource(r.DIAG_SOURCE)))
              .ForMember(rn => rn.EcgPath, opt => opt.MapFrom(r => r.ECG_PATH))
              .ForMember(rn => rn.EcgUrl, opt => { opt.Ignore(); })
              .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.NAME))
              .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => r.SEX))
              .ForMember(rn => rn.Age, opt => opt.MapFrom(r => r.AGE.ToString()))
              .ForMember(rn => rn.EcgPatientId, opt => { opt.Ignore(); })
              .ForMember(rn => rn.EcgData, opt => { opt.Ignore(); })
              .ForMember(rn => rn.PdfData, opt => opt.MapFrom(r => r.ECG_DATA));
            #endregion
        }

        private string ConvertREPEAT_INDICATOR(int? REPEAT_INDICATOR)
        {
            string result = null;
            if (REPEAT_INDICATOR.HasValue)
            {
                if (REPEAT_INDICATOR == 1)
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }
            }
            return result;
        }

        private string ConvertEcgSource(string DIAG_SOURCE)
        {
            string result = null;
            if (!string.IsNullOrEmpty(DIAG_SOURCE))
            {
                if (DIAG_SOURCE == "门诊" || DIAG_SOURCE == "住院" || DIAG_SOURCE == "急诊")
                {
                    result = EcgSource.InHospital.ToString("d");
                }
                else
                {
                    result = DIAG_SOURCE;
                }
            }
            return result;
        }

        private string ConvertABNORMAL_INDICATOR(string ABNORMAL_INDICATOR)
        {
            string result = ABNORMAL_INDICATOR;
            if (result.IndexOf('|') > -1)
            {
                string[] array = result.Split('|');
                if (array.Length > 1)
                {
                    result = array[1];
                }
            }
            return result;
        }
    }
}
