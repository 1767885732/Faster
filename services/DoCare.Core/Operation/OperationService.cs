using DoCare.Core.Operation.Dtos;
using DoCare.Core.Operation.Entities;
using DoCare.Core.Patient.Entities;
using FastFrame.Core;
using FastFrame.Data;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Core.Operation
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class OperationService
    {
        private readonly ILogger<OperationService> logger;
        private readonly IEmergency emergency;
        private readonly IOutPatient outPatient;
        private readonly IInPatient inPatient;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelation;
        private readonly IRepository<MedPatOperation, string> medPatOperation;
        public OperationService(IEmergency emergency, IOutPatient outPatient, IInPatient inPatient,
            ILogger<OperationService> logger,
            IRepository<MedPatRelationInfo, string> medPatRelation,
           IRepository<MedPatOperation, string> medPatOperation)
        {
            this.logger = logger;
            this.emergency = emergency;
            this.outPatient = outPatient;
            this.inPatient = inPatient;
            this.medPatRelation = medPatRelation;
            this.medPatOperation = medPatOperation;
        }

        public async Task<bool> Run(string patientNo)
        {
            logger.LogDebug($"开始同步手术信息,患者编号:{patientNo}");
            if (string.IsNullOrEmpty(patientNo)) return false;

            var relation = await medPatRelation.GetAsync(patientNo);
            if (relation == null) return false;

            OperationDto operationDto = new OperationDto();
            operationDto.MedPatOperations = new List<MedPatOperation>();

            #region 急诊
            try
            {
                if (emergency is IOperation emergencyOperation)
                {
                    var operationInfo = await emergencyOperation.GetOperations(relation);
                    if ((operationInfo?.MedPatOperations?.Count ?? 0) > 0)
                    {
                        operationDto.MedPatOperations.AddRange(operationInfo.MedPatOperations);
                        logger.LogDebug($"急诊同步行数:{operationInfo.MedPatOperations.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"手术-急诊同步异常，{ex.Message}");
            }
            #endregion

            #region 门诊
            try
            {
                if (outPatient is IOperation outPatientOperation)
                {
                    var operationInfo = await outPatientOperation.GetOperations(relation);
                    if ((operationInfo?.MedPatOperations?.Count ?? 0) > 0)
                    {
                        operationDto.MedPatOperations.AddRange(operationInfo.MedPatOperations);
                        logger.LogDebug($"门诊同步行数:{operationInfo.MedPatOperations.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"手术-门诊同步异常，{ex.Message}");
            }
            #endregion

            #region 住院
            try
            {
                if (inPatient is IOperation inPatientOperation)
                {
                    var operationInfo = await inPatientOperation.GetOperations(relation);
                    if ((operationInfo?.MedPatOperations?.Count ?? 0) > 0)
                    {
                        operationDto.MedPatOperations.AddRange(operationInfo.MedPatOperations);
                        logger.LogDebug($"住院同步行数:{operationInfo.MedPatOperations.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"手术-住院同步异常，{ex.Message}");
            }
            #endregion

            logger.LogDebug($"手术数据行数:{operationDto.MedPatOperations.Count}");
            if (operationDto.MedPatOperations.Count == 0) return true;

            foreach (var operation in operationDto.MedPatOperations)
            {
                operation.PatientNo = patientNo;
                if (!string.IsNullOrEmpty(operation.DiagBeforeOperation))
                {
                    operation.DiagBeforeOperation = operation.DiagBeforeOperation.ToShowLimitLength(200);
                }
                if (!string.IsNullOrEmpty(operation.DiagAfterOperation))
                {
                    operation.DiagAfterOperation = operation.DiagAfterOperation.ToShowLimitLength(200);
                }
                if (!string.IsNullOrEmpty(operation.OperationName))
                {
                    operation.OperationName = operation.OperationName.ToShowLimitLength(200);
                }

                try
                {
                    var operationDb = await medPatOperation.GetAsync(operation.Id);
                    if (operationDb == null)
                    {
                        await medPatOperation.InsertAsync(operation);
                    }
                    else
                    {
                        operationDb = operation;
                        await medPatOperation.UpdateAsync(operationDb);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"手术信息入库错误:{ex.Message},数据:{operation.ToJson()}");
                    continue;
                }
            }
            logger.LogDebug($"同步手术信息结束");
            return true;
        }
    }
}
