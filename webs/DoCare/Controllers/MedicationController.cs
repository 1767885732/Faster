using DoCare.Core.Common;
using DoCare.Core.Exam_Inspec_Medication;
using FastFrame.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DoCare.Controllers
{
    public class MedicationController : DoCareApiController
    {
        private readonly MedicationService medicationService;
        public MedicationController(MedicationService medicationService)
        {
            this.medicationService = medicationService;
        }

        /// <summary>
        /// 同步用药数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncMedications(SyncPatientDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                var isSucc = await medicationService.Run(dto.PatientNo);
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