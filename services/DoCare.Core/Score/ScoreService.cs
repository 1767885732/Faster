using DoCare.Core.Common;
using DoCare.Core.Patient.Entities;
using DoCare.Core.Score.Entities;
using FastFrame.Core;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Core.Score
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class ScoreService
    {
        private readonly IFirstAid firstAid;
        private readonly IEmergency emergency;
        private readonly IOutPatient outPatient;
        private readonly IInPatient inPatient;
        private readonly IRepository<MedPatRelationInfo, string> medPatRelation;
        private readonly IRepository<MedPatScore, System.Guid> medPatScore;
        private readonly ILogger<ScoreService> logger;
        public ScoreService(IFirstAid firstAid, IEmergency emergency, IOutPatient outPatient,
           IInPatient inPatient, IRepository<MedPatRelationInfo, string> medPatRelation,
           IRepository<MedPatScore, System.Guid> medPatScore,
            ILogger<ScoreService> logger)
        {
            this.firstAid = firstAid;
            this.emergency = emergency;
            this.outPatient = outPatient;
            this.inPatient = inPatient;
            this.medPatRelation = medPatRelation;
            this.medPatScore = medPatScore;
            this.logger = logger;
        }

        public async Task<bool> RunAsync(SyncPatientDto dto)
        {
            var result = false;
            var relation = await medPatRelation.GetAsync(dto.PatientNo);
            if (relation == null) return result;

            List<MedPatScore> medPatScores = new List<MedPatScore>();
            if (firstAid is IScores firstAidScoresImpl)
            {
                try
                {
                    var firstAidScores = await firstAidScoresImpl.GetMedPatScore(relation);
                    if ((firstAidScores?.Count ?? 0) > 0)
                        medPatScores.AddRange(firstAidScores);
                }
                catch (Exception ex)
                {
                    logger.LogError($"评分-急救同步错误:{ex.Message}");
                }
            }

            if (emergency is IScores emergencyScoresImpl)
            {
                try
                {
                    var emergencyScores = await emergencyScoresImpl.GetMedPatScore(relation);
                    if ((emergencyScores?.Count ?? 0) > 0)
                        medPatScores.AddRange(emergencyScores);
                }
                catch (Exception ex)
                {
                    logger.LogError($"评分-急诊同步错误:{ex.Message}");
                }
            }

            if (outPatient is IScores outPatientScoresImpl)
            {
                try
                {
                    var outPatientScores = await outPatientScoresImpl.GetMedPatScore(relation);
                    if ((outPatientScores?.Count ?? 0) > 0)
                        medPatScores.AddRange(outPatientScores);
                }
                catch (Exception ex)
                {
                    logger.LogError($"评分-门诊同步错误:{ex.Message}");
                }
            }

            if (inPatient is IScores inPatientScoresImpl)
            {
                try
                {
                    var inpatientScores = await inPatientScoresImpl.GetMedPatScore(relation);
                    if ((inpatientScores?.Count ?? 0) > 0)
                        medPatScores.AddRange(inpatientScores);
                }
                catch (Exception ex)
                {
                    logger.LogError($"评分-住院同步错误:{ex.Message}");
                }
            }

            logger.LogDebug($"SyncScoresProcess-medPatScores.Count:{medPatScores.Count}");
            if (medPatScores.Count > 0)
            {
                medPatScores.ForEach(async item =>
                {
                    var db = await medPatScore.GetFirstAsync(q => q.PatientNo == item.PatientNo && q.ScoreType == item.ScoreType && q.ScoreSource == item.ScoreSource && q.ScoreItem == item.ScoreItem);
                    if (db == null)
                    {
                        await medPatScore.InsertAsync(item);
                    }
                    else
                    {
                        item.Id = db.Id;
                        db = item;
                        await medPatScore.UpdateAsync(db);
                    }
                });
            }
            return true;
        }
    }
}
