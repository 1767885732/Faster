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
    public class MedicationService
    {
        private readonly ILogger<ExaminationService> logger;
        private readonly IEmergency emergency;
        private readonly IOutPatient outPatient;
        private readonly IInPatient inPatient;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelation;
        private readonly IRepository<MedPatOrders, Guid> medPatOrders;
        public MedicationService(IEmergency emergency, IOutPatient outPatient, IInPatient inPatient,
            ILogger<ExaminationService> logger,
            IRepository<MedPatRelationInfo, string> medPatRelation,
            IRepository<MedPatOrders, Guid> medPatOrders)
        {
            this.logger = logger;
            this.emergency = emergency;
            this.outPatient = outPatient;
            this.inPatient = inPatient;
            this.medPatRelation = medPatRelation;
            this.medPatOrders = medPatOrders;
        }

        public async Task<bool> Run(string patientNo)
        {
            logger.LogDebug($"开始同步用药信息,患者编号:{patientNo}");
            if (string.IsNullOrEmpty(patientNo)) return false;

            var relation = await medPatRelation.GetAsync(patientNo);
            if (relation == null) return false;

            MedicationDto medicationDto = new MedicationDto();
            medicationDto.MedPatOrderses = new List<MedPatOrders>();

            #region 急诊
            //try
            //{
            //    if (emergency is IMedication emergencyMedication)
            //    {
            //        var medicationInfo = await emergencyMedication.GetMedications(relation);
            //        if ((medicationInfo?.MedPatOrderses?.Count ?? 0) > 0)
            //        {
            //            medicationDto.MedPatOrderses.AddRange(medicationInfo.MedPatOrderses);
            //            logger.LogDebug($"急诊同步行数:{medicationInfo.MedPatOrderses.Count}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError($"用药-急诊同步异常，{ex.Message}");
            //}
            #endregion

            #region 门诊
            //try
            //{
            //    if (outPatient is IMedication outPatientMedication)
            //    {
            //        var medicationInfo = await outPatientMedication.GetMedications(relation);
            //        if ((medicationInfo?.MedPatOrderses?.Count ?? 0) > 0)
            //        {
            //            medicationDto.MedPatOrderses.AddRange(medicationInfo.MedPatOrderses);
            //            logger.LogDebug($"门诊同步行数:{medicationInfo.MedPatOrderses.Count}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError($"用药-门诊同步异常，{ex.Message}");
            //}
            #endregion

            #region 住院
            try
            {
                if (inPatient is IMedication inPatientMedication)
                {
                    var medicationInfo = await inPatientMedication.GetMedications(relation);
                    if ((medicationInfo?.MedPatOrderses?.Count ?? 0) > 0)
                    {
                        medicationDto.MedPatOrderses.AddRange(medicationInfo.MedPatOrderses);
                        logger.LogDebug($"住院同步行数:{medicationInfo.MedPatOrderses.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"用药-住院同步异常，{ex.Message}");
            }
            #endregion

            if (medicationDto.MedPatOrderses.Count == 0) return true;

            logger.LogDebug($"用药数据总行数:{medicationDto.MedPatOrderses.Count},数据:{medicationDto.ToJson()}");
            foreach (var medication in medicationDto.MedPatOrderses)
            {
                try
                {
                    var medicationDb = await medPatOrders.GetFirstAsync(o => o.PatientNo == medication.PatientNo && o.OrderNo == medication.OrderNo && o.OrderSubNo == medication.OrderSubNo);
                    if (medicationDb == null)
                    {
                        await medPatOrders.InsertAsync(medication);
                    }
                    else
                    {
                        medication.Id = medicationDb.Id;
                        medicationDb = medication;
                        await medPatOrders.UpdateAsync(medicationDb);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"用药信息入库错误:{ex.Message},数据:{medication.ToJson()}");
                    continue;
                }
            };
            logger.LogDebug($"同步用药信息结束");
            return true;
        }
    }
}
