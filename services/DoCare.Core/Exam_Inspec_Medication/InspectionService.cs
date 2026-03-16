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
    public class InspectionService
    {
        private readonly ILogger<InspectionService> logger;
        private readonly IEmergency emergency;
        private readonly IOutPatient outPatient;
        private readonly IInPatient inPatient;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelation;
        private readonly IRepository<MedLabMaster, string> medLabMaster;
        private readonly IRepository<MedLabResult, string> medLabResult;
        public InspectionService(IEmergency emergency, IOutPatient outPatient, IInPatient inPatient,
            ILogger<InspectionService> logger,
            IRepository<MedPatRelationInfo, string> medPatRelation,
            IRepository<MedLabMaster, string> medLabMaster,
            IRepository<MedLabResult, string> medLabResult)
        {
            this.logger = logger;
            this.emergency = emergency;
            this.outPatient = outPatient;
            this.inPatient = inPatient;
            this.medPatRelation = medPatRelation;
            this.medLabMaster = medLabMaster;
            this.medLabResult = medLabResult;
        }

        public async Task<bool> Run(string patientNo)
        {
            logger.LogDebug($"开始同步检验单信息,患者编号:{patientNo}");
            if (string.IsNullOrEmpty(patientNo)) return false;

            ////测试代码
            //var relation = new MedPatRelationInfo();
            //relation.Id = patientNo;
            //relation.InpRegistId = patientNo;
            //relation.RegistId = patientNo;
            var relation = await medPatRelation.GetAsync(patientNo);
            logger.LogDebug($"[InspectionService] patientNo:{patientNo} -> relation.Id:{relation?.Id}, OtherNo:'{relation?.OtherNo}', RegistId:'{relation?.RegistId}', InpRegistId:'{relation?.InpRegistId}'");
            if (relation == null) return false;

            InspectionDto inspectionDto = new InspectionDto();
            inspectionDto.MedLabMasters = new List<MedLabMaster>();
            inspectionDto.MedLabResults = new List<MedLabResult>();

            #region 急诊
            try
            {
                if (emergency is IInspection emergencyInspection)
                {
                    var inspectionInfo = await emergencyInspection.GetInspections(relation);
                    if ((inspectionInfo?.MedLabMasters?.Count ?? 0) > 0)
                    {
                        inspectionDto.MedLabMasters.AddRange(inspectionInfo.MedLabMasters);
                        logger.LogDebug($"急诊同步检验申请单行数:{inspectionInfo.MedLabMasters.Count}");
                    }
                    if ((inspectionInfo?.MedLabResults?.Count ?? 0) > 0)
                    {
                        inspectionDto.MedLabResults.AddRange(inspectionInfo.MedLabResults);
                        logger.LogDebug($"急诊同步检验报告单行数:{inspectionInfo.MedLabResults.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"检验-急诊同步异常，{ex.Message}");
            }
            #endregion

            #region 门诊
            try
            {
                if (outPatient is IInspection outPatientInspection)
                {
                    var inspectionInfo = await outPatientInspection.GetInspections(relation);
                    if ((inspectionInfo?.MedLabMasters?.Count ?? 0) > 0)
                    {
                        inspectionDto.MedLabMasters.AddRange(inspectionInfo.MedLabMasters);
                        logger.LogDebug($"门诊同步检验申请单行数:{inspectionInfo.MedLabMasters.Count}");
                    }
                    if ((inspectionInfo?.MedLabResults?.Count ?? 0) > 0)
                    {
                        inspectionDto.MedLabResults.AddRange(inspectionInfo.MedLabResults);
                        logger.LogDebug($"门诊同步检验报告单行数:{inspectionInfo.MedLabResults.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"检验-门诊同步异常，{ex.Message}");
            }
            #endregion

            #region 住院
            try
            {
                if (inPatient is IInspection inPatientInspection)
                {
                    var inspectionInfo = await inPatientInspection.GetInspections(relation);
                    logger.LogDebug($"接口GetInspections:{inspectionInfo}");
                    if ((inspectionInfo?.MedLabMasters?.Count ?? 0) > 0)
                    {
                        inspectionDto.MedLabMasters.AddRange(inspectionInfo.MedLabMasters);
                        logger.LogDebug($"住院同步检验申请单行数:{inspectionInfo.MedLabMasters.Count}");
                    }
                    if ((inspectionInfo?.MedLabResults?.Count ?? 0) > 0)
                    {
                        inspectionDto.MedLabResults.AddRange(inspectionInfo.MedLabResults);
                        logger.LogDebug($"住院同步检验报告单行数:{inspectionInfo.MedLabResults.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"检验-住院同步异常，{ex.Message}");
            }
            #endregion

            if (inspectionDto.MedLabMasters.Count == 0) return true;

            logger.LogDebug($"检验申请单总行数:{inspectionDto.MedLabMasters.Count}");
            foreach (var master in inspectionDto.MedLabMasters)
            {
                try
                {
                    var masterDb = await medLabMaster.GetAsync(master.Id);
                    if (masterDb == null)
                    {
                        await medLabMaster.InsertAsync(master);
                    }
                    else
                    {
                        masterDb = master;
                        await medLabMaster.UpdateAsync(masterDb);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"检验申请单同步错误:{ex.Message},数据:{master.ToJson()}");
                    continue;
                }
            };

            logger.LogDebug($"检验报告单行数:{inspectionDto.MedLabResults.Count}");
            foreach (var result in inspectionDto.MedLabResults)
            {
                try
                {
                    var resultDb = await medLabResult.GetFirstAsync(o => o.Id == result.Id);
                    if (resultDb == null)
                    {
                        await medLabResult.InsertAsync(result);
                    }
                    else
                    {
                        result.Id = resultDb.Id;
                        resultDb = result;
                        await medLabResult.UpdateAsync(resultDb);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"检验报告单入库错误:{ex.Message},数据:{result.ToJson()}");
                    continue;
                }
            };
            logger.LogDebug($"同步检验信息结束");
            return true;
        }
    }
}
