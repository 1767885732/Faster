using DoCare.Core.Ecg.Entities;
using DoCare.Core.Patient.Entities;
using FastFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoCare.Core.Ecg
{
    /// <summary>
    /// 心电图相关接口
    /// </summary>
    [IgnoreDependency]
    public interface IEcg
    {
        Task<List<MedPatEcgInfo>> RunEcg(MedPatRelationInfo relationEntity);
    }
}
