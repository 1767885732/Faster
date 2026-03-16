using DoCare.Core.Common;
using DoCare.Core.Score;
using FastFrame.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DoCare.Controllers
{
    /// <summary>
    /// 评分
    /// </summary>
    public class ScoreController : DoCareApiController
    {
        private readonly ScoreService scoreService;
        public ScoreController(ScoreService scoreService)
        {
            this.scoreService = scoreService;
        }

        /// <summary>
        /// 同步评分
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        public async Task<DoCareResponse<bool>> SyncScores(SyncPatientDto dto)
        {
            DoCareResponse<bool> result = null;
            try
            {
                var isSucc = await scoreService.RunAsync(dto);
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