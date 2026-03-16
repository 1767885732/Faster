using DoCare.Core.Common;
using DoCare.Core.Exam_Inspec_Medication;
using DoCare.Core.Operation;
using FastFrame.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DoCare.Controllers
{
    public class OperationController : DoCareApiController
    {
        private readonly OperationService operationService;
        public OperationController(OperationService operationService)
        {
            this.operationService = operationService;
        }

        /// <summary>
        /// 同步手术数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncOperations(SyncPatientDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                var isSucc = await operationService.Run(dto.PatientNo);
                result = Success<bool>(isSucc);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = Error<bool>();
            }
            return result;
        }
    }
}