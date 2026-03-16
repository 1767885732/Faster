using DoCare.Core.Patient.Entities;
using DoCare.Core.Score.Entities;
using FastFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Core.Score
{
    /// <summary>
    /// 评分接口
    /// </summary>
    [IgnoreDependency]
    public interface IScores
    {
        Task<List<MedPatScore>> GetMedPatScore(MedPatRelationInfo relationEntity);
    }
}
