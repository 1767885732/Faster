using DoCare.Core.Exam_Inspec_Medication.Dtos;
using DoCare.Core.Exam_Inspec_Medication.Entities;
using DoCare.Core.Patient.Entities;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Core.Exam_Inspec_Medication
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class ExaminationService
    {
        private readonly ILogger<ExaminationService> logger;
        private readonly IEmergency emergency;
        private readonly IOutPatient outPatient;
        private readonly IInPatient inPatient;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelation;
        private readonly IRepository<MedExamReport, string> medExamReport;
        public ExaminationService(IEmergency emergency, IOutPatient outPatient, IInPatient inPatient,
            ILogger<ExaminationService> logger,
            IRepository<MedPatRelationInfo, string> medPatRelation,
            IRepository<MedExamReport, string> medExamReport)
        {
            this.logger = logger;
            this.emergency = emergency;
            this.outPatient = outPatient;
            this.inPatient = inPatient;
            this.medPatRelation = medPatRelation;
            this.medExamReport = medExamReport;
        }

        public async Task<bool> Run(string patientNo)
        {
            logger.LogDebug($"开始同步检查数据,患者编号:{patientNo}");
            if (string.IsNullOrEmpty(patientNo)) return false;

            ////测试代码
            //var relation = new MedPatRelationInfo();
            //relation.Id = patientNo;
            //relation.InpRegistId = patientNo;
            //relation.RegistId = patientNo;
            var relation = await medPatRelation.GetAsync(patientNo);
            if (relation == null) return false;

            ExaminationDto examinationDto = new ExaminationDto();
            examinationDto.MedExamReports = new List<MedExamReport>();

            #region 急诊
            //try
            //{
            //    if (emergency is IExamination emergencyExamination)
            //    {
            //        var examinationInfo = await emergencyExamination.GetExaminations(relation);
            //        if ((examinationInfo?.MedExamReports?.Count ?? 0) > 0)
            //        {
            //            examinationDto.MedExamReports.AddRange(examinationInfo.MedExamReports);
            //            logger.LogDebug($"急诊同步行数:{examinationInfo.MedExamReports.Count}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError($"检查-急诊同步异常，{ex.Message}");
            //}
            #endregion

            #region 门诊
            //try
            //{
            //    if (outPatient is IExamination outPatientExamination)
            //    {
            //        var examinationInfo = await outPatientExamination.GetExaminations(relation);
            //        if ((examinationInfo?.MedExamReports?.Count ?? 0) > 0)
            //        {
            //            examinationDto.MedExamReports.AddRange(examinationInfo.MedExamReports);
            //            logger.LogDebug($"门诊同步行数:{examinationInfo.MedExamReports.Count}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError($"检查-门诊同步异常，{ex.Message}");
            //}
            #endregion

            #region 住院
            try
            {
                if (inPatient is IExamination inPatientExamination)
                {
                    var examinationInfo = await inPatientExamination.GetExaminations(relation);
                    if ((examinationInfo?.MedExamReports?.Count ?? 0) > 0)
                    {
                        examinationDto.MedExamReports.AddRange(examinationInfo.MedExamReports);
                        logger.LogDebug($"住院同步行数:{examinationInfo.MedExamReports.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"检查-住院同步异常，{ex.Message}");
            }
            #endregion

            logger.LogDebug($"检查数据总行数:{examinationDto.MedExamReports.Count}");
            if (examinationDto.MedExamReports.Count == 0) return true;

            foreach (var exam in examinationDto.MedExamReports)
            {
                exam.PatientNo = relation.Id;
                if (!string.IsNullOrEmpty(exam.Description))
                {
                    exam.Description = exam.Description.ToShowLimitLength(245);
                    exam.ExamMode = exam.ExamMode.ToShowLimitLength(45);
                    exam.ClinDiag = exam.ClinDiag.ToShowLimitLength(95);
                }
                try
                {
                    var examDb = await medExamReport.GetAsync(exam.Id);
                    if (examDb == null)
                    {
                        await medExamReport.InsertAsync(exam);
                    }
                    else
                    {
                        examDb = exam;
                        await medExamReport.UpdateAsync(examDb);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"检查数据入库错误:{ex.Message},数据:{exam.ToJson()}");
                    continue;
                }
            }
            logger.LogDebug($"同步检查数据结束");
            return true;
        }
    }
}
