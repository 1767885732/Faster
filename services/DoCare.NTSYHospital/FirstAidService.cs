using DoCare.Core;
using DoCare.Core.Patient.Dtos;
using DoCare.Core.Patient.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.NTSYHospital
{
    public class FirstAidService : IFirstAid
    {
        private readonly ILogger<FirstAidService> logger;
        public FirstAidService(ILogger<FirstAidService> logger)
        {
            this.logger = logger;
        }

        public Task<List<PatRegistOutDto>> GetList(PatRegistInputDto queryParms)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SyncPatientData(MedPatRelationInfo relation)
        {
            logger.LogDebug($"FirstAidService.SyncPatientData()未实现");
            return "string";
        }
    }
}
